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

using System;
using System.IO;
using IfcDotNet;

namespace IfcDotNet_UnitTests
{
	/// <summary>
	/// Utilities holds helper methods, particularly those for generating IfcXml strings and objects
	/// </summary>
	public class Utilities
	{
		public static TextReader getMinimumExample(){
			string xml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n" +
				"<ex:iso_10303_28\n" +
				"xmlns:xlink=\"http://www.w3.org/1999/xlink\"\n" +
				"xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"\n" +
				"xmlns:ex=\n" +
				"\"urn:iso.org:standard:10303:part(28):version(2):xmlschema:common\"\n" +
				"xmlns=\"http://www.iai-tech.org/ifcXML/IFC2x3/FINAL\"\n" +
				"xsi:schemaLocation=\"http://www.iai-tech.org/ifcXML/IFC2x3/FINAL\n" +
				"http://www.iai-tech.org/ifcXML/IFC2x3/FINAL/IFC2x3.xsd\"\n" +
				"version=\"2.0\"\n" +
				">\n" +
				"<ex:iso_10303_28_header>\n" +
				"<ex:name>An Example</ex:name>\n" +
				"<ex:time_stamp>2010-11-12T13:04:00</ex:time_stamp>\n" +
				"<ex:author>John Hancock</ex:author>\n" +
				"<ex:organization>MegaCorp</ex:organization>\n" +
				"<ex:preprocessor_version>a preprocessor</ex:preprocessor_version>\n" +
				"<ex:originating_system>IfcDotNet Library</ex:originating_system>\n" +
				"<ex:authorization>none</ex:authorization>\n" +
				"<ex:documentation>documentation</ex:documentation>\n" +
				"</ex:iso_10303_28_header>\n" +
				"<uos id=\"uos_1\" description=\"\" configuration=\"i-ifc2x3\" edo=\"\">\n" +
				"<IfcOrganization id=\"i1101\">\n" +
				"<Name>MegaCorp</Name>\n" +
				"</IfcOrganization>\n" +
				"<IfcCartesianPoint id=\"i101\">\n" +
				"<Coordinates>\n" +
				"<IfcLengthMeasure>2500.0</IfcLengthMeasure>\n" +
				"<IfcLengthMeasure>0.0</IfcLengthMeasure>\n" +
				"<IfcLengthMeasure>0.0</IfcLengthMeasure>\n" +
				"</Coordinates>\n" +
				"</IfcCartesianPoint>\n" +
				"<IfcDirection id=\"i102\">\n" +
				"<DirectionRatios>\n" +
				"<ex:double-wrapper>0.</ex:double-wrapper>\n" +
				"<ex:double-wrapper>1.</ex:double-wrapper>\n" +
				"<ex:double-wrapper>0.</ex:double-wrapper>\n" +
				"</DirectionRatios>\n" +
				"</IfcDirection>\n" +
				"</uos>\n" +
				"</ex:iso_10303_28>";
			return new StringReader( xml );
		}
		
