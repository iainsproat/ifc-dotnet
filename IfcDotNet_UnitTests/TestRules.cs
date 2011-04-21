
using System;
using System.Collections.Generic;

using IfcDotNet;

using NUnit.Framework;

using log4net;
using log4net.Config;

namespace IfcDotNet_UnitTests
{
	public class StubClassWithRules : HasRulesBase
	{
		private int number;
		public int Number{
			get{ return this.number; }
			set{ this.number = value; }
		}
		
		public override void RegisterRules()
		{
			this.rules.Add(Rule1);
		}
		
		public bool Rule1(ref RuleEventArgs e){
			e = new RuleEventArgs("Rule1", "Number is not less than 2", this.GetType().GetProperty("Number"));
			return this.Number < 2;
		}
	}
	
	[TestFixture]
	public class TestRules
	{
		private static readonly ILog logger = LogManager.GetLogger(typeof(TestIfcStepSerializer));
		private readonly Stack<string> calledRules = new Stack<string>();
		
		[SetUp]
		public void SetUp()
		{
			BasicConfigurator.Configure();
			calledRules.Clear();
		}
		[Test]
		public void TestMethod()
		{
			StubClassWithRules SUT = new StubClassWithRules();
			SUT.Number = 3;
			Assert.AreEqual(3, SUT.Number);
			
			SUT.RuleFailed += new RuleDelegate(HandleFailedRule);
			SUT.RegisterRules();
			
			SUT.InvokeRules();
			Assert.AreEqual(1, calledRules.Count);
			Assert.AreEqual("Rule1 failed", calledRules.Pop());
			
			Assert.AreEqual(3, SUT.Number);
			SUT.Number = -1;
			Assert.AreEqual(-1, SUT.Number);
			SUT.RulePassed += new RuleDelegate(HandlePassedRule);
			
			SUT.InvokeRules();
			Assert.AreEqual(1, calledRules.Count);
			Assert.AreEqual("Rule1 passed", calledRules.Pop());
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
