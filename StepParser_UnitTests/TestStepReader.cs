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

using StepParser;
using StepParser.IO;

namespace StepParser_UnitTests
{
    [TestFixture]
    public class TestStepReader
    {
        
        
        private static readonly ILog logger = LogManager.GetLogger(typeof(TestStepReader));
        IStepReader SUT;
        
        [SetUp]
        public void SetUp()
        {
            BasicConfigurator.Configure();
        }
        
        [Test]
        public void CanReadSmallWallExample()
        {
        	SUT = this.createStepReader( ExampleData.StepIFC2X3SmallWallExample() );
            int count = 0;
            while(SUT.Read()){
                count++;
            }
            Assert.AreEqual(StepToken.EndSTEP, SUT.TokenType);
            Assert.AreEqual(1927, count );
        }
        
        [Test]
        public void CanReadSimple()
        {
        	createSUT( ExampleData.simpleStepWithCommentString() );
            
            //read Iso declaration
            Assert.IsTrue( SUT.Read() );
            Assert.AreEqual( StepToken.StartSTEP, SUT.TokenType );
            Assert.AreEqual("ISO-10303-21", SUT.Value);
            
            Assert.IsTrue( SUT.Read() );
            Assert.AreEqual( StepToken.EndLine, SUT.TokenType );
            
            //read header section definition
            Assert.IsTrue( SUT.Read() );
            Assert.AreEqual( StepToken.StartSection, SUT.TokenType );
            Assert.AreEqual( "HEADER", SUT.Value );
            
            Assert.IsTrue( SUT.Read() );
            Assert.AreEqual( StepToken.EndLine, SUT.TokenType );
            
            //read file description name
            Assert.IsTrue( SUT.Read() );
            Assert.AreEqual( StepToken.EntityName, SUT.TokenType );
            Assert.AreEqual( "FILE_DESCRIPTION", SUT.Value );
            
            //read start of file_description object
            Assert.IsTrue( SUT.Read() );
            Assert.AreEqual( StepToken.StartEntity, SUT.TokenType );
            Assert.IsNull( SUT.Value );
            
            //read start of array
            Assert.IsTrue( SUT.Read() );
            Assert.AreEqual( StepToken.StartArray, SUT.TokenType );
            Assert.IsNull( SUT.Value );
            
            //read only value in array
            Assert.IsTrue( SUT.Read() );
            Assert.AreEqual( StepToken.String, SUT.TokenType );
            Assert.AreEqual( "ViewDefinition [CoordinationView, QuantityTakeOffAddOnView]", SUT.Value );
            
            //read end of array
            Assert.IsTrue( SUT.Read() );
            Assert.AreEqual( StepToken.EndArray, SUT.TokenType );
            Assert.IsNull( SUT.Value );
            
            //read implementation level
            Assert.IsTrue( SUT.Read() );
            Assert.AreEqual( StepToken.String, SUT.TokenType );
            Assert.AreEqual( "2;1", SUT.Value );
            
            //read close of entity
            Assert.IsTrue( SUT.Read() );
            Assert.AreEqual( StepToken.EndEntity, SUT.TokenType );
            Assert.IsNull( SUT.Value );
            
            Assert.IsTrue( SUT.Read() );
            Assert.AreEqual( StepToken.EndLine, SUT.TokenType );
            
            //read file_name function name
            Assert.IsTrue( SUT.Read() );
            Assert.AreEqual( StepToken.EntityName, SUT.TokenType );
            Assert.AreEqual( "FILE_NAME", SUT.Value );
            
            Assert.IsTrue( SUT.Read() );
            Assert.AreEqual( StepToken.StartEntity, SUT.TokenType );
            
            Assert.IsTrue( SUT.Read() );
            Assert.AreEqual( StepToken.String, SUT.TokenType );
            
            Assert.IsTrue( SUT.Read() );
            Assert.AreEqual( StepToken.Date, SUT.TokenType );
            
            Assert.IsTrue( SUT.Read() );
            Assert.AreEqual( StepToken.StartArray, SUT.TokenType );
            
            Assert.IsTrue( SUT.Read() );
            Assert.AreEqual( StepToken.String, SUT.TokenType );
            
            Assert.IsTrue( SUT.Read() );
            Assert.AreEqual( StepToken.EndArray, SUT.TokenType );
            
            Assert.IsTrue( SUT.Read() );
            Assert.AreEqual( StepToken.EndEntity, SUT.TokenType );
            
            Assert.IsTrue( SUT.Read() );
            Assert.AreEqual( StepToken.EndLine, SUT.TokenType );
            
            Assert.IsTrue( SUT.Read() );
            Assert.AreEqual( StepToken.EntityName, SUT.TokenType );
            
            Assert.IsTrue( SUT.Read() );
            Assert.AreEqual( StepToken.StartEntity, SUT.TokenType );
            
            Assert.IsTrue( SUT.Read() );
            Assert.AreEqual( StepToken.StartArray, SUT.TokenType );
            
            Assert.IsTrue( SUT.Read() );
            Assert.AreEqual( StepToken.String, SUT.TokenType );
            
            Assert.IsTrue( SUT.Read() );
            Assert.AreEqual( StepToken.EndArray, SUT.TokenType );
            
            Assert.IsTrue( SUT.Read() );
            Assert.AreEqual( StepToken.EndEntity, SUT.TokenType );
            
            Assert.IsTrue( SUT.Read() );
            Assert.AreEqual( StepToken.EndLine, SUT.TokenType );
            
            Assert.IsTrue( SUT.Read() );
            Assert.AreEqual( StepToken.EndSection, SUT.TokenType );
            Assert.AreEqual( "ENDSEC", SUT.Value );
            
            Assert.IsTrue( SUT.Read() );
            Assert.AreEqual( StepToken.EndLine, SUT.TokenType );
            
            Assert.IsTrue( SUT.Read() );
            Assert.AreEqual( StepToken.StartSection, SUT.TokenType );
            Assert.AreEqual( "DATA", SUT.Value );
            
            Assert.IsTrue( SUT.Read() );
            Assert.AreEqual( StepToken.EndLine, SUT.TokenType );
                      
            Assert.IsTrue( SUT.Read() );
            Assert.AreEqual( StepToken.LineIdentifier, SUT.TokenType );
            Assert.AreEqual( "#1", SUT.Value );
            
            Assert.IsTrue( SUT.Read() );
            Assert.AreEqual( StepToken.Operator, SUT.TokenType );
            
            Assert.IsTrue( SUT.Read() );
            Assert.AreEqual( StepToken.EntityName, SUT.TokenType );
            Assert.AreEqual( "IFCPROJECT", SUT.Value );
            
            Assert.IsTrue( SUT.Read() );
            Assert.AreEqual( StepToken.StartEntity, SUT.TokenType );
            
            Assert.IsTrue( SUT.Read() );
            Assert.AreEqual( StepToken.String, SUT.TokenType );
            
            Assert.IsTrue( SUT.Read() );
            Assert.AreEqual( StepToken.LineReference, SUT.TokenType );
            Assert.AreEqual( "#2", SUT.Value );
            
            Assert.IsTrue( SUT.Read() );
            Assert.AreEqual( StepToken.String, SUT.TokenType );
            
            Assert.IsTrue( SUT.Read() );
            Assert.AreEqual( StepToken.String, SUT.TokenType );
            
            Assert.IsTrue( SUT.Read() );
            Assert.AreEqual( StepToken.Null, SUT.TokenType );
            
            Assert.IsTrue( SUT.Read() );
            Assert.AreEqual( StepToken.Float, SUT.TokenType );
            Assert.AreEqual( -22.4, SUT.Value );
            
            Assert.IsTrue( SUT.Read() );
            Assert.AreEqual( StepToken.Null, SUT.TokenType );
            
            
            Assert.IsTrue( SUT.Read() );
            Assert.AreEqual( StepToken.StartArray, SUT.TokenType );
            
            Assert.IsTrue( SUT.Read() );
            Assert.AreEqual( StepToken.LineReference, SUT.TokenType );
            
            Assert.IsTrue( SUT.Read() );
            Assert.AreEqual( StepToken.EndArray, SUT.TokenType );
            
            Assert.IsTrue( SUT.Read() );
            Assert.AreEqual( StepToken.LineReference, SUT.TokenType );
            
            Assert.IsTrue( SUT.Read() );
            Assert.AreEqual( StepToken.EndEntity, SUT.TokenType );
            
            Assert.IsTrue( SUT.Read() );
            Assert.AreEqual( StepToken.EndLine, SUT.TokenType );
            
            Assert.IsTrue( SUT.Read() );
            Assert.AreEqual( StepToken.Comment, SUT.TokenType );
            Assert.AreEqual( " a comment ", SUT.Value );
            
            Assert.IsTrue( SUT.Read() );
            Assert.AreEqual( StepToken.LineIdentifier, SUT.TokenType );
            Assert.AreEqual("#2", SUT.Value );
            
            Assert.IsTrue( SUT.Read() );
            Assert.AreEqual( StepToken.Operator, SUT.TokenType );
            
            Assert.IsTrue( SUT.Read() );
            Assert.AreEqual( StepToken.EntityName, SUT.TokenType );
            
            Assert.IsTrue( SUT.Read() );
            Assert.AreEqual( StepToken.StartEntity, SUT.TokenType );
            
            Assert.IsTrue( SUT.Read() );
            Assert.AreEqual( StepToken.LineReference, SUT.TokenType );
            Assert.AreEqual( "#3", SUT.Value );
            
            Assert.IsTrue( SUT.Read() );
            Assert.AreEqual( StepToken.EntityName, SUT.TokenType );
            Assert.AreEqual( "IFCTEXT", SUT.Value );
            
            Assert.IsTrue( SUT.Read() );
            Assert.AreEqual( StepToken.StartEntity, SUT.TokenType );
            
            Assert.IsTrue( SUT.Read() );
            Assert.AreEqual( StepToken.String, SUT.TokenType );
            Assert.AreEqual( "foobar", SUT.Value );
            
            Assert.IsTrue( SUT.Read() );
            Assert.AreEqual( StepToken.EndEntity, SUT.TokenType );
            
            Assert.IsTrue( SUT.Read() );
            Assert.AreEqual( StepToken.Null, SUT.TokenType );
            
            Assert.IsTrue( SUT.Read() );
            Assert.AreEqual( StepToken.Enumeration, SUT.TokenType );
            Assert.AreEqual( "ADDED", SUT.Value );
            
            Assert.IsTrue( SUT.Read() );
            Assert.AreEqual( StepToken.Null, SUT.TokenType );
            
            Assert.IsTrue( SUT.Read() );
            Assert.AreEqual( StepToken.Boolean, SUT.TokenType );
            Assert.AreEqual( false, SUT.Value );
            
            Assert.IsTrue( SUT.Read() );
            Assert.AreEqual( StepToken.Overridden, SUT.TokenType );
            
            Assert.IsTrue( SUT.Read() );
            Assert.AreEqual( StepToken.Integer, SUT.TokenType );
            Assert.AreEqual( 1217620436, SUT.Value );
            Assert.AreEqual( typeof(int), SUT.ValueType );
            
            Assert.IsTrue( SUT.Read() );
            Assert.AreEqual( StepToken.EndEntity, SUT.TokenType );
            
            Assert.IsTrue( SUT.Read() );
            Assert.AreEqual( StepToken.EndLine, SUT.TokenType );
            
            Assert.IsTrue( SUT.Read() );
            Assert.AreEqual( StepToken.EndSection, SUT.TokenType );
            
            Assert.IsTrue( SUT.Read() );
            Assert.AreEqual( StepToken.EndLine, SUT.TokenType );
            
            Assert.IsTrue( SUT.Read() );
            Assert.AreEqual( StepToken.EndSTEP, SUT.TokenType );
            
            Assert.IsFalse( SUT.Read() );
        }
        
        private void createSUT( string sample ){
        	StringReader reader = new StringReader( ExampleData.simpleStepWithCommentString() );
            SUT = new StepReader( reader );
        }
        
        private IStepReader createStepReader(String stringToRead){
        	return new StepReader( new StringReader( stringToRead ) );
        }
    }
}
