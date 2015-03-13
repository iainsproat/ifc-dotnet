Ifc-dotnet is a library which provides .Net classes and serializers/deserializers for working with [Industry Foundation Classes](http://buildingsmart.com/standards/buildingsmart-standards/ifc).

**Ifc-dotnet is very much in [Alpha](http://en.wikipedia.org/wiki/Software_release_life_cycle#Alpha) stage of development and should be treated accordingly, please do not run in a production environment and do expect things to be broken.** Thanks for testing Ifc-dotnet.  Please report any bugs you find to the [Issue Tracker](http://code.google.com/p/ifc-dotnet/issues/list).

Currently Ifc-dotnet supports the serialization (export) and deserialization (import) of STEP and ifcXml files to .Net classes representing the IFC2x3 standard.

To deserialize into an ifcXml format file:

```
using IfcDotNet;
using IfcDotNet.Schema;

IfcXmlSerializer serializer = new IfcXmlSerializer();
iso_10303 deserialized = serializer.Deserialize( new StreamReader( "/path/to/myFile.ifcXML" ) );
```

To deserialize into an IFC STEP format file:

```
using IfcDotNet;
using IfcDotNet.Schema;
using IfcDotNet.StepSerializer;

IfcStepSerializer serializer = new IfcStepSerializer();
iso_10303 deserialized = serializer.Deserialize( new StreamReader( "/path/to/myFile.ifc" ) );
```

and to serialize to both ifcXml and IFC STEP format files:
```
using IfcDotNet;
using IfcDotNet.Schema;
using IfcDotNet.StepSerializer;

//first build the object
iso_10303 iso10303                                  = new iso_10303();
            
iso10303.uos                                        = new uos1();
iso10303.uos.id                                     = "uos_1";
iso10303.uos.configuration                          = new string[]{"i-ifc2x3"};
            
iso10303.version                                    = "2.0";
            
iso10303.iso_10303_28_header                        = new iso_10303_28_header();
iso10303.iso_10303_28_header.author                 = "John Hancock";
iso10303.iso_10303_28_header.organization           = "MegaCorp";
iso10303.iso_10303_28_header.time_stamp             = new DateTime(2010,11,12,13,04,00);
iso10303.iso_10303_28_header.name                   = "An Example";
iso10303.iso_10303_28_header.preprocessor_version   = "a preprocessor";
iso10303.iso_10303_28_header.originating_system     = "IfcDotNet Library";
iso10303.iso_10303_28_header.authorization          = "none";
iso10303.iso_10303_28_header.documentation          = "documentation";

IfcOrganization organization                        = new IfcOrganization();
organization.entityid                               = "i1101";
organization.Name                                   = "MegaCorp";

IfcCartesianPoint point                             = new IfcCartesianPoint("i101",2500, 0, 0);

IfcDirection dir                                    = new IfcDirection("i102",0,1,0);

((uos1)iso10303.uos).Items                          = new Entity[]{organization, point, dir};

//create the writer in which to serialize
StringBuilder sb    = new StringBuilder();
StringWriter writer = new StringWriter( sb, CultureInfo.InvariantCulture );

//now serialize to IfcXml
IfcXmlSerializer xmlSerializer = new IfcXmlSerializer();
xmlSerializer.Serialize(writer, iso10303);
Console.Write( sb.ToString() ); //print the IfcXml to the console

//now serialize to Ifc STEP
sb = new StringBuilder();
writer = new StringWriter( sb, CultureInfo.InvariantCulture );
StepSerializer stepSerializer = new StepSerializer();
stepSerializer.Serialize(writer, iso10303);
Console.Write( sb.ToString() ); //print the Ifc STEP to the console
```

As the library is in Alpha stage of development there are no compiled downloads currently available.  Please instead [checkout](http://code.google.com/p/ifc-dotnet/source/checkout) the source from the [subversion repository](http://code.google.com/p/ifc-dotnet/source/browse/).  Please refer to the [unit tests](http://code.google.com/p/ifc-dotnet/source/browse/#svn%2Ftrunk%2FIfcDotNet_UnitTests) for further examples of the library in use.