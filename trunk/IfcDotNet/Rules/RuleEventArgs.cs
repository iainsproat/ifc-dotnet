
using System;

namespace IfcDotNet.Rules
{
	/// <summary>
	/// Captures information about a rule which has just run
	/// </summary>
	public class RuleEventArgs : EventArgs
	{
		private string _name = string.Empty;
		private string _description = string.Empty;
		private object _evaluatedObj = null;
		
		/// <summary>
		/// Default constructor
		/// </summary>
		public RuleEventArgs(){}
		
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="ruleName"></param>
		/// <param name="evaluatedObj"></param>
		public RuleEventArgs(string ruleName, object evaluatedObj):this(ruleName, evaluatedObj, string.Empty)
		{}
		
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="ruleName"></param>
		/// <param name="evaluatedObj"></param>
		/// <param name="description"></param>
		public RuleEventArgs(string ruleName, object evaluatedObj, string description)
		{
			if(String.IsNullOrEmpty(ruleName))
				throw new ArgumentNullException("ruleName");
			if(evaluatedObj == null)
				throw new ArgumentNullException("evaluatedObj");
			
			this._name = ruleName;
			this._evaluatedObj = evaluatedObj;
			this._description = description;
		}
		
		/// <summary>
		/// Constructor. Copies the data from a RuleAttribute.
		/// </summary>
		/// <param name="attribute"></param>
		/// <param name="evaluatedObj"></param>
		public RuleEventArgs(RuleAttribute attribute, object evaluatedObj){
			if(attribute != null){
				this._name = attribute.Name;
				this._description = attribute.Description;
			}
			this._evaluatedObj = evaluatedObj;
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
		
		/// <summary>
		/// The object on which this rule was run
		/// </summary>
		public object EvaluatedObject{
			get{ return this._evaluatedObj; }
		}
	}
}
