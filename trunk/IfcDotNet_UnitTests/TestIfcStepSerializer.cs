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
		
		private static string ifcHeader(){
			return "ISO-10303-21;\r\n" +
				"HEADER;\r\n" +
				"FILE_DESCRIPTION (('ViewDefinition [CoordinationView, QuantityTakeOffAddOnView]'), '2;1');\r\n" +
				"FILE_NAME ('example.ifc', '2008-08-01T21:53:56', ('Architect'), ('Building Designer Office'), 'IFC Engine DLL version 1.02 beta', 'IFC Engine DLL version 1.02 beta', 'The authorising person');\r\n" +
				"FILE_SCHEMA (('IFC2X3'));\r\n" +
				"ENDSEC;\r\n" +
				"DATA;\r\n";
		}
		
		public static string ifcEnd(){
			return "ENDSEC;\r\n" +
				"END-ISO-10303-21;";
		}
		
		[Test]
		public void CanDeserializeSimpleLine(){
			string ifc = ifcHeader() +
				"#1 = IFCQUANTITYLENGTH('Depth', 'Depth', $, 3.000E-1);\r\n" +
				ifcEnd();
			StepReader sr = new StepReader( new StringReader( ifc ) );
			iso_10303_28 iso10303 = serializer.Deserialize( sr );
			
			Assert.IsNotNull(iso10303.uos);
			uos1 uos = iso10303.uos as uos1;
			Assert.IsNotNull(uos);
			Assert.IsNotNull(uos.Items);
			Assert.AreEqual(1, uos.Items.Length);
			Assert.IsNotNull(uos.Items[0]);
			IfcQuantityLength ql = uos.Items[0] as IfcQuantityLength;
			Assert.IsNotNull(ql);
			Assert.AreEqual("Depth", ql.Name);
			Assert.AreEqual("Depth", ql.Description);
			Assert.AreEqual(0.3, ql.LengthValue);
			Assert.IsNull(ql.Unit);
		}
		
		[Test]
		public void CanDeserializeWithReference(){
			string ifc = ifcHeader() +
				"#1 = IFCAXIS2PLACEMENT3D(#2, #3, #4);\r\n" +
				"#2 = IFCCARTESIANPOINT((9.000E-1, 0., 2.500E-1));\r\n" +
				"#3 = IFCDIRECTION((0., 0., 1.));\r\n" +
				"#4 = IFCDIRECTION((1., 0., 0.));\r\n" +
				ifcEnd();
			StepReader sr = new StepReader( new StringReader( ifc ) );
			iso_10303_28 iso10303 = serializer.Deserialize( sr );
			Assert.IsNotNull(iso10303);
			Assert.IsNotNull(iso10303.uos);
			uos1 uos1 = iso10303.uos as uos1;
			Assert.IsNotNull(uos1);
			Assert.IsNotNull(uos1.Items);
			Assert.AreEqual(4, uos1.Items.Length); //FIXME should trim tree so only the root item is left (length == 1)
			Assert.IsNotNull(uos1.Items[0]);
			IfcAxis2Placement3D a2p3d = uos1.Items[0] as IfcAxis2Placement3D;
			Assert.IsNotNull(a2p3d);
			Assert.IsNotNull(a2p3d.Location);
			//TODO further assertions
		}
		
		[Test]
		public void CanDeserializeArrayWithReferences(){
			string ifc = ifcHeader() +
				"#1 = IFCPOLYLINE((#2, #3, #4, #5, #6));\r\n" +
				"#2 = IFCCARTESIANPOINT((0., 0.));\r\n" +
				"#3 = IFCCARTESIANPOINT((0., 3.000E-1));\r\n" +
				"#4 = IFCCARTESIANPOINT((7.500E-1, 3.000E-1));\r\n" +
				"#5 = IFCCARTESIANPOINT((7.500E-1, 0.));\r\n" +
				"#6 = IFCCARTESIANPOINT((0., 0.));\r\n" +
				ifcEnd();
			StepReader sr = new StepReader( new StringReader( ifc ) );
			iso_10303_28 iso10303 = serializer.Deserialize( sr );
			Assert.IsNotNull(iso10303);
			Assert.IsNotNull(iso10303.uos);
			uos1 uos1 = iso10303.uos as uos1;
			Assert.IsNotNull(uos1);
			Assert.IsNotNull(uos1.Items);
			Assert.AreEqual(6, uos1.Items.Length); //FIXME should trim tree so only the root item is left (length == 1)
			Assert.IsNotNull(uos1.Items[0]);
			IfcPolyline poly = uos1.Items[0] as IfcPolyline;
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
			//not valid IFC, but is valid STEP
			string ifc = ifcHeader() +
				"#1 = IFCBUILDINGSTOREY('0C87kaqBXF$xpGmTZ7zxN$', $, 'Default Building Storey', 'Description of Default Building Storey', $, $, $, $, .ELEMENT., 0.);\r\n" +
                "#2 = IFCRELCONTAINEDINSPATIALSTRUCTURE('2O_dMuDnr1Ahv28oR6ZVpr', $, 'Default Building', 'Contents of Building Storey', (#3, #4), #1);\r\n" +
                "#3 = IFCWALLSTANDARDCASE('3vB2YO$MX4xv5uCqZZG05x', $, 'Wall xyz', 'Description of Wall', $, $, $, $);\r\n" +
                "#4 = IFCWINDOW('0LV8Pid0X3IA3jJLVDPidY', $, 'Window xyz', 'Description of Window', $, $, $, $, 1.400, 7.500E-1);\r\n" +
				ifcEnd();
			StepReader sr = new StepReader( new StringReader( ifc ) );
			iso_10303_28 iso10303 = serializer.Deserialize( sr );
		}
		
		[Test]
		public void CanDeserializeNestedStructure(){
			string ifc = ifcHeader() +
				"#1 = IFCPROPERTYSINGLEVALUE('Reference', 'Reference', IFCTEXT(''), $);\r\n" +
				ifcEnd();
			StepReader sr = new StepReader( new StringReader( ifc ) );
			iso_10303_28 iso10303 = serializer.Deserialize( sr );
			Assert.AreEqual(1, ((uos1)iso10303.uos).Items.Length );
			Entity e = ((uos1)iso10303.uos).Items[0];
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
		public void CanDeserializeArray(){
			
			string ifc = ifcHeader() +
				"#1 = IFCCARTESIANPOINT((0., 1., 4.5));\r\n" +
				ifcEnd();
			StepReader sr = new StepReader( new StringReader( ifc ) );
			iso_10303_28 iso10303 = serializer.Deserialize( sr );
			Assert.AreEqual(1, ((uos1)iso10303.uos).Items.Length );
			Entity e = ((uos1)iso10303.uos).Items[0];
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
		public void CanDeserializeSmallWallExample()
		{
			
			iso_10303_28 iso10303 = serializer.Deserialize( Utilities.getSmallWallExampleSTEP() );
			
			Assert.IsNotNull(iso10303.uos);
			uos1 uos = iso10303.uos as uos1;
			Assert.IsNotNull(uos);
			Assert.IsNotNull(uos.Items);
			Assert.AreEqual(163, uos.Items.Length);
			
			Assert.IsNotNull(uos.Items[0]);
			IfcProject project = uos.Items[0] as IfcProject;
			Assert.IsNotNull(project);
			Assert.AreEqual("3MD_HkJ6X2EwpfIbCFm0g_", project.GlobalId);
			
			Assert.IsNotNull(uos.Items[123]);
			IfcWindow window = uos.Items[123] as IfcWindow;
			Assert.IsNotNull(window);
			Assert.AreEqual("0LV8Pid0X3IA3jJLVDPidY", window.GlobalId);
		}
		
		
	}
}
