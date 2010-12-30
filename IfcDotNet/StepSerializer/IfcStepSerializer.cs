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
	/// Reads IFC data in STEP (10303) format.
	/// </summary>
	public class IfcStepSerializer
	{
		private static readonly ILog logger = LogManager.GetLogger(typeof(IfcStepSerializer));
		
		private InternalStepSerializer _internalSerializer;
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
			
			this._internalSerializer = new InternalStepSerializer();
			this.dataObjects = this._internalSerializer.Deserialize(reader);
			
			iso_10303_28 iso10303 = new iso_10303_28();
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
			
			foreach(ObjectReferenceLink orl in this.objectLinks){
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
						//TODO error catching
						
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
									throw new ApplicationException("Object could not be cast as an Array");
								array.SetValue( referenced, orl.Index );
								//wrappingProperty.SetValue( wrappingObj, referenced, new object[]{orl.Index});
							}
						}else
							wrappingProperty.SetValue( wrappingObj, referenced, null);
						
						
						//now insert the wrapping object into the referencing object
						orl.Property.SetValue(referencing, wrappingObj, null);
						break;
					}
				}else{
					if(orl.IsIndexed){
						
						Object arr = orl.Property.GetValue(referencing, null);
						logger.Debug("Found array, " + arr);
						orl.Property.SetValue(referencing, referenced, new object[]{orl.Index});
					}else
						orl.Property.SetValue(referencing,
						                      referenced,
						                      null);
					
				}
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
			throw new ApplicationException(String.Format(CultureInfo.InvariantCulture,
			                                             "Could not find a property in type {0} which would wrap an object of type {1}", 
			                                             typeToSearch.Name,
			                                             referencedType.Name ));
		}
		
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
		
		private Object deserializeObject(StepDataObject sdo){
			if(sdo == null)
				throw new ArgumentNullException("sdo");
			
			string name = sdo.ObjectName;
			logger.Debug("deserializing entity : " + name );
			
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
				throw new ApplicationException(String.Format(CultureInfo.InvariantCulture,
				                                             "No entity to map {0} was found.",
				                                             name));
			//FIXME further checks required (is the type a subclass of Entity etc..)
			
			logger.Debug("Assembly found a type for the entity : " + t.FullName);
			
			IList<PropertyInfo> typeProperties = entityProperties[t.FullName]; //TODO error catching
			//TODO assert that the property count for this type equals the property count in our object. (as this will catch problems with overridden properties in the Express schema)
			//debugging
			logger.Debug("Property Names : ");
			foreach(PropertyInfo pi in typeProperties)
				logger.Debug(pi.Name);
			//FIXME the below check fails with overridden properties
			//if(typeProperties.Count != edo.Properties.Count)
			//	throw new ApplicationException("The number of properties in the Step entity {" + edo.Properties.Count + "} do not equal the number of properties in the object {" + typeProperties.Count + "}");
			
			Object instance = System.Activator.CreateInstance(t);
			
			setProperties(ref instance, sdo, typeProperties);
			
			return instance;
		}
		
		private void setProperties(ref Object obj, StepDataObject sdo, IList<PropertyInfo> typeProperties){
		    if(obj == null)
		        throw new ArgumentNullException("obj");
		    if(typeProperties == null || typeProperties.Count < 1)
		        throw new ArgumentNullException("typeProperties");
		    
			int propCount = 0;
			foreach(StepProperty sp in sdo.Properties){
				PropertyInfo pi = typeProperties[propCount];//TODO error catching
				//debugging
				logger.Debug("property : "           + propCount);
				
				mapProperty(pi, ref obj, sp, sdo.StepId);
				propCount++;
			}
		}
		
		private bool IsNullableType(Type theType)
		{
			return (theType.IsGenericType && theType.
			        GetGenericTypeDefinition().Equals
			        (typeof(Nullable<>)));
		}
		
		private bool IsIgnorableProperty(PropertyInfo pi)
		{
			try{
				return pi.GetCustomAttributes(typeof(XmlIgnoreAttribute), true).Length > 0;
			}catch{
				return false;
			}
		}
		
		private void mapProperty(PropertyInfo pi, ref object obj, StepProperty sp, int stepId ){
			//debugging
			logger.Debug("The property being assigned to in the .Net object : "       + pi.Name);
			logger.Debug("The type of values held by that .Net property     : "  + pi.PropertyType);
			logger.Debug("STEP token     : "     + sp.Token);
			logger.Debug("STEP value     : "     + sp.Value);
			logger.Debug("STEP valueType : " + sp.ValueType);
			
			
			switch(sp.Token){
				case StepToken.StartEntity:
					mapObject(pi, ref obj, sp);
					break;
				case StepToken.String:
					mapString(pi, ref obj, sp);
					break;
				case StepToken.LineReference:
					storeLineReference(pi, ref obj, stepId, sp);
					break;
				case StepToken.StartArray:
					mapArray(pi, ref obj, sp, stepId);
					break;
				case StepToken.Enumeration:
					mapEnumeration(pi, ref obj, sp);
					break;
				case StepToken.Integer:
					mapInteger(pi, ref obj, sp);
					break;
				case StepToken.Float:
					mapFloat(pi, ref obj, sp);
					break;
				case StepToken.Null:
					//do nothing, the property value will already be null.
					//FIXME check that this is a correct assumption
					//TODO assert that the property is actually nullable
					break;
				case StepToken.Overridden:
					//do nothing
					break;
				default:
					throw new NotImplementedException(String.Format(CultureInfo.InvariantCulture,
					                                                "Failed on attempting to set value of property. This token type is not yet implemented: {0}",
					                                                sp.Token));
			}
		}
		
		#region Mapping functions
		
		/// <summary>
		/// Maps a STEP entity to a .Net object
		/// </summary>
		/// <param name="pi"></param>
		/// <param name="obj"></param>
		/// <param name="sp"></param>
		private void mapObject(PropertyInfo pi, ref Object obj, StepProperty sp){
			if(pi == null)
				throw new ArgumentNullException("pi");
			if(obj == null)
				throw new ArgumentNullException("obj");
			if(sp.Value == null)
				throw new ArgumentNullException("sp.Value");
			StepDataObject sdo = sp.Value as StepDataObject;
			if(sdo == null)
				throw new ArgumentException("sp.Value is not of type StepDataObject");
			Object nestedObj = deserializeObject(sdo);
			
			//as a quirk of the automatically generated schema
			//nested properties are wrapped in an intermediate class.
			Object wrappingObj = Activator.CreateInstance(pi.PropertyType);
			PropertyInfo wrappingProp = pi.PropertyType.GetProperty("Item"); //HACK it's possible that the property may not always be called 'Item' !!
			if(wrappingProp == null)
				throw new ApplicationException("Could not find a suitable property in the wrapping class around a nested object");
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
		private void mapString(PropertyInfo pi, ref Object obj, StepProperty sp){
			if(pi == null)
				throw new ArgumentNullException("pi");
			if(obj == null)
				throw new ArgumentNullException("obj");
			if(sp.Value == null)
				throw new ArgumentNullException("sp.Value");
			if(!(sp.Value is string))
				throw new ArgumentException("sp.Value is not of type String");
			pi.SetValue(obj, (string)sp.Value, null);
		}
		
		/// <summary>
		/// Maps a STEP enumeration to a .Net enum
		/// </summary>
		/// <param name="pi"></param>
		/// <param name="obj"></param>
		/// <param name="sp"></param>
		private void mapEnumeration(PropertyInfo pi, ref Object obj, StepProperty sp){
			if(pi == null)
				throw new ArgumentNullException("pi");
			if(obj == null)
				throw new ArgumentNullException("obj");
			if(sp.Value == null)
				throw new ArgumentNullException("sp.Value");
			if(!(sp.Value is string))
				throw new ArgumentException("sp.Value should be a type of string");
			string spv = sp.Value as string;
			if(string.IsNullOrEmpty(spv))
				throw new ArgumentException("sp.Value should not be null or empty");
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
				val = Enum.Parse(pi.PropertyType, spv.ToLower());
			}
			
			if(val == null)
				throw new FormatException(String.Format(CultureInfo.InvariantCulture,
				                                        "Could not find a suitable value for {0} in Enum type {1}",
				                                        spv.ToLower(),
				                                        pi.PropertyType.Name));
			pi.SetValue(obj, val, null);//HACK the ToLower may not work in all cases
		}
		
		/// <summary>
		/// Maps a STEP integer to a .Net System.Int32
		/// </summary>
		/// <param name="pi"></param>
		/// <param name="obj"></param>
		/// <param name="sp"></param>
		private void mapInteger(PropertyInfo pi, ref Object obj, StepProperty sp){
			if(pi == null)
				throw new ArgumentNullException("pi");
			if(obj == null)
				throw new ArgumentNullException("obj");
			if(!(sp.Value is int))
				throw new ArgumentException("sp.Value cannot be cast to an int");
			pi.SetValue(obj, (int)sp.Value, null);
		}
		
		/// <summary>
		/// Maps a STEP float to .Net System.Double
		/// </summary>
		/// <param name="pi"></param>
		/// <param name="obj"></param>
		/// <param name="sp"></param>
		private void mapFloat(PropertyInfo pi, ref Object obj, StepProperty sp){
			if(pi == null)
				throw new ArgumentNullException("pi");
			if(obj == null)
				throw new ArgumentNullException("obj");
			if(!(sp.Value is double))
				throw new ArgumentException("sp.Value cannot be cast to a double");
			pi.SetValue(obj, (double)sp.Value, null);
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="pi">The property of the entity which holds an array</param>
		/// <param name="obj">The entity as currently constructed</param>
		/// <param name="sp">The value to be entered into the object</param>
		/// <param name="stepId">the number of the object as given in the step format being deserialized</param>
		private void mapArray(PropertyInfo pi, ref Object obj, StepProperty sp, int stepId){
			if(pi == null)
				throw new ArgumentNullException("pi");
			if(obj == null)
				throw new ArgumentNullException("obj");
			if(sp.Value == null)
				throw new ArgumentNullException("sp.Value");
			
			//due to a quirk in the schema,
			//the property will have a PropertyType which is not
			//directly an array.  It will instead be of another type
			//which wraps an array.  It is not always clear then which property
			//in the wrapping type holds the array of values
			//the findArrayProperty attempts to seek this out
			PropertyInfo arrayProperty = findArrayProperty( pi );
			if(arrayProperty == null)
				throw new InvalidCastException(String.Format(CultureInfo.InvariantCulture,
				                                             "Cannot find a suitable property in the array wrapping type {0} which would hold an array",
				                                             pi.PropertyType.Name));
			Object arrayWrappingObject = System.Activator.CreateInstance(pi.PropertyType);
			IList<StepProperty> itemProperties = (IList<StepProperty>)sp.Value;
			
			if(itemProperties == null)
				throw new NullReferenceException("sp.Value cannot be converted to a list of STEP properties");
			
			logger.Debug("Number of items : " + itemProperties.Count); //HACK debugging only
			
			//insert the array if it doesn't exist
			Array array = (Array)arrayProperty.GetValue(arrayWrappingObject, null);
			if(array == null){
				logger.Debug("Attempting to create an array of type " + arrayProperty.PropertyType.GetElementType().Name
				             + " for property " + arrayProperty.Name + " of type " + pi.PropertyType);
				array = Array.CreateInstance( arrayProperty.PropertyType.GetElementType(), itemProperties.Count );
			}
			if(array.Length < itemProperties.Count)
				throw new FormatException("The array length is not long enough to hold all the properties being mapped to it");
			
			//iterate through each item and add it to the array.
			int arrayIndex = -1;
			foreach(StepProperty spInner in itemProperties){
				arrayIndex++;
				
				//debugging
				logger.Debug("Mapping property in array. Index : " + arrayIndex);
				logger.Debug("Property being assigned to in the .Net object : "       + arrayProperty.Name);
				logger.Debug("The Type of values held by that .Net property : " + arrayProperty.PropertyType.GetElementType().Name);
				logger.Debug("STEP token     : "     + spInner.Token);
				logger.Debug("STEP value     : "     + spInner.Value);
				logger.Debug("STEP valueType : " + spInner.ValueType);
				
				switch(spInner.Token){
					case StepToken.String:
						array.SetValue((string)spInner.Value, arrayIndex);
						continue;
					case StepToken.Float:
					case StepToken.Integer:
						//TODO more error checking required
						MethodInfo parseMethod = array.GetType().GetElementType().GetMethod("Parse");
						if(parseMethod == null)
							throw new NotImplementedException("Cannot convert from a " + spInner.Token + " to a type of " + array.GetType().GetElementType().Name + ", as there is no static Parse(object) method present on the type we're casting to");
						Object parsedValue = parseMethod.Invoke(null, new object[]{spInner.Value});
						array.SetValue(parsedValue, arrayIndex);
						continue;
					case StepToken.LineReference:
						storeLineReference(pi, ref obj, stepId, spInner, arrayIndex);
						continue;
					default:
						throw new NotImplementedException(spInner.Token + " is not yet implemented in mapArray");
				}
			}
			
			//now add the array to the array wrapping object
			arrayProperty.SetValue(arrayWrappingObject, array, null);
			//and the array wrapping object to the entity
			pi.SetValue(obj, arrayWrappingObject, null);
		}
		
		/// <summary>
		/// In the schema, arrays are wrapped in objects.
		/// Within these wrapping objects it is not always obvious as to which property should hold the array.
		/// This method attempts to seek out that property.
		/// </summary>
		/// <param name="pi"></param>
		/// <returns></returns>
		private PropertyInfo findArrayProperty(PropertyInfo pi){
			//HACK horrible, horrible code follows:
			PropertyInfo arrayProperty = pi.PropertyType.GetProperty("Items");
			if(arrayProperty != null)
				return arrayProperty;
			PropertyInfo itemTypeField = pi.PropertyType.GetProperty("itemType");
			if(itemTypeField == null)
				throw new ApplicationException("Could not find a suitable array property in which to map an array to");
			
			Object o = Activator.CreateInstance(pi.PropertyType);
			Object arrFieldName = itemTypeField.GetValue(o, null);
			if(arrFieldName == null)
				throw new ApplicationException("Could not find a suitable array property in which to map an array to");
			String arrayFieldName = arrFieldName as String;
			if(String.IsNullOrEmpty(arrayFieldName))
				throw new ApplicationException("Could not find a suitable array property in which to map an array to");
			int colonPos = arrayFieldName.IndexOf(':');
			if(colonPos != -1)
				arrayFieldName = arrayFieldName.Substring(colonPos + 1);
			arrayFieldName = arrayFieldName.Replace("-", String.Empty);
			logger.Debug("searching for property named : " + arrayFieldName);
			
			return pi.PropertyType.GetProperty(arrayFieldName);
		}
		#endregion
		
		private void storeLineReference(PropertyInfo pi, ref Object obj, int stepId, StepProperty sp){
			this.objectLinks.Add(new ObjectReferenceLink(stepId, pi, (int)sp.Value));
		}
		
		private void storeLineReference(PropertyInfo pi, ref Object obj, int stepId, StepProperty sp, int index){
			this.objectLinks.Add(new ObjectReferenceLink(stepId, pi, (int)sp.Value, index));
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
		
		#region Internal Structures
		/// <summary>
		/// A struct which holds information about the references between entities
		/// e.g. #20, when found within an entity declaration, is a reference to the entity in line 20.
		/// </summary>
		private struct ObjectReferenceLink{
			int referencingObject;
			PropertyInfo referencingProperty;
			int referencedObject;
			int index;
			bool isIndexed;
			
			/// <summary>
			/// The object which makes reference to another line
			/// </summary>
			public int ReferencingObject{
				get{ return this.referencingObject; }
			}
			
			/// <summary>
			/// The property which is to be filled by the referenced object
			/// </summary>
			public PropertyInfo Property{
				get{ return this.referencingProperty; }
			}
			
			/// <summary>
			/// The external object which is referenced
			/// </summary>
			public int ReferencedObject{
				get{ return this.referencedObject; }
			}
			
			/// <summary>
			/// If the property is an array, then an index is required for each individual value
			/// </summary>
			public int Index{
				get{
					if(!this.isIndexed)
						throw new ApplicationException("Do not call Index if IsIndexed is false");
					return this.index;
				}
			}
			
			public bool IsIndexed{
				get{ return this.isIndexed; }
			}
			
			/// <summary>
			/// 
			/// </summary>
			/// <param name="referencingId"></param>
			/// <param name="prop"></param>
			/// <param name="referencedId"></param>
			public ObjectReferenceLink(int referencingId, PropertyInfo prop, int referencedId){
				this.referencingObject = referencingId;
				this.referencedObject = referencedId;
				if(prop == null)
					throw new ArgumentNullException("prop");
				this.referencingProperty = prop;
				this.index = -1;
				this.isIndexed = false;
			}
			
			/// <summary>
			/// 
			/// </summary>
			/// <param name="referencingId"></param>
			/// <param name="prop"></param>
			/// <param name="referencedId"></param>
			/// <param name="indx"></param>
			public ObjectReferenceLink(int referencingId, PropertyInfo prop, int referencedId, int indx){
				this.referencingObject = referencingId;
				this.referencedObject = referencedId;
				if(prop == null)
					throw new ArgumentNullException("prop");
				this.referencingProperty = prop;
				this.index = indx;
				this.isIndexed = true;
			}
		}
		#endregion
	}
}
