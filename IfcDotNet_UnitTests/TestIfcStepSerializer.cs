#region License
/*

Copyright 2010, Iain Sproat
All rights reserved.

Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions are
met:

 * Redistributions of source code must retain the above copyright
notice, this list of conditions and the following disclaimer.
 * Redistributions in binary form must reproduce the above
copyright notice, this list of conditions and the following disclaimer
in the documentation and/or other materials provided with the
distribution.
 * The names of the contributors may not be used to endorse or promote products derived from
this software without specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
"AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR
A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT
OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT
LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY
THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
(INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

 */
#endregion

using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;

using log4net;
using log4net.Config;

using IfcDotNet;
using IfcDotNet.Schema;
using IfcDotNet.StepSerializer;

namespace IfcDotNet_UnitTests
{
    [TestFixture]
    public class TestIfcStepSerializer
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(TestIfcStepSerializer));
        
        IfcStepSerializer serializer;
        
        [SetUp]
        public void SetUp()
        {
            BasicConfigurator.Configure();
            
            serializer = new IfcStepSerializer();
        }
        
        [Test]
        public void CanDeserializeNoData(){
            Entity[] Items = DeserializeAndAssert( Utilities.StepNoData() );
            Assert.AreEqual( 0, Items.Length );
        }
        [Test]
        public void CanSerializeNoData(){
            iso_10303 iso10303 = serializer.Deserialize( Utilities.StepNoData() );
            
            StringBuilder sb = new StringBuilder();
            StepWriter stepwriter = new StepWriter( new StringWriter( sb ) );
            
            serializer.Serialize( stepwriter, iso10303 );
            
            Assert.AreEqual( Utilities.StepNoDataString(), sb.ToString() );
        }
        
        [Test]
        public void CanDeserializeSimpleLine(){
            Entity[] Items = DeserializeAndAssert( Utilities.StepSimpleLine() );
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
        public void CanSerializeSimpleLine(){
            iso_10303 iso10303 = serializer.Deserialize( Utilities.StepSimpleLine() );
            
            StringBuilder sb = new StringBuilder();
            StepWriter stepwriter = new StepWriter( new StringWriter( sb ) );
            
            serializer.Serialize( stepwriter, iso10303 );
            
            Assert.AreEqual( Utilities.StepSimpleLineString(), sb.ToString() );
        }
        
        [Test]
        public void CanDeserializeWithReference(){
            Entity[] Items = DeserializeAndAssert( Utilities.StepWithReference() );
            Assert.AreEqual(4, Items.Length); //FIXME should trim tree so only the root item is left (length == 1)
            Assert.IsNotNull(Items[0]);
            IfcAxis2Placement3D a2p3d = Items[0] as IfcAxis2Placement3D;
            Assert.IsNotNull(a2p3d);
            Assert.IsNotNull(a2p3d.Location);
            //TODO further assertions
        }
        [Test]
        public void CanSerializeWithReference(){
            iso_10303 iso10303 = serializer.Deserialize( Utilities.StepWithReference() );
            
            StringBuilder sb = new StringBuilder();
            StepWriter stepwriter = new StepWriter( new StringWriter( sb ) );
            
            serializer.Serialize( stepwriter, iso10303 );
            
            Assert.AreEqual( Utilities.StepWithReferenceString(), sb.ToString() );
        }
        
        [Test]
        public void CanDeserializeArrayWithReferences(){
            Entity[] Items = DeserializeAndAssert( Utilities.StepArrayWithReferences() );
            
            Assert.AreEqual(6, Items.Length); //FIXME should trim tree so only the root item is left (length == 1)
            Assert.IsNotNull(Items[0]);
            IfcPolyline poly = Items[0] as IfcPolyline;
            Assert.IsNotNull(poly);
            Assert.IsNotNull(poly.Points);
            Assert.IsNotNull(poly.Points.IfcCartesianPoint);
            Assert.AreEqual(5, poly.Points.IfcCartesianPoint.Length );
            Assert.IsNotNull(poly.Points.IfcCartesianPoint[1]);
            IfcCartesianPoint pnt1 = poly.Points.IfcCartesianPoint[1];
            Assert.IsNotNull(pnt1);
            Assert.IsNotNull(pnt1.Coordinates);
            Assert.IsNotNull(pnt1.Coordinates.IfcLengthMeasure);
            Assert.AreEqual(2, pnt1.Coordinates.IfcLengthMeasure.Length);
            Assert.AreEqual(0.3, pnt1.Coordinates.IfcLengthMeasure[1].Value);
        }
        
        [Test]
        public void CanDeserializeComplexReferences(){
            Entity[] Items = DeserializeAndAssert( Utilities.StepComplexReferences() );
            //TODO more assertions
        }
        
        [Test]
        public void CanDeserializeNestedStructure(){
            Entity[] Items = DeserializeAndAssert( Utilities.StepNestedObjects() );
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
        public void CanSerializeNestedStructure(){
            iso_10303 iso10303 = serializer.Deserialize( Utilities.StepNestedObjects() );
            
            StringBuilder sb = new StringBuilder();
            StepWriter stepwriter = new StepWriter( new StringWriter( sb ) );
            
            serializer.Serialize( stepwriter, iso10303 );
            
            Assert.AreEqual( Utilities.StepNestedObjectsString(), sb.ToString() );
        }
        
        [Test]
        public void CanDeserializeNestedObjectWithinArray(){
            Entity[] items = DeserializeAndAssert( Utilities.StepNestedObjectWithinArray() );
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
            
            Entity[] Items = DeserializeAndAssert( Utilities.StepArray() );
            Assert.AreEqual(1, Items.Length);
            Entity e = Items[0];
            Assert.IsNotNull(e);
            IfcCartesianPoint cp = e as IfcCartesianPoint;
            Assert.IsNotNull(cp);
            Assert.IsNotNull(cp.Coordinates);
            Assert.IsNotNull(cp.Coordinates.IfcLengthMeasure);
            Assert.AreEqual(3,   cp.Coordinates.IfcLengthMeasure.Length);
            Assert.AreEqual(0,   cp.Coordinates.IfcLengthMeasure[0].Value);
            Assert.AreEqual(1,   cp.Coordinates.IfcLengthMeasure[1].Value);
            Assert.AreEqual(4.5, cp.Coordinates.IfcLengthMeasure[2].Value);
        }
        
        [Test]
        [Explicit]
        public void CanDeserializeSmallWallExample()
        {
            Entity[] Items = DeserializeAndAssert( Utilities.StepSmallWallExample() );
            
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
        public void CanSerializeSmallWallExample(){
            iso_10303 iso10303 = serializer.Deserialize( Utilities.StepSmallWallExample() );
            
            StringBuilder sb = new StringBuilder();
            StepWriter stepwriter = new StepWriter( new StringWriter( sb ) );
            
            serializer.Serialize( stepwriter, iso10303 );
            
            Assert.AreEqual( Utilities.StepSmallWallExampleString(), sb.ToString() );
        }
        
        [Test]
        [Explicit]
        public void NIST_TrainingStructure(){
            StreamReader sr = new StreamReader("./sampleData/NIST_TrainingStructure_param.ifc");
            StepReader reader = new StepReader( sr );
            iso_10303 iso10303 = serializer.Deserialize( reader );
            uos1 uos1 = iso10303.uos as uos1;
            Entity[] entities = uos1.Items;
            Assert.AreEqual( 17227, entities.Length );
        }
        
        private Entity[] DeserializeAndAssert(StepReader reader){
            iso_10303 iso10303 = serializer.Deserialize( reader );
            AssertIso10303( iso10303 );
            uos1 uos1 = iso10303.uos as uos1;
            return uos1.Items;
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
    }
}
