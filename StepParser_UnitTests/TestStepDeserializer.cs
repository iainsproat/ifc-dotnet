/*
 * Created by Iain Sproat
 * Date: 24/05/2012
 * Time: 20:15
 * 
 */
using System;
using System.IO;
using System.Collections.Generic;

using NUnit.Framework;

using log4net;
using log4net.Config;

using StepParser;
using StepParser.StepFileRepresentation;
using StepParser.IO;
using StepParser.Serialization;

namespace StepParser_UnitTests
{
	/// <summary>
	/// Description of TestStepDeserializer.
	/// </summary>
	[TestFixture]
	public class TestStepDeserializer
	{
		StepDeserializer SUT;
		
		[SetUp]
		public void SetUp()
		{
			BasicConfigurator.Configure();
			SUT = new StepDeserializer();
		}
		
		[Test]
		public void CanDeserializeSimpleStep(){
			StepReader sr = createStepReader(ExampleData.simpleStepWithCommentString());
			
			StepFile result = SUT.Deserialize( sr );
			Assert.IsNotNull( result );
			
			Assert.IsNotNull( result.Header );
			Assert.AreEqual(3, result.Header.Count);
			
			StepDataObject header0 = result.Header[0];
			AssertObject("FILE_DESCRIPTION", 2, header0);
			AssertArray(1, header0.Properties[0]);
			AssertString("ViewDefinition [CoordinationView, QuantityTakeOffAddOnView]", getArray(header0.Properties[0])[0]);
			AssertString("2;1", header0.Properties[1]);
			
			StepDataObject header1 = result.Header[1];
			AssertObject("FILE_NAME", 3, header1);
			AssertString("example.ifc", header1.Properties[0]);
			AssertDate("2008-08-01T21:53:56", header1.Properties[1]);
			AssertArray(1, header1.Properties[2]);
			AssertString("Architect", getArray(header1.Properties[2])[0]);
			
			StepDataObject header2 = result.Header[2];
			AssertObject("FILE_SCHEMA", 1, header2);
			AssertArray(1, header2.Properties[0]);
			AssertString("IFC2X3", getArray(header2.Properties[0])[0]);
			
			AssertKeysAreInOrder(2, result.Data);
			ICollection<StepDataObject> valuesCollection = result.Data.Values;
			IList<StepDataObject> values = new List<StepDataObject>(valuesCollection);
			
			StepDataObject entity0 = values[0];
			AssertObject("IFCPROJECT", 9, entity0);
			AssertString("3MD_HkJ6X2EwpfIbCFm0g_", entity0.Properties[0]);
			AssertLineReference(2, entity0.Properties[1]);
			AssertString("Default Project", entity0.Properties[2]);
			AssertString("Description of Default Project", entity0.Properties[3]);
			AssertNull(entity0.Properties[4]);
			AssertFloat(-22.4, entity0.Properties[5]);
			AssertNull(entity0.Properties[6]);
			AssertArray(1, entity0.Properties[7]);
			AssertLineReference(20, getArray(entity0.Properties[7])[0]);
			AssertLineReference(7, entity0.Properties[8]);
			
			StepDataObject entity1 = values[1];
			AssertObject("IFCOWNERHISTORY", 8, entity1);
			AssertLineReference(3, entity1.Properties[0]);
			AssertNestedObject("IFCTEXT", 1, entity1.Properties[1]);
			//AssertLineReference(6, entity1.Properties[1]);
			AssertNull(entity1.Properties[2]);
			AssertEnum("ADDED", entity1.Properties[3]);
			AssertNull(entity1.Properties[4]);
			AssertBoolean(false, entity1.Properties[5]);
			AssertOverridden(entity1.Properties[6]);
			AssertInteger(1217620436, entity1.Properties[7]);
		}
		
		[Test]
		public void CanCorrectlyDeserializeAmbiguousNumber(){
			StepReader sr = createStepReader(ExampleData.AmbiguousNumberString());
			StepFile result = SUT.Deserialize( sr );
			List<StepDataObject> values = new List<StepDataObject>(result.Data.Values);
			StepDataObject entity0 = values[0];
			Assert.IsNotNull( entity0 );
			Assert.IsNotNull( entity0.Properties );
			Assert.AreEqual( 1, entity0.Properties.Count );
			AssertFloat(1E-5, entity0.Properties[0]);
		}
		
