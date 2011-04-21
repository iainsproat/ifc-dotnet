#region License
/*

Copyright 2011, Iain Sproat
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
using System.Text;

using IfcDotNet.Schema;
using IfcDotNet.JsonSerializer;

using NUnit.Framework;

using log4net;
using log4net.Config;

namespace IfcDotNet_UnitTests
{
	[TestFixture]
	public class TestJsonSerializer
	{
		private static readonly ILog logger = LogManager.GetLogger(typeof(TestIfcXmlSerializer));
        
		IfcJsonSerializer SUT;
		
		[SetUp]
		public void SetUp(){
            BasicConfigurator.Configure();
            
			SUT = new IfcJsonSerializer();
		}
		
		[Test]
		public void CanSerialize()
        {
            iso_10303 iso10303   = Utilities.buildMinimumExampleObject();
            
            StringBuilder sb = new StringBuilder();
            StringWriter writer = new StringWriter(sb);
            
            SUT.Serialize(writer, iso10303);
            
            string returnedValue = sb.ToString();
            Assert.IsFalse(String.IsNullOrEmpty( returnedValue ) );
            
            //dumping to the console
            logger.Debug( returnedValue );
            
            //Assert.AreEqual(Utilities.getExpectedXmlOutputString(), returnedValue );
		}
		
		[Test]
		public void CanDeserialize()
		{
			//TODO move the below string to Utilities
			//FIXME the header in the below json string is entirely incorrect!
			//FIXME the output $type is specific to this library.  It would be better if the ifcXml names were used
			string json = @"{
  ""$type"":""IfcDotNet.Schema.iso_10303, IfcDotNet"",
  ""iso_10303_28_header"":""An Example"",
  ""uos"":{
    ""$type"":""IfcDotNet.Schema.uos1, IfcDotNet"",
    ""Items"":[
      {
        ""$type"":""IfcDotNet.Schema.IfcOrganization, IfcDotNet"",
        ""Id"":null,
        ""Name"":""MegaCorp"",
        ""Description"":null,
        ""Roles"":null,
        ""Addresses"":null,
        ""href"":null,
        ""ref"":null,
        ""proxy"":null,
        ""edo"":null,
        ""entityid"":""i1101"",
        ""entitypath"":null,
        ""pos"":null
      },
      {
        ""$type"":""IfcDotNet.Schema.IfcCartesianPoint, IfcDotNet"",
        ""Coordinates"":[
          {
            ""$type"":""IfcDotNet.Schema.IfcLengthMeasure1, IfcDotNet"",
            ""id"":null,
            ""path"":null,
            ""pos"":null,
            ""Value"":2500.0
          },
          {
            ""$type"":""IfcDotNet.Schema.IfcLengthMeasure1, IfcDotNet"",
            ""id"":null,
            ""path"":null,
            ""pos"":null,
            ""Value"":0.0
          },
          {
            ""$type"":""IfcDotNet.Schema.IfcLengthMeasure1, IfcDotNet"",
            ""id"":null,
            ""path"":null,
            ""pos"":null,
            ""Value"":0.0
          }
        ],
        ""href"":null,
        ""ref"":null,
        ""proxy"":null,
        ""edo"":null,
        ""entityid"":""i101"",
        ""entitypath"":null,
        ""pos"":null
      },
      {
        ""$type"":""IfcDotNet.Schema.IfcDirection, IfcDotNet"",
        ""DirectionRatios"":[
          {
            ""$type"":""IfcDotNet.Schema.doublewrapper, IfcDotNet"",
            ""id"":null,
            ""path"":null,
            ""pos"":null,
            ""Value"":0.0
          },
          {
            ""$type"":""IfcDotNet.Schema.doublewrapper, IfcDotNet"",
            ""id"":null,
            ""path"":null,
            ""pos"":null,
            ""Value"":1.0
          },
          {
            ""$type"":""IfcDotNet.Schema.doublewrapper, IfcDotNet"",
            ""id"":null,
            ""path"":null,
            ""pos"":null,
            ""Value"":0.0
          }
        ],
        ""href"":null,
        ""ref"":null,
        ""proxy"":null,
        ""edo"":null,
        ""entityid"":""i102"",
        ""entitypath"":null,
        ""pos"":null
      }
    ],
    ""id"":""uos_1"",
    ""express"":null,
    ""configuration"":[
      ""i-ifc2x3""
    ],
    ""schemaLocation"":null,
    ""edo"":null,
    ""description"":null
  },
  ""version"":""2.0""
}";
			
            StringReader reader = new StringReader(json);
            
            iso_10303 iso10303 = SUT.Deserialize(reader);
            Utilities.AssertIsMinimumExample(iso10303);
		}
	}
}
