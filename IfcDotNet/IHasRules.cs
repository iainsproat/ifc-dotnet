
using System;
using System.Reflection;
using System.Collections.Generic;

namespace IfcDotNet
{
	/// <summary>
	/// A rule is a method which checks a property for conformance
	/// </summary>
	public delegate bool Rule(ref RuleEventArgs e);
	
	/// <summary>
	/// A rule delegate coordinates the process of checking rules.
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
		protected readonly IList<Rule> rules = new List<Rule>();
		
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
			OnBeginRulesEvaluation(this);
			
			if(rules == null){
				OnEndRulesEvaluation(this);
				return;
			}
			
			foreach(Rule rule in rules){
				RuleEventArgs args = new RuleEventArgs();
				if(rule(ref args))
					OnRulePassed(this, args);
				else
					OnRuleFailed(this, args);
			}
			
			OnEndRulesEvaluation(this);
		}
		
		/// <summary>
		/// Registers individual rules of this class in the ruleset.
		/// </summary>
		public abstract void RegisterRules();
	}
	
	/// <summary>
	/// Captures information about a rule which has just run
	/// </summary>
	public class RuleEventArgs : EventArgs
	{
		private string _name;
		private string _message;
		private readonly IList<PropertyInfo> _props = new List<PropertyInfo>();
		
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
		/// <param name="failureMessage"></param>
		public RuleEventArgs(string ruleName, string failureMessage):this(ruleName, failureMessage, null)
		{}
		
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="ruleName"></param>
		/// <param name="failureMessage"></param>
		/// <param name="props"></param>
		public RuleEventArgs(string ruleName, string failureMessage, params PropertyInfo[] props){
			this._name = ruleName;
			this._message = failureMessage;
			if(props != null){
				foreach(PropertyInfo prop in props){
					this._props.Add(prop);
				}
			}
		}
		
		/// <summary>
		/// The name of the rule which has just run
		/// </summary>
		public string RuleName{
			get{ return this._name; }
			set{ this._name = value; }
		}
		
		/// <summary>
		/// The failure message of this rule
		/// </summary>
		public string Message{
			get{ return this._message; }
			set{ this._message = value; }
		}
		
		/// <summary>
		/// The properties which were checked by this rule
		/// </summary>
		public IList<PropertyInfo> Properties{
			get{ return this._props; }
		}
	}
}
