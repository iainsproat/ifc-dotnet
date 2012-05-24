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

using StepParser;

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
        public void CanSerializeNoData(){
        	AssertCanSerialize( Utilities.StepNoDataString() );
        }
        
        
        [Test]
        public void CanSerializeSimpleLine(){
        	AssertCanSerialize( Utilities.StepSimpleLineString() );
        }
        
       
        [Test]
        public void CanSerializeWithReference(){
            AssertCanSerialize( Utilities.StepWithReferenceString() );
        }
        
        
        
        [Test]
        public void CanSerializeArrayWithReferences(){
        	AssertCanSerialize( Utilities.StepArrayWithReferencesString());
        }
        
        
        
        [Test]
        public void CanSerializeNestedStructure(){
        	AssertCanSerialize( Utilities.StepNestedObjectsString() );
        }
        
        
        
        [Test]
        public void CanSerializeArray(){
        	AssertCanSerialize( Utilities.StepArrayString() );
        }
        
        
        
        [Test]
        public void CanSerializeArrayWrapper(){
        	AssertCanSerialize( Utilities.StepArrayWrapperString() );
        }
        
        
        
        [Test]
        public void CanSerializeSelect(){
        	AssertCanSerialize( Utilities.StepSelectString() );
        }
        
        
        
        [Test]
        [Explicit]
        public void CanSerializeSmallWallExample(){
        	AssertCanSerialize(Utilities.StepSmallWallExampleString() );
        }
        
        
        
        private void AssertCanSerialize(String itemToEqual){
        	IStepReader itemToDeserialize = new StepReader( new StringReader( itemToEqual ) );
        	iso_10303 iso10303 = serializer.Deserialize( itemToDeserialize );
            
            StringBuilder sb = new StringBuilder();
            StepWriter stepwriter = new StepWriter( new StringWriter( sb ) );
            
            serializer.Serialize( stepwriter, iso10303 );
            
            logger.Debug(sb.ToString());
            
            Assert.AreEqual( itemToEqual, sb.ToString() );
        }
        
        
    }
}
