/*
 * Created by Iain Sproat
 * Date: 23/05/2012
 * Time: 20:35
 * 
 */
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml.Serialization;
using System.Globalization;

using log4net;

using IfcDotNet.Schema;

namespace IfcDotNet.StepSerializer.Utilities
{
	/// <summary>
	/// This caches all the properties for the schema
	/// </summary>
	public class StepBinderCache
	{
		private static ILog logger = LogManager.GetLogger(typeof(StepBinderCache));
		
		private bool created = false;
		private IDictionary<string, Type> _entitiesMappedToUpperCaseName = new Dictionary<string, Type>();
		private IDictionary<string, IList<PropertyInfo>> _entityProperties = new Dictionary<string, IList<PropertyInfo>>();
		
		
		/// <summary>
		/// 
		/// </summary>
		public StepBinderCache()
		{
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public Type getTypeOfEntity(String name){
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
			return t;
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="entityType"></param>
		/// <returns></returns>
		public IList<PropertyInfo> getPropertiesOfEntity(Type entityType){
			return this.getPropertiesOfEntity(entityType.FullName);
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="entityTypeFullName"></param>
		/// <returns></returns>
		public IList<PropertyInfo> getPropertiesOfEntity(String entityTypeFullName){
			return this._entityProperties[entityTypeFullName];
		}
		
		/// <summary>
		/// 
		/// </summary>
		public void Create(){
			if(created){
				throw new ApplicationException("Attempting to create a cache that has already been created");
			}
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
				IDictionary<PropertyInfo, int> stepPropertyAttributeOrders = new Dictionary<PropertyInfo, int>();
				foreach(PropertyInfo pi in properties){
					if(IsEntityProperty(pi)){ //filter out all the entity properties (these are a required for IfcXml format only, and are not relevant to STEP format)
						continue;
					}
					if(IsIgnorableProperty(pi)){
						continue;
					}
					
					
					PropertyInfo tempPi = pi;
					tempPi = filterOutXmlHackForValueTypes(t, pi);
					if(isItemPropertyOfAnArrayType(t, pi)){
						continue;
					}
						
					int orderNo = GetStepPropertyOrderNumber(tempPi);
					if(orderNo != -1){
						stepPropertyAttributeOrders.Add(tempPi, orderNo);
					}else{
						cachedProperties.Add(tempPi);
					}
				}
				
				foreach(KeyValuePair<PropertyInfo, int> kvp in stepPropertyAttributeOrders){
					cachedProperties.Insert(kvp.Value, kvp.Key);
				}
				
				this._entityProperties.Add(t.FullName, cachedProperties);
			}
			created = true;
		}

		
		/// <summary>
		/// Determines if a property is defined in the xml specification only, and therefore not relevant to the STEP specification
		/// </summary>
		/// <param name="pi"></param>
		/// <returns></returns>
		private bool IsEntityProperty(PropertyInfo pi){
			if(pi == null)
				return false;

			switch(pi.Name){
				case "id":
				case "path":
					
				//entity property names
				case "href":
				case "ref":
				case "proxy":
				case "edo":
				case "entityid":
				case "entitypath":
				case "pos":
					
				//xml array property names
				case "cType":
				case "itemType":
				case "arraySize":
					return true;
				default:
					return false;
			}
		}
		
		/// <summary>
		/// Determines whether a property has an XmlIgnoreAttribute on it
		/// </summary>
		/// <param name="pi"></param>
		/// <returns></returns>
		private bool IsIgnorableProperty(PropertyInfo pi)
		{
			try{
				return pi.GetCustomAttributes(typeof(XmlIgnoreAttribute), true).Length > 0;
			}catch{
				return false;
			}
		}
		
		/// <summary>
		/// </summary>
		/// <param name="pi"></param>
		/// <returns>Returns -1 if no attribute or order number was found</returns>
		private int GetStepPropertyOrderNumber(PropertyInfo pi){
			if(pi == null) throw new ArgumentNullException("pi");
			object[] stepPropertyAttributes = pi.GetCustomAttributes(typeof(StepPropertyAttribute), true);
			if(stepPropertyAttributes == null) return -1;
			foreach(object o in stepPropertyAttributes){
				StepPropertyAttribute stepPropAtt = o as StepPropertyAttribute;
				if(stepPropAtt.Order != -1)
					return stepPropAtt.Order;
			}
			return -1;
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="t"></param>
		/// <param name="pi"></param>
		/// <returns></returns>
		private PropertyInfo filterOutXmlHackForValueTypes(Type t, PropertyInfo pi){
			if(pi.Name == "ValueAsString"){
				PropertyInfo tempPi = t.GetProperty("Value", BindingFlags.DeclaredOnly | 
                                                  BindingFlags.Public |
                                                  BindingFlags.Instance);
				
				if(tempPi != null){
					return tempPi;
				}
			}
			return pi;
		}
		
		/// <summary>
		/// For a type with the property Item, it checks whether there exists
		/// the property Items.
		/// If so, it indicates that this is an Array type and the Item property is to be ignored.
		/// </summary>
		/// <param name="t"></param>
		/// <param name="pi"></param>
		/// <returns></returns>
		private bool isItemPropertyOfAnArrayType(Type t, PropertyInfo pi){
			if(t.IsSubclassOf(typeof(IArray)) && pi.Name == "Item"){
				PropertyInfo itemsPi = t.GetProperty("Items", BindingFlags.DeclaredOnly |
				                                    BindingFlags.Public |
				                                    BindingFlags.Instance);
				
				if(itemsPi != null)
					return true;
			}
			return false;
		}
	}
}
