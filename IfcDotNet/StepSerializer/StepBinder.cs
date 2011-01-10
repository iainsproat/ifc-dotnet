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
using System.Xml.Serialization;
using System.Globalization;
using System.Reflection;
using System.Collections.Generic;

using log4net;

using IfcDotNet.Schema;
using IfcDotNet.StepSerializer.Utilities;

namespace IfcDotNet.StepSerializer
{
    /// <summary>
    /// The binder converts StepDataObjects to .Net objects, and vice versa
    /// </summary>
    internal class StepBinder
    {
        private static ILog logger = LogManager.GetLogger(typeof(StepBinder));
        
        //HACK
        private IList<StepEntityReference> _objectLinks = new List<StepEntityReference>();
        /// <summary>
        /// Do not access directly. This should only be accessed via RegisterEntity(Entity) method.
        /// </summary>
        int _entityCounter = 0;
        /// <summary>
        /// Do not add entities directly. Please use the RegisterEntity(Entity) method to add entities.
        /// </summary>
        private IDictionary<Entity, int> _entityRegister = new Dictionary<Entity, int>();
        private Queue<Entity> _queuedEntities = new Queue<Entity>();
        
        //caches of the schema types and properties
        IDictionary<string, Type> _entitiesMappedToUpperCaseName = new Dictionary<string, Type>();
        IDictionary<string, IList<PropertyInfo>> _entityProperties = new Dictionary<string, IList<PropertyInfo>>();
        
        
        public StepBinder()
        {
            cacheEntityProperties();
        }
        
        public iso_10303 Bind(StepFile step){
            if(step == null) throw new ArgumentNullException("step");
            if(step.Data == null) throw new ArgumentNullException("step.Data");
            if(step.Header == null) throw new ArgumentNullException("step.Header");
            
            iso_10303 iso10303 = new iso_10303();
            
            //TODO fill in meta data from STEP header
            iso10303.iso_10303_28_header = new iso_10303_28_header();
            foreach(StepDataObject sdo in step.Header){
                logger.Debug("Found header object : " + sdo.ObjectName);
                if(sdo.ObjectName == "FILE_NAME")
                    bindFileName(sdo, iso10303.iso_10303_28_header);
            }
            
            iso10303.uos = new uos1();
            IDictionary<int, Entity> entities = new SortedDictionary<int, Entity>();
            
            //try and instantiate a type for each STEP entity
            //and fill its properties
            foreach(KeyValuePair<int, StepDataObject> kvp in step.Data){
                if(kvp.Value == null)
                    continue;
                
                Object o = this.bindObject(kvp.Key, kvp.Value);
                
                //TODO check that instance type is derived from Entity.
                Entity e = o as Entity;
                
                logger.Debug("Adding a deserialized item.  Item Id : " + kvp.Key );
                
                entities.Add( kvp.Key, e );
            }
            logger.Info(String.Format(CultureInfo.InvariantCulture, "Deserialized {0} entities", entities.Count ));
            
            LinkReferences( entities );
            
            Entity[] items = new Entity[entities.Count];
            entities.Values.CopyTo( items, 0 );
            ((uos1)iso10303.uos).Items = items;
            
            //clear object links so there's no issues next time this method is run
            this._objectLinks = new List<StepEntityReference>();
            
            return iso10303;
        }
        
        /// <summary>
        /// Extracts StepDataObjects from a .Net object
        /// </summary>
        /// <param name="iso10303"></param>
        /// <returns></returns>
        public StepFile Extract(iso_10303 iso10303){
            if(iso10303 == null) throw new ArgumentNullException("iso10303");
            if(iso10303.iso_10303_28_header == null) throw new ArgumentNullException("iso10303.iso_10303_28_header");
            if(iso10303.uos == null) throw new ArgumentNullException("iso10303.uos");
            
            StepFile stepFile = new StepFile();
            
            //header
            stepFile.Header.Add( ExtractFileDescription( iso10303 ) );
            stepFile.Header.Add( ExtractFileName( iso10303 ) );
            stepFile.Header.Add( ExtractFileSchema( iso10303 ) );
            
            //data
            uos1 uos1 = iso10303.uos as uos1;
            if(uos1 == null){ //no data
                logger.Info( "Extract(iso_10303) could not extract, as iso10303.uos was not a type of uos1" );
                return stepFile;
            }
            
            //putting the entities in a dictionary so we can deal with references
            foreach(Entity e in uos1.Items){
                RegisterEntity( e );
            }
            
            while(this._queuedEntities.Count > 0){
                Entity e = this._queuedEntities.Dequeue();
                int entityId = this._entityRegister[e];
                
                StepDataObject sdo = ExtractEntity( e );
                stepFile.Data.Add( entityId, sdo );
            }
            
            //clear entityQueue, so next time this method is run it starts empty
            this._entityRegister = new Dictionary<Entity, int>();
            
            return stepFile;
        }
        
