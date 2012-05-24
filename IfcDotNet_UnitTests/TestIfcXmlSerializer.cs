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
using System.Text;

using IfcDotNet.XmlSerializer;
using IfcDotNet.Schema;

using IfcDotNet_UnitTests.sampleData;

using log4net;
using log4net.Config;

using NUnit.Framework;

namespace IfcDotNet_UnitTests
{
    /// <summary>
    /// Test serialization and deserialization of an IfcXml string
    /// </summary>
    [TestFixture]
    public class TestIfcXmlSerializer
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(TestIfcXmlSerializer));
        
        IfcXmlSerializer serializer;
        
        [SetUp]
        public void SetUp()
        {
            BasicConfigurator.Configure();
            
            serializer = new IfcXmlSerializer();
        }
        
        [Test]
        public void CanDeserialize() //FIXME this test currently fails, but the string seems to validate OK
        {
            iso_10303 iso10303 = serializer.Deserialize( ExampleIfcXmlData.getMinimumExampleXml() );
            
            Utilities.AssertIsMinimumExample( iso10303 );
        }
        
        [Test]
        public void CanDeserializeAlternativeFormat()
        {
            iso_10303 iso10303 = serializer.Deserialize( ExampleIfcXmlData.getAlternativeMinimumExampleXml() );
            
            Utilities.AssertIsMinimumExample( iso10303 );
        }
        
        
        
        [Test]
        public void CanSerialize()
        {
            iso_10303 iso10303   = Utilities.buildMinimumExampleObject();
            
            
            string returnedValue = serializer.SerializeToString(iso10303);
            
            Assert.IsFalse(string.IsNullOrEmpty( returnedValue ) );
            
            //dumping to the console
            logger.Debug( returnedValue );
            
            IfcXmlValidator validator = new IfcXmlValidator( returnedValue );
            Assert.IsTrue( validator.IsValid );
            Assert.AreEqual(ExampleIfcXmlData.getExpectedXmlOutputString(), returnedValue );
        }
        
        [Test]
        public void CanRoundTrip()
        {
            iso_10303 initial = Utilities.buildMinimumExampleObject();
            string serialized = serializer.SerializeToString( initial );
            
            //logger.Debug( serialized );
            IfcXmlValidator validator = new IfcXmlValidator( serialized );
            Assert.IsTrue( validator.IsValid );
            
            StringReader reader = new StringReader( serialized );
            iso_10303 returned = serializer.Deserialize( reader );
            
            Utilities.AssertIsMinimumExample( returned );
        }
        
    }
}
