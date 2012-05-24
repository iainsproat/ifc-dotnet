
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml.Serialization;

using IfcDotNet.Schema;
using IfcDotNet;

using log4net;
using log4net.Config;

using NUnit.Framework;

namespace IfcDotNet_UnitTests
{
	/// <summary>
	/// Some helper methods for querying the schema to determine how much development still needs to be done.
	/// </summary>
	[TestFixture]
	public class Reflection
	{
		private static ILog logger = LogManager.GetLogger(typeof(Reflection));
		Assembly ass;
		Type[] allTypes;
		
		[SetUpAttribute]
		public void SetUp(){
			BasicConfigurator.Configure();
			ass = Assembly.GetAssembly(typeof(iso_10303));
			allTypes = ass.GetTypes();
		}
		
		[Test]
		[Explicit]
		public void RuleTypesWithoutRuleMethods(){
			IList<Type> ruleTypesWithoutRules = new List<Type>(allTypes.Length);
			foreach(Type t in allTypes){
				if(!t.IsClass)
					continue;
				if(t.Namespace == null)
					continue;
				if(!t.Namespace.Equals("IfcDotNet.Schema"))
					continue;
				if(!typeof(IHasRules).IsAssignableFrom(t))
					continue;
				MethodInfo[] methods = t.GetMethods(BindingFlags.Public);
				bool foundRule = false;
				foreach(MethodInfo method in methods){
					object[] attributes = t.GetCustomAttributes(typeof(RuleAttribute), false);
					if(attributes != null && attributes.Length == 1){
						foundRule = true;
						break; //break out of methods loop
					}
				}
				if(!foundRule)
					ruleTypesWithoutRules.Add(t);
			}
			foreach(Type t in ruleTypesWithoutRules)
				logger.Debug(t.FullName);
			logger.Debug(ruleTypesWithoutRules.Count);
		}
		
		[Test]
		[Explicit]
		public void GetAllAnonymousSchemaClasses(){
			IList<Type> filteredTypes = new List<Type>(allTypes.Length);
			foreach(Type t in allTypes){
				if(!t.IsClass)
					continue;
				if(t.Namespace == null)
					continue;
				if(!t.Namespace.Equals("IfcDotNet.Schema"))
					continue;
				object[] attributes = t.GetCustomAttributes(typeof(XmlTypeAttribute), true);
				if(attributes == null || attributes.Length < 1)
					continue;
				foreach(object o in attributes){
					XmlTypeAttribute att = o as XmlTypeAttribute;
					if(att != null){
						if(att.AnonymousType){
							logger.Debug(t.FullName);
							break;
						}
					}
				}
			}
			
		}
		
		[Test]
		[Explicit]
		public void GetAllNonEntityNonAnonymousClasses(){
			IList<Type> filteredTypes = new List<Type>(allTypes.Length);
			IDictionary<Type, PropertyInfo> anonymousTypes = new Dictionary<Type, PropertyInfo>();
			IDictionary<Type, PropertyInfo> valueTypes = new Dictionary<Type, PropertyInfo>();
			IDictionary<Type, PropertyInfo> wrappedArrays = new Dictionary<Type, PropertyInfo>();
			IDictionary<Type, PropertyInfo> doubleValueType= new Dictionary<Type, PropertyInfo>();
			logger.Debug("<<<<<<<<<<< NonEntityAnonymousClasses >>>>>>>>>>>>>");
			foreach(Type t in allTypes){
				if(!t.IsClass)
					continue;
				if(t.Namespace == null)
					continue;
				if(!t.Namespace.Equals("IfcDotNet.Schema"))
					continue;
				if(typeof(Attribute).IsAssignableFrom(t))
					continue;
				if(typeof(Entity).IsAssignableFrom( t ))
					continue;
				if(t.BaseType.IsGenericType)
					continue;
				if(typeof(AnonymousType<,>).IsAssignableFrom(t))
					continue;
				if(typeof(ValueType<,>).IsAssignableFrom(t))
					continue;
				if(typeof(ArrayWrapper<,>).IsAssignableFrom(t))
					continue;
				filteredTypes.Add(t);
				logger.Debug(t.FullName);
				PropertyInfo itemProp = t.GetProperty("Item");
				PropertyInfo valueProp = null;
				try{
					valueProp = t.GetProperty("Value", BindingFlags.Instance | BindingFlags.Public, null, typeof(object), Type.EmptyTypes, null);
				}catch(AmbiguousMatchException ame){
					logger.Debug("Ambiguous match for type : " + t.FullName);
					throw ame;
				}
				if(itemProp != null)
					anonymousTypes.Add(t, itemProp);
				else if(valueProp != null){
					if(valueProp.PropertyType.Equals(typeof(double)))
						doubleValueType.Add(t, valueProp);
					else
						valueTypes.Add(t, valueProp);
				}
				else{
					PropertyInfo[] props = t.GetProperties();
					foreach(PropertyInfo prop in props){
						if(prop.GetCustomAttributes(typeof(XmlElementAttribute), false).Length > 0 && prop.PropertyType.GetElementType() != null){
							wrappedArrays.Add(t, prop);
							break;
						}
					}
				}
			}
			logger.Debug(filteredTypes.Count);
			logger.Debug("------------------anonymousTypes--------------");
			foreach(KeyValuePair<Type, PropertyInfo> kvp in anonymousTypes){
				logger.Debug("public partial class " + kvp.Key.Name + " : AnonymousType<" + kvp.Value.PropertyType.Name + ", " + kvp.Key.Name + ">{}");
			}
			logger.Debug(anonymousTypes.Count);
			logger.Debug("------------------valueTypes------------------");
			foreach(KeyValuePair<Type, PropertyInfo> kvp in valueTypes){
				logger.Debug("public partial class " + kvp.Key.Name + " : ValueType<" + kvp.Value.PropertyType.Name + ", " + kvp.Key.Name + ">{}");
			}
			logger.Debug(valueTypes.Count);
			logger.Debug("------------------DoubleValueTypes------------------");
			foreach(KeyValuePair<Type, PropertyInfo> kvp in doubleValueType){
				logger.Debug("public partial class " + kvp.Key.Name + " : DoubleValueType<" + kvp.Key.Name + ">{}");
			}
			logger.Debug(doubleValueType.Count);
			logger.Debug("------------------arrayWrapper----------------");
			foreach(KeyValuePair<Type, PropertyInfo> kvp in wrappedArrays){
				logger.Debug("public partial class " + kvp.Key.Name + " : ArrayWrapper<" + kvp.Value.PropertyType.GetElementType().Name + ", " + kvp.Key.Name + ">{}");
			}
			logger.Debug(wrappedArrays.Count);
		}
	}
}
