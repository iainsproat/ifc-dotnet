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
using System.Reflection;

using IfcDotNet.Schema;

using log4net;

namespace IfcDotNet.StepSerializer
{
    /// <summary>
    /// Reads IFC data in STEP (10303) format.
    /// </summary>
    public class IfcStepSerializer
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(IfcStepSerializer));
        
        /// <summary>
        /// 
        /// </summary>
        private StepReader _reader;
        private IList<ExpressDataObject> dataObjects = new List<ExpressDataObject>();
        
        
        
        public IfcStepSerializer(){}
        
        
        public iso_10303_28 Deserialize(StepReader reader)
        {
            if( reader == null )
                throw new ArgumentNullException( "reader" );
            this._reader = reader;
            
            while(this._reader.Read()){
                if(_reader.TokenType == StepToken.LineIdentifier){
                    int objectNumber = -1;
                    try{
                        objectNumber = getObjectNumber();//FIXME this can throw exceptions, need to try/catch this.
                        
                    }catch(Exception e){
                        //fail silently
                        logger.Debug(String.Format(CultureInfo.InvariantCulture,
                                                   "Failed while trying to deserialize an entity. {0}",
                                                   e.Message));
                    }
                    if(objectNumber > 0){//HACK
                        logger.Debug(String.Format(CultureInfo.InvariantCulture,
                                                   "objectNumber : {0}", objectNumber));
                        this.dataObjects.Add(deserializeEntity());//FIXME where should the objectNumber be stored??
                        //HACK should be within the try/catch above
                    }
                }
            }
            
            
            Assembly asm = Assembly.GetExecutingAssembly();
            Type[] types = asm.GetTypes();
            
            IDictionary<string, string> typesUpperCase = new Dictionary<string, string>();
            foreach(Type t in types){
                typesUpperCase.Add(t.Name.ToUpperInvariant(), t.Name);
            }
            
            foreach(ExpressDataObject edo in this.dataObjects){
                if(edo == null)
                    continue;
                //TODO need to instantiate the corresponding IFC classes
                string name = edo.ObjectName;
                logger.Debug("Entity name : " + name);
                //find the corresponding class in the IFC2X3 schema
                
                if(typesUpperCase.ContainsKey(name)){
                    name = typesUpperCase[name];
                    logger.Debug("Changed name to : " + name);    
                }
                
                Type t = asm.GetType(name);//FIXME currently failing here!!!
                if(t != null)
                    logger.Debug("Assembly found a type for the entity : " + t.FullName);
                
                /*
                //TODO need to  fill in the data
                //TODO keeping track of references so they can be linked up
                int propCount = 0;
                foreach(ExpressPropertyValue epv in edo.Properties){
                    
                    //debugging
                    logger.Debug("property : " + propCount);
                    logger.Debug("property token : " + epv.Token);
                    logger.Debug("property value : " + epv.Value);
                    logger.Debug("property valueType : " + epv.ValueType);
                    
                    propCount++;
                }
                 */
            }
            
            throw new NotImplementedException("Deserialize(ExpressReader) is not yet fully implemented");
        }
        
        /// <summary>
        /// Should only be called when reader is at a LineIdentifier token.
        /// Attempts to read the value of the line identifier, e.g. "#24"
        /// and cast it to an integer, e.g. 24.
        /// </summary>
        /// <returns></returns>
        private int getObjectNumber(){
            //FIXME reduce the verbosity of the error catching.
            if(_reader == null){
                string msg = "getObjectNumber() was called, but the internal reader is null";
                logger.Error(msg);
                throw new NullReferenceException(msg);
            }
            if(_reader.TokenType != StepToken.LineIdentifier){
                string msg = "getObjectNumber() was called when the ExpressReader was not at a LineIdentifier token";
                logger.Error(msg);
                throw new InvalidOperationException(msg);
            }
            if(_reader.ValueType != typeof(string)){
                string msg = "The line identifier isn't a string";
                logger.Error(msg);
                throw new FormatException(msg);
            }
            string lineIdent = _reader.Value.ToString();
            if(String.IsNullOrEmpty(lineIdent)){
                string msg = "The lineIdentifier has no value";
                logger.Error(msg);
                throw new NullReferenceException(msg);
            }
            if(!lineIdent.StartsWith("#")){
                string msg = String.Format(CultureInfo.InvariantCulture,
                                           "The lineIdentifier does not start with a # character.  The line identifier is instead {0}",
                                           lineIdent);
                logger.Error(msg);
                throw new FormatException(msg);
            }
            
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
        
        private ExpressDataObject deserializeEntity(){
            ExpressDataObject edo = new ExpressDataObject();
            while(_reader.Read()){
                switch(_reader.TokenType){
                    case StepToken.EntityName:
                        edo.ObjectName = _reader.Value.ToString();//FIXME should probably check the _reader.ValueType to make sure it is a string
                        continue;
                    case StepToken.LineReference:
                    case StepToken.Enumeration:
                    case StepToken.Boolean:
                    case StepToken.Integer:
                    case StepToken.Float:
                    case StepToken.String:
                        edo.Properties.Add(deserializeProperty());
                        continue;
                    case StepToken.StartArray:
                        edo.Properties.Add(deserializeArray());
                        continue;
                    case StepToken.StartEntity:
                    case StepToken.Operator:
                    case StepToken.Overridden:
                        continue;
                    case StepToken.Null:
                        edo.Properties.Add(deserializeNull());//HACK is this the best way to handle null properties?
                        continue;
                    case StepToken.EndEntity:
                        return edo;
                    case StepToken.EndLine:
                    case StepToken.EndSection:
                    case StepToken.EndExpress:
                    case StepToken.StartSTEP:
                    case StepToken.StartSection:
                        string msg = String.Format(CultureInfo.InvariantCulture,
                                                   "A token was found which was not expected: {0}",
                                                   _reader.TokenType);
                        logger.Error(msg);
                        throw new Exception(msg);//HACK need a more specific exception type
                    default:
                        throw new NotImplementedException(String.Format(CultureInfo.InvariantCulture,
                                                                        "The {0} ExpressToken type is not yet implemented by deserializeEntity()",
                                                                        _reader.TokenType));
                }
                //TODO should do some verification here (properties are after entityStart and before EntityEnd etc.)
            }
            string errorMsg = "The reader reached the end without finding an endEntity token";
            logger.Error(errorMsg);
            throw new Exception(errorMsg);//HACK need to throw a more specific exception type
        }
        
        private ExpressPropertyValue deserializeProperty(){
            if(_reader == null){
                string msg = "deserializeProperty() has been called, but the internal reader is null";
                logger.Error(msg);
                throw new NullReferenceException(msg);
            }
            ExpressPropertyValue epv = new ExpressPropertyValue();
            epv.Token = _reader.TokenType;//FIXME is this passed by reference or value (do I need to clone/deep copy?)
            epv.Value = _reader.Value;  //FIXME is this passed by reference or value (do I need to clone/deep copy?)
            epv.ValueType = _reader.ValueType; //FIXME is this passed by reference or value (do I need to clone/deep copy?)
            return epv;
        }
        
        private ExpressPropertyValue deserializeNull(){
            if(_reader == null){
                string msg = "deserializeNull() has been called, but the internal reader is null";
                logger.Error(msg);
                throw new NullReferenceException(msg);
            }
            ExpressPropertyValue epv = new ExpressPropertyValue();
            epv.Token = StepToken.Null;
            epv.Value = null;
            epv.ValueType = null; //FIXME is this going to cause issues elsewhere?  Need to remember when using this that it can be null
            return epv;
        }
        
        private ExpressPropertyValue deserializeArray(){
            if(_reader == null){
                string msg = "deserializeArray() has been called, but the internal reader is null";
                logger.Error(msg);
                throw new NullReferenceException(msg);
            }
            ExpressPropertyValue epv = new ExpressPropertyValue();
            epv.Token = StepToken.StartArray;
            IList<ExpressPropertyValue> values = new List<ExpressPropertyValue>();
            while(_reader.Read()){
                switch(_reader.TokenType){
                    case StepToken.EndArray:
                        epv.Value = values;
                        epv.ValueType = typeof(IList<ExpressPropertyValue>);
                        return epv;
                    case StepToken.LineReference:
                    case StepToken.Enumeration:
                    case StepToken.Boolean:
                    case StepToken.Integer:
                    case StepToken.Float:
                    case StepToken.String:
                        values.Add(deserializeProperty());
                        continue;
                    case StepToken.Null:
                        values.Add(deserializeNull());//HACK is this the best way to handle null properties?
                        continue;
                    case StepToken.StartEntity:
                    case StepToken.Operator:
                    case StepToken.Overridden:
                    case StepToken.EndEntity:
                    case StepToken.EndLine:
                    case StepToken.EndSection:
                    case StepToken.EndExpress:
                    case StepToken.StartSTEP:
                    case StepToken.StartSection:
                        string msg = String.Format(CultureInfo.InvariantCulture,
                                                   "deserializeArray found a token which was not expected: {0}",
                                                   _reader.TokenType);
                        logger.Error(msg);
                        throw new Exception(msg);//HACK need a more specific exception type
                    default:
                        throw new NotImplementedException(String.Format(CultureInfo.InvariantCulture,
                                                                        "This ExpressToken type is not yet implemented by deserializeArray(), {0}",
                                                                        _reader.TokenType));
                }
            }
            string errorMsg = "deserializeArray() reached the end of the reader without finding an endArray token";
            logger.Error(errorMsg);
            throw new Exception(errorMsg);//HACK need more specific exception type
        }

        private class ExpressDataObject{
            private string _name;
            private IList<ExpressPropertyValue> _properties = new List<ExpressPropertyValue>();
            public string ObjectName{
                get{return this._name;}
                set{ this._name = value;}
            }
            public IList<ExpressPropertyValue> Properties{
                get{ return this._properties; }
            }
        }
        
        private struct ExpressPropertyValue{
            private StepToken _token;
            private Object _value;
            private Type _valueType;
            
            public StepToken Token{
                get{ return this._token; }
                set{ this._token = value; }
            }
            public Object Value{  //FIXME what about arrays of arrays!!
                get{ return this._value; }
                set{ this._value = value; }
            }
            public Type ValueType{
                get{ return this._valueType; }
                set{ this._valueType = value; }
            }
        }
    }
}
