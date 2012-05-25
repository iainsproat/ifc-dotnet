/*
 * Created by Iain Sproat
 * Date: 24/05/2012
 * Time: 19:12
 * 
 */
using System;
using System.Text;
using System.IO;

using StepParser;
using StepParser.IO;

using NUnit.Framework;

using log4net;
using log4net.Config;

namespace StepParser_UnitTests
{
	/// <summary>
	/// Description of TestStepWriter.
	/// </summary>
	[TestFixture]
	public class TestStepWriter
	{
		StepWriter SUT;
		StringBuilder sb;
		TextWriter sw;
		
		[SetUp]
		public void SetUp(){
			BasicConfigurator.Configure();
			
			sb = new StringBuilder();
			sw = new StringWriter(sb);
			SUT = new StepWriter(sw);
		}
		
		[Test]
		[ExpectedException(typeof(StepWriterException))]
		public void ThrowsIfStepNotWritten(){
			SUT.WriteStartHeader();
		}
		
		[Test]
		[ExpectedException(typeof(StepWriterException))]
		public void ThrowsIfHeaderNotWritten(){
			SUT.WriteStartStep();
			SUT.WriteStartObject();
		}
		
		[Test]
		[ExpectedException(typeof(StepWriterException))]
		public void ThrowsIfDataNotWritten(){
			SUT.WriteStartStep();
			SUT.WriteLineIdentifier(1);
		}
		
		[Test]
		[ExpectedException(typeof(StepWriterException))]
		public void ThrowsIfObjectNotWritten(){
			SUT.WriteStartStep();
			SUT.WriteStartData();
			SUT.WriteValue(1);
		}
		
		[Test]
		public void CanWriteFloatWithoutFloatPart(){
			SUT.WriteStartStep();
			SUT.WriteStartData();
			SUT.WriteStartObject();
			SUT.WriteValue(22.0);
			Assert.AreEqual("ISO-10303-21;\r\nDATA;\r\n(22.", sb.ToString());
		}
		
		[Test]
		public void CanWriteFloat(){
			SUT.WriteStartStep();
			SUT.WriteStartData();
			SUT.WriteStartObject();
			SUT.WriteValue(22.56);
			Assert.AreEqual("ISO-10303-21;\r\nDATA;\r\n(22.56", sb.ToString());
		}
		
		[Test]
		[Explicit]
		public void PrintSimpleWithMockWriter(){
			sw = new MockTextWriter();
			SUT = new StepWriter(sw);
			
			
		}
	}
}
