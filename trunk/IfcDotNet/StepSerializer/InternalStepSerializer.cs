#region License
/*

Copyright 2010, Iain Sproat
All rights reserved.

Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions are
met:

 * Redistributions of source code must retain the above copyright
notice, this list of conditions and the following disclaimer.
 * Redistributions in binary form must reproduce the above
copyright notice, this list of conditions and the following disclaimer
in the documentation and/or other materials provided with the
distribution.
 * The names of the contributors may not be used to endorse or promote products derived from
this software without specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
"AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR
A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT
OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT
LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY
THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
(INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.


The majority of the below code originate from the Json.NET project, for which the following additional license applies:

Copyright (c) 2007 James Newton-King

Permission is hereby granted, free of charge, to any person
obtaining a copy of this software and associated documentation
files (the "Software"), to deal in the Software without
restriction, including without limitation the rights to use,
copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the
Software is furnished to do so, subject to the following
conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
OTHER DEALINGS IN THE SOFTWARE.
 */
#endregion

using System;
using System.Globalization;
using System.Collections.Generic;

using log4net;

namespace IfcDotNet.StepSerializer
{
	/// <summary>
	/// InternalStepSerializer converts raw StepReader output into
	/// semi-structured 'objects'.
	/// </summary>
	internal class InternalStepSerializer
	{
		private static readonly ILog logger = LogManager.GetLogger(typeof(InternalStepSerializer));
		
		private StepReader _reader;
		private IList<StepDataObject> dataObjects = new List<StepDataObject>();
		
		public InternalStepSerializer()
		{
		}
		
		public IList<StepDataObject> Deserialize(StepReader reader)
		{
			if( reader == null )
				throw new ArgumentNullException( "reader" );
			this._reader = reader;
			
			while(this._reader.Read()){
				if(_reader.TokenType == StepToken.LineIdentifier){
					int objectNumber = -1;
					try{
						objectNumber = getObjectNumber();//FIXME this can throw exceptions, need to try/catch this.
						logger.Debug("Object Number : " + objectNumber);
					}catch(Exception e){
						//fail silently
						logger.Debug(String.Format(CultureInfo.InvariantCulture,
						                           "Failed while trying to deserialize an entity. {0}",
						                           e.Message));
					}
					if(objectNumber > 0){//HACK
						logger.Debug(String.Format(CultureInfo.InvariantCulture,
						                           "objectNumber : {0}", objectNumber));
						this.dataObjects.Add(deserializeEntity(objectNumber));//FIXME where should the objectNumber be stored??
						//HACK should be within the try/catch above
					}
				}
			}
			
			//TODO should try to create the tree here.  Need to handle circular references though.
			
			return this.dataObjects;
			
		}
		
