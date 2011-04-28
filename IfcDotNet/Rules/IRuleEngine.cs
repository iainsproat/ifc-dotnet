
using System;

namespace IfcDotNet.Rules
{
	/// <summary>
	/// A rule delegate handles the change of state, including dealing with failing rules, when rules are being checked.
	/// </summary>
	public delegate void RuleDelegate(object sender, RuleEventArgs e);
	
	/// <summary>
	/// Responsible for evaluating rules, and notifying listeners of the outcome of evaluations
	/// </summary>
	public interface IRuleEngine
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
		void InvokeRules(object objToEvaluate);
	}
}
