/*
 * Created by Iain Sproat
 * Date: 23/05/2012
 * Time: 21:17
 * 
 */
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml.Serialization;
using System.Globalization;

using log4net;

using IfcDotNet.Schema;
using IfcDotNet.StepSerializer.Utilities;

using StepParser;
using StepParser.StepFileRepresentation;

namespace IfcDotNet.StepSerializer
{
	/// <summary>
	/// This extracts the relevant data from an Entity class and
	/// creates a StepDataObject class containing the data
	/// </summary>
	public class StepObjectExtractor
	{
		private static ILog logger = LogManager.GetLogger(typeof(StepObjectExtractor));
		
		private StepBinderCache _cache = new StepBinderCache();
		
		private StepBinderEntityRegister _entityRegister = new StepBinderEntityRegister();
		private Queue<Entity> _queuedEntities = new Queue<Entity>();
		
		/// <summary>
		/// 
		/// </summary>
		public StepObjectExtractor()
		{
			_cache.Create();
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
			stepFile.Header.Add( GenerateFileDescription( ) );
			stepFile.Header.Add( ExtractFileName( iso10303 ) );
			stepFile.Header.Add( ExtractFileSchema( iso10303 ) );
			
			//data
			uos1 uos1 = iso10303.uos as uos1;
			if(uos1 == null){ //no data
				logger.Error( "Extract(iso_10303) could not extract, as iso10303.uos was not a type of uos1" );
				return stepFile;
			}
			
			//putting the entities in a dictionary so we can deal with references
			foreach(Entity e in uos1.Items){
				if(!this._entityRegister.isAlreadyRegistered(e)){
					this._entityRegister.RegisterEntity( e );
					this._queuedEntities.Enqueue( e );
				}
			}
			
			while(this._queuedEntities.Count > 0){
				Entity e = this._queuedEntities.Dequeue();
				int entityId = this._entityRegister.getEntityId(e);
				
				StepDataObject sdo = this.ExtractObject( e );
				stepFile.Data.Add( entityId, sdo );
			}
			
			//clear entityQueue, so next time this method is run it starts empty
			this._entityRegister = new StepBinderEntityRegister();
			
			return stepFile;
		}
		
		internal StepDataObject ExtractObject( Object obj ){
			if(obj == null) throw new ArgumentNullException("entity");
			Type objType = obj.GetType();
			
			StepDataObject sdo = new StepDataObject();
			sdo.ObjectName = GetObjectName( objType );
			
			IList<PropertyInfo> objProps = this._cache.getPropertiesOfEntity(objType);
			foreach(PropertyInfo pi in objProps){
				sdo.Properties.Add( ExtractProperty( obj, pi ) );
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
			                           "Method ExtractProperty(Object, PropertyInfo) called with parameters 'entity', an object of type {0}, and 'pi', a PropertyInfo of name '{1}' and PropertyType {2}",
			                           entity.GetType().FullName, pi.Name, pi.PropertyType));
			
			if(pi.Name == "ValueAsString"){
				throw new StepBindingException("Property extraction has gone wrong - it should not be working with any properties named 'ValueAsString'");
			}
			
			if(IsOverriddenProperty(pi))
				return StepValue.CreateOverridden();
			
			object value = pi.GetValue( entity, null );
			if(value == null){
				return StepValue.CreateNull();
			}
			if(IsIndirectProperty( pi  )){
				
				return ExtractProperty(value, GetIndirectProperty( pi.PropertyType ));//TODO what if this is indexed??
			}
			if(pi.PropertyType == typeof(object)){
				//TODO
				/*
				 * The value should be wrapped as an object
				 * e.g. IFCMEASUREWITHUNIT(1.745E-2, ...
				 * should become
				 * IFCMEASUREWITHUNIT(IFCPLANEANGLEMEASURE(1.745E-2), ...
				 * 
				 * 
				 **/
				logger.Debug(String.Format("\t\tFound an object property. Type of value is {0}", value.GetType()));
				return StepValue.CreateNestedEntity(ExtractObject(value));
			}
			return ExtractPropertyValue( value );
		}
		
