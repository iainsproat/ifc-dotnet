
using System;
using System.Reflection;
using System.Collections.Generic;

using IfcDotNet;
using IfcDotNet.Rules;
using IfcDotNet.Schema;

using NUnit.Framework;

using log4net;
using log4net.Config;

namespace IfcDotNet_UnitTests
{
	
	[TestFixture]
	public class TestRules
	{
		private static readonly ILog logger = LogManager.GetLogger(typeof(TestIfcStepSerializer));
		private readonly Stack<string> calledRules = new Stack<string>();
		IRuleEngine SUT;
		
		[SetUp]
		public void SetUp()
		{
			BasicConfigurator.Configure();
			calledRules.Clear();
			SUT = new RuleEngine();
		}
		

		
		[Test]
		public void RuleEngineEvaluateRules(){
			IfcPositiveRatioMeasure1 ratio = 2;
			
			SUT.RulePassed += new RuleDelegate( HandlePassedRule );
			SUT.RuleFailed += new RuleDelegate( HandleFailedRule );
			
			SUT.InvokeRules(ratio);
			
			Assert.AreEqual(1, calledRules.Count);
			Assert.AreEqual("WR1 passed", calledRules.Pop());
			
			ratio = -1;
			SUT.InvokeRules(ratio);
			
			Assert.AreEqual(1, calledRules.Count);
			Assert.AreEqual("WR1 failed", calledRules.Pop());
		}
		
		public void HandleFailedRule(object sender, RuleEventArgs args){
			logger.Debug("Handle Failed Rule was called");
			this.calledRules.Push(args.RuleName + " failed");
		}
		
		public void HandlePassedRule(object sender, RuleEventArgs args){
			logger.Debug("Handle Passed Rule was called");
			this.calledRules.Push(args.RuleName + " passed");
		}
	}
}
