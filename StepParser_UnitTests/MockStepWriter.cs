/*
 * Created by Iain Sproat
 * Date: 24/05/2012
 * Time: 23:28
 * 
 */
using System;

using log4net;

using StepParser;

namespace StepParser_UnitTests
{
	/// <summary>
	/// Description of MockStepWriter.
	/// </summary>
	public class MockStepWriter : IStepWriter
	{
		private static ILog logger = LogManager.GetLogger(typeof(MockStepWriter));
		
		public MockStepWriter()
		{
		}
		
		public void WriteStartStep()
		{
			logger.Debug("WriteStartStep();");
		}
		
		public void WriteStartHeader()
		{
			logger.Debug("WriteStartHeader();");
		}
		
		public void WriteStartData()
		{
			logger.Debug("WriteStartData();");
		}
		
		public void WriteLineIdentifier(int entityId)
		{
			logger.Debug(String.Format("WriteLineIdentifier( {0} );", entityId));
		}
		
		public void WriteObjectName(string objectName)
		{
			logger.Debug(String.Format("WriteObjectName( \"{0}\" );", objectName));
		}
		
		public void WriteStartObject()
		{
			logger.Debug("WriteStartObject();");
		}
		
		public void WriteOverridden()
		{
			logger.Debug("WriteOverridden();");
		}
		
		public void WriteNull()
		{
			logger.Debug("WriteNull();");
		}
		
		public void WriteStartArray()
		{
			logger.Debug("WriteStartArray();");
		}
		
		public void WriteEndArray()
		{
			logger.Debug("WriteEndArray();");
		}
		
		public void WriteEnum(string value)
		{
			logger.Debug(String.Format("WriteEnum( \"{0}\" );", value));
		}
		
		public void WriteLineReference(int lineReference)
		{
			logger.Debug(String.Format("WriteLineReference( {0} );", lineReference));
		}
		
		public void WriteValue(double value)
		{
			logger.Debug(String.Format("WriteValue( {0} );", value));
		}
		
		public void WriteValue(int value)
		{
			logger.Debug(String.Format("WriteValue( {0} );", value));
		}
		
		public void WriteValue(long value)
		{
			logger.Debug(String.Format("WriteValue( {0} );", value));
		}
		
		public void WriteValue(short value)
		{
			logger.Debug(String.Format("WriteValue( {0} );", value));
		}
		
		public void WriteValue(string value)
		{
			logger.Debug(String.Format("WriteValue( \"{0}\" );", value));
		}
		
		public void WriteBool(bool value)
		{
			logger.Debug(String.Format("WriteBool( {0} );", value));
		}
		
		public void WriteEndObject()
		{
			logger.Debug("WriteEndObject();");
		}
		
		public void WriteEndSection()
		{
			logger.Debug("WriteEndSection();");
		}
		
		public void WriteEndStep()
		{
			logger.Debug("WriteEndStep();");
		}
		
		public void Close()
		{
			logger.Debug("Close();");
		}
		
		public void Dispose()
		{
			logger.Debug("Dispose();");
		}
	}
}
