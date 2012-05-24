/*
 * Created by Iain Sproat
 * Date: 24/05/2012
 * Time: 15:44
 * 
 */
using System;
using System.IO;

namespace IfcDotNet_UnitTests.sampleData
{
	/// <summary>
	/// This class holds string data for various IfcXml examples used in unit testing
	/// </summary>
	public class ExampleIfcXmlData
	{
		public static string getMinimumExampleXmlString(){
            return "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\r\n" +
                "<ex:iso_10303_28 " +
                "xmlns:xlink=\"http://www.w3.org/1999/xlink\" " +
                "xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" " +
                "xmlns=\"http://www.iai-tech.org/ifcXML/IFC2x3/FINAL\" " +
                "version=\"2.0\" " +
                "xmlns:ex=\"urn:iso.org:standard:10303:part(28):version(2):xmlschema:common\"" +
                " xsi:schemaLocation=\"http://www.iai-tech.org/ifcXML/IFC2x3/FINAL " +
                "http://www.iai-tech.org/ifcXML/IFC2x3/FINAL/IFC2x3.xsd\"" +
                ">\r\n" +
                "  <ex:iso_10303_28_header>\r\n" +
                "    <ex:name>An Example</ex:name>\r\n" +
                "    <ex:time_stamp>2010-11-12T13:04:00</ex:time_stamp>\r\n" +
                "    <ex:author>John Hancock</ex:author>\r\n" +
                "    <ex:organization>MegaCorp</ex:organization>\r\n" +
                "    <ex:preprocessor_version>a preprocessor</ex:preprocessor_version>\r\n" +
                "    <ex:originating_system>IfcDotNet Library</ex:originating_system>\r\n" +
                "    <ex:authorization>none</ex:authorization>\r\n" +
                "    <ex:documentation>documentation</ex:documentation>\r\n" +
                "  </ex:iso_10303_28_header>\r\n" +
                "  <uos id=\"uos_1\" configuration=\"i-ifc2x3\">\r\n" +
                "    <IfcOrganization id=\"i1101\">\r\n" +
                "      <Name>MegaCorp</Name>\r\n" +
                "    </IfcOrganization>\r\n" +
                "    <IfcCartesianPoint id=\"i101\">\r\n" +
                "      <Coordinates>\r\n" +
                "        <IfcLengthMeasure>2500.0</IfcLengthMeasure>\r\n" +
                "        <IfcLengthMeasure>0.0</IfcLengthMeasure>\r\n" +
                "        <IfcLengthMeasure>0.0</IfcLengthMeasure>\r\n" +
                "      </Coordinates>\r\n" +
                "    </IfcCartesianPoint>\r\n" +
                "    <IfcDirection id=\"i102\">\r\n" +
                "      <DirectionRatios>\r\n" +
                "        <ex:double-wrapper>0.</ex:double-wrapper>\r\n" +
                "        <ex:double-wrapper>1.</ex:double-wrapper>\r\n" +
                "        <ex:double-wrapper>0.</ex:double-wrapper>\r\n" +
                "      </DirectionRatios>\r\n" +
                "    </IfcDirection>\r\n" +
                "  </uos>\r\n" +
                "</ex:iso_10303_28>";
        }
		
		        public static TextReader getMinimumExampleXml(){
            return new StringReader( getMinimumExampleXmlString() );
        }
        
