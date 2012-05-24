/*
 * Created by Iain Sproat
 * Date: 24/05/2012
 * Time: 15:46
 * 
 */
using System;
using System.IO;

using StepParser;

using IfcDotNet.Schema;
using IfcDotNet.StepSerializer;

using log4net;
using log4net.Config;
using NUnit.Framework;

namespace IfcDotNet_UnitTests
{
	/// <summary>
	/// Unit tests for the deserialisation of IFC Step files
	/// </summary>
	[TestFixture]
	public class TestIfcStepDeserializer
	{
		private static readonly ILog logger = LogManager.GetLogger(typeof(TestIfcStepDeserializer));
        
        IfcStepSerializer serializer;
        
        [SetUp]
        public void SetUp()
        {
            BasicConfigurator.Configure();
            
            serializer = new IfcStepSerializer();
        }
        
        [Test]
        public void CanDeserializeNoData(){
            Entity[] Items = DeserializeAssertISO10303AndExtractItems( Utilities.StepNoDataString() );
            Assert.AreEqual( 0, Items.Length );
        }
        
        [Test]
        public void CanDeserializeSimpleLine(){
            Entity[] Items = DeserializeAssertISO10303AndExtractItems( Utilities.StepSimpleLineString() );
            Assert.AreEqual(1, Items.Length);
            Assert.IsNotNull(Items[0]);
            IfcQuantityLength ql = Items[0] as IfcQuantityLength;
            Assert.IsNotNull(ql);
            Assert.AreEqual("Depth", ql.Name);
            Assert.AreEqual("Depth", ql.Description);
            Assert.AreEqual(0.3, ql.LengthValue);
            Assert.IsNull(ql.Unit);
        }
        
         [Test]
        public void CanDeserializeWithReference(){
            Entity[] Items = DeserializeAssertISO10303AndExtractItems( Utilities.StepWithReferenceString() );
            Assert.AreEqual(4, Items.Length); //FIXME should trim tree so only the root item is left (length == 1)
            Assert.IsNotNull(Items[0]);
            IfcAxis2Placement3D a2p3d = Items[0] as IfcAxis2Placement3D;
            Assert.IsNotNull(a2p3d);
            Assert.IsNotNull(a2p3d.Location);
            //TODO further assertions
        }
         
         [Test]
        public void CanDeserializeArrayWithReferences(){
            Entity[] Items = DeserializeAssertISO10303AndExtractItems( Utilities.StepArrayWithReferencesString() );
            
            Assert.AreEqual(6, Items.Length); //FIXME should trim tree so only the root item is left (length == 1)
            Assert.IsNotNull(Items[0]);
            IfcPolyline poly = Items[0] as IfcPolyline;
            Assert.IsNotNull(poly);
            Assert.IsNotNull(poly.Points);
            Assert.IsNotNull(poly.Points.Items);
            Assert.AreEqual(5, poly.Points.Items.Length );
            Assert.IsNotNull(poly.Points[1]);
            IfcCartesianPoint pnt1 = poly.Points[1];
            Assert.IsNotNull(pnt1);
            Assert.IsNotNull(pnt1.Coordinates);
            Assert.IsNotNull(pnt1.Coordinates.Items);
            Assert.AreEqual(2, pnt1.Coordinates.Items.Length);
            Assert.AreEqual(0.3, pnt1.Coordinates[1].Value);
        }
         
         [Test]
        public void CanDeserializeComplexReferences(){
            Entity[] Items = DeserializeAssertISO10303AndExtractItems( Utilities.StepComplexReferencesString() );
            //TODO more assertions
        }
        
        [Test]
        public void CanDeserializeNestedStructure(){
            Entity[] Items = DeserializeAssertISO10303AndExtractItems( Utilities.StepNestedObjectsString() );
            Assert.AreEqual(1, Items.Length );
            Entity e = Items[0];
            Assert.IsNotNull(e);
            IfcPropertySingleValue psv = e as IfcPropertySingleValue;
            Assert.IsNotNull(psv);
            Assert.AreEqual("Reference",psv.Name);
            Assert.AreEqual("Reference",psv.Description);
            Assert.IsNotNull(psv.NominalValue);
            Assert.IsNotNull(psv.NominalValue.Item);
            Assert.IsNotNull(psv.NominalValue.Item as IfcText1);
            Assert.IsNull(psv.Unit);
        }
        
        [Test]
        public void CanDeserializeNestedObjectWithinArray(){
            Entity[] items = DeserializeAssertISO10303AndExtractItems( Utilities.StepNestedObjectWithinArrayString() );
            Assert.AreEqual( 1, items.Length );
            Assert.IsNotNull( items[0] );
            IfcPropertyEnumeratedValue pev = items[0] as IfcPropertyEnumeratedValue;
            Assert.IsNotNull( pev );
            Assert.IsNotNull(pev.EnumerationValues);
            Assert.IsNotNull(pev.EnumerationValues.Items);
            Assert.AreEqual(1, pev.EnumerationValues.Items.Length);
            Assert.IsNotNull(pev.EnumerationValues.Items[0]);
            IfcText1 txt = pev.EnumerationValues.Items[0] as IfcText1;
            Assert.IsNotNull( txt );
            Assert.AreEqual( "bottom_edge", txt.Value );
        }
        
        [Test]
        public void CanDeserializeArray(){
            
            Entity[] Items = DeserializeAssertISO10303AndExtractItems( Utilities.StepArrayString() );
            Assert.AreEqual(1, Items.Length);
            Entity e = Items[0];
            Assert.IsNotNull(e);
            IfcCartesianPoint cp = e as IfcCartesianPoint;
            Assert.IsNotNull(cp);
            Assert.IsNotNull(cp.Coordinates);
            Assert.IsNotNull(cp.Coordinates.Items);
            Assert.AreEqual(3,   cp.Coordinates.Items.Length);
            Assert.AreEqual(0,   cp.Coordinates[0].Value);
            Assert.AreEqual(1,   cp.Coordinates[1].Value);
            Assert.AreEqual(4.5, cp.Coordinates[2].Value);
        }
        
