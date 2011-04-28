
using System;

namespace IfcDotNet
{
	/// <summary>
	/// Each method should be equivalent to one Ifc rule
	/// Should be attached to a parameterless method which returns a boolean.
	/// Only one RuleAttribute is allowed per method.
	/// </summary>
	[AttributeUsage(AttributeTargets.Method, AllowMultiple=false, Inherited = true)]
	public class RuleAttribute : Attribute
	{
		private string ruleName = string.Empty;
		private string ruleDescription = string.Empty;
		private Type ruleType = null;
		//private readonly IList<PropertyInfo> rulePropertiesChecked = new List<PropertyInfo>();
		
		/// <summary>
		/// The name of the rule
		/// </summary>
		public string Name{
			get{ return this.ruleName; }
		}
		
		/// <summary>
		/// A description of the rule, indicating how it may be passed and possible reasons for failure.
		/// </summary>
		public string Description{
			get{ return this.ruleDescription; }
		}
		
		/// <summary>
		/// The type on which this rule applies
		/// </summary>
		public Type Type{
			get{ return this.ruleType; }
		}
		
		/// <summary>
		/// .ctor
		/// </summary>
		/// <param name="name"></param>
		/// <param name="applicableType"></param>
		/// <param name="description"></param>
		public RuleAttribute(string name, Type applicableType, string description){
			this.ruleName = name;
			this.ruleType = applicableType;
			this.ruleDescription = description;
		}
	}
}
