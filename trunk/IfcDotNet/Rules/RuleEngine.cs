
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;

using log4net;

using IfcDotNet.Schema;

namespace IfcDotNet.Rules
{
	/// <summary>
	/// Base class for ensuring that IHasRules works correctly.
	/// Implementation of RegisterRules is left to the concrete class.
	/// </summary>
	public class RuleEngine : IRuleEngine
	{
		private static readonly ILog logger = LogManager.GetLogger(typeof(RuleEngine));
		
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
		/// List of all rules associated with all classes (the ruleset)
		/// </summary>
		private static readonly IDictionary<string,IList<MethodInfo>> rules = new Dictionary<string,IList<MethodInfo>>();
		private static bool haveRegisteredRules = false;
		
		/// <summary>
		/// Called when rules are begun to be evaluated.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="objToEvaluate"></param>
		private void OnBeginRulesEvaluation(object sender, object objToEvaluate)
		{
			//TODO create RuleEventArgs
			if(BeginRulesEvaluation != null)
				BeginRulesEvaluation(sender, null);
		}

		/// <summary>
		/// Called when all rules have been evaluated.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="objToEvaluate"></param>
		private void OnEndRulesEvaluation(object sender, object objToEvaluate)
		{
			//TODO create RuleEventArgs
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
		public void InvokeRules(object objToEvaluate){
			if(objToEvaluate == null) throw new ArgumentNullException("objToEvaluate");
			
			if(!haveRegisteredRules)
				RegisterRules();
			
			OnBeginRulesEvaluation(this, objToEvaluate);
			
			if(rules == null){
				OnEndRulesEvaluation(this, objToEvaluate);
				return;
			}
			
			string type = objToEvaluate.GetType().FullName;
			if(!rules.ContainsKey(type)){
				OnEndRulesEvaluation(this, objToEvaluate);
				logger.Info(String.Format(CultureInfo.InvariantCulture, "No rules exist for type {0}", type));
				return;
			}
			
			IList<MethodInfo> typeRules = rules[type];
			foreach(MethodInfo ruleMethod in typeRules){

				object[] ruleAttributes = ruleMethod.GetCustomAttributes(typeof(RuleAttribute), false);
				if(ruleAttributes != null && ruleAttributes.Length == 1){
					RuleAttribute att = ruleAttributes[0] as RuleAttribute;
					RuleEventArgs args = new RuleEventArgs(att, objToEvaluate);
					
					object ruleResponse = ruleMethod.Invoke(objToEvaluate, null);
					bool rulePassed = (bool)ruleResponse;
					if(rulePassed)
						OnRulePassed(this, args);
					else
						OnRuleFailed(this, args);
				}else{
					throw new InvalidOperationException(String.Format(CultureInfo.InvariantCulture,
					                                                  "All methods in rules must have a single RuleAttribute. The failing method is {0} of type {1}",
					                                                  ruleMethod.Name, type ));
				}
			}
			
			OnEndRulesEvaluation(this, objToEvaluate);
		}
		
		/// <summary>
		/// Constructor. Initiates Registering of rules.
		/// Must be called by all derived classes
		/// </summary>
		public RuleEngine(){
			RegisterRules();
		}
		
		/// <summary>
		/// Registers individual rules of this class in the ruleset.
		/// </summary>
		private void RegisterRules(){
			if(haveRegisteredRules) //don't register rules if we've done it before
				return;
			
			IList<Type> schemaTypes = GetAllTypesWithRulesInSchema();
			foreach(Type t in schemaTypes){
				
				MethodInfo[] methods = t.GetMethods();
				if(methods == null || methods.Length < 1)
					continue;
				
				IList<MethodInfo> typeRules = new List<MethodInfo>(methods.Length);
				foreach(MethodInfo method in methods){
					//rule methods should have no parameters and return a type of Boolean
					if(method.GetParameters().Length > 0)
						continue;
					if(!method.ReturnType.Equals(typeof(Boolean)))
						continue;
					
					object[] ruleAttributes = method.GetCustomAttributes(typeof(RuleAttribute), false);
					if(ruleAttributes != null && ruleAttributes.Length == 1){
						
						if(!typeRules.Contains(method)){
							typeRules.Add(method);
						}
					}
				}
				rules.Add(t.FullName, typeRules);
			}
			haveRegisteredRules = true;
		}
		
		private IList<Type> GetAllTypesWithRulesInSchema(){
			Assembly ass = Assembly.GetAssembly(typeof(iso_10303));
			Type[] allTypes = ass.GetTypes();
			IList<Type> filteredTypes = new List<Type>(allTypes.Length);
			foreach(Type t in allTypes){
				if(rules.ContainsKey(t.FullName))
					continue;
				if(!t.IsClass)
					continue;
				if(t.Namespace == null)
					continue;
				if(!t.Namespace.Equals("IfcDotNet.Schema"))
					continue;
				if(!typeof(IHasRules).IsAssignableFrom(t))
					continue;
				
				filteredTypes.Add(t);
			}
			return filteredTypes;
		}
		
		#if DEBUG
		/// <summary>
		/// For testing purposes only
		/// </summary>
		public IDictionary<string, IList<MethodInfo>> Rules{
			get{ return rules; }
		}
		#endif
	}
}