		/// <summary>
		/// Should only be called when reader is at a LineIdentifier token.
		/// Attempts to read the value of the line identifier, e.g. "#24"
		/// and cast it to an integer, e.g. 24.
		/// </summary>
		/// <returns></returns>
		private int getObjectNumber(){
			//FIXME reduce the verbosity of the error catching.
			if(_reader == null){
				string msg = "getObjectNumber() was called, but the internal reader is null";
				logger.Error(msg);
				throw new NullReferenceException(msg);
			}
			if(_reader.TokenType != StepToken.LineIdentifier){
				string msg = "getObjectNumber() was called when the ExpressReader was not at a LineIdentifier token";
				logger.Error(msg);
				throw new InvalidOperationException(msg);
			}
			if(_reader.ValueType != typeof(string)){
				string msg = "The line identifier isn't a string";
				logger.Error(msg);
				throw new FormatException(msg);
			}
			return CastLineIdToInt( _reader.Value.ToString() );
			
		}
		
		
		private StepDataObject deserializeEntity(int objectNumber){
			logger.Debug("Deserializing object #" + objectNumber);
			StepDataObject edo = new StepDataObject();
			edo.StepId = objectNumber;
			bool entityStarted = false;
			
			//nested entities are already on the EntityName token
			if(_reader.TokenType == StepToken.EntityName)
				edo.ObjectName = _reader.Value.ToString();
			
			while(_reader.Read()){
				logger.Debug("deserializer read : " +_reader.TokenType + " of value " + _reader.Value);
				switch(_reader.TokenType){
					case StepToken.EntityName:
						if(!entityStarted)
							edo.ObjectName = _reader.Value.ToString();//FIXME should probably check the _reader.ValueType to make sure it is a string
						else //it's a nested entity
							edo.Properties.Add(deserializeNestedEntity());
						continue;
					case StepToken.LineReference:
						edo.Properties.Add(deserializeLineReference());
						continue;
					case StepToken.Enumeration:
					case StepToken.Boolean:
					case StepToken.Integer:
					case StepToken.Float:
					case StepToken.String:
						edo.Properties.Add(deserializeProperty());
						continue;
					case StepToken.StartArray:
						edo.Properties.Add(deserializeArray());
						continue;
					case StepToken.StartEntity:
						if(!entityStarted)
							entityStarted = true;
						continue;
					case StepToken.Operator:
						continue;
					case StepToken.Overridden:
						edo.Properties.Add(deserializeOverridden());
						continue;
					case StepToken.Null:
						edo.Properties.Add(deserializeNull());//HACK is this the best way to handle null properties?
						continue;
					case StepToken.EndEntity:
						return edo;
					case StepToken.EndLine:
					case StepToken.EndSection:
					case StepToken.EndExpress:
					case StepToken.StartSTEP:
					case StepToken.StartSection:
						string msg = String.Format(CultureInfo.InvariantCulture,
						                           "A token was found which was not expected: {0}",
						                           _reader.TokenType);
						logger.Error(msg);
						throw new Exception(msg);//HACK need a more specific exception type
					default:
						throw new NotImplementedException(String.Format(CultureInfo.InvariantCulture,
						                                                "The {0} ExpressToken type is not yet implemented by deserializeEntity()",
						                                                _reader.TokenType));
				}
				//TODO should do some verification here (properties are after entityStart and before EntityEnd etc.)
			}
			string errorMsg = "The reader reached the end without finding an endEntity token";
			logger.Error(errorMsg);
			throw new Exception(errorMsg);//HACK need to throw a more specific exception type
		}
		
		private StepProperty deserializeNestedEntity(){
			if(_reader == null){
				string msg = "deserializeNestedEntity() has been called, but the internal reader is null";
				logger.Error(msg);
				throw new NullReferenceException(msg);
			}
			StepProperty epv = new StepProperty();
			epv.Token = StepToken.StartEntity;
			epv.Value = deserializeEntity(-1);
			epv.ValueType = typeof(StepDataObject);
			return epv;
		}
		
		private StepProperty deserializeProperty(){
			if(_reader == null){
				string msg = "deserializeProperty() has been called, but the internal reader is null";
				logger.Error(msg);
				throw new NullReferenceException(msg);
			}
			StepProperty epv = new StepProperty();
			epv.Token = _reader.TokenType;//FIXME is this passed by reference or value (do I need to clone/deep copy?)
			epv.Value = _reader.Value;  //FIXME is this passed by reference or value (do I need to clone/deep copy?)
			epv.ValueType = _reader.ValueType; //FIXME is this passed by reference or value (do I need to clone/deep copy?)
			return epv;
		}
		
		private StepProperty deserializeLineReference(){
			if(_reader == null){
				string msg = "deserializeLineReference() has been called, but the internal reader is null";
				logger.Error(msg);
				throw new NullReferenceException(msg);
			}
			StepProperty epv = new StepProperty();
			epv.Token = _reader.TokenType;
			epv.Value = CastLineIdToInt((string)_reader.Value);
			epv.ValueType = typeof(int);
			return epv;
		}
		
