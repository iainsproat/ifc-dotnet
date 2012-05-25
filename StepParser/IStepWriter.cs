/*
 * Created by Iain Sproat
 * Date: 24/05/2012
 * Time: 19:53
 * 
 */
using System;

namespace StepParser
{
	/// <summary>
	/// Writes a STEP object representation to a stream, or other output
	/// </summary>
	public interface IStepWriter  : IDisposable
	{
		void WriteStartStep();
		void WriteStartHeader();
		void WriteStartData();
		
		void WriteLineIdentifier(int entityId);
		void WriteObjectName(string objectName);
		void WriteStartObject();
		
		void WriteOverridden();
		void WriteNull();
		void WriteStartArray();
		void WriteEndArray();
		
		void WriteEnum(string value);
		void WriteLineReference(int lineReference);
		
		void WriteValue(double value);
		void WriteValue(int value);
		void WriteValue(long value);
		void WriteValue(short value);
		void WriteValue(string value);
		
		void WriteBool(bool value);
		
		
		void WriteEndObject();
		void WriteEndSection();
		void WriteEndStep();
		
		void Close();
	}
}
