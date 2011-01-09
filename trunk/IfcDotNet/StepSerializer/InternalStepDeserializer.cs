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


The majority of the below code originate from the Json.NET project, for which the following additional license applies:

Copyright (c) 2007 James Newton-King

Permission is hereby granted, free of charge, to any person
obtaining a copy of this software and associated documentation
files (the "Software"), to deal in the Software without
restriction, including without limitation the rights to use,
copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the
Software is furnished to do so, subject to the following
conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
OTHER DEALINGS IN THE SOFTWARE.
 */
#endregion

using System;
using System.Globalization;
using System.Collections.Generic;

using log4net;

namespace IfcDotNet.StepSerializer
{
    /// <summary>
    /// InternalStepSerializer converts raw StepReader output into
    /// semi-structured 'STEP objects'.
    /// </summary>
    internal class InternalStepDeserializer
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(InternalStepDeserializer));
        
        public InternalStepDeserializer()
        {
        }
        
        public StepFile Deserialize(StepReader reader)
        {
            if( reader == null )
                throw new ArgumentNullException( "reader" );
            
            StepFile step = new StepFile();
            
            while(reader.Read()){
                switch(reader.TokenType){
                    case StepToken.EntityName:
                        step.Header.Add( deserializeEntity( reader ) );
                        continue;
                    case StepToken.LineIdentifier:
                        int objectNumber = getObjectNumber( reader );
                        step.Data.Add( objectNumber, deserializeEntity( reader ) );
                        continue;
                    default:
                        continue;
                }
            }
            
            //TODO should try to create the tree here.  Need to handle circular references though.
            
            return step;
        }
        
        /// <summary>
        /// Should only be called when reader is at a LineIdentifier token.
        /// Attempts to read the value of the line identifier, e.g. "#24"
        /// and cast it to an integer, e.g. 24.
        /// </summary>
        /// <returns></returns>
        private int getObjectNumber(StepReader reader){
            if(reader == null)
                throw new ArgumentNullException("reader");
            if(reader.TokenType != StepToken.LineIdentifier)
                throw new StepSerializerException( "getObjectNumber() was called when the ExpressReader was not at a LineIdentifier token" );
            if(reader.ValueType != typeof(string))
                throw new StepSerializerException( String.Format(CultureInfo.InvariantCulture,
                                                                 "getObjectNumber() expects the line identifier to be a string, it is instead a type of {0}",
                                                                 reader.ValueType));
            
            return CastLineIdToInt( reader.Value.ToString() );
        }
        
        /// <summary>
        /// Attempts to read a nested entity
        /// </summary>
        /// <param name="objectNumber"></param>
        /// <returns></returns>
        private StepDataObject deserializeEntity(StepReader reader){
            if(reader == null)
                throw new ArgumentNullException( "reader" );
            
            StepDataObject edo = new StepDataObject();
            bool entityStarted = false;
            
            //nested entities are already on the EntityName token
            if(reader.TokenType == StepToken.EntityName)
                edo.ObjectName = getObjectName(reader);
            
            while(reader.Read()){
                logger.Debug(String.Format(CultureInfo.InvariantCulture,
                                           "deserializer read token {0}. value {1}",
                                           reader.TokenType,
                                           reader.Value));
                switch(reader.TokenType){
                    case StepToken.EntityName:
                        if(!entityStarted)
                            edo.ObjectName = getObjectName( reader );
                        else //it's a nested entity
                            edo.Properties.Add( deserializeNestedEntity( reader ) );
                        continue;
                    case StepToken.LineReference:
                        edo.Properties.Add(deserializeLineReference( reader ));
                        continue;
                    case StepToken.Enumeration:
                    case StepToken.Boolean:
                    case StepToken.Integer:
                    case StepToken.Float:
                    case StepToken.String:
                    case StepToken.Date:
                        edo.Properties.Add(deserializeProperty( reader ));
                        continue;
                    case StepToken.StartArray:
                        edo.Properties.Add(deserializeArray( reader ));
                        continue;
                    case StepToken.StartEntity:
                        if(!entityStarted)
                            entityStarted = true;
                        else
                            throw new StepSerializerException(String.Format(CultureInfo.InvariantCulture,
                                                                            "A token was found which was not expected: {0}",
                                                                            reader.TokenType));
                        continue;
                    case StepToken.Operator:
                        continue;
                    case StepToken.Overridden:
                        edo.Properties.Add(deserializeOverridden());
                        continue;
                    case StepToken.Null:
                        edo.Properties.Add(deserializeNull());
                        continue;
                    case StepToken.EndEntity:
                        return edo;
                    case StepToken.EndLine:
                    case StepToken.EndSection:
                    case StepToken.EndSTEP:
                    case StepToken.StartSTEP:
                    case StepToken.StartSection:
                        throw new StepSerializerException(String.Format(CultureInfo.InvariantCulture,
                                                                        "A token was found which was not expected: {0}",
                                                                        reader.TokenType));
                    default:
                        throw new NotImplementedException(String.Format(CultureInfo.InvariantCulture,
                                                                        "The {0} ExpressToken type is not yet implemented by deserializeEntity()",
                                                                        reader.TokenType));
                }
                //TODO should do some verification here (properties are after entityStart and before EntityEnd etc.)
            }
            throw new StepSerializerException( "The reader reached the end without finding an endEntity token" );
        }
        
        /// <summary>
        /// If the reader is at an EntityName context, then this will return the entity name (with error checking)
        /// </summary>
        /// <returns></returns>
        private string getObjectName(StepReader reader){
            if(reader == null)
                throw new ArgumentNullException("reader");
            if(reader.TokenType != StepToken.EntityName)
                throw new StepSerializerException("getObjectName() has been called, but the reader context is not that of an EntityName token");
            if(reader.ValueType != typeof(string))
                throw new StepSerializerException("getObjectName() cannot create an object name from a non-string value type");
            string s = reader.Value.ToString();
            if(string.IsNullOrEmpty(s))
                throw new StepSerializerException("getObjectName() only found a null Object name");
            return s;
        }
        
        private StepValue deserializeNestedEntity(StepReader reader){
            if(reader == null)
                throw new ArgumentNullException("reader");
            
            return new StepValue(StepToken.StartEntity,
                                 deserializeEntity(reader));
        }
        
        private StepValue deserializeProperty(StepReader reader){
            if(reader == null)
                throw new ArgumentNullException("reader");
            
            return new StepValue(reader.TokenType, reader.Value);
        }
        
        private StepValue deserializeLineReference(StepReader reader){
            if(reader == null)
                throw new ArgumentNullException("reader");
            
            return new StepValue(reader.TokenType,
                                 CastLineIdToInt((string)reader.Value));
        }
        
        private StepValue deserializeNull(){
            return new StepValue(StepToken.Null, null);
        }
        
        private StepValue deserializeOverridden(){
            return new StepValue( StepToken.Overridden, null);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private StepValue deserializeArray(StepReader reader){
            if(reader == null)
                throw new ArgumentNullException("reader");
            
            IList<StepValue> values = new List<StepValue>();
            while(reader.Read()){
                logger.Debug(String.Format(CultureInfo.InvariantCulture,
                                           "deserializeArray read : {0} of value {1}",
                                           reader.TokenType,
                                           reader.Value));
                switch(reader.TokenType){
                    case StepToken.EndArray:
                        return new StepValue(StepToken.StartArray, values);
                    case StepToken.LineReference:
                        values.Add(deserializeLineReference(reader));
                        continue;
                    case StepToken.Enumeration:
                    case StepToken.Boolean:
                    case StepToken.Integer:
                    case StepToken.Float:
                    case StepToken.String:
                        values.Add(deserializeProperty(reader));
                        continue;
                    case StepToken.Null:
                        values.Add(deserializeNull());
                        continue;
                    case StepToken.EntityName:
                    case StepToken.StartEntity:
                        values.Add(deserializeNestedEntity(reader));
                        continue;
                    case StepToken.Operator:
                    case StepToken.Overridden:
                    case StepToken.EndEntity:
                    case StepToken.EndLine:
                    case StepToken.EndSection:
                    case StepToken.EndSTEP:
                    case StepToken.StartSTEP:
                    case StepToken.StartSection:
                        throw new StepSerializerException( String.Format(CultureInfo.InvariantCulture,
                                                                         "deserializeArray found a token which was not expected: {0}",
                                                                         reader.TokenType));
                    default:
                        throw new NotImplementedException(String.Format(CultureInfo.InvariantCulture,
                                                                        "This ExpressToken type is not yet implemented by deserializeArray(), {0}",
                                                                        reader.TokenType));
                }
            }
            throw new StepSerializerException( "deserializeArray() reached the end of the reader without finding an endArray token" );
        }
        
        /// <summary>
        /// Converts a string in line identifier format, e.g #21, to the corresponding integer, 21.
        /// </summary>
        /// <param name="lineIdent"></param>
        /// <returns></returns>
        private int CastLineIdToInt(string lineIdent){
            if(String.IsNullOrEmpty(lineIdent))
                throw new StepSerializerException( "The lineIdentifier has no value" );

            if(!lineIdent.StartsWith("#"))
                throw new FormatException( String.Format(CultureInfo.InvariantCulture,
                                                         "The lineIdentifier does not start with a # character.  The line identifier is instead {0}",
                                                         lineIdent));
            
            lineIdent = lineIdent.TrimStart('#');
            try{
                return int.Parse(lineIdent);
            }catch(FormatException fe){
                logger.Error(fe.Message);
                throw;
            }catch(OverflowException oe){
                logger.Error(oe.Message);
                throw;
            }
        }
    }
}