        #region Deserializer Methods
        private void LinkReferences(IDictionary<int, Entity> entities){
            foreach(StepEntityReference orl in this._objectLinks){
                //HACK need some error catching
                if(orl.ReferencingObject < 1)
                    throw new StepSerializerException("Attempting to link STEP objects, but the referencing object number is not within bounds (it's less than 1)");
                if(orl.ReferencedObject < 1)
                    throw new StepSerializerException("Attempting to link STEP objects, but the referenced object number is not within bounds (it's less than 1)");
                
                Entity referencing;
                try{
                    referencing = entities[orl.ReferencingObject];
                }catch(Exception e){
                    throw new StepSerializerException(String.Format(CultureInfo.InvariantCulture,
                                                                    "Could not locate referencing Entity #{0}",
                                                                    orl.ReferencingObject), e);
                }
                if(referencing == null)
                    throw new StepSerializerException(String.Format(CultureInfo.InvariantCulture,
                                                                    "Attempting to link STEP objects but the referencing object, #{0}, is null",
                                                                    orl.ReferencingObject));
                
                Entity referenced;
                try{
                    referenced = entities[orl.ReferencedObject];
                }catch(Exception e){
                    throw new StepSerializerException(String.Format(CultureInfo.InvariantCulture,
                                                                    "Could not locate referenced Entity #{0}",
                                                                    orl.ReferencedObject), e);
                }
                if(referenced == null)
                    throw new StepSerializerException(String.Format(CultureInfo.InvariantCulture,
                                                                    "Attempting to link STEP objects but the referenced object, #{0}, is null",
                                                                    orl.ReferencedObject));
                
                logger.Debug(String.Format(CultureInfo.InvariantCulture,
                                           "Attempting to link object #{0} of type {1} into {2}property {3}, expecting a type of {4}, of object #{5}, a type of {6}",
                                           orl.ReferencedObject,
                                           referenced.GetType().Name,
                                           orl.IsIndexed ? "index " + orl.Index + " of " : String.Empty,
                                           orl.Property.Name,
                                           orl.Property.PropertyType.Name,
                                           orl.ReferencingObject,
                                           referencing.GetType().Name));
                
                Object objToInsert = referenced;
                
                //a quirk of the automatically generated schema is that there are occasionaly intermediate
                //objects which wrap the value we wish to insert
                object[] xmlTypeAttributes = orl.Property.PropertyType.GetCustomAttributes(typeof(XmlTypeAttribute),true);
                logger.Debug("found xmlTypeAttributes : " + xmlTypeAttributes.Length);
                if(xmlTypeAttributes.Length > 0 ){
                    foreach(object xmlTypeAttribute in xmlTypeAttributes){
                        XmlTypeAttribute xta = xmlTypeAttribute as XmlTypeAttribute;
                        if(xta == null)
                            continue;
                        if(!xta.AnonymousType)
                            continue;
                        
                        //search for the actual property within the wrapping object to insert the data into
                        Type typeToSearchWithin = orl.Property.PropertyType;
                        Object wrappingObj = orl.Property.GetValue( referencing, null );
                        if(wrappingObj == null)
                            wrappingObj = Activator.CreateInstance(typeToSearchWithin);
                        PropertyInfo wrappingProperty = findWrappingProperty( typeToSearchWithin, referenced.GetType() );
                        
                        if(wrappingProperty == null)
                            continue;
                        
                        logger.Debug(String.Format(CultureInfo.InvariantCulture,
                                                   "Found an intermediate wrapping object of type {0}.  Found a property {1} within the wrapping object, this property expects a type of {2}",
                                                   typeToSearchWithin.Name,
                                                   wrappingProperty.Name,
                                                   wrappingProperty.PropertyType.Name));
                        
                        //insert the referenced into the wrapping object
                        if(orl.IsIndexed){
                            //it's an array, or similar indexed type
                            Object arr = wrappingProperty.GetValue( wrappingObj, null );
                            if(arr == null){
                                int length = orl.Index > 0 ? orl.Index + 1 : 1; //FIXME we may require larger arrays
                                logger.Debug("Creating a new array of type " + referenced.GetType().Name + " with length " + length + " to hold data to be inserted into index " + orl.Index);
                                Array array = Array.CreateInstance(referenced.GetType(), length);
                                array.SetValue( referenced, orl.Index );  //FIXME array may be out of index
                                wrappingProperty.SetValue( wrappingObj, array, null );
                            }else{
                                Array array = arr as Array;
                                if(array == null)
                                    throw new StepSerializerException("Object could not be cast as an Array");
                                array.SetValue( referenced, orl.Index );
                                //wrappingProperty.SetValue( wrappingObj, referenced, new object[]{orl.Index});
                            }
                        }else
                            wrappingProperty.SetValue( wrappingObj, referenced, null);
                        
                        objToInsert = wrappingObj;
                        break;
                    }
                }
                
                orl.Property.SetValue(referencing,
                                      objToInsert,
                                      null); //unlikely to be indexed if there is no wrapping type (case handled above)
            }
        }
        