		private StepValue ExtractPropertyValue(Object value ){
			if(value == null)
				return StepValue.CreateNull();
			
			if(typeof(Entity).IsAssignableFrom( value.GetType() ))//it's a nested entity, which should be referenced
				return ExtractNestedEntity(value as Entity);
			
			Type typeOfValue = value.GetType();
			if(IsAnonymousType( typeOfValue )){
				if(IsValueType( typeOfValue )){
					return ExtractProperty( value, this._cache.getPropertiesOfEntity(typeOfValue)[0]); //HACK
				}else{
					logger.Debug(String.Format("{0} is not a ValueType", typeOfValue.FullName));
					return ExtractProperty( value, GetIndirectProperty( value.GetType() ));
				}
			}
			
			if(value.GetType().Equals(typeof(string)))
				return StepValue.CreateString((string)value);
			
			if(value.GetType().IsArray){
				Array array = (Array)value;
				IList<StepValue> arrayValues = extractValuesFromArray(array);
				return StepValue.CreateArray(arrayValues);
			}
			
			if(value is IArray){
				Type t = value.GetType();
				
				
				PropertyInfo itemsPi = t.GetProperty("Items", BindingFlags.DeclaredOnly |
				                                     BindingFlags.Public |
				                                     BindingFlags.Instance);
				if(itemsPi == null){
					throw new StepBindingException(
						String.Format("We believe to have located a type, {0}, which derives from IArray but does not implement the Items property",
						              t.FullName));
				}
				Array array = (Array)itemsPi.GetValue(value, null);
				
				IList<StepValue> arrayValues = extractValuesFromArray(array);
				return StepValue.CreateArray(arrayValues);
			}
			
			if(value.GetType().IsEnum){
				return StepValue.CreateEnum(String.Format(CultureInfo.InvariantCulture,
				                                                          "{0}",
				                                                          value.ToString().ToUpper()));
			}
			
			if(value.GetType().IsPrimitive){
				switch(value.GetType().FullName){//HACK, there must be a better way of
					case "System.Boolean":
						return StepValue.CreateBoolean((bool)value);
					case "System.Single":
						return StepValue.CreateFloat((Single)value);
					case "System.Double":
						return StepValue.CreateFloat((double)value);
					case "System.Int16":
						return StepValue.CreateInteger((System.Int16)value);
					case "System.Int32":
						return StepValue.CreateInteger((System.Int32)value);
					case "System.Int64":
						return StepValue.CreateInteger((System.Int64)value);
					default:
						throw new NotImplementedException(String.Format(CultureInfo.InvariantCulture,
						                                                "ExtractProperty method has not yet implemented for a primitive of type {0}",
						                                                value.GetType().FullName));
				}
			}
			
			//nested objects
			return StepValue.CreateNestedEntity(this.ExtractObject(value));
		}
		
		/// <summary>
		/// Determines whether the Overridden property of a StepPropertyAttribute is set to true
		/// </summary>
		/// <param name="pi"></param>
		/// <returns></returns>
		private bool IsOverriddenProperty( PropertyInfo pi ){
			if(pi == null) throw new ArgumentNullException("pi");
			object[] stepPropertyAttributes = pi.GetCustomAttributes(typeof(StepPropertyAttribute), false);
			if(stepPropertyAttributes == null || stepPropertyAttributes.Length < 1) return false;
			StepPropertyAttribute spa = stepPropertyAttributes[0] as StepPropertyAttribute;
			if(spa == null) return false;
			return spa.Overridden;
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
			
			//check for the presence of an XmlTypeAttribute(AnonymousType = true) flag.
			object[] xmlTypeAttributes = t.GetCustomAttributes(typeof(XmlTypeAttribute), false);
			if(xmlTypeAttributes != null && xmlTypeAttributes.Length >0){
				XmlTypeAttribute xmlTypeAtt = xmlTypeAttributes[0] as XmlTypeAttribute;
				if(xmlTypeAtt != null && xmlTypeAtt.AnonymousType) return true;
			}
			return false;
			//else it may be an aggregate type.//UNDONE
			//PropertyInfo cTypeProp = t.GetProperty("cType");//HACK assuming all types with a cType property are wrappers for arrays!
			//return cTypeProp != null;
		}
		
