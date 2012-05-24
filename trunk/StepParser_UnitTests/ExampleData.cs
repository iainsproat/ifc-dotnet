/*
 * Created by Iain Sproat
 * Date: 24/05/2012
 * Time: 15:33
 * 
 */
using System;

namespace StepParser_UnitTests
{
	/// <summary>
	/// Description of ExampleData.
	/// </summary>
	public class ExampleData
	{
		/// <summary>
        /// The following IFC example is copyright buildingSmart International.
        /// http://www.iai-tech.org/developers/get-started/hello-world/example-1
        /// </summary>
        /// <returns></returns>
        public static string StepIFC2X3SmallWallExample(){
            return "ISO-10303-21;\r\n" +
                "HEADER;\r\n" +
                "FILE_DESCRIPTION(('ViewDefinition [CoordinationView, QuantityTakeOffAddOnView]'), '2;1');\r\n" +
                "FILE_NAME('example.ifc', '2008-08-01T21:53:56', ('Architect'), ('Building Designer Office'), 'IFC Engine DLL version 1.02 beta', 'IFC Engine DLL version 1.02 beta', 'The authorising person');\r\n" +
                "FILE_SCHEMA(('IFC2X3'));\r\n" +
                "ENDSEC;\r\n" +
                "DATA;\r\n" +
                "#1 = IFCPROJECT('3MD_HkJ6X2EwpfIbCFm0g_', #2, 'Default Project', 'Description of Default Project', $, $, $, (#20), #7);\r\n" +
                "#2 = IFCOWNERHISTORY(#3, #6, $, .ADDED., $, $, $, 1217620436);\r\n" +
                "#3 = IFCPERSONANDORGANIZATION(#4, #5, $);\r\n" +
                "#4 = IFCPERSON('ID001', 'Bonsma', 'Peter', $, $, $, $, $);\r\n" +
                "#5 = IFCORGANIZATION($, 'TNO', 'TNO Building Innovation', $, $);\r\n" +
                "#6 = IFCAPPLICATION(#5, '0.10', 'Test Application', 'TA 1001');\r\n" +
                "#7 = IFCUNITASSIGNMENT((#8, #9, #10, #11, #15, #16, #17, #18, #19));\r\n" +
                "#8 = IFCSIUNIT(*, .LENGTHUNIT., $, .METRE.);\r\n" +
                "#9 = IFCSIUNIT(*, .AREAUNIT., $, .SQUARE_METRE.);\r\n" +
                "#10 = IFCSIUNIT(*, .VOLUMEUNIT., $, .CUBIC_METRE.);\r\n" +
                "#11 = IFCCONVERSIONBASEDUNIT(#12, .PLANEANGLEUNIT., 'DEGREE', #13);\r\n" +
                "#12 = IFCDIMENSIONALEXPONENTS(0, 0, 0, 0, 0, 0, 0);\r\n" +
                "#13 = IFCMEASUREWITHUNIT(IFCPLANEANGLEMEASURE(1.745E-2), #14);\r\n" +
                "#14 = IFCSIUNIT(*, .PLANEANGLEUNIT., $, .RADIAN.);\r\n" +
                "#15 = IFCSIUNIT(*, .SOLIDANGLEUNIT., $, .STERADIAN.);\r\n" +
                "#16 = IFCSIUNIT(*, .MASSUNIT., $, .GRAM.);\r\n" +
                "#17 = IFCSIUNIT(*, .TIMEUNIT., $, .SECOND.);\r\n" +
                "#18 = IFCSIUNIT(*, .THERMODYNAMICTEMPERATUREUNIT., $, .DEGREE_CELSIUS.);\r\n" +
                "#19 = IFCSIUNIT(*, .LUMINOUSINTENSITYUNIT., $, .LUMEN.);\r\n" +
                "#20 = IFCGEOMETRICREPRESENTATIONCONTEXT($, 'Model', 3, 1.000E-5, #21, $);\r\n" +
                "#21 = IFCAXIS2PLACEMENT3D(#22, $, $);\r\n" +
                "#22 = IFCCARTESIANPOINT((0., 0., 0.));\r\n" +
                "#23 = IFCSITE('3rNg_N55v4CRBpQVbZJoHB', #2, 'Default Site', 'Description of Default Site', $, #24, $, $, .ELEMENT., (24, 28, 0), (54, 25, 0), $, $, $);\r\n" +
                "#24 = IFCLOCALPLACEMENT($, #25);\r\n" +
                "#25 = IFCAXIS2PLACEMENT3D(#26, #27, #28);\r\n" +
                "#26 = IFCCARTESIANPOINT((0., 0., 0.));\r\n" +
                "#27 = IFCDIRECTION((0., 0., 1.));\r\n" +
                "#28 = IFCDIRECTION((1., 0., 0.));\r\n" +
                "#29 = IFCBUILDING('0yf_M5JZv9QQXly4dq_zvI', #2, 'Default Building', 'Description of Default Building', $, #30, $, $, .ELEMENT., $, $, $);\r\n" +
                "#30 = IFCLOCALPLACEMENT(#24, #31);\r\n" +
                "#31 = IFCAXIS2PLACEMENT3D(#32, #33, #34);\r\n" +
                "#32 = IFCCARTESIANPOINT((0., 0., 0.));\r\n" +
                "#33 = IFCDIRECTION((0., 0., 1.));\r\n" +
                "#34 = IFCDIRECTION((1., 0., 0.));\r\n" +
                "#35 = IFCBUILDINGSTOREY('0C87kaqBXF$xpGmTZ7zxN$', #2, 'Default Building Storey', 'Description of Default Building Storey', $, #36, $, $, .ELEMENT., 0.);\r\n" +
                "#36 = IFCLOCALPLACEMENT(#30, #37);\r\n" +
                "#37 = IFCAXIS2PLACEMENT3D(#38, #39, #40);\r\n" +
                "#38 = IFCCARTESIANPOINT((0., 0., 0.));\r\n" +
                "#39 = IFCDIRECTION((0., 0., 1.));\r\n" +
                "#40 = IFCDIRECTION((1., 0., 0.));\r\n" +
                "#41 = IFCRELAGGREGATES('2168U9nPH5xB3UpDx_uK11', #2, 'BuildingContainer', 'BuildingContainer for BuildingStories', #29, (#35));\r\n" +
                "#42 = IFCRELAGGREGATES('3JuhmQJDj9xPnAnWoNb94X', #2, 'SiteContainer', 'SiteContainer For Buildings', #23, (#29));\r\n" +
                "#43 = IFCRELAGGREGATES('1Nl_BIjGLBke9u_6U3IWlW', #2, 'ProjectContainer', 'ProjectContainer for Sites', #1, (#23));\r\n" +
                "#44 = IFCRELCONTAINEDINSPATIALSTRUCTURE('2O_dMuDnr1Ahv28oR6ZVpr', #2, 'Default Building', 'Contents of Building Storey', (#45, #124), #35);\r\n" +
                "#45 = IFCWALLSTANDARDCASE('3vB2YO$MX4xv5uCqZZG05x', #2, 'Wall xyz', 'Description of Wall', $, #46, #51, $);\r\n" +
                "#46 = IFCLOCALPLACEMENT(#36, #47);\r\n" +
                "#47 = IFCAXIS2PLACEMENT3D(#48, #49, #50);\r\n" +
                "#48 = IFCCARTESIANPOINT((0., 0., 0.));\r\n" +
                "#49 = IFCDIRECTION((0., 0., 1.));\r\n" +
                "#50 = IFCDIRECTION((1., 0., 0.));\r\n" +
                "#51 = IFCPRODUCTDEFINITIONSHAPE($, $, (#79, #83));\r\n" +
                "#52 = IFCPROPERTYSET('18RtPv6efDwuUOMduCZ7rH', #2, 'Pset_WallCommon', $, (#53, #54, #55, #56, #57, #58, #59, #60, #61, #62));\r\n" +
                "#53 = IFCPROPERTYSINGLEVALUE('Reference', 'Reference', IFCTEXT(''), $);\r\n" +
                "#54 = IFCPROPERTYSINGLEVALUE('AcousticRating', 'AcousticRating', IFCTEXT(''), $);\r\n" +
                "#55 = IFCPROPERTYSINGLEVALUE('FireRating', 'FireRating', IFCTEXT(''), $);\r\n" +
                "#56 = IFCPROPERTYSINGLEVALUE('Combustible', 'Combustible', IFCBOOLEAN(.F.), $);\r\n" +
                "#57 = IFCPROPERTYSINGLEVALUE('SurfaceSpreadOfFlame', 'SurfaceSpreadOfFlame', IFCTEXT(''), $);\r\n" +
                "#58 = IFCPROPERTYSINGLEVALUE('ThermalTransmittance', 'ThermalTransmittance', IFCREAL(2.400E-1), $);\r\n" +
                "#59 = IFCPROPERTYSINGLEVALUE('IsExternal', 'IsExternal', IFCBOOLEAN(.T.), $);\r\n" +
                "#60 = IFCPROPERTYSINGLEVALUE('ExtendToStructure', 'ExtendToStructure', IFCBOOLEAN(.F.), $);\r\n" +
                "#61 = IFCPROPERTYSINGLEVALUE('LoadBearing', 'LoadBearing', IFCBOOLEAN(.F.), $);\r\n" +
                "#62 = IFCPROPERTYSINGLEVALUE('Compartmentation', 'Compartmentation', IFCBOOLEAN(.F.), $);\r\n" +
                "#63 = IFCRELDEFINESBYPROPERTIES('3IxFuNHRvBDfMT6_FiWPEz', #2, $, $, (#45), #52);\r\n" +
                "#64 = IFCELEMENTQUANTITY('10m6qcXSj0Iu4RVOK1omPJ', #2, 'BaseQuantities', $, $, (#65, #66, #67, #68, #69, #70, #71, #72));\r\n" +
                "#65 = IFCQUANTITYLENGTH('Width', 'Width', $, 3.000E-1);\r\n" +
                "#66 = IFCQUANTITYLENGTH('Lenght', 'Lenght', $, 5.);\r\n" +
                "#67 = IFCQUANTITYAREA('GrossSideArea', 'GrossSideArea', $, 11.500);\r\n" +
                "#68 = IFCQUANTITYAREA('NetSideArea', 'NetSideArea', $, 10.450);\r\n" +
                "#69 = IFCQUANTITYVOLUME('GrossVolume', 'GrossVolume', $, 3.450);\r\n" +
                "#70 = IFCQUANTITYVOLUME('NetVolume', 'NetVolume', $, 3.135);\r\n" +
                "#71 = IFCQUANTITYLENGTH('Height', 'Height', $, 2.300);\r\n" +
                "#72 = IFCQUANTITYAREA('GrossFootprintArea', 'GrossFootprintArea', $, 1.500);\r\n" +
                "#73 = IFCRELDEFINESBYPROPERTIES('0cpLgxVi9Ew8B08wF2Ql1w', #2, $, $, (#45), #64);\r\n" +
                "#74 = IFCRELASSOCIATESMATERIAL('2zeggBjk9A5wcc3k9CYqdL', #2, $, $, (#45), #75);\r\n" +
                "#75 = IFCMATERIALLAYERSETUSAGE(#76, .AXIS2., .POSITIVE., -1.500E-1);\r\n" +
                "#76 = IFCMATERIALLAYERSET((#77), $);\r\n" +
                "#77 = IFCMATERIALLAYER(#78, 3.000E-1, $);\r\n" +
                "#78 = IFCMATERIAL('Name of the material used for the wall');\r\n" +
                "#79 = IFCSHAPEREPRESENTATION(#20, 'Axis', 'Curve2D', (#80));\r\n" +
                "#80 = IFCPOLYLINE((#81, #82));\r\n" +
                "#81 = IFCCARTESIANPOINT((0., 1.500E-1));\r\n" +
                "#82 = IFCCARTESIANPOINT((5., 1.500E-1));\r\n" +
                "#83 = IFCSHAPEREPRESENTATION(#20, 'Body', 'SweptSolid', (#84));\r\n" +
                "#84 = IFCEXTRUDEDAREASOLID(#85, #92, #96, 2.300);\r\n" +
                "#85 = IFCARBITRARYCLOSEDPROFILEDEF(.AREA., $, #86);\r\n" +
                "#86 = IFCPOLYLINE((#87, #88, #89, #90, #91));\r\n" +
                "#87 = IFCCARTESIANPOINT((0., 0.));\r\n" +
                "#88 = IFCCARTESIANPOINT((0., 3.000E-1));\r\n" +
                "#89 = IFCCARTESIANPOINT((5., 3.000E-1));\r\n" +
                "#90 = IFCCARTESIANPOINT((5., 0.));\r\n" +
                "#91 = IFCCARTESIANPOINT((0., 0.));\r\n" +
                "#92 = IFCAXIS2PLACEMENT3D(#93, #94, #95);\r\n" +
                "#93 = IFCCARTESIANPOINT((0., 0., 0.));\r\n" +
                "#94 = IFCDIRECTION((0., 0., 1.));\r\n" +
                "#95 = IFCDIRECTION((1., 0., 0.));\r\n" +
                "#96 = IFCDIRECTION((0., 0., 1.));\r\n" +
                "#97 = IFCOPENINGELEMENT('2LcE70iQb51PEZynawyvuT', #2, 'Opening Element xyz', 'Description of Opening', $, #98, #103, $);\r\n" +
                "#98 = IFCLOCALPLACEMENT(#46, #99);\r\n" +
                "#99 = IFCAXIS2PLACEMENT3D(#100, #101, #102);\r\n" +
                "#100 = IFCCARTESIANPOINT((9.000E-1, 0., 2.500E-1));\r\n" +
                "#101 = IFCDIRECTION((0., 0., 1.));\r\n" +
                "#102 = IFCDIRECTION((1., 0., 0.));\r\n" +
                "#103 = IFCPRODUCTDEFINITIONSHAPE($, $, (#110));\r\n" +
                "#104 = IFCELEMENTQUANTITY('2yDPSWYWf319fWaWWvPxwA', #2, 'BaseQuantities', $, $, (#105, #106, #107));\r\n" +
                "#105 = IFCQUANTITYLENGTH('Depth', 'Depth', $, 3.000E-1);\r\n" +
                "#106 = IFCQUANTITYLENGTH('Height', 'Height', $, 1.400);\r\n" +
                "#107 = IFCQUANTITYLENGTH('Width', 'Width', $, 7.500E-1);\r\n" +
                "#108 = IFCRELDEFINESBYPROPERTIES('2UEO1blXL9sPmb1AMeW7Ax', #2, $, $, (#97), #104);\r\n" +
                "#109 = IFCRELVOIDSELEMENT('3lR5koIT51Kwudkm5eIoTu', #2, $, $, #45, #97);\r\n" +
                "#110 = IFCSHAPEREPRESENTATION(#20, 'Body', 'SweptSolid', (#111));\r\n" +
                "#111 = IFCEXTRUDEDAREASOLID(#112, #119, #123, 1.400);\r\n" +
                "#112 = IFCARBITRARYCLOSEDPROFILEDEF(.AREA., $, #113);\r\n" +
                "#113 = IFCPOLYLINE((#114, #115, #116, #117, #118));\r\n" +
                "#114 = IFCCARTESIANPOINT((0., 0.));\r\n" +
                "#115 = IFCCARTESIANPOINT((0., 3.000E-1));\r\n" +
                "#116 = IFCCARTESIANPOINT((7.500E-1, 3.000E-1));\r\n" +
                "#117 = IFCCARTESIANPOINT((7.500E-1, 0.));\r\n" +
                "#118 = IFCCARTESIANPOINT((0., 0.));\r\n" +
                "#119 = IFCAXIS2PLACEMENT3D(#120, #121, #122);\r\n" +
                "#120 = IFCCARTESIANPOINT((0., 0., 0.));\r\n" +
                "#121 = IFCDIRECTION((0., 0., 1.));\r\n" +
                "#122 = IFCDIRECTION((1., 0., 0.));\r\n" +
                "#123 = IFCDIRECTION((0., 0., 1.));\r\n" +
                "#124 = IFCWINDOW('0LV8Pid0X3IA3jJLVDPidY', #2, 'Window xyz', 'Description of Window', $, #125, #130, $, 1.400, 7.500E-1);\r\n" +
                "#125 = IFCLOCALPLACEMENT(#98, #126);\r\n" +
                "#126 = IFCAXIS2PLACEMENT3D(#127, #128, #129);\r\n" +
                "#127 = IFCCARTESIANPOINT((0., 1.000E-1, 0.));\r\n" +
                "#128 = IFCDIRECTION((0., 0., 1.));\r\n" +
                "#129 = IFCDIRECTION((1., 0., 0.));\r\n" +
                "#130 = IFCPRODUCTDEFINITIONSHAPE($, $, (#150));\r\n" +
                "#131 = IFCRELFILLSELEMENT('1CDlLMVMv1qw1giUXpQgxI', #2, $, $, #97, #124);\r\n" +
                "#132 = IFCPROPERTYSET('0fhz_bHU54xB$tXHjHPUZl', #2, 'Pset_WindowCommon', $, (#133, #134, #135, #136, #137, #138, #139, #140, #141, #142, #143, #144));\r\n" +
                "#133 = IFCPROPERTYSINGLEVALUE('Reference', 'Reference', IFCTEXT(''), $);\r\n" +
                "#134 = IFCPROPERTYSINGLEVALUE('FireRating', 'FireRating', IFCTEXT(''), $);\r\n" +
                "#135 = IFCPROPERTYSINGLEVALUE('AcousticRating', 'AcousticRating', IFCTEXT(''), $);\r\n" +
                "#136 = IFCPROPERTYSINGLEVALUE('SecurityRating', 'SecurityRating', IFCTEXT(''), $);\r\n" +
                "#137 = IFCPROPERTYSINGLEVALUE('IsExternal', 'IsExternal', IFCBOOLEAN(.T.), $);\r\n" +
                "#138 = IFCPROPERTYSINGLEVALUE('Infiltration', 'Infiltration', IFCBOOLEAN(.F.), $);\r\n" +
                "#139 = IFCPROPERTYSINGLEVALUE('ThermalTransmittance', 'ThermalTransmittance', IFCREAL(2.400E-1), $);\r\n" +
                "#140 = IFCPROPERTYSINGLEVALUE('GlazingAresFraction', 'GlazingAresFraction', IFCREAL(7.000E-1), $);\r\n" +
                "#141 = IFCPROPERTYSINGLEVALUE('HandicapAccessible', 'HandicapAccessible', IFCBOOLEAN(.F.), $);\r\n" +
                "#142 = IFCPROPERTYSINGLEVALUE('FireExit', 'FireExit', IFCBOOLEAN(.F.), $);\r\n" +
                "#143 = IFCPROPERTYSINGLEVALUE('SelfClosing', 'SelfClosing', IFCBOOLEAN(.F.), $);\r\n" +
                "#144 = IFCPROPERTYSINGLEVALUE('SmokeStop', 'SmokeStop', IFCBOOLEAN(.F.), $);\r\n" +
                "#145 = IFCRELDEFINESBYPROPERTIES('2fHMxamlj5DvGvEKfCk8nj', #2, $, $, (#124), #132);\r\n" +
                "#146 = IFCELEMENTQUANTITY('0bB_7AP5v5OBZ90TDvo0Fo', #2, 'BaseQuantities', $, $, (#147, #148));\r\n" +
                "#147 = IFCQUANTITYLENGTH('Height', 'Height', $, 1.400);\r\n" +
                "#148 = IFCQUANTITYLENGTH('Width', 'Width', $, 7.500E-1);\r\n" +
                "#149 = IFCRELDEFINESBYPROPERTIES('0FmgI0DRX49OXL_$Wa2P1E', #2, $, $, (#124), #146);\r\n" +
                "#150 = IFCSHAPEREPRESENTATION(#20, 'Body', 'SweptSolid', (#151));\r\n" +
                "#151 = IFCEXTRUDEDAREASOLID(#152, #159, #163, 1.400);\r\n" +
                "#152 = IFCARBITRARYCLOSEDPROFILEDEF(.AREA., $, #153);\r\n" +
                "#153 = IFCPOLYLINE((#154, #155, #156, #157, #158));\r\n" +
                "#154 = IFCCARTESIANPOINT((0., 0.));\r\n" +
                "#155 = IFCCARTESIANPOINT((0., 1.000E-1));\r\n" +
                "#156 = IFCCARTESIANPOINT((7.500E-1, 1.000E-1));\r\n" +
                "#157 = IFCCARTESIANPOINT((7.500E-1, 0.));\r\n" +
                "#158 = IFCCARTESIANPOINT((0., 0.));\r\n" +
                "#159 = IFCAXIS2PLACEMENT3D(#160, #161, #162);\r\n" +
                "#160 = IFCCARTESIANPOINT((0., 0., 0.));\r\n" +
                "#161 = IFCDIRECTION((0., 0., 1.));\r\n" +
                "#162 = IFCDIRECTION((1., 0., 0.));\r\n" +
                "#163 = IFCDIRECTION((0., 0., 1.));\r\n" +
                "ENDSEC;\r\n" +
                "END-ISO-10303-21;";
        }
        
	}
}
