/*
 * Created by Iain Sproat
 * Date: 24/05/2012
 * Time: 13:05
 * 
 */
using System;

namespace StepParser
{
	/// <summary>
	/// Description of IStepReader.
	/// </summary>
	public interface IStepReader : IDisposable
	{
		StepToken TokenType{ get; }
		Type ValueType{ get; }
		object Value{ get; }
		bool Read();
		void Close();
	}
}