        [Test]
        public void CanDeserializeDoubleWrapper(){
        	Entity[] Items = DeserializeAssertISO10303AndExtractItems( Utilities.StepDoubleWrapperItemString() );
        	Assert.AreEqual(2, Items.Length);
        	Entity e = Items[0];
        	Assert.IsNotNull(e);
        }
        
        [Test]
        public void CanDeserializeArrayWrapper(){
        	Entity[] Items = DeserializeAssertISO10303AndExtractItems( Utilities.StepArrayWrapperString() );
        	Assert.AreEqual( 11, Items.Length );
        }
        
        [Test]
        public void CanDeserializeSelect(){
        	Entity[] Items = DeserializeAssertISO10303AndExtractItems( Utilities.StepSelectString() );
        	Assert.AreEqual( 2, Items.Length );
        }
        
        [Test]
        [Explicit]
        public void CanDeserializeSmallWallExample()
        {
            Entity[] Items = DeserializeAssertISO10303AndExtractItems( Utilities.StepSmallWallExampleString() );
            
            Assert.AreEqual(163, Items.Length);
            
            Assert.IsNotNull(Items[0]);
            IfcProject project = Items[0] as IfcProject;
            Assert.IsNotNull(project);
            Assert.AreEqual("3MD_HkJ6X2EwpfIbCFm0g_", project.GlobalId);
            
            Assert.IsNotNull(Items[123]);
            IfcWindow window = Items[123] as IfcWindow;
            Assert.IsNotNull(window);
            Assert.AreEqual("0LV8Pid0X3IA3jJLVDPidY", window.GlobalId);
        }
        
        [Test]
        [Explicit]
        public void Recreate_NIST_TrainingStructure(){
        	IStepReader reader = getNISTTrainingStructure();
        	iso_10303 iso10303 = serializer.Deserialize( reader );
        	reader.Close();
        	
        	string path = "./sampleData/NIST_TrainingStructure_param_output.ifc";
        	if(File.Exists(path))
        		File.Delete(path);
        	Assert.IsFalse(File.Exists(path));
        	
        	StreamWriter sr = new StreamWriter( path );
        	StepWriter writer = new StepWriter( sr );
        	serializer.Serialize( writer, iso10303 );
        	writer.Close();
        	
        	Assert.IsTrue(File.Exists(path));
        	
        	//quick and dirty method for checking file
        	string[] lines = File.ReadAllLines(path);
        	Assert.IsNotNull(lines);
        	Assert.AreEqual(17227 + 9, lines.Length);
        }
        
        [Test]
        [Explicit]
        public void CanDeserialize_NIST_TrainingStructure(){
        	IStepReader reader = getNISTTrainingStructure();
            iso_10303 iso10303 = serializer.Deserialize( reader );
            uos1 uos1 = iso10303.uos as uos1;
            Entity[] entities = uos1.Items;
            Assert.AreEqual( 17227, entities.Length );
        }
		
		public static IStepReader StepSmallWallExample(){
            return new StepReader( new StringReader( Utilities.StepSmallWallExampleString() ) );
        }
		
		#region Helpers
		private IStepReader getNISTTrainingStructure(){
			StreamReader sr = new StreamReader("./sampleData/NIST_TrainingStructure_param.ifc");
			return new StepReader( sr );
		}
		
		private IStepReader createStepReaderFromString(string stringToRead){
			return new StepReader( new StringReader( stringToRead ) );
		}
		
		private Entity[] DeserializeAssertISO10303AndExtractItems(String stringToDeserialize){
        	return this.DeserializeAssertISO10303AndExtractItems( new StringReader( stringToDeserialize ) );
        }
        
        private Entity[] DeserializeAssertISO10303AndExtractItems(StringReader stringReaderToDeserialize){
        	StepReader reader = new StepReader( stringReaderToDeserialize );
            iso_10303 iso10303 = Deserialize( reader );
            AssertIso10303( iso10303 );
            return extractItems( iso10303 );
        }
        
        private iso_10303 Deserialize(IStepReader reader){
        	return serializer.Deserialize( reader );
        }
		
		private void AssertIso10303(iso_10303 iso10303){
            Assert.IsNotNull(iso10303);
            Assert.IsNotNull(iso10303.iso_10303_28_header);
            Assert.AreEqual("example.ifc", iso10303.iso_10303_28_header.name);
            Assert.AreEqual(new DateTime(2008,08,01,21,53,56), iso10303.iso_10303_28_header.time_stamp);
            Assert.AreEqual("Architect", iso10303.iso_10303_28_header.author);
            Assert.AreEqual("Building Designer Office", iso10303.iso_10303_28_header.organization);
            Assert.AreEqual("IFC Engine DLL version 1.02 beta", iso10303.iso_10303_28_header.preprocessor_version);
            Assert.AreEqual("IFC Engine DLL version 1.02 beta", iso10303.iso_10303_28_header.originating_system);
            Assert.AreEqual("The authorising person", iso10303.iso_10303_28_header.authorization);
                
            Assert.IsNotNull(iso10303.uos);
            uos1 uos1 = iso10303.uos as uos1;
            Assert.IsNotNull(uos1);
            Assert.IsNotNull(uos1.Items);
        }
		
		private Entity[] extractItems(iso_10303 iso10303){
        	uos1 uos1 = iso10303.uos as uos1;
            return uos1.Items;
        }
		#endregion
	}
}