        /// <summary>
        /// Some properties of the automatically generated  expect intermediate types around Arrays and some Objects.
        /// This method finds a property within typeToSearchWithin which can hold an object of type typeToSearchFor
        /// </summary>
        /// <param name="typeToSearch"></param>
        /// <param name="referencedType"></param>
        /// <returns></returns>
        private PropertyInfo findWrappingProperty(Type typeToSearchWithin, Type typeToSearchFor ){
            if(typeToSearchWithin == null)
                throw new ArgumentNullException("typeToSearch");
            if(typeToSearchFor == null)
                throw new ArgumentNullException("referencedType");
            logger.Debug(String.Format(CultureInfo.InvariantCulture,
                                       "Attempting  to find a property in type {0} which directly, or via an intermediate type, holds objects of type {1}",
                                       typeToSearchWithin.Name,
                                       typeToSearchFor.Name));
            
            Assembly asm = Assembly.GetAssembly(typeToSearchWithin);
            
            PropertyInfo[] intProps = typeToSearchWithin.GetProperties();
            
            foreach(PropertyInfo prop in intProps){
                if(prop == null)
                    continue;
                logger.Debug(String.Format(CultureInfo.InvariantCulture,
                                           "Investigating property {0} (type {1}) in type {2}",
                                           prop.Name,
                                           prop.PropertyType,
                                           typeToSearchWithin.Name));
                
                //check this anonType is not a base Type of the typeToSearch
                Type elementType = prop.PropertyType.GetElementType();
                if(elementType != null){
                    logger.Debug("Checking if " + elementType.FullName + " is a base type");
                    if(elementType.IsAssignableFrom( typeToSearchFor ))
                        return prop;
                }
                
                object[] xmlElementAttributes = prop.GetCustomAttributes(typeof(XmlElementAttribute), true);
                if(xmlElementAttributes == null || xmlElementAttributes.Length < 1){
                    logger.Debug("The property has no XmlElementAttributes to investigate");
                    continue;
                }
                foreach(object o in xmlElementAttributes){
                    XmlElementAttribute att = o as XmlElementAttribute;
                    if(o == null)
                        continue;
                    if(att.ElementName == typeToSearchFor.Name)
                        return prop;
                    
                    logger.Debug(String.Format(CultureInfo.InvariantCulture,
                                               "Attempting to search for XmlIncludeAttributes in type {0} defined in an XmlElementAttribute",
                                               att.ElementName));
                    Type anonType = asm.GetType("IfcDotNet.Schema." + att.ElementName); //HACK brittle as namespace is directly input
                    
                    
                    
                    //it may be via an intermediary anonymous type (using the XmlIncludeAttribute)
                    object[] xmlIncludeAttributes = anonType.GetCustomAttributes(typeof(XmlIncludeAttribute), false);
                    if(xmlIncludeAttributes == null || xmlIncludeAttributes.Length < 1)
                        continue;
                    foreach(object inc in xmlIncludeAttributes){
                        XmlIncludeAttribute incAtt = inc as XmlIncludeAttribute;
                        if(inc == null)
                            continue;
                        logger.Debug("Found an XmlIncludeAttribute applied to " + anonType.Name + " which includes type : " + incAtt.Type.Name);
                        if(incAtt.Type.FullName == typeToSearchFor.FullName)
                            return prop;
                    }
                }
            }
            throw new StepSerializerException(String.Format(CultureInfo.InvariantCulture,
                                                            "Could not find a property in type {0} which would wrap an object of type {1}",
                                                            typeToSearchWithin.Name,
                                                            typeToSearchFor.Name ));
        }
        
        /// <summary>
        /// binds the data from the FILE_NAME object of the STEP HEADER section
        /// </summary>
        /// <param name="sdo"></param>
        /// <param name="header"></param>
        private void bindFileName(StepDataObject sdo, iso_10303_28_header header){
            if(sdo == null) throw new ArgumentNullException("sdo");
            if(header == null) throw new ArgumentNullException("header");
            if(sdo.ObjectName != "FILE_NAME") throw new ArgumentException(String.Format(CultureInfo.InvariantCulture,
                                                                                        "bindFileName(StepDataObject, iso_10303_28_header) should only be called for StepDataObject FILE_NAME, and not {0}",
                                                                                        sdo.ObjectName));
            if(sdo.Properties == null || sdo.Properties.Count != 7) throw new ArgumentException("sdo does not have the correct number of properties");

            header.name = (string)sdo.Properties[0].Value;
            header.time_stamp = (DateTime)sdo.Properties[1].Value;
            header.author = (string)((IList<StepValue>)sdo.Properties[2].Value)[0].Value; //FIXME only copies first value in the array as header.author is a string, and not a list
            header.organization = (string)((IList<StepValue>)sdo.Properties[3].Value)[0].Value; //FIXME only copies first value in the array as header.author is a string, and not a list
            header.preprocessor_version = (string)sdo.Properties[4].Value;
            header.originating_system = (string)sdo.Properties[5].Value;
            header.authorization = (string)sdo.Properties[6].Value;
        }
        
        /// <summary>
        /// Converts a StepDataObject to a .Net object, entering the data into its properties
        /// or, for references, adding an entry
        /// </summary>
        /// <param name="sdo"></param>
        /// <returns></returns>
        private Object bindObject(int sdoId, StepDataObject sdo){
            if(sdo == null) throw new ArgumentNullException("sdo");
            
            string name = sdo.ObjectName;
            
            Object instance = createObject(sdo.ObjectName);
            
            //get the properties from the cache
            IList<PropertyInfo> typeProperties = _entityProperties[instance.GetType().FullName]; //TODO error catching
            
            //debugging
            logger.Debug("Property Names : ");
            foreach(PropertyInfo pi in typeProperties)
                logger.Debug(pi.Name);
            
            if(typeProperties.Count != sdo.Properties.Count)
                throw new StepSerializerException(String.Format(CultureInfo.InvariantCulture,
                                                                "The number of properties in the Step entity ( {0} ) do not equal the number of properties in the object ({1})",
                                                                sdo.Properties.Count,
                                                                typeProperties.Count));
            
            
            populateObject(ref instance, sdoId, sdo, typeProperties);
            
            return instance;
        }
        