		[Test]
		public void CanParseObjectNamesBeginningWithE(){
			StepReader sr = createStepReader(ExampleData.ObjectNameBeginningWithE());
			StepFile result = SUT.Deserialize( sr );
			
			List<StepDataObject> values = new List<StepDataObject>(result.Data.Values);
			StepDataObject entity0 = values[0];
			Assert.AreEqual("EXAMPLE", entity0.ObjectName);
		}
		
		#region Helpers
		public StepReader createStepReader(string stepFormatStringToRead){
			return new StepReader( new StringReader( stepFormatStringToRead ) );
		}
		
		public List<StepValue> getArray(StepValue stepValueContainingArray){
			return stepValueContainingArray.Value as List<StepValue>;
		}
		
		public void AssertKeysAreInOrder(int expectedCount, IDictionary<int, StepDataObject> data){
			Assert.IsNotNull(data);
			
			ICollection<int> keys = data.Keys;
			int currentIndex = 1;
			foreach(int key in keys){
				Assert.AreEqual(currentIndex++, key);
			}
			
			Assert.AreEqual(expectedCount, data.Count);
		}
		
		public void AssertObject(string expectedName, int expectedPropertiesCount, StepDataObject actual){
			Assert.IsNotNull(actual);
			Assert.AreEqual(expectedName, actual.ObjectName);
			Assert.IsNotNull(actual.Properties);
			Assert.AreEqual(expectedPropertiesCount, actual.Properties.Count);
		}
		
		public void AssertStepValue(StepToken expectedToken, Type expectedValueType, object expectedValue, StepValue actual){
			Assert.IsNotNull(actual);
			Assert.AreEqual(expectedToken, actual.Token);
			Assert.AreEqual(expectedValueType, actual.ValueType);
			Assert.AreEqual(expectedValue, actual.Value);
		}
		
		public void AssertString(string expectedValue, StepValue actual){
			AssertStepValue(StepToken.String, typeof(string), expectedValue, actual);
		}
		
		public void AssertDate(string expectedDate, StepValue actual){
			AssertStepValue(StepToken.Date, typeof(DateTime), DateTime.Parse(expectedDate), actual);
		}
		
		/// <summary>
		/// Asserts a StepValue is an array and is not null, but does not assert anything of the array contents
		/// </summary>
		/// <param name="actual"></param>
		public void AssertArray(StepValue actual){
			Assert.IsNotNull(actual);
			Assert.AreEqual(StepToken.StartArray, actual.Token);
			Assert.AreEqual(typeof(List<StepValue>), actual.ValueType);
			Assert.IsNotNull(actual.Value);
		}
		
		public void AssertArray(int expectedCount, StepValue actual){
			AssertArray(actual);
			List<StepValue> array = actual.Value as List<StepValue>;
			Assert.IsNotNull(array);
			Assert.AreEqual(expectedCount, array.Count);
		}
		
		public void AssertLineReference(int expectedLineReference, StepValue actual){
			AssertStepValue(StepToken.LineReference, typeof(int), expectedLineReference, actual);
		}
		
		public void AssertNestedObject(string expectedName, int expectedNumberOfProperties, StepValue actual){
			Assert.IsNotNull(actual);
			Assert.IsNotNull(actual.Value);
			Assert.AreEqual(StepToken.StartEntity, actual.Token);
			Assert.AreEqual(typeof(StepDataObject), actual.ValueType);
			
			StepDataObject sdo = actual.Value as StepDataObject;
			Assert.IsNotNull(sdo);
			AssertObject(expectedName, expectedNumberOfProperties, sdo);
		}
		
		public void AssertNull(StepValue actual){
			AssertStepValue(StepToken.Null, null, null, actual);
		}
		
		public void AssertFloat(double expectedValue, StepValue actual){
			AssertStepValue(StepToken.Float, typeof(double), expectedValue, actual);
		}
		
		public void AssertEnum(string expectedValue, StepValue actual){
			AssertStepValue(StepToken.Enumeration, typeof(string), expectedValue, actual);
		}
		
		public void AssertBoolean(bool expectedValue, StepValue actual){
			AssertStepValue(StepToken.Boolean, typeof(bool), expectedValue, actual);
		}
		
		public void AssertOverridden(StepValue actual){
			AssertStepValue(StepToken.Overridden, null, null, actual);
		}
		
		public void AssertInteger(int expectedValue, StepValue actual){
			AssertStepValue(StepToken.Integer, typeof(int), expectedValue, actual);
		}
		#endregion
	}
}