        public static string getAlternativeMinimumExampleXmlString(){
            return "<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n" + //FIXME why is this utf-16 and not UTF-8?
                "<ex:iso_10303_28 xmlns:xlink=\"http://www.w3.org/1999/xlink\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns=\"http://www.iai-tech.org/ifcXML/IFC2x3/FINAL\" version=\"2.0\" xmlns:ex=\"urn:iso.org:standard:10303:part(28):version(2):xmlschema:common\">\r\n" +
                "  <ex:iso_10303_28_header>\r\n" +
                "    <ex:name>An Example</ex:name>\r\n" +
                "    <ex:time_stamp>2010-11-12T13:04:00</ex:time_stamp>\r\n" +
                "    <ex:author>John Hancock</ex:author>\r\n" +
                "    <ex:organization>MegaCorp</ex:organization>\r\n" +
                "    <ex:preprocessor_version>a preprocessor</ex:preprocessor_version>\r\n" +
                "    <ex:originating_system>IfcDotNet Library</ex:originating_system>\r\n" +
                "    <ex:authorization>none</ex:authorization>\r\n" +
                "    <ex:documentation>documentation</ex:documentation>\r\n" +
                "  </ex:iso_10303_28_header>\r\n" +
                "  <ex:uos xsi:type=\"uos\" id=\"uos_1\" configuration=\"i-ifc2x3\">\r\n" +
                "    <ex:Entity xsi:type=\"IfcOrganization\" id=\"i1101\">\r\n" +
                "      <Id xsi:nil=\"true\" />\r\n" +
                "      <Name>MegaCorp</Name>\r\n" +
                "      <Description xsi:nil=\"true\" />\r\n" +
                "      <Roles xsi:nil=\"true\" />\r\n" +
                "      <Addresses xsi:nil=\"true\" />\r\n" +
                "    </ex:Entity>\r\n" +
                "    <ex:Entity xsi:type=\"IfcCartesianPoint\" id=\"i101\">\r\n" +
                "      <Coordinates ex:itemType=\"ifc:IfcLengthMeasure\" ex:cType=\"list\">\r\n" +
                "        <IfcLengthMeasure>2500</IfcLengthMeasure>\r\n" +
                "        <IfcLengthMeasure>0</IfcLengthMeasure>\r\n" +
                "        <IfcLengthMeasure>0</IfcLengthMeasure>\r\n" +
                "      </Coordinates>\r\n" +
                "    </ex:Entity>\r\n" +
                "    <ex:Entity xsi:type=\"IfcDirection\" id=\"i102\">\r\n" +
                "      <DirectionRatios ex:itemType=\"ex:double-wrapper\" ex:cType=\"list\">\r\n" +
                "        <ex:double-wrapper>0</ex:double-wrapper>\r\n" +
                "        <ex:double-wrapper>1</ex:double-wrapper>\r\n" +
                "        <ex:double-wrapper>0</ex:double-wrapper>\r\n" +
                "      </DirectionRatios>\r\n" +
                "    </ex:Entity>\r\n" +
                "  </ex:uos>\r\n" +
                "</ex:iso_10303_28>";
        }
        public static TextReader getAlternativeMinimumExampleXml(){
            return new StringReader( getAlternativeMinimumExampleXmlString() );
        }
        