        /// <summary>
        /// Creates a .Net object from a class string
        /// </summary>
        /// <returns></returns>
        private Object createObject(string name){
            if(String.IsNullOrEmpty( name ))
                throw new ArgumentNullException("name");
            
            logger.Debug("creatingObject from STEP entity name " + name );
            
            Type t = null;
            try{
                t = _entitiesMappedToUpperCaseName[name];
            }catch{
                t = _entitiesMappedToUpperCaseName[name + "1"]; //HACK some types end with the digit 1.
            }
            if(t == null)
                throw new StepSerializerException(String.Format(CultureInfo.InvariantCulture,
                                                                "No entity to map {0} was found.",
                                                                name));
            logger.Debug("Assembly found a type for the entity : " + t.FullName);
            
            Object instance = System.Activator.CreateInstance(t);
            
            //TODO error checking
            return instance;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="sdo"></param>
        /// <param name="typeProperties"></param>
        private void populateObject(ref Object obj, int sdoId, StepDataObject sdo, IList<PropertyInfo> typeProperties){
            if(obj == null)
                throw new ArgumentNullException("obj");
            if(typeProperties == null || typeProperties.Count < 1)
                throw new ArgumentNullException("typeProperties");
            
            if(sdo.Properties.Count != typeProperties.Count)
                throw new StepSerializerException(String.Format(CultureInfo.InvariantCulture,
                                                                "The number of data values, {0}, provided by the STEP data object, {1}, does not equal the number of properties, {2}, available in the .Net object, {3}",
                                                                sdo.Properties.Count,
                                                                sdo.ObjectName,
                                                                typeProperties.Count,
                                                                obj.GetType().FullName));
            
            for(int propertyIndex = 0; propertyIndex < sdo.Properties.Count; propertyIndex++){
                StepValue sv = sdo.Properties[propertyIndex];
                
                PropertyInfo pi = typeProperties[propertyIndex];
                
                if(pi == null)
                    throw new StepSerializerException(String.Format(CultureInfo.InvariantCulture,
                                                                    "A null property was found at index {0} of the cached properties provided for type {1}",
                                                                    propertyIndex,
                                                                    obj.GetType().Name));
                populateProperty(pi, ref obj, sv, sdoId);
            }
        }
        
        private bool IsNullableType(Type theType)
        {
            return !theType.IsValueType
                ||(theType.IsGenericType
                   &&
                   theType.GetGenericTypeDefinition().Equals( typeof(Nullable<>) )
                  );
        }
        
        private bool IsIgnorableProperty(PropertyInfo pi)
        {
            try{
                return pi.GetCustomAttributes(typeof(XmlIgnoreAttribute), true).Length > 0;
            }catch{
                return false;
            }
        }
        
        private void populateProperty(PropertyInfo pi, ref object obj, StepValue sv, int stepId ){
            if(pi == null)
                throw new ArgumentNullException("pi");
            if(obj == null)
                throw new ArgumentNullException("obj");
            
            //debugging
            logger.Debug("The property being assigned to in the .Net object : "       + pi.Name);
            logger.Debug("The type of values held by that .Net property     : "  + pi.PropertyType);
            logger.Debug("STEP token     : "     + sv.Token);
            logger.Debug("STEP value     : "     + sv.Value);
            logger.Debug("STEP valueType : " + sv.ValueType);
            
            switch(sv.Token){
                case StepToken.StartEntity:
                    mapObject(pi, ref obj, sv);
                    break;
                case StepToken.String:
                    mapString(pi, ref obj, sv);
                    break;
                case StepToken.LineReference:
                    storeLineReference(pi, ref obj, stepId, sv);
                    break;
                case StepToken.StartArray:
                    mapArray(pi, ref obj, sv, stepId);
                    break;
                case StepToken.Enumeration:
                    mapEnumeration(pi, ref obj, sv);
                    break;
                case StepToken.Integer:
                    mapInteger(pi, ref obj, sv);
                    break;
                case StepToken.Float:
                    mapFloat(pi, ref obj, sv);
                    break;
                case StepToken.Null:
                    //do nothing, the property value will already be null or default value (if a value type).
                    break;
                case StepToken.Overridden:
                    //do nothing
                    break;
                default:
                    throw new NotImplementedException(String.Format(CultureInfo.InvariantCulture,
                                                                    "Failed on attempting to set value of property. This token type is not yet implemented: {0}",
                                                                    sv.Token));
            }
        }
        
        #region Mapping functions
        
        /// <summary>
        /// Maps a STEP entity to a .Net object
        /// </summary>
        /// <param name="pi"></param>
        /// <param name="obj"></param>
        /// <param name="sp"></param>
        private void mapObject(PropertyInfo pi, ref Object obj, StepValue sv){
            if(pi == null)
                throw new ArgumentNullException("pi");
            if(obj == null)
                throw new ArgumentNullException("obj");
            if(sv.Value == null)
                throw new ArgumentNullException("sv.Value");
            StepDataObject sdo = sv.Value as StepDataObject;
            if(sdo == null)
                throw new ArgumentException("sv.Value is not of type StepDataObject");
            Object nestedObj = bindObject(-1, sdo);
            
            //as a quirk of the automatically generated schema
            //nested properties are wrapped in an intermediate class.
            Object wrappingObj = Activator.CreateInstance(pi.PropertyType);
            PropertyInfo wrappingProp = pi.PropertyType.GetProperty("Item"); //HACK it's possible that the property may not always be called 'Item' !!
            if(wrappingProp == null)
                throw new StepSerializerException("Could not find a suitable property in the wrapping class around a nested object");
            wrappingProp.SetValue(wrappingObj, nestedObj, null);
            
            //now insert the wrapping object
            pi.SetValue(obj, wrappingObj, null);
        }
        
        /// <summary>
        /// Maps a STEP string to a .Net System.String
        /// </summary>
        /// <param name="pi"></param>
        /// <param name="obj"></param>
        /// <param name="sp"></param>
        private void mapString(PropertyInfo pi, ref Object obj, StepValue sv){
            if(pi == null)
                throw new ArgumentNullException("pi");
            if(obj == null)
                throw new ArgumentNullException("obj");
            if(sv.Value == null)
                throw new ArgumentNullException("sv.Value");
            if(!(sv.Value is string))
                throw new ArgumentException("sv.Value is not of type String");
            pi.SetValue(obj, (string)sv.Value, null);
        }
        
        /// <summary>
        /// Maps a STEP enumeration to a .Net enum
        /// </summary>
        /// <param name="pi"></param>
        /// <param name="obj"></param>
        /// <param name="sp"></param>
        private void mapEnumeration(PropertyInfo pi, ref Object obj, StepValue sv){
            if(pi == null)
                throw new ArgumentNullException("pi");
            if(obj == null)
                throw new ArgumentNullException("obj");
            if(sv.Value == null)
                throw new ArgumentNullException("sv.Value");
            if(!(sv.Value is string))
                throw new ArgumentException("sv.Value should be a type of string");
            string spv = sv.Value as string;
            if(string.IsNullOrEmpty(spv))
                throw new ArgumentException("sv.Value should not be null or empty");
            spv = spv.ToLower();
            Object val;
            
            //STEP treats boolean values as enumerations
            if(pi.PropertyType == typeof(System.Boolean)){
                if(spv.Equals("t") || spv.Equals("f"))
                    val = spv.Equals("t");
                else
                    throw new FormatException(String.Format(CultureInfo.InvariantCulture,
                                                            "mapEnumeration found a boolean to parse, but the value was neither 't' nor 'f'.  The value was instead {0}",
                                                            spv));
            }else{
                //pi.Property type may be Nullable<theTypeWeNeed>
                Type enumType = Nullable.GetUnderlyingType( pi.PropertyType );
                if(enumType == null)
                    enumType = pi.PropertyType;
                val = Enum.Parse(enumType, spv.ToLower());//HACK the ToLower may not work in all cases
            }
            
            if(val == null)
                throw new FormatException(String.Format(CultureInfo.InvariantCulture,
                                                        "Could not find a suitable value for {0} in Enum type {1}",
                                                        spv.ToLower(),
                                                        pi.PropertyType.Name));
            pi.SetValue(obj, val, null);
        }
        
        /// <summary>
        /// Maps a STEP integer to a .Net System.Int32
        /// </summary>
        /// <param name="pi"></param>
        /// <param name="obj"></param>
        /// <param name="sp"></param>
        private void mapInteger(PropertyInfo pi, ref Object obj, StepValue sv){
            if(pi == null)
                throw new ArgumentNullException("pi");
            if(obj == null)
                throw new ArgumentNullException("obj");
            if(!(sv.Value is int))
                throw new ArgumentException("sv.Value cannot be cast to an int");
            pi.SetValue(obj, (int)sv.Value, null);
        }
        
        /// <summary>
        /// Maps a STEP float to .Net System.Double
        /// </summary>
        /// <param name="pi"></param>
        /// <param name="obj"></param>
        /// <param name="sp"></param>
        private void mapFloat(PropertyInfo pi, ref Object obj, StepValue sv){
            if(pi == null)
                throw new ArgumentNullException("pi");
            if(obj == null)
                throw new ArgumentNullException("obj");
            if(!(sv.Value is double))
                throw new ArgumentException("sv.Value cannot be cast to a double");
            pi.SetValue(obj, (double)sv.Value, null);
        }
        
        /// <summary>
        /// Maps a STEP array to a .Net array.
        /// </summary>
        /// <remarks>The .Net schema often wraps arrays in intermediate objects, and this case is dealt with by the function</remarks>
        /// <param name="pi">The property of the entity which holds an array</param>
        /// <param name="obj">The entity as currently constructed</param>
        /// <param name="sp">The value to be entered into the object</param>
        /// <param name="stepId">the number of the object as given in the step format being deserialized</param>
        private void mapArray(PropertyInfo pi, ref Object obj, StepValue sv, int stepId){
            if(pi == null)
                throw new ArgumentNullException("pi");
            if(obj == null)
                throw new ArgumentNullException("obj");
            if(sv.Value == null)
                throw new ArgumentNullException("sv.Value");
            
            IList<StepValue> stepValues = sv.Value as IList<StepValue>;
            if(stepValues == null)
                throw new StepSerializerException("sv.Value cannot be converted to List<StepValues>");
            if(stepValues.Count < 1)
                return;
            logger.Debug("Number of items in array : " + stepValues.Count);
            
            
            PropertyInfo arrayProperty = findArrayProperty( pi );
            if(arrayProperty == null)
                throw new StepSerializerException(String.Format(CultureInfo.InvariantCulture,
                                                                "Cannot find a suitable property in the array wrapping type {0} which would hold an array",
                                                                pi.PropertyType.Name));
            
            Object arrayWrappingObject;
            //check incase there is an intermediate type
            if(arrayProperty.Equals(pi))
                arrayWrappingObject = obj;
            else
                arrayWrappingObject = System.Activator.CreateInstance(pi.PropertyType);
            
            //get the array, or create it if it doesn't already exist
            Array array = (Array)arrayProperty.GetValue(arrayWrappingObject, null);
            if(array == null)
                array = Array.CreateInstance( arrayProperty.PropertyType.GetElementType(), stepValues.Count );
            if(array.Length != stepValues.Count)
                throw new StepSerializerException(String.Format(CultureInfo.InvariantCulture,
                                                                "The array length, {0}, is not long enough to hold all the properties, {1}, being mapped to it",
                                                                array.Length,
                                                                stepValues.Count));
            
            //iterate through each item and add it to the array.
            for(int arrayIndex = 0; arrayIndex < stepValues.Count; arrayIndex++){
                StepValue svInner = stepValues[arrayIndex];
                
                //debugging
                logger.Debug("Mapping property in array. Index : " + arrayIndex);
                logger.Debug("Property being assigned to in the .Net object : "       + arrayProperty.Name);
                logger.Debug("The Type of values held by that .Net property : " + arrayProperty.PropertyType.GetElementType().Name);
                logger.Debug("STEP token     : "     + svInner.Token);
                logger.Debug("STEP value     : "     + svInner.Value);
                logger.Debug("STEP valueType : " + svInner.ValueType);
                
                switch(svInner.Token){
                    case StepToken.String:
                    case StepToken.Float:
                    case StepToken.Integer:
                        if(array.GetType().GetElementType().Equals( svInner.ValueType ))
                            array.SetValue(svInner.Value, arrayIndex);
                        else{
                            //we need to convert/cast the type, and need the conversion operator of the target type to assist with this
                            MethodInfo mi = array.GetType().GetElementType().GetMethod(
                                "op_Implicit",
                                (BindingFlags.Public | BindingFlags.Static),
                                null,
                                new Type[] { svInner.ValueType },
                                new ParameterModifier[0]
                               );
                            if(mi == null)
                                throw new NotImplementedException(String.Format(CultureInfo.InvariantCulture,
                                                                                "Cannot convert from a {0} to a type of {1}, as there is no static implicit cast method present on the type we're casting to",
                                                                                svInner.ValueType.Name,
                                                                                array.GetType().GetElementType().Name));
                            Object parsedValue = mi.Invoke(null, BindingFlags.InvokeMethod | (BindingFlags.Public | BindingFlags.Static), null, new object[] { svInner.Value }, CultureInfo.InvariantCulture);
                            if(parsedValue == null)
                                throw new StepSerializerException(String.Format(CultureInfo.InvariantCulture,
                                                                                "Was unable to invoke the conversion operator to convert the value {0}, a type of {1}, to the type {2}",
                                                                                svInner.Value.ToString(),
                                                                                svInner.ValueType.Name,
                                                                                array.GetType().GetElementType().Name));
                            array.SetValue(parsedValue, arrayIndex);
                        }
                        continue;
                    case StepToken.StartEntity:
                        Object nestedObj = bindObject(-1, svInner.Value as StepDataObject );
                        array.SetValue(nestedObj, arrayIndex);
                        continue;
                    case StepToken.LineReference:
                        storeLineReference(pi, ref obj, stepId, svInner, arrayIndex);
                        continue;
                    default:
                        throw new NotImplementedException(svInner.Token + " is not yet implemented in mapArray");
                }
            }
            
            //now add the array to the array wrapping object
            arrayProperty.SetValue(arrayWrappingObject, array, null);
            
            //if there is a wrapping object, then we need to insert that into the object.
            if(!arrayProperty.Equals(pi)){
                pi.SetValue(obj, arrayWrappingObject, null);
            }
        }
        
        /// <summary>
        /// In the schema, arrays are wrapped in objects.
        /// Within these wrapping objects it is not always obvious as to which property should hold the array.
        /// This method attempts to seek out that property.
        /// </summary>
        /// <param name="pi"></param>
        /// <returns></returns>
        private PropertyInfo findArrayProperty( PropertyInfo pi ){
            if(pi == null)
                throw new ArgumentNullException("pi");
            
            logger.Debug(String.Format(CultureInfo.InvariantCulture,
                                       "findArrayProperty(PropertyInfo), for property {0} of type {1}",
                                       pi.Name,
                                       pi.DeclaringType.Name));
            
            //check if the PropertyType can hold an array
            if(pi.PropertyType.IsArray)
                return pi;
            
            //if not, then there must be a wrapping type
            //one of the properties of the wrapping type must be able to hold an array
            PropertyInfo[] propertiesOfWrappingType = pi.PropertyType.GetProperties();
            foreach(PropertyInfo prop in propertiesOfWrappingType){
                if(prop.PropertyType.IsArray){
                    object[] elementAttributes = prop.GetCustomAttributes(typeof(XmlElementAttribute), false);
                    if(elementAttributes.Length > 0)
                        return prop;
                }
            }
            throw new StepSerializerException("Could not find a suitable array property in which to map an array to");
        }
        #endregion
        
        private void storeLineReference(PropertyInfo pi, ref Object obj, int stepId, StepValue sp){
            this._objectLinks.Add(new StepEntityReference(stepId, pi, (int)sp.Value));
        }
        
        private void storeLineReference(PropertyInfo pi, ref Object obj, int stepId, StepValue sp, int index){
            this._objectLinks.Add(new StepEntityReference(stepId, pi, (int)sp.Value, index));
        }

        
        #endregion
        
        #region Serializer Methods
        private int RegisterEntity( Entity e ){
            int entityId = -1;
            bool alreadyRegistered = this._entityRegister.TryGetValue( e, out entityId );
            if(alreadyRegistered)
                return entityId;
            
            //else it's not yet registered, so register and queue it
            this._queuedEntities.Enqueue( e );
            this._entityCounter++;
            this._entityRegister.Add(e, _entityCounter);
            return _entityCounter; //the number we registered the entity with
        }
        
        private StepDataObject ExtractFileDescription( iso_10303 iso10303 ){
            if(iso10303 == null) throw new ArgumentNullException("iso10303");
            //FIXME iso10303 is not actually used.
            
            StepDataObject sdo = new StepDataObject();
            sdo.ObjectName = "FILE_DESCRIPTION";
            
            StepValue sv1 = new StepValue( StepToken.StartArray,
                                          new List<StepValue>(1));
            
            StepValue sv11 = new StepValue(StepToken.String,
                                           "ViewDefinition [CoordinationView, QuantityTakeOffAddOnView]");
            sdo.Properties.Add( sv1 );
            ((IList<StepValue>)sv1.Value).Add( sv11 );
            
            StepValue sv2 = new StepValue(StepToken.String, "2;1");
            sdo.Properties.Add( sv2 );
            return sdo;
        }
        
        private StepDataObject ExtractFileName( iso_10303 iso10303 ){
            if(iso10303 == null) throw new ArgumentNullException("iso10303");
            iso_10303_28_header header = iso10303.iso_10303_28_header;
            if(header == null) throw new ArgumentNullException("iso10303.iso_10303_28_header");
            StepDataObject sdo = new StepDataObject();
            sdo.ObjectName = "FILE_NAME";
            
            sdo.Properties.Add( new StepValue( StepToken.String,     header.name));
            sdo.Properties.Add( new StepValue( StepToken.Date,       header.time_stamp ) );
            
            IList<StepValue> authorList = new List<StepValue>(1);//FIXME header.author is a string and not a list, but the Step file expects an array
            authorList.Add( new StepValue( StepToken.String, header.author ) );
            sdo.Properties.Add( new StepValue( StepToken.StartArray, authorList ) );
            
            IList<StepValue> orgList = new List<StepValue>(1);//FIXME header.organization is a string and not a list, but the Step file expects an array
            orgList.Add( new StepValue( StepToken.String, header.organization ) );
            sdo.Properties.Add( new StepValue( StepToken.StartArray, orgList ) );
            
            sdo.Properties.Add( new StepValue( StepToken.String,     header.preprocessor_version ));
            sdo.Properties.Add( new StepValue( StepToken.String,     header.originating_system ));
            sdo.Properties.Add( new StepValue( StepToken.String,     header.authorization ));
            return sdo;
        }
        
        private StepDataObject ExtractFileSchema( iso_10303 iso10303 ){
            if(iso10303 == null) throw new ArgumentNullException( "iso10303" );
            StepDataObject sdo = new StepDataObject();
            sdo.ObjectName = "FILE_SCHEMA";
            IList<StepValue> version = new List<StepValue>(1);
            version.Add( new StepValue( StepToken.String, "IFC2X3" ));
            sdo.Properties.Add( new StepValue(StepToken.StartArray, version ));
            return sdo;
        }
        
        private StepDataObject ExtractEntity( Entity entity ){
            if(entity == null) throw new ArgumentNullException("entity");
            Type entityType = entity.GetType();
            IList<PropertyInfo> entityProps = this._entityProperties[entityType.FullName];
            
            StepDataObject sdo = new StepDataObject();
            sdo.ObjectName = GetObjectName( entityType );
            
            foreach(PropertyInfo pi in entityProps){
                sdo.Properties.Add( ExtractProperty( entity, pi ) );
            }
            return sdo;
        }
        
        private string GetObjectName( Type t ){
            if(t == null) throw new ArgumentNullException("t");
            string name = t.Name.ToUpper();
            
            object[] xmlRootAttributes = t.GetCustomAttributes(typeof(XmlRootAttribute),false);
            if(xmlRootAttributes == null || xmlRootAttributes.Length < 1) return name;
            XmlRootAttribute xmlRootAtt = xmlRootAttributes[0] as XmlRootAttribute;
            if(xmlRootAtt == null) return name;
            
            return String.IsNullOrEmpty(xmlRootAtt.ElementName) ? name : xmlRootAtt.ElementName.ToUpper();
        }
        
        private StepValue ExtractProperty( Object entity, PropertyInfo pi ){
            if(entity == null) throw new ArgumentNullException("entity");
            if(pi == null) throw new ArgumentNullException("pi");
            logger.Debug(String.Format(CultureInfo.InvariantCulture,
                                       "ExtractProperty(Object, PropertyInfo, object[]) called for entity of type {0}, property {1}",
                                       entity.GetType().FullName, pi.Name));
            
            object value = pi.GetValue( entity, null );
            if(value == null)
                return new StepValue(StepToken.Null, null);
            if(IsIndirectProperty( pi  )){
                return ExtractProperty(value, GetIndirectProperty( pi.PropertyType ));//FIXME what if this is indexed??
            }
            
            return ExtractPropertyValue( value );
        }
        
        private StepValue ExtractPropertyValue(Object value ){
            if(value == null)
                return new StepValue(StepToken.Null, null);
            
            if(typeof(Entity).IsAssignableFrom( value.GetType() ))//it's a nested entity, which should be referenced
                return ExtractNestedEntity(value as Entity);
            
            if(IsAnonymousType( value.GetType() )){
                return ExtractProperty( value, GetIndirectProperty( value.GetType() ));
            }
            
            if(value.GetType().Equals(typeof(string)))
                return new StepValue(StepToken.String, value);
            
            if(value.GetType().IsArray){
                Array array = (Array)value;
                IList<StepValue> arrayValues = new List<StepValue>(array.Length);
                for(int i = 0; i < array.Length; i++){
                    object o = array.GetValue( i );
                    if(o == null)
                        logger.Debug("Found null object at index " + i + " in array ");
                    else
                        logger.Debug("Found in array at index " + i + " an object " + o.ToString() + " of type " + o.GetType());
                    arrayValues.Add( ExtractPropertyValue( o ) );
                }
                return new StepValue(StepToken.StartArray, arrayValues);
            }
            
            //TODO enum
            
            //TODO nested object (not an array, not an enum and in the IfcDotNet.Schema namespace)
            
            //TODO complete primitive types
            if(value.GetType().IsPrimitive){
                switch(value.GetType().FullName){//HACK, there must be a better way of
                    case "System.Boolean":
                        return new StepValue(StepToken.Boolean, (bool)value ? ".TRUE." : ".FALSE.");
                    case "System.Double":
                        return new StepValue(StepToken.Float, value);
                    default:
                        throw new NotImplementedException(String.Format(CultureInfo.InvariantCulture,
                                                                        "ExtractProperty method has not yet implemented for a primitive of type {0}",
                                                                        value.GetType().FullName));
                }
            }
            
            throw new StepSerializerException(String.Format(CultureInfo.InvariantCulture,
                                                            "ExtractProperty method has an object of type {0}, which it doesn't know how to extract",
                                                            value.GetType().FullName));
        }
        
        /// <summary>
        /// Due to the quirks of the automatically generated schema some properties do not directly
        /// contain the data, and instead contain a wrapping class
        /// which in turn holds a property which contains the data we require.
        /// </summary>
        /// <param name="pi"></param>
        /// <returns></returns>
        private bool IsIndirectProperty( PropertyInfo pi ){
            if(pi == null) throw new ArgumentNullException("pi");
            return IsAnonymousType( pi.PropertyType );
            
        }
        
        private bool IsAnonymousType( Type t){
            if(t == null) throw new ArgumentNullException("t");
            object[] xmlTypeAttributes = t.GetCustomAttributes(typeof(XmlTypeAttribute), false);
            if(xmlTypeAttributes == null || xmlTypeAttributes.Length < 1) return false;
            XmlTypeAttribute xmlTypeAtt = xmlTypeAttributes[0] as XmlTypeAttribute;
            if(xmlTypeAtt == null) return false;
            return xmlTypeAtt.AnonymousType;
        }
        
        private PropertyInfo GetIndirectProperty( Type t ){
            if(t == null) throw new ArgumentNullException("t");
            
            PropertyInfo[] propertiesOnIndirectType = t.GetProperties();//FIXME should filter by bindi
            foreach(PropertyInfo propertyOnIndirectType in propertiesOnIndirectType){
                object[] xmlElementAttributes = propertyOnIndirectType.GetCustomAttributes(typeof(XmlElementAttribute), true);
                if(xmlElementAttributes != null && xmlElementAttributes.Length > 0)
                    return propertyOnIndirectType;
                    
                object[] xmlTextAttributes = propertyOnIndirectType.GetCustomAttributes(typeof(XmlTextAttribute), true);
                if(xmlTextAttributes != null && xmlTextAttributes.Length > 0)
                    return propertyOnIndirectType;
                //FIXME what if this itself is indirect? (multiple layers of indirection?)
            }
            
            throw new StepSerializerException(String.Format(CultureInfo.InvariantCulture,
                                                            "GetIndirectProperty could not find a property with an XmlElementAttribute in type {0}",
                                                            t.FullName));
        }
        
        public StepValue ExtractNestedEntity(Entity value){
            int nestedEntityId = this.RegisterEntity( value );
            return new StepValue(StepToken.LineReference, nestedEntityId);
        }
        
        static Type GetEnumerableType(Type type) {
            foreach (Type intType in type.GetInterfaces()) {
                if (intType.IsGenericType
                    && intType.GetGenericTypeDefinition() == typeof(IEnumerable<>)) {
                    return intType.GetGenericArguments()[0];
                }
            }
            return null;
        }
        
        #endregion
        /// <summary>
        /// Determines if a property is defined in the Entity class.
        /// </summary>
        /// <param name="pi"></param>
        /// <returns></returns>
        private bool IsEntityProperty(PropertyInfo pi){
            if(pi == null)
                return false;
            //FIXME there's a better way to do this!
            switch(pi.Name){
                case "id"://FIXME not part of entity, but to be ignored anyway
                case "path": //FIXME not part of entity, but to be ignored anyway
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
        /// Why are we caching??! is there any point to it?
        /// </summary>
        private void cacheEntityProperties(){//FIXME
            if( this._entitiesMappedToUpperCaseName == null )
                this._entitiesMappedToUpperCaseName = new Dictionary<string, Type>();
            if( this._entityProperties == null)
                this._entityProperties = new Dictionary<string, IList<PropertyInfo>>();
            
            //cache the data for each type
            Assembly asm = Assembly.GetExecutingAssembly();
            
            Type[] types = asm.GetTypes();
            foreach(Type t in types){
                if(t.Namespace != "IfcDotNet.Schema") continue; //HACK hardcoded namespace
                
                //TODO filter out digits from the name
                this._entitiesMappedToUpperCaseName.Add(t.Name.ToUpperInvariant(), t);
                
                //now cache the properties
                PropertyInfo[] properties = t.GetProperties();
                Array.Sort(properties, new DeclarationOrderComparator()); //HACK order the properties http://www.sebastienmahe.com/v3/seb.blog/2010/03/08/c-reflection-getproperties-kept-in-declaration-order/
                
                IList<PropertyInfo> cachedProperties = new List<PropertyInfo>();
                
                foreach(PropertyInfo pi in properties){
                    if(IsEntityProperty(pi)) //filter out all the entity properties (these are a required for IfcXml format only, and are not relevant to STEP format)
                        continue;
                    if(IsIgnorableProperty(pi))
                        continue;
                    cachedProperties.Add(pi);
                }
                this._entityProperties.Add(t.FullName, cachedProperties);
            }
        }
    }
}