		private StepProperty deserializeNull(){
			if(_reader == null){
				string msg = "deserializeNull() has been called, but the internal reader is null";
				logger.Error(msg);
				throw new NullReferenceException(msg);
			}
			StepProperty epv = new StepProperty();
			epv.Token = StepToken.Null;
			epv.Value = null;
			epv.ValueType = null; //FIXME is this going to cause issues elsewhere?  Need to remember when using this that it can be null
			return epv;
		}
		
		private StepProperty deserializeOverridden(){
			if(_reader == null){
				string msg = "deserializeOverridden() has been called, but the internal reader is null";
				logger.Error(msg);
				throw new NullReferenceException(msg);
			}
			StepProperty epv = new StepProperty();
			epv.Token = StepToken.Overridden;
			epv.Value = null;
			epv.ValueType = null; //FIXME
			return epv;
		}
		
		private StepProperty deserializeArray(){
			if(_reader == null){
				string msg = "deserializeArray() has been called, but the internal reader is null";
				logger.Error(msg);
				throw new NullReferenceException(msg);
			}
			StepProperty epv = new StepProperty();
			epv.Token = StepToken.StartArray;
			IList<StepProperty> values = new List<StepProperty>();
			while(_reader.Read()){
				logger.Debug("deserializeArray read : " + _reader.TokenType + " of value " + _reader.Value);
				switch(_reader.TokenType){
					case StepToken.EndArray:
						epv.Value = values;
						epv.ValueType = typeof(IList<StepProperty>);
						return epv;
					case StepToken.LineReference:
						values.Add(deserializeLineReference());
						continue;
					case StepToken.Enumeration:
					case StepToken.Boolean:
					case StepToken.Integer:
					case StepToken.Float:
					case StepToken.String:
						values.Add(deserializeProperty());
						continue;
					case StepToken.Null:
						values.Add(deserializeNull());//HACK is this the best way to handle null properties?
						continue;
					case StepToken.StartEntity:
						values.Add(deserializeNestedEntity());
						continue;
					case StepToken.Operator:
					case StepToken.Overridden:
					case StepToken.EndEntity:
					case StepToken.EndLine:
					case StepToken.EndSection:
					case StepToken.EndExpress:
					case StepToken.StartSTEP:
					case StepToken.StartSection:
						string msg = String.Format(CultureInfo.InvariantCulture,
						                           "deserializeArray found a token which was not expected: {0}",
						                           _reader.TokenType);
						logger.Error(msg);
						throw new Exception(msg);//HACK need a more specific exception type
					default:
						throw new NotImplementedException(String.Format(CultureInfo.InvariantCulture,
						                                                "This ExpressToken type is not yet implemented by deserializeArray(), {0}",
						                                                _reader.TokenType));
				}
			}
			string errorMsg = "deserializeArray() reached the end of the reader without finding an endArray token";
			logger.Error(errorMsg);
			throw new Exception(errorMsg);//HACK need more specific exception type
		}
		
		/// <summary>
		/// Converts a string in line identifier format, e.g #21, to the corresponding integer, 21.
		/// </summary>
		/// <param name="lineIdent"></param>
		/// <returns></returns>
		private int CastLineIdToInt(string lineIdent){
			if(String.IsNullOrEmpty(lineIdent)){
				string msg = "The lineIdentifier has no value";
				logger.Error(msg);
				throw new NullReferenceException(msg);
			}
			if(!lineIdent.StartsWith("#")){
				string msg = String.Format(CultureInfo.InvariantCulture,
				                           "The lineIdentifier does not start with a # character.  The line identifier is instead {0}",
				                           lineIdent);
				logger.Error(msg);
				throw new FormatException(msg);
			}
			
			lineIdent = lineIdent.TrimStart('#');
			try{
				return int.Parse(lineIdent);
			}catch(FormatException fe){
				logger.Error(fe.Message);
				throw;
			}catch(OverflowException oe){
				logger.Error(oe.Message);
				throw;
			}
		}
	}
}