        public static string getExpectedXmlOutputString(){
            return "<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n" + //FIXME why is this utf-16 and not utf-8
                "<ex:iso_10303_28 " +
                "xmlns:xlink=\"http://www.w3.org/1999/xlink\" " +
                "xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" " +
                "xmlns=\"http://www.iai-tech.org/ifcXML/IFC2x3/FINAL\" " +
                "version=\"2.0\" " +
                "xmlns:ex=\"urn:iso.org:standard:10303:part(28):version(2):xmlschema:common\">\r\n" +
                "  <ex:iso_10303_28_header>\r\n" +
                "    <ex:name>An Example</ex:name>\r\n" +
                "    <ex:time_stamp>2010-11-12T13:04:00</ex:time_stamp>\r\n" +
                "    <ex:author>John Hancock</ex:author>\r\n" +
                "    <ex:organization>MegaCorp</ex:organization>\r\n" +
                "    <ex:preprocessor_version>a preprocessor</ex:preprocessor_version>\r\n" +
                "    <ex:originating_system>IfcDotNet Library</ex:originating_system>\r\n" +
                "    <ex:authorization>none</ex:authorization>\r\n" +
                "    <ex:documentation>documentation</ex:documentation>\r\n" +
                "  </ex:iso_10303_28_header>\r\n" +
                "  <uos id=\"uos_1\" configuration=\"i-ifc2x3\">\r\n" +
                "    <IfcOrganization id=\"i1101\">\r\n" +
                "      <Id xsi:nil=\"true\" />\r\n" +
                "      <Name>MegaCorp</Name>\r\n" +
                "      <Description xsi:nil=\"true\" />\r\n" +
                "      <Roles xsi:nil=\"true\" />\r\n" +
                "      <Addresses xsi:nil=\"true\" />\r\n" +
                "    </IfcOrganization>\r\n" +
                "    <IfcCartesianPoint id=\"i101\">\r\n" +
                "      <Coordinates ex:itemType=\"ifc:IfcLengthMeasure\" ex:cType=\"list\">\r\n" +
                "        <IfcLengthMeasure>2500</IfcLengthMeasure>\r\n" +
                "        <IfcLengthMeasure>0</IfcLengthMeasure>\r\n" +
                "        <IfcLengthMeasure>0</IfcLengthMeasure>\r\n" +
                "      </Coordinates>\r\n" +
                "    </IfcCartesianPoint>\r\n" +
                "    <IfcDirection id=\"i102\">\r\n" +
                "      <DirectionRatios ex:itemType=\"ex:double-wrapper\" ex:cType=\"list\">\r\n" +
                "        <ex:double-wrapper>0</ex:double-wrapper>\r\n" +
                "        <ex:double-wrapper>1</ex:double-wrapper>\r\n" +
                "        <ex:double-wrapper>0</ex:double-wrapper>\r\n" +
                "      </DirectionRatios>\r\n" +
                "    </IfcDirection>\r\n" +
                "  </uos>\r\n" +
                "</ex:iso_10303_28>";
        }
        
        public static TextReader getFailingMinimumExampleXml(){
            string xml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\r\n" +
                "<ex:iso_10303_28\r\n" +
                "xmlns:xlink=\"http://www.w3.org/1999/xlink\"\r\n" +
                "xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"\r\n" +
                "xmlns:ex=\r\n" +
                "\"urn:iso.org:standard:10303:part(28):version(2):xmlschema:common\"\r\n" +
                "xmlns=\"http://www.iai-tech.org/ifcXML/IFC2x3/FINAL\"\r\n" +
                "xsi:schemaLocation=\"http://www.iai-tech.org/ifcXML/IFC2x3/FINAL\r\n" +
                "http://www.iai-tech.org/ifcXML/IFC2x3/FINAL/IFC2x3.xsd\"\r\n" +
                "version=\"2.0\"\r\n" +
                ">\r\n" +
                "<ex:iso_10303_28_header>\r\n" +
                "<ex:name>An Example</ex:name>\r\n" +
                "<ex:time_stamp>2010-11-12T13:04:00</ex:time_stamp>\r\n" +
                "<ex:author>John Hancock</ex:author>\r\n" +
                "<ex:organization>MegaCorp</ex:organization>\r\n" +
                "<ex:preprocessor_version>a preprocessor</ex:preprocessor_version>\r\n" +
                "<ex:originating_system>IfcDotNet Library</ex:originating_system>\r\n" +
                "<ex:authorization>none</ex:authorization>\r\n" +
                "<ex:documentation>documentation</ex:documentation>\r\n" +
                "</ex:iso_10303_28_header>\r\n" +
                "<uos description=\"\" configuration=\"i-ifc2x3\" edo=\"\">\r\n" +
                "<IfcOrganization id=\"i1101\">\r\n" +
                "<Name>Ramboll</Name>\r\n" +
                "</IfcOrganization>\r\n" +
                "<IfcCartesianPoint id=\"i101\">\r\n" +
                "<Coordinates>\r\n" +
                "<IfcLengthMeasure>2500.0</IfcLengthMeasure>\r\n" +
                "<IfcLengthMeasure>0.0</IfcLengthMeasure>\r\n" +
                "<IfcLengthMeasure>0.0</IfcLengthMeasure>\r\n" +
                "</Coordinates>\r\n" +
                "</IfcCartesianPoint>\r\n" +
                "<IfcDirection id=\"i102\">\r\n" +
                "<DirectionRatios>\r\n" +
                "<ex:double-wrapper>0.</ex:double-wrapper>\r\n" +
                "<ex:double-wrapper>1.</ex:double-wrapper>\r\n" +
                "<ex:double-wrapper>0.</ex:double-wrapper>\r\n" +
                "</DirectionRatios>\r\n" +
                "</IfcDirection>\r\n" +
                "</uos>\r\n" +
                "</ex:iso_10303_28>";
            return new StringReader( xml );
        }
    	
