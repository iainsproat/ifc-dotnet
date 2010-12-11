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
using IfcDotNet.StepSerializer;
using IfcDotNet.StepSerializer.Utilities;
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
        private IList<StepDataObject> dataObjects = new List<StepDataObject>();
        private IList<ObjectReferenceLink> objectLinks = new List<ObjectReferenceLink>();
        
        //caches of the model types and properties
        IDictionary<string, Type> entitiesMappedToUpperCaseName = new Dictionary<string, Type>();
        IDictionary<string, IList<PropertyInfo>> entityProperties = new Dictionary<string, IList<PropertyInfo>>();
        
        
        public IfcStepSerializer(){
            cacheEntityProperties();
        }
        
        
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
                        this.dataObjects.Add(deserializeEntity(objectNumber));//FIXME where should the objectNumber be stored??
                        //HACK should be within the try/catch above
                    }
                }
            }
            
            
            
            //try and instantiate a type for each STEP object
            //and fill its properties
            foreach(StepDataObject edo in this.dataObjects){
                if(edo == null)
                    continue;
                
                string name = edo.ObjectName;
                logger.Debug("name : " + name );
                
                if(String.IsNullOrEmpty(name))
                    throw new NullReferenceException("Failed trying to serialize an object with no Entity name");
                Type t = null;
                try{
                    t = entitiesMappedToUpperCaseName[name];
                }catch(KeyNotFoundException knfe){
                    try{
                        t = entitiesMappedToUpperCaseName[name + "1"]; //HACK some types end with the digit 1.
                    }catch(KeyNotFoundException){
                        logger.Debug(knfe.Message); //fail silently //FIXME is this the correct thing to do
                    }
                }
                if(t == null)
                    continue; //fail silently //FIXME is this the correct thing to do??
                //FIXME further checks required (is the type a subclass of Entity etc..)
                
                logger.Debug("Assembly found a type for the entity : " + t.FullName);
                
                IList<PropertyInfo> typeProperties = entityProperties[t.FullName]; //TODO error catching
                //TODO assert that the property count for this type equals the property count in our object. (as this will catch problems with overridden properties in the Express schema)
                
                Object instance = System.Activator.CreateInstance(t);
                //TODO save the instance somewhere
                
                
                setProperties(ref instance, edo, typeProperties);
                
            }
            
            //TODO link up references.
            
            throw new NotImplementedException("Deserialize(ExpressReader) is not yet fully implemented");
        }
        
        private void setProperties(ref Object obj, StepDataObject sdo, IList<PropertyInfo> typeProperties){
            //TODO need to  fill in the data
            //TODO keeping track of references so they can be linked up
            int propCount = 0;
            foreach(StepProperty sp in sdo.Properties){
                PropertyInfo pi = typeProperties[propCount];//TODO error catching
                
                //debugging
                logger.Debug("property : " + propCount);
                logger.Debug("typeProperty : " + pi.Name);
                logger.Debug("property token : " + sp.Token);
                logger.Debug("property value : " + sp.Value);
                logger.Debug("property valueType : " + sp.ValueType);
                logger.Debug("typeProperty type : " + pi.PropertyType);
                
                
                switch(sp.Token){
                    case StepToken.String:
                        pi.SetValue(obj, (String)sp.Value, null);
                        break;
                    case StepToken.LineReference:
                        int referencedId = CastLineIdToInt((String)sp.Value);
                        this.objectLinks.Add(new ObjectReferenceLink(sdo.StepId, pi, referencedId));
                        break;
                        //TODO the rest of the tokens
                    case StepToken.Null:
                        //do nothing, the property value will already be null.
                        break;
                    default:
                        throw new NotImplementedException(String.Format(CultureInfo.InvariantCulture,
                                                                        "Failed on attempting to set value of property. This token type is not yet implemented: {0}",
                                                                        sp.Token));
                }
                
                propCount++;
            }
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
            return CastLineIdToInt( _reader.Value.ToString() );
            
        }
        
        private int CastLineIdToInt(string lineIdent){
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
        
        private StepDataObject deserializeEntity(int objectNumber){
            StepDataObject edo = new StepDataObject();
            edo.StepId = objectNumber;
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
        
        private StepProperty deserializeProperty(){
            if(_reader == null){
                string msg = "deserializeProperty() has been called, but the internal reader is null";
                logger.Error(msg);
                throw new NullReferenceException(msg);
            }
            StepProperty epv = new StepProperty();
            epv.Token = _reader.TokenType;//FIXME is this passed by reference or value (do I need to clone/deep copy?)
            epv.Value = _reader.Value;  //FIXME is this passed by reference or value (do I need to clone/deep copy?)
            epv.ValueType = _reader.ValueType; //FIXME is this passed by reference or value (do I need to clone/deep copy?)
            return epv;
        }
        
        private StepProperty deserializeNull(){
            if(_reader == null){
                string msg = "deserializeNull() has been called, but the internal reader is null";
                logger.Error(msg);
                throw new NullReferenceException(msg);
            }
            StepProperty epv = new StepProperty();
            epv.Token = StepToken.Null;
            epv.Value = null;
            epv.ValueType = null; //FIXME is this going to cause issues elsewhere?  Need to remember when using this that it can be null
            return epv;
        }
        
        private StepProperty deserializeArray(){
            if(_reader == null){
                string msg = "deserializeArray() has been called, but the internal reader is null";
                logger.Error(msg);
                throw new NullReferenceException(msg);
            }
            StepProperty epv = new StepProperty();
            epv.Token = StepToken.StartArray;
            IList<StepProperty> values = new List<StepProperty>();
            while(_reader.Read()){
                switch(_reader.TokenType){
                    case StepToken.EndArray:
                        epv.Value = values;
                        epv.ValueType = typeof(IList<StepProperty>);
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
        
        

        private void cacheEntityProperties(){
            if( this.entitiesMappedToUpperCaseName == null )
                this.entitiesMappedToUpperCaseName = new Dictionary<string, Type>();
            if( this.entityProperties == null)
                this.entityProperties = new Dictionary<string, IList<PropertyInfo>>();
            
            //cache the data for each type
            Assembly asm = Assembly.GetExecutingAssembly();
            Type[] types = asm.GetTypes();
            foreach(Type t in types){
                //TODO filter out all classes which do not inherit (directly or indirectly) from Entity
                this.entitiesMappedToUpperCaseName.Add(t.Name.ToUpperInvariant(), t);
                
                //now cache the properties
                PropertyInfo[] properties = t.GetProperties();
                Array.Sort(properties, new DeclarationOrderComparator()); //HACK order the properties http://www.sebastienmahe.com/v3/seb.blog/2010/03/08/c-reflection-getproperties-kept-in-declaration-order/
                
                IList<PropertyInfo> cachedProperties = new List<PropertyInfo>();
                foreach(PropertyInfo pi in properties){
                    if(IsEntityProperty(pi)) //filter out all the entity properties (these are a required for IfcXml format only, and are not relevant to STEP format)
                        continue;
                    cachedProperties.Add(pi);
                }
                this.entityProperties.Add(t.FullName, cachedProperties);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pi"></param>
        /// <returns></returns>
        private bool IsEntityProperty(PropertyInfo pi){
            if(pi == null)
                return false;
            switch(pi.Name){
                case "href":
                case "ref":
                case "proxy":
                case "edo":
                case "entityid":
                case "entitypath":
                case "pos":
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// An ExpressDataObject is a representation of an IFC entity as entered in an IFC file.
        /// </summary>
        private class StepDataObject{
            private int _stepId;
            private string _name;
            private IList<StepProperty> _properties = new List<StepProperty>();
            
            /// <summary>
            /// Each entity occupies its own line and has its own Id within the step file.
            /// </summary>
            public int StepId{
                get{ return this._stepId; }
                set{ this._stepId = value; }
            }
            
            /// <summary>
            /// The Step entity has a name, which represents an IFC Entity and a class in the object model
            /// </summary>
            public string ObjectName{
                get{return this._name;}
                set{ this._name = value;}
            }
            
            /// <summary>
            /// The properties as given in the STEP format.
            /// </summary>
            public IList<StepProperty> Properties{
                get{ return this._properties; }
            }
        }
        
        /// <summary>
        /// The property name, data type and data value of a Step entity as represented in a Step file.
        /// </summary>
        private struct StepProperty{
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
        
        
        private struct ObjectReferenceLink{
            int referencingObject;
            PropertyInfo referencingProperty;
            int referencedObject;
            
            public int ReferencingObject{
                get{ return this.referencingObject; }
            }
            
            public PropertyInfo Property{
                get{ return this.referencingProperty; }
            }
            
            public int ReferencedObject{
                get{ return this.referencedObject; }
            }
            
            public ObjectReferenceLink(int referencingId, PropertyInfo prop, int referencedId){
                this.referencingObject = referencingId;
                this.referencedObject = referencedId;
                if(prop == null)
                    throw new ArgumentNullException("prop");
                this.referencingProperty = prop;
            }
        }
    }
}
