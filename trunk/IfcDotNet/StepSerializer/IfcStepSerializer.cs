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
using System.Xml.Serialization;
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
    /// Reads IFC data in STEP (10303 part 21) format.
    /// </summary>
    public class IfcStepSerializer
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(IfcStepSerializer));
        
        private InternalStepSerializer _internalSerializer;
        private IList<StepDataObject> dataObjects = new List<StepDataObject>();
        private IList<StepEntityReference> objectLinks = new List<StepEntityReference>();
        
        //caches of the model types and properties
        IDictionary<string, Type> entitiesMappedToUpperCaseName = new Dictionary<string, Type>();
        IDictionary<string, IList<PropertyInfo>> entityProperties = new Dictionary<string, IList<PropertyInfo>>();
        
        
        public IfcStepSerializer(){
            cacheEntityProperties();
        }
        
        
        public iso_10303 Deserialize(StepReader reader)
        {
            if( reader == null )
                throw new ArgumentNullException( "reader" );
            
            this._internalSerializer = new InternalStepSerializer();
            this.dataObjects = this._internalSerializer.Deserialize(reader);
            
            iso_10303 iso10303 = new iso_10303();
            //TODO fill in meta data from STEP header
            iso10303.uos = new uos1();
            ((uos1)iso10303.uos).Items = new Entity[this.dataObjects.Count];//FIXME this is larger than required. There may be some trimming of the data objects as they are placed into the object tree
            
            //try and instantiate a type for each STEP object
            //and fill its properties
            int itemCount = 0;
            foreach(StepDataObject edo in this.dataObjects){
                if(edo == null)
                    continue;
                
                Object o = deserializeObject(edo);
                
                //TODO check that instance type is derived from Entity.
                Entity e = o as Entity;
                
                logger.Debug("Adding an item to uos1.Items at position : " + itemCount );
                
                ((uos1)iso10303.uos).Items[itemCount] = e;//FIXME
                itemCount++;
            }
            
            foreach(StepEntityReference orl in this.objectLinks){
                //HACK need some error catching
                Entity referencing = ((uos1)iso10303.uos).Items[orl.ReferencingObject - 1];
                Entity referenced = ((uos1)iso10303.uos).Items[orl.ReferencedObject - 1];
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
                        Type typeToSearch = orl.Property.PropertyType;
                        Object wrappingObj = orl.Property.GetValue( referencing, null );
                        if(wrappingObj == null)
                            wrappingObj = Activator.CreateInstance(typeToSearch);
                        PropertyInfo wrappingProperty = findWrappingProperty( typeToSearch, referenced.GetType() );
                        
                        if(wrappingProperty == null)
                            continue;
                        
                        logger.Debug(String.Format(CultureInfo.InvariantCulture,
                                                   "Found an intermediate wrapping object of type {0}.  Found a property {1} within the wrapping object, this property expects a type of {2}",
                                                   typeToSearch.Name,
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
            
            return iso10303;
        }
        
        private PropertyInfo findWrappingProperty(Type typeToSearch, Type referencedType ){
            if(typeToSearch == null)
                throw new ArgumentNullException("typeToSearch");
            if(referencedType == null)
                throw new ArgumentNullException("referencedType");
            logger.Debug(String.Format(CultureInfo.InvariantCulture,
                                       "Attempting  to find a property in type {0} which directly, or via an intermediate type, holds objects of type {1}",
                                       typeToSearch.Name,
                                       referencedType.Name));
            
            Assembly asm = Assembly.GetAssembly(typeToSearch);
            
            PropertyInfo[] intProps = typeToSearch.GetProperties();
            
            IList<String> baseTypes = getBaseTypes(referencedType);
            foreach(String s in baseTypes)
                logger.Debug("base Type : " + s);
            
            
            foreach(PropertyInfo prop in intProps){
                if(prop == null)
                    continue;
                logger.Debug(String.Format(CultureInfo.InvariantCulture,
                                           "Investigating property {0} (type {1}) in type {2}",
                                           prop.Name,
                                           prop.PropertyType,
                                           typeToSearch.Name));
                
                //check this anonType is not a base Type of the typeToSearch
                Type elementType = prop.PropertyType.GetElementType();
                if(elementType != null){
                    logger.Debug("Checking if " + elementType.FullName + " is a base type");
                    if(baseTypes.Contains( elementType.FullName ))
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
                    if(att.ElementName == referencedType.Name)
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
                        if(incAtt.Type.FullName == referencedType.FullName)
                            return prop;
                    }
                }
            }
            throw new StepSerializerException(String.Format(CultureInfo.InvariantCulture,
                                                            "Could not find a property in type {0} which would wrap an object of type {1}",
                                                            typeToSearch.Name,
                                                            referencedType.Name ));
        }
        
        /// <summary>
        /// Method to find all classes from which a class inherits
        /// </summary>
        /// <param name="inheritingType"></param>
        /// <returns></returns>
        private IList<String> getBaseTypes(Type inheritingType){
            if(inheritingType == null)
                throw new ArgumentNullException("inheritingType");
            
            IList<String> baseTypes = new List<String>();
            baseTypes.Add( inheritingType.FullName );
            Type baseType = inheritingType.BaseType;
            if(baseType.Namespace == "IfcDotNet.Schema"){//HACK brittle hardcoded namespace
                IList<String> basesBaseTypes = getBaseTypes( baseType );
                foreach(String t in basesBaseTypes ){
                    if(!baseTypes.Contains( t ))
                        baseTypes.Add( t );
                }
            }
            return baseTypes;
        }
        
        /// <summary>
        /// Converts a StepDataObject to a .Net object, entering the data into its properties
        /// or, for references, adding an entry
        /// </summary>
        /// <param name="sdo"></param>
        /// <returns></returns>
        private Object deserializeObject(StepDataObject sdo){
            if(sdo == null)
                throw new ArgumentNullException("sdo");
            
            string name = sdo.ObjectName;
            
            Object instance = createObject(sdo.ObjectName);
            
            //get the properties from the cache
            IList<PropertyInfo> typeProperties = entityProperties[instance.GetType().FullName]; //TODO error catching
            
            //debugging
            logger.Debug("Property Names : ");
            foreach(PropertyInfo pi in typeProperties)
                logger.Debug(pi.Name);
            
            if(typeProperties.Count != sdo.Properties.Count)
                throw new StepSerializerException(String.Format(CultureInfo.InvariantCulture,
                                                                "The number of properties in the Step entity ( {0} ) do not equal the number of properties in the object ({1})",
                                                                sdo.Properties.Count,
                                                                typeProperties.Count));
            
            
            populateObject(ref instance, sdo, typeProperties);
            
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
                t = entitiesMappedToUpperCaseName[name];
            }catch{
                t = entitiesMappedToUpperCaseName[name + "1"]; //HACK some types end with the digit 1.
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
        private void populateObject(ref Object obj, StepDataObject sdo, IList<PropertyInfo> typeProperties){
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
                populateProperty(pi, ref obj, sv, sdo.StepId);
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
            Object nestedObj = deserializeObject(sdo);
            
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
                val = Enum.Parse(pi.PropertyType, spv.ToLower());//HACK the ToLower may not work in all cases
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
            this.objectLinks.Add(new StepEntityReference(stepId, pi, (int)sp.Value));
        }
        
        private void storeLineReference(PropertyInfo pi, ref Object obj, int stepId, StepValue sp, int index){
            this.objectLinks.Add(new StepEntityReference(stepId, pi, (int)sp.Value, index));
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
                    if(IsIgnorableProperty(pi))
                        continue;
                    cachedProperties.Add(pi);
                }
                this.entityProperties.Add(t.FullName, cachedProperties);
            }
        }
        
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
                case "id"://FIXME not part of entity
                case "path": //FIXME not part of entity
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
    }
}