    	public static string getExpectedJsonOutputString(){
            return "<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n" + //FIXME why is this utf-16 and not utf-8?
                "<ex:iso_10303_28 " +
                "xmlns:xlink=\"http://www.w3.org/1999/xlink\" " +
                "xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" " +
                "xmlns=\"http://www.iai-tech.org/ifcXML/IFC2x3/FINAL\" " +
                "version=\"2.0\" " +
                "xmlns:ex=\"urn:iso.org:standard:10303:part(28):version(2):xmlschema:common\">\r\n" +
                "  <ex:iso_10303_28_header>\r\n" +
                "    <ex:name>An Example</ex:name>\r\n" +
                "    <ex:time_stamp>2010-11-12T13:04:00</ex:time_stamp>\r\n" +
                "    <ex:author>John Hancock</ex:author>\r\n" +
                "    <ex:organization>MegaCorp</ex:organization>\r\n" +
                "    <ex:preprocessor_version>a preprocessor</ex:preprocessor_version>\r\n" +
                "    <ex:originating_system>IfcDotNet Library</ex:originating_system>\r\n" +
                "    <ex:authorization>none</ex:authorization>\r\n" +
                "    <ex:documentation>documentation</ex:documentation>\r\n" +
                "  </ex:iso_10303_28_header>\r\n" +
                "  <uos id=\"uos_1\" configuration=\"i-ifc2x3\">\r\n" +
                "    <IfcOrganization id=\"i1101\">\r\n" +
                "      <Id xsi:nil=\"true\" />\r\n" +
                "      <Name>MegaCorp</Name>\r\n" +
                "      <Description xsi:nil=\"true\" />\r\n" +
                "      <Roles xsi:nil=\"true\" />\r\n" +
                "      <Addresses xsi:nil=\"true\" />\r\n" +
                "    </IfcOrganization>\r\n" +
                "    <IfcCartesianPoint id=\"i101\">\r\n" +
                "      <Coordinates ex:itemType=\"ifc:IfcLengthMeasure\" ex:cType=\"list\">\r\n" +
                "        <IfcLengthMeasure>2500</IfcLengthMeasure>\r\n" +
                "        <IfcLengthMeasure>0</IfcLengthMeasure>\r\n" +
                "        <IfcLengthMeasure>0</IfcLengthMeasure>\r\n" +
                "      </Coordinates>\r\n" +
                "    </IfcCartesianPoint>\r\n" +
                "    <IfcDirection id=\"i102\">\r\n" +
                "      <DirectionRatios ex:itemType=\"ex:double-wrapper\" ex:cType=\"list\">\r\n" +
                "        <ex:double-wrapper>0</ex:double-wrapper>\r\n" +
                "        <ex:double-wrapper>1</ex:double-wrapper>\r\n" +
                "        <ex:double-wrapper>0</ex:double-wrapper>\r\n" +
                "      </DirectionRatios>\r\n" +
                "    </IfcDirection>\r\n" +
                "  </uos>\r\n" +
                "</ex:iso_10303_28>";
        }
	}
}