		public static TextReader getAlternativeMinimumExample(){
			string xml = "<ex:iso_10303_28 xmlns:xlink=\"http://www.w3.org/1999/xlink\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns=\"http://www.iai-tech.org/ifcXML/IFC2x3/FINAL\" version=\"2.0\" xmlns:ex=\"urn:iso.org:standard:10303:part(28):version(2):xmlschema:common\">\n" +
				"<ex:iso_10303_28_header>\n" +
				"<ex:name>An Example</ex:name>\n" +
				"<ex:time_stamp>2010-11-12T13:04:00</ex:time_stamp>\n" +
				"<ex:author>John Hancock</ex:author>\n" +
				"<ex:organization>MegaCorp</ex:organization>\n" +
				"<ex:preprocessor_version>a preprocessor</ex:preprocessor_version>\n" +
				"<ex:originating_system>IfcDotNet Library</ex:originating_system>\n" +
				"<ex:authorization>none</ex:authorization>\n" +
				"<ex:documentation>documentation</ex:documentation>\n" +
				"</ex:iso_10303_28_header>\n" +
				"<ex:uos xsi:type=\"uos\" id=\"uos_1\" configuration=\"i-ifc2x3\">\n" +
				"<ex:Entity xsi:type=\"IfcOrganization\" id=\"i1101\">\n" +
				"<Id xsi:nil=\"true\" />\n" +
				"<Name>MegaCorp</Name>\n" +
				"<Description xsi:nil=\"true\" />\n" +
				"<Roles xsi:nil=\"true\" />\n" +
				"<Addresses xsi:nil=\"true\" />\n" +
				"</ex:Entity>\n" +
				"<ex:Entity xsi:type=\"IfcCartesianPoint\" id=\"i101\">\n" +
				"<Coordinates ex:itemType=\"ifc:IfcLengthMeasure\" ex:cType=\"list\">\n" +
				"<IfcLengthMeasure>2500</IfcLengthMeasure>\n" +
				"<IfcLengthMeasure>0</IfcLengthMeasure>\n" +
				"<IfcLengthMeasure>0</IfcLengthMeasure>\n" +
				"</Coordinates>\n" +
				"</ex:Entity>\n" +
				"<ex:Entity xsi:type=\"IfcDirection\" id=\"i102\">\n" +
				"<DirectionRatios ex:itemType=\"ex:double-wrapper\" ex:cType=\"list\">\n" +
				"<ex:double-wrapper>0</ex:double-wrapper>\n" +
				"<ex:double-wrapper>1</ex:double-wrapper>\n" +
				"<ex:double-wrapper>0</ex:double-wrapper>\n" +
				"</DirectionRatios>\n" +
				"</ex:Entity>\n" +
				"</ex:uos>\n" +
				"</ex:iso_10303_28>";
			return new StringReader( xml );
		}
		
		public static TextReader getFailingMinimumExample(){
			string xml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n" +
				"<ex:iso_10303_28\n" +
				"xmlns:xlink=\"http://www.w3.org/1999/xlink\"\n" +
				"xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"\n" +
				"xmlns:ex=\n" +
				"\"urn:iso.org:standard:10303:part(28):version(2):xmlschema:common\"\n" +
				"xmlns=\"http://www.iai-tech.org/ifcXML/IFC2x3/FINAL\"\n" +
				"xsi:schemaLocation=\"http://www.iai-tech.org/ifcXML/IFC2x3/FINAL\n" +
				"http://www.iai-tech.org/ifcXML/IFC2x3/FINAL/IFC2x3.xsd\"\n" +
				"version=\"2.0\"\n" +
				">\n" +
				"<ex:iso_10303_28_header>\n" +
				"<ex:name>An Example</ex:name>\n" +
				"<ex:time_stamp>2010-11-12T13:04:00</ex:time_stamp>\n" +
				"<ex:author>John Hancock</ex:author>\n" +
				"<ex:organization>MegaCorp</ex:organization>\n" +
				"<ex:preprocessor_version>a preprocessor</ex:preprocessor_version>\n" +
				"<ex:originating_system>IfcDotNet Library</ex:originating_system>\n" +
				"<ex:authorization>none</ex:authorization>\n" +
				"<ex:documentation>documentation</ex:documentation>\n" +
				"</ex:iso_10303_28_header>\n" +
				"<uos description=\"\" configuration=\"i-ifc2x3\" edo=\"\">\n" +
				"<IfcOrganization id=\"i1101\">\n" +
				"<Name>Ramboll</Name>\n" +
				"</IfcOrganization>\n" +
				"<IfcCartesianPoint id=\"i101\">\n" +
				"<Coordinates>\n" +
				"<IfcLengthMeasure>2500.0</IfcLengthMeasure>\n" +
				"<IfcLengthMeasure>0.0</IfcLengthMeasure>\n" +
				"<IfcLengthMeasure>0.0</IfcLengthMeasure>\n" +
				"</Coordinates>\n" +
				"</IfcCartesianPoint>\n" +
				"<IfcDirection id=\"i102\">\n" +
				"<DirectionRatios>\n" +
				"<ex:double-wrapper>0.</ex:double-wrapper>\n" +
				"<ex:double-wrapper>1.</ex:double-wrapper>\n" +
				"<ex:double-wrapper>0.</ex:double-wrapper>\n" +
				"</DirectionRatios>\n" +
				"</IfcDirection>\n" +
				"</uos>\n" +
				"</ex:iso_10303_28>";
			return new StringReader( xml );
		}
		
