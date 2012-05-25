/*
 * Created by Iain Sproat
 * Date: 24/05/2012
 * Time: 23:38
 * 
 */
using System;
using System.IO;

using StepParser;
using StepParser.IO;

using log4net;

namespace StepParser_UnitTests
{
	/// <summary>
	/// Description of MockTextWriter.
	/// </summary>
	public class MockTextWriter : TextWriter
	{
		private static ILog logger = LogManager.GetLogger(typeof(MockTextWriter));
		private StepWriter _parentWriter;
		
		public MockTextWriter()
		{
			
		}
		
		public void setParent(StepWriter parentWriter){
			this._parentWriter = parentWriter;
		}
		
		public override System.Text.Encoding Encoding {
			get {
				throw new NotImplementedException();
			}
		}
		
		public override void Close()
		{
			logger.Debug("Close();");
		}
		
		public override void Flush(){
			logger.Debug("Flush();");
		}
		
		public override void Write(object value)
		{
			logger.Debug(String.Format("Write( {0} ); //object.  Current WriteState: {1}", value, this._parentWriter.WriteState));
		}
		
		public override void Write(bool value)
		{
			logger.Debug(String.Format("Write( {0} ); //bool.  Current WriteState: {1}", value, this._parentWriter.WriteState));
		}
		
		public override void Write(string value)
		{
			logger.Debug(String.Format("Write( \"{0}\" ); //string.  Current WriteState: {1}", value, this._parentWriter.WriteState));
		}
	}
}
