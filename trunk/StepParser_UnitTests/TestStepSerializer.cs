/*
 * Created by Iain Sproat
 * Date: 24/05/2012
 * Time: 22:20
 * 
 */
using System;
using System.IO;
using System.Text;

using NUnit.Framework;

using StepParser;
using StepParser.Serialization;
using StepParser.IO;

using log4net;
using log4net.Config;

namespace StepParser_UnitTests
{
	/// <summary>
	/// Description of TestStepSerializer.
	/// </summary>
	[TestFixture]
	public class TestStepSerializer
	{
		private static ILog logger = LogManager.GetLogger(typeof(TestStepSerializer));
		
		StepSerializer SUT;
		
		[SetUp]
		public void SetUp(){
			BasicConfigurator.Configure();
			
			SUT = new StepSerializer();
		}
		
		[Test]
		public void CanSerializeSimpleStep(){
			StringBuilder sb = new StringBuilder();
			TextWriter tw = new StringWriter(sb);
			IStepWriter writer = new StepWriter(tw);
			SUT.Serialize(writer, ExampleData.simpleStepRepresentation());
			
			logger.Debug(sb.ToString());
			Assert.AreEqual(ExampleData.simpleStepString(), sb.ToString());
		}
		
		[Test]
		[Explicit]
		public void PrintSimpleStepWithMockStepWriter(){
			IStepWriter writer = new MockStepWriter();
			SUT.Serialize(writer, ExampleData.simpleStepRepresentation());
		}
		
		[Test]
		[Explicit]
		public void PrintSimpleStepWithMockTextWriter(){

			//TextWriter tw = new MockTextWriter();
			StringBuilder sb = new StringBuilder();
			TextWriter tw = new StringWriter(sb);
			StepWriter writer = new StepWriter(tw);
			//((MockTextWriter)tw).setParent(writer);
			SUT.Serialize(writer, ExampleData.simpleStepRepresentation());
		}
	}
}
