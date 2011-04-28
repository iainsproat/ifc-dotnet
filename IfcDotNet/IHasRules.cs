
using System;
using System.Globalization;
using System.Reflection;
using System.Collections.Generic;

using log4net;

namespace IfcDotNet
{	
	/// <summary>
	/// A rule delegate handles the change of state, including dealing with failing rules, when rules are being checked.
	/// </summary>
	public delegate void RuleDelegate(object sender, RuleEventArgs e);
	
	/// <summary>
	/// A class which has rules that act on one or more properties of this class, independent of any other classes.
	/// </summary>
	public interface IHasRules
	{
		/// <summary>
		/// Fired when an individual rule in the ruleset is evaluated and passes
		/// </summary>
		event RuleDelegate RulePassed;
		/// <summary>
		/// Fired when an individual rule in the ruleset is evaluated and fails
		/// </summary>
		event RuleDelegate RuleFailed;
		/// <summary>
		/// Fired when a ruleset begins to be evaluated
		/// </summary>
		event RuleDelegate BeginRulesEvaluation;
		/// <summary>
		/// Fired when an entire ruleset finishes evaluation.
		/// </summary>
		event RuleDelegate EndRulesEvaluation;
		/// <summary>
		/// Checks all rules in this classes ruleset
		/// </summary>
		void InvokeRules();
	}
	
	
	/// <summary>
	/// Base class for ensuring that IHasRules works correctly.
	/// Implementation of RegisterRules is left to the concrete class.
	/// </summary>
	public abstract class HasRulesBase : IHasRules
	{
		private static readonly ILog logger = LogManager.GetLogger(typeof(HasRulesBase));
		
		/// <summary>
		/// Fired when a rule is evaluated and passes
		/// </summary>
		public event RuleDelegate RulePassed;
		/// <summary>
		/// Fired when a rule is evaluated and fails
		/// </summary>
		public event RuleDelegate RuleFailed;
		/// <summary>
		/// Fired when a ruleset begins to be evaluated
		/// </summary>
		public event RuleDelegate BeginRulesEvaluation;
		/// <summary>
		/// Fired when an entire ruleset finishes evaluation.
		/// </summary>
		public event RuleDelegate EndRulesEvaluation;
		
		/// <summary>
		/// List of all rules associated with this class (the ruleset)
		/// </summary>
		private static readonly IList<MethodInfo> rules = new List<MethodInfo>();
		private static bool haveRegisteredRules = false;
		
		/// <summary>
		/// Called when rules are begun to be evaluated.
		/// </summary>
		/// <param name="sender"></param>
		private void OnBeginRulesEvaluation(object sender)
		{
			if(BeginRulesEvaluation != null)
				BeginRulesEvaluation(sender, null);
		}

		/// <summary>
		/// Called when all rules have been evaluated.
		/// </summary>
		/// <param name="sender"></param>
		private void OnEndRulesEvaluation(object sender)
		{
			if(EndRulesEvaluation != null)
				EndRulesEvaluation( sender, null);
		}
		
		/// <summary>
		/// Called when a rule passes
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="args"></param>
		private void OnRulePassed(object sender, RuleEventArgs args)
		{
			if(RulePassed != null)
				RulePassed( sender, args );
		}
		
		/// <summary>
		/// Called when a rule fails
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="args"></param>
		private void OnRuleFailed(object sender, RuleEventArgs args)
		{
			if(RuleFailed != null)
				RuleFailed( sender, args );
		}
		
		/// <summary>
		/// Checks all rules in this classes ruleset
		/// </summary>
		public void InvokeRules(){
			if(!haveRegisteredRules)
				RegisterRules();
			
			OnBeginRulesEvaluation(this);
			
			if(rules == null){
				OnEndRulesEvaluation(this);
				return;
			}
			
			foreach(MethodInfo ruleMethod in rules){

				RuleEventArgs args;
				object[] ruleAttributes = ruleMethod.GetCustomAttributes(typeof(RuleAttribute), false);
				if(ruleAttributes != null && ruleAttributes.Length == 1){
					RuleAttribute att = ruleAttributes[0] as RuleAttribute;
					args = new RuleEventArgs(att);
				}else{
					args = new RuleEventArgs();
				}
				
				object ruleResponse = ruleMethod.Invoke(this, null);
				bool rulePassed = (bool)ruleResponse;
				if(rulePassed)
					OnRulePassed(this, args);
				else
					OnRuleFailed(this, args);
			}
			
			OnEndRulesEvaluation(this);
		}
		
		/// <summary>
		/// Constructor. Initiates Registering of rules.
		/// Must be called by all derived classes
		/// </summary>
		protected HasRulesBase(){
			RegisterRules();
		}
		
		/// <summary>
		/// Registers individual rules of this class in the ruleset.
		/// </summary>
		private void RegisterRules(){
			if(haveRegisteredRules) //don't register rules if we've done it before
				return;
			
			MethodInfo[] methods = this.GetType().GetMethods();
			foreach(MethodInfo method in methods){
				//rule methods should have no parameters and return a type of Boolean
				if(method.GetParameters().Length > 0)
					continue;
				if(!method.ReturnType.Equals(typeof(Boolean)))
					continue;
				
				object[] ruleAttributes = method.GetCustomAttributes(typeof(RuleAttribute), false);
				if(ruleAttributes != null && ruleAttributes.Length == 1){
					
					if(!rules.Contains(method)){
						rules.Add(method);
					}
				}
			}
			haveRegisteredRules = true;
		}
		
		#if DEBUG
		/// <summary>
		/// For testing purposes only
		/// </summary>
		public IList<MethodInfo> Rules{
			get{ return rules; }
		}
		#endif
	}
	
	/// <summary>
	/// 
	/// </summary>
	[AttributeUsage(AttributeTargets.Method, AllowMultiple=false, Inherited = true)]
	public class RuleAttribute : Attribute
	{
		private string ruleName = string.Empty;
		private string ruleDescription = string.Empty;
		private readonly IList<PropertyInfo> rulePropertiesChecked = new List<PropertyInfo>();
		
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
		/// .ctor
		/// </summary>
		/// <param name="name"></param>
		/// <param name="description"></param>
		public RuleAttribute(string name, string description){
			this.ruleName = name;
			this.ruleDescription = description;
		}
	}
	
	/// <summary>
	/// Captures information about a rule which has just run
	/// </summary>
	public class RuleEventArgs : EventArgs
	{
		private string _name = string.Empty;
		private string _description = string.Empty;
		
		/// <summary>
		/// Default constructor
		/// </summary>
		public RuleEventArgs(){}
		
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="ruleName"></param>
		public RuleEventArgs(string ruleName):this(ruleName, string.Empty)
		{}
		
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="ruleName"></param>
		/// <param name="description"></param>
		public RuleEventArgs(string ruleName, string description)
		{
			this._name = ruleName;
			this._description = description;
		}
		
		/// <summary>
		/// Constructor. Copies the data from a RuleAttribute.
		/// </summary>
		/// <param name="attribute"></param>
		public RuleEventArgs(RuleAttribute attribute){
			if(attribute != null){
				this._name = attribute.Name;
				this._description = attribute.Description;
			}
		}
		
		
		/// <summary>
		/// The name of the rule which has just run
		/// </summary>
		public string RuleName{
			get{ return this._name; }
		}
		
		/// <summary>
		/// The description of this rule
		/// </summary>
		public string Description{
			get{ return this._description; }
		}
	}
}