		public static iso_10303_28 buildFailingMinimumExampleObject(){
			iso_10303_28 iso10303                           = new iso_10303_28();
			iso10303.version                                = "2.0";
			iso10303.iso_10303_28_header                    = new iso_10303_28_header();
			iso10303.iso_10303_28_header.author             = "John Hancock";
			iso10303.iso_10303_28_header.organization       = "MegaCorp";
			iso10303.iso_10303_28_header.time_stamp         = new DateTime(2010,11,12,13,04,00);
			iso10303.iso_10303_28_header.name               = "An Example";
			iso10303.iso_10303_28_header.preprocessor_version = "a preprocessor";
			iso10303.iso_10303_28_header.originating_system = "IfcDotNet Library";
			iso10303.iso_10303_28_header.authorization      = "none";
			iso10303.iso_10303_28_header.documentation      = "documentation";
			
			
			IfcOrganization organization                    = new IfcOrganization();
			organization.entityid                           = "i1101";
			organization.Name                               = "MegaCorp";
			
			IfcCartesianPoint point                         = new IfcCartesianPoint();
			point.entityid                                  = "i101";
			
			IfcCartesianPointCoordinates coords             = new IfcCartesianPointCoordinates();
			IfcLengthMeasure1 measure0                      = new IfcLengthMeasure1();
			measure0.Value                                  = 2500.0;
			IfcLengthMeasure1 measure1                      = new IfcLengthMeasure1();
			measure1.Value                                  = 0;
			IfcLengthMeasure1 measure2                      = new IfcLengthMeasure1();
			measure2.Value                                  = 0;
			coords.IfcLengthMeasure                         = new IfcLengthMeasure1[3];
			coords.IfcLengthMeasure[0]                      = measure0;
			coords.IfcLengthMeasure[1]                      = measure1;
			coords.IfcLengthMeasure[2]                      = measure2;
			point.Coordinates = coords;
			
			IfcDirection dir                                = new IfcDirection();
			dir.entityid                                    = "i102";
			IfcDirectionDirectionRatios dirRatios           = new IfcDirectionDirectionRatios();
			doublewrapper double0                           = new doublewrapper();
			double0.Value                                   = 0;
			doublewrapper double1                           = new doublewrapper();
			double1.Value                                   = 1;
			doublewrapper double2                           = new doublewrapper();
			double2.Value                                   = 0;
			
			dirRatios.doublewrapper                         = new doublewrapper[3];
			dirRatios.doublewrapper[0]                      = double0;
			dirRatios.doublewrapper[1]                      = double1;
			dirRatios.doublewrapper[2]                      = double2;
			
			dir.DirectionRatios                             = dirRatios;
			
			uos1 uos        = new uos1();
			uos.configuration = new string[]{"i-ifc2x3"};
			uos.Items       = new Entity[3];
			uos.Items[0]    = organization;
			uos.Items[1]    = point;
			uos.Items[2]    = dir;
			iso10303.uos    = uos;
			
			return iso10303;
		}
		
		public static iso_10303_28 buildMinimumExampleObject(){
			iso_10303_28 iso = buildFailingMinimumExampleObject();
			iso.uos.id = "uos_1";
			return iso;
		}
	}
}