		private bool IsValueType( Type t ){
			if(t == null) throw new ArgumentNullException("t");
			
			return t.GetInterface(typeof(IValueTypeBase).FullName) != null;
		}
		
		private PropertyInfo GetIndirectProperty( Type t ){
			if(t == null) throw new ArgumentNullException("t");
			
			PropertyInfo[] propertiesOnIndirectType = t.GetProperties();
			foreach(PropertyInfo propertyOnIndirectType in propertiesOnIndirectType){
				object[] xmlElementAttributes = propertyOnIndirectType.GetCustomAttributes(typeof(XmlElementAttribute), true);
				if(xmlElementAttributes != null && xmlElementAttributes.Length > 0)
					return propertyOnIndirectType;
				
				object[] xmlTextAttributes = propertyOnIndirectType.GetCustomAttributes(typeof(XmlTextAttribute), true);
				if(xmlTextAttributes != null && xmlTextAttributes.Length > 0)
					return propertyOnIndirectType;
				//TODO what if this itself is indirect? (multiple layers of indirection?)
			}
			
			throw new StepBindingException(String.Format(CultureInfo.InvariantCulture,
			                                                "GetIndirectProperty could not find a property with an XmlElementAttribute in type {0}",
			                                                t.FullName));
		}
		
		private IList<StepValue> extractValuesFromArray(Array array){
			IList<StepValue> arrayValues = new List<StepValue>(array.Length);
			for(int i = 0; i < array.Length; i++){
				object o = array.GetValue( i );
				if(o == null)
					logger.Debug(String.Format("Found null object at index {0} in array ", i));
				else
					logger.Debug(String.Format("Found in array at index {0} an object {1} of type {2}", i, o.ToString(), o.GetType()));
				arrayValues.Add( ExtractPropertyValue( o ) );
			}
			return arrayValues;
		}
		
		private StepValue ExtractNestedEntity(Entity value){
			int nestedEntityId = this._entityRegister.getEntityId( value );
			return StepValue.CreateLineReference(nestedEntityId);
		}
		
		/// <summary>
		/// Creates the file description step data object.
		/// This is the same for all STEP files.
		/// </summary>
		/// <returns></returns>
		private StepDataObject GenerateFileDescription( ){
			return new StepDataObject("FILE_DESCRIPTION",
			                                        StepValue.CreateArray(
			                                                              StepValue.CreateString("ViewDefinition [CoordinationView, QuantityTakeOffAddOnView]")
			                                                             ),
			                                       StepValue.CreateString("2;1")
			                                      );
		}
		
		private StepDataObject ExtractFileName( iso_10303 iso10303 ){
			if(iso10303 == null) throw new ArgumentNullException("iso10303");
			iso_10303_28_header header = iso10303.iso_10303_28_header;
			if(header == null) throw new ArgumentNullException("iso10303.iso_10303_28_header");
			StepDataObject sdo = new StepDataObject();
			sdo.ObjectName = "FILE_NAME";
			
			sdo.Properties.Add( StepValue.CreateString(header.name));
			sdo.Properties.Add( StepValue.CreateDate(header.time_stamp ) );
			
			sdo.Properties.Add( StepValue.CreateArray( StepValue.CreateString( header.author ) ) );
			
			//FIXME header.organization is a string and not a list, but the Step file expects an array
			sdo.Properties.Add( StepValue.CreateArray( StepValue.CreateString( header.organization ) ) );
			
			sdo.Properties.Add( StepValue.CreateString( header.preprocessor_version ));
			sdo.Properties.Add( StepValue.CreateString( header.originating_system ));
			sdo.Properties.Add( StepValue.CreateString( header.authorization ));
			return sdo;
		}
		
		private StepDataObject ExtractFileSchema( iso_10303 iso10303 ){
			if(iso10303 == null) throw new ArgumentNullException( "iso10303" );
			StepDataObject sdo = new StepDataObject();
			sdo.ObjectName = "FILE_SCHEMA";
			sdo.Properties.Add( StepValue.CreateArray( StepValue.CreateString("IFC2X3") ) );
			return sdo;
		}
	}
}
