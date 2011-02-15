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
            iso_10303 iso10303 = serializer.Deserialize( Utilities.getMinimumExampleXml() );
            
            AssertIsMinimumExample( iso10303 );
        }
        
        [Test]
        public void CanDeserializeAlternativeFormat()
        {
            iso_10303 iso10303 = serializer.Deserialize( Utilities.getAlternativeMinimumExampleXml() );
            
            AssertIsMinimumExample( iso10303 );
        }
        
        private void AssertIsMinimumExample(iso_10303 iso10303){
            Assert.IsNotNull(iso10303);
            Assert.IsNotNull(iso10303.iso_10303_28_header);
            Assert.AreEqual("An Example",                       iso10303.iso_10303_28_header.name);
            Assert.AreEqual(new DateTime(2010,11,12,13,04,00),  iso10303.iso_10303_28_header.time_stamp);
            Assert.AreEqual("John Hancock",                     iso10303.iso_10303_28_header.author);
            Assert.AreEqual("MegaCorp",                         iso10303.iso_10303_28_header.organization);
            Assert.AreEqual("IfcDotNet Library",                iso10303.iso_10303_28_header.originating_system);
            Assert.AreEqual("a preprocessor",                   iso10303.iso_10303_28_header.preprocessor_version);
            Assert.AreEqual("documentation",                    iso10303.iso_10303_28_header.documentation);
            Assert.AreEqual("none",                             iso10303.iso_10303_28_header.authorization);
            
            Assert.IsNotNull(iso10303.uos, "iso10303.uos is null");
            uos uos = iso10303.uos;
            Assert.AreEqual("uos_1",    uos.id);
            Assert.IsNotNull(uos.configuration, "iso10303.uos.configuration is null");
            Assert.AreEqual(1, uos.configuration.Length, "uos.configuration does not have 1 item in it");
            Assert.AreEqual("i-ifc2x3", uos.configuration[0]);
            
            Assert.IsNotNull(uos as uos1, "uos cannot be converted to uos1");
            uos1 uos1 = uos as uos1;
            
            Assert.IsNotNull(uos1, "uos1 is null");
            Assert.IsNotNull(uos1.Items, "uos1.items is null");
            Assert.AreEqual(3, uos1.Items.Length, "uos1.Items does not have 3 items in it");
            
            IfcOrganization org = uos1.Items[0] as IfcOrganization;
            Assert.IsNotNull( org , "org is null");
            Assert.AreEqual( "i1101", org.entityid , "entityid is not i1101");
            Assert.AreEqual("MegaCorp", org.Name );
            
            IfcCartesianPoint pnt = uos1.Items[1] as IfcCartesianPoint;
            Assert.IsNotNull( pnt, "pnt is null");
            Assert.AreEqual( "i101", pnt.entityid );
            Assert.IsNotNull( pnt.Coordinates );
            Assert.IsNotNull( pnt.Coordinates.IfcLengthMeasure );
            Assert.AreEqual( 3, pnt.Coordinates.IfcLengthMeasure.Length );
            Assert.AreEqual( 2500, pnt.Coordinates.IfcLengthMeasure[0].Value );//TODO shorten the number of properties needed to be called to get the value. pnt.Coordinates[0] would be perfect!
            Assert.AreEqual( 0, pnt.Coordinates.IfcLengthMeasure[1].Value );
            Assert.AreEqual( 0, pnt.Coordinates.IfcLengthMeasure[2].Value );
            
            IfcDirection dir = uos1.Items[2] as IfcDirection;
            Assert.IsNotNull( dir , "dir is null");
            Assert.AreEqual( "i102", dir.entityid );
            Assert.IsNotNull( dir.DirectionRatios );
            Assert.IsNotNull( dir.DirectionRatios.doublewrapper );
            Assert.AreEqual( 3, dir.DirectionRatios.doublewrapper.Length ); 
            Assert.AreEqual( 0, dir.DirectionRatios.doublewrapper[0].Value );
            Assert.AreEqual( 1, dir.DirectionRatios.doublewrapper[1].Value );
            Assert.AreEqual( 0, dir.DirectionRatios.doublewrapper[0].Value );
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
            Assert.AreEqual(Utilities.getExpectedXmlOutputString(), returnedValue );
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
            
            AssertIsMinimumExample( returned );
        }
        
    }
}
