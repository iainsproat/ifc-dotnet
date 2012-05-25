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
using System.Text;
using System.IO;
using System.Collections.Generic;

using log4net;

using StepParser;

//using IfcDotNet.StepSerializer.Utilities;

namespace StepParser.IO
{
    /// <summary>
    /// Specifies the state of the <see cref="StepWriter"/>.
    /// </summary>
    public enum WriteState
    {
        /// <summary>
        /// An exception has been thrown, which has left the <see cref="StepWriter"/> in an invalid state.
        /// You may call the <see cref="StepWriter.Close"/> method to put the <see cref="StepWriter"/> in the <c>Closed</c> state.
        /// Any other <see cref="StepWriter"/> method calls results in an <see cref="InvalidOperationException"/> being thrown.
        /// </summary>
        Error,
        /// <summary>
        /// The <see cref="StepWriter.Close"/> method has been called.
        /// </summary>
        Closed,
        /// <summary>
        /// A Step file is being written
        /// </summary>
        Step,
        /// <summary>
        /// A Step file section (header or data) is being written
        /// </summary>
        Section,
        /// <summary>
        /// An object is being written.
        /// </summary>
        Entity,
        /// <summary>
        /// A array is being written.
        /// </summary>
        Array,
        /// <summary>
        /// A token which identifies the line number
        /// </summary>
        LineIdentifier,
        /// <summary>
        /// A property is being written.
        /// </summary>
        Property,
        /// <summary>
        /// A write method has not been called.
        /// </summary>
        Start
    }
    
    /// <summary>
    /// StepWriter is responsible for writing STEP file to a textwriter
    /// </summary>
    public class StepWriter : IStepWriter
    {
        private static ILog logger = LogManager.GetLogger(typeof(StepWriter));
        
        private enum State //FIXME may need StartStep, StartSection etc.
        {
            Start,
            Property,
            STEPStart,
            STEP,
            SectionStart,
            Section,
            LineIdentifier,
            EntityStart,
            Entity,
            ArrayStart,
            Array,
            Closed,
            Error
        }
        
        // array that gives a new state based on the current state an the token being written
        private static readonly State[][] stateArray = new State[][] {
            //Current State in columns->       Start                   Property                STEPStart                STEP                SectionStart                Section                Line Identifier       EntityStart         Entity             ArrayStart              Array                   Closed          Error
            //Token being written in rows
            /* None             */new State[]{ State.Error,            State.Error,            State.Error,             State.Error,        State.Error,                State.Error,           State.Error,          State.Error,        State.Error,       State.Error,            State.Error,            State.Error,    State.Error },
            /* StartSTEP        */new State[]{ State.STEP,             State.Error,            State.Error,             State.Error,        State.Error,                State.Error,           State.Error,          State.Error,        State.Error,       State.Error,            State.Error,            State.Error,    State.Error },
            /* StartSection     */new State[]{ State.Error,            State.Error,            State.Error,             State.Section,      State.Error,                State.Error,           State.Error,          State.Error,        State.Error,       State.Error,            State.Error,            State.Error,    State.Error },
            /* LineIdentifier   */new State[]{ State.Error,            State.Error,            State.Error,             State.Error,        State.Error,                State.LineIdentifier,  State.LineIdentifier, State.Error,        State.Error,       State.Error,            State.Error,            State.Error,    State.Error },
            /* StartEntity      */new State[]{ State.Error,            State.Entity,           State.Error,             State.Error,        State.Error,                State.EntityStart,     State.Entity,         State.EntityStart,  State.EntityStart, State.Error,            State.Entity,           State.Error,    State.Error },
            /* StartArray       */new State[]{ State.Error,            State.Array,            State.Error,             State.Error,        State.Error,                State.Error,           State.Error,          State.ArrayStart,   State.ArrayStart,  State.Error,            State.Array,            State.Error,    State.Error },
            /* Comment          */new State[]{ State.Start,            State.Property,         State.Error,             State.STEP,         State.Error,                State.Section,         State.LineIdentifier, State.Error,        State.Entity,      State.Error,            State.Array,            State.Error,    State.Error },
            /* Value            */new State[]{ State.Error,            State.Entity,           State.Error,             State.Error,        State.Error,                State.Error,           State.Error,          State.Entity,       State.Entity,      State.Array,            State.Array,            State.Error,    State.Error },
            /* EntityName       */new State[]{ State.Error,            State.Error,            State.Error,             State.Error,        State.Error,                State.Section,         State.Section,        State.EntityStart,  State.EntityStart, State.Error,            State.Error,            State.Error,    State.Error },
        };
        
        private int _top;

        private readonly List<StepTokenType> _stack;
        private State _currentState;
        private readonly TextWriter _writer;
        
        /// <summary>
        /// Gets the top.
        /// </summary>
        /// <value>The top.</value>
        protected internal int Top
        {
            get { return _top; }
        }

        /// <summary>
        /// Gets the state of the writer.
        /// </summary>
        public WriteState WriteState
        {
            get
            {
                switch (_currentState)
                {
                    case State.Error:
                        return WriteState.Error;
                    case State.Closed:
                        return WriteState.Closed;
                    case State.STEP:
                    case State.STEPStart:
                        return WriteState.Step;
                    case State.Section:
                    case State.SectionStart:
                        return WriteState.Section;
                    case State.Entity:
                    case State.EntityStart:
                        return WriteState.Entity;
                    case State.Array:
                    case State.ArrayStart:
                        return WriteState.Array;
                    case State.LineIdentifier:
                        return WriteState.LineIdentifier;
                    case State.Property:
                        return WriteState.Property;
                    case State.Start:
                        return WriteState.Start;
                    default:
                        throw new StepWriterException("Invalid state: " + _currentState);
                }
            }
        }
        
        /// <summary>
        /// Creates a new stepwriter using the given textWriter
        /// </summary>
        /// <param name="textWriter"></param>
        public StepWriter(TextWriter textWriter)
        {
            if (textWriter == null) throw new ArgumentNullException("textWriter");
            
            _writer = textWriter;
            _stack = new List<StepTokenType>(8);//FIXME why only 8?
            _stack.Add(StepTokenType.None);
            _currentState = State.Start;
        }
        
        private void Push(StepTokenType value)
        {
            _top++;
            if (_stack.Count <= _top)
                _stack.Add(value);
            else
                _stack[_top] = value;
        }

        private StepTokenType Pop()
        {
            StepTokenType value = Peek();
            _top--;

            return value;
        }

        private StepTokenType Peek()
        {
            return _stack[_top];
        }
        
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        void IDisposable.Dispose()
        {
            Dispose(true);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected void Dispose(bool disposing)
        {
            if (WriteState != WriteState.Closed && disposing)
                Close();
        }
        
        /// <summary>
        /// Flushes whatever is in the buffer to the underlying streams and also flushes the underlying stream.
        /// </summary>
        public void Flush(){
            _writer.Flush();
        }
        
        /// <summary>
        /// Changes the <see cref="State"/> to Closed.
        /// </summary>
        public void Close()
        {
            AutoCompleteAll();
            _writer.Close();
        }
        
        /// <summary>
        /// Writes the required line necessary to indicate the start of a STEP file
        /// </summary>
        public void WriteStartStep(){
        	logger.Debug(String.Format("WriteStartStep(); //_currentState : {0}; _top : {1};", _currentState, _top));
            AutoComplete(StepToken.StartSTEP);
            Push(StepTokenType.STEP);
            _writer.Write("ISO-10303-21");
            WriteEndLine();
        }
        
        /// <summary>
        /// Writes the required line to close the end of a STEP file
        /// </summary>
        public void WriteEndStep(){
        	logger.Debug(String.Format("WriteEndStep(); //_currentState : {0}; _top : {1};", _currentState, _top));
            AutoCompleteClose(StepToken.EndSTEP);
        }
        
        /// <summary>
        /// Writes the required line necessary to indicate the start of the Header section in a STEP file
        /// </summary>
        public void WriteStartHeader(){
        	logger.Debug(String.Format("WriteStartHeader(); //_currentState : {0}; _top : {1};", _currentState, _top));
            AutoComplete(StepToken.StartSection);
            Push(StepTokenType.Section);
            _writer.Write("HEADER");
            WriteEndLine();
        }
        
        /// <summary>
        /// Writes the required line necessary to indicate the start of the Data section of a STEP file
        /// </summary>
        public void WriteStartData(){
        	logger.Debug(String.Format("WriteStartData(); //_currentState : {0}; _top : {1};", _currentState, _top));
            AutoComplete(StepToken.StartSection);
            Push(StepTokenType.Section);
            _writer.Write("DATA");
            WriteEndLine();
        }
        
        /// <summary>
        /// Writes the end section to the stream
        /// </summary>
        public void WriteEndSection(){
        	logger.Debug(String.Format("WriteEndSection(); //_currentState : {0}; _top : {1};", _currentState, _top));
            AutoCompleteClose(StepToken.EndSection);
        }
        
        /// <summary>
        /// Writes the value of a line identifier to the stream
        /// </summary>
        /// <param name="entityId"></param>
        public void WriteLineIdentifier(int entityId)
        {
        	logger.Debug(String.Format("WriteLineIdentifier( {0} ); //_currentState : {1}; _top : {2};", entityId, _currentState, _top));
            AutoComplete(StepToken.LineIdentifier);
            Push(StepTokenType.LineIdentifier);
            _writer.Write(String.Format(CultureInfo.InvariantCulture,
                                        "#{0}", entityId));
            WriteLineAssignment();
        }
        
        private void WriteLineAssignment()
        {
        	_writer.Write(" = ");
        }
        
        /// <summary>
        /// Writes the name of an entity to the stream
        /// </summary>
        /// <param name="objectName"></param>
        public void WriteObjectName(string objectName){
        	logger.Debug(String.Format("WriteObjectName( {0} ); //_currentState : {1}; _top : {2};", objectName, this._currentState, this._top));
        	AutoComplete(StepToken.EntityName);
            _writer.Write(objectName); //FIXME what about the Autocomplete?
        }

        /// <summary>
        /// Writes the beginning of a STEP object.
        /// </summary>
        public void WriteStartObject()
        {
        	logger.Debug(String.Format("WriteStartObject(); //_currentState : {0}; _top : {1};", this._currentState, this._top));
            AutoComplete(StepToken.StartEntity);
            Push(StepTokenType.Entity);
            _writer.Write("(");
        }
        
        /// <summary>
        /// Writes the end of a STEP object.
        /// This does not handle nested object, as an endline is called
        /// </summary>
        public void WriteEndObject()
        {
        	logger.Debug(String.Format("WriteEndObject(); //_currentState : {0}; _top : {1};", this._currentState, this._top));
            AutoCompleteClose(StepToken.EndEntity);
            
            //new line if it is not a nested entity
            if(this._currentState != State.Entity){
            	WriteEndLine();
            }
        }

        /// <summary>
        /// Writes the beginning of a STEP array.
        /// </summary>
        public virtual void WriteStartArray()
        {
        	logger.Debug(String.Format("WriteStartArray(); //_currentState : {0}; _top : {1};", this._currentState, this._top));
            AutoComplete(StepToken.StartArray);
            Push(StepTokenType.Array);
            _writer.Write("(");
        }

        /// <summary>
        /// Writes the end of an array.
        /// </summary>
        public void WriteEndArray()
        {
        	logger.Debug(String.Format("WriteEndArray(); //_currentState : {0}; _top : {1};", this._currentState, this._top));
            AutoCompleteClose(StepToken.EndArray);
        }
        
        /// <summary>
        /// Writes the end line character
        /// </summary>
        public void WriteEndLine(){
        	logger.Debug(String.Format("WriteEndLine(); //_currentState : {0}; _top : {1};", this._currentState, this._top));
            WriteEnd(StepToken.EndLine);
        }

        /// <summary>
        /// Writes the end of the current STEP object or array.
        /// </summary>
        public void WriteEnd()
        {
        	logger.Debug(String.Format("WriteEnd(); //_currentState : {0}; _top : {1};", this._currentState, this._top));
            WriteEnd(Peek());
        }
        
        

        private void WriteEnd(StepTokenType type)
        {
        	logger.Debug(String.Format("WriteEnd( {0} ); //_currentState : {0}; _top : {1};", type, _currentState, _top));
            switch (type)
            {
                case StepTokenType.Entity:
                    WriteEndObject();
                    break;
                case StepTokenType.Array:
                    WriteEndArray();
                    break;
                default:
                    throw new StepWriterException(String.Format(CultureInfo.InvariantCulture,
                                                                "Unexpected type when writing end: {0}", type));
            }
        }

        private void AutoCompleteAll()
        {
            while (_top > 0)
            {
                WriteEnd();
            }
        }

        private StepTokenType GetTypeForCloseToken(StepToken token)
        {
            switch (token)
            {
                case StepToken.EndSTEP:
                    return StepTokenType.STEP;
                case StepToken.EndSection:
                    return StepTokenType.Section;
                case StepToken.EndEntity:
                    return StepTokenType.Entity;
                case StepToken.EndArray:
                    return StepTokenType.Array;
                default:
                    throw new StepWriterException("No type for token: " + token);
            }
        }

        private StepToken GetCloseTokenForType(StepTokenType type)
        {
            switch (type)
            {
                case StepTokenType.STEP:
                    return StepToken.EndSTEP;
                case StepTokenType.Section:
                    return StepToken.EndSection;
                case StepTokenType.Entity:
                    return StepToken.EndEntity;
                case StepTokenType.Array:
                    return StepToken.EndArray;
                case StepTokenType.LineIdentifier:
                    return StepToken.None; //HACK
                default:
                    throw new StepWriterException("No close token for type: " + type);
            }
        }

        private void AutoCompleteClose(StepToken tokenBeingClosed)
        {
            // write closing symbol and calculate new state

            int levelsToComplete = 0;

            for (int i = 0; i < _top; i++)
            {
                int currentLevel = _top - i;

                if (_stack[currentLevel] == GetTypeForCloseToken(tokenBeingClosed))
                {
                    levelsToComplete = i + 1;
                    break;
                }
            }

            if (levelsToComplete == 0)
                throw new StepWriterException("No token to close.");

            for (int i = 0; i < levelsToComplete; i++)
            {
                StepToken token = GetCloseTokenForType(Pop());

                WriteEnd(token);
            }

            StepTokenType currentLevelType = Peek();

            switch (currentLevelType)
            {
                case StepTokenType.STEP:
                    _currentState = State.STEP;
                    break;
                case StepTokenType.Section:
                    _currentState = State.Section;
                    break;
                case StepTokenType.Entity:
                    _currentState = State.Entity;
                    break;
                case StepTokenType.Array:
                    _currentState = State.Array;
                    break;
                case StepTokenType.LineIdentifier:
                    _currentState = State.LineIdentifier;
                    break;
                case StepTokenType.None:
                    _currentState = State.Start;
                    break;
                default:
                    throw new StepWriterException("Unknown StepType: " + currentLevelType);
            }
        }

        /// <summary>
        /// Writes the specified end token to the stream
        /// </summary>
        /// <param name="token">The end token to write.</param>
        protected void WriteEnd(StepToken token)
        {
        	logger.Debug(String.Format("WriteEnd( {0} ); //_currentState : {1}; _top : {2};", token, _currentState, _top));
            switch (token)
            {
                case StepToken.EndSTEP:
                    _writer.Write("END-ISO-10303-21;");
                    break;
                case StepToken.EndSection:
                    _writer.Write("ENDSEC");
                    WriteEndLine();
                    break;
                case StepToken.EndLine:
                    _writer.Write(";\r\n");
                    break;
                case StepToken.EndEntity:
                    _writer.Write(")");
                    break;
                case StepToken.EndArray:
                    _writer.Write(")");
                    break;
                case StepToken.None://HACK using None as the end token for LineIdentifier
                    break;
                default:
                    throw new StepWriterException("Invalid StepToken: " + token);
            }
        }

        /// <summary>
        /// Writes the Step value delimiter.
        /// </summary>
        protected void WriteValueDelimiter()
        {
        	logger.Debug(String.Format("WriteValueDelimiter(); //_currentState : {0}; _top : {1};", _currentState, _top));
            _writer.Write(", ");
        }
        
        /// <summary>
        /// Attempts to complete the token being written
        /// </summary>
        /// <param name="tokenBeingWritten"></param>
        internal void AutoComplete(StepToken tokenBeingWritten)
        {
        	logger.Debug(String.Format("AutoComplete( {0} ); //tokenBeingWritten : {1}; _currentState : {2}, _top : {3};", 
        	                           tokenBeingWritten,
        	                           (int)tokenBeingWritten,
        	                           _currentState, _top));
        	
            int token;
            switch (tokenBeingWritten)
            {
                default:
                    token = (int)tokenBeingWritten;
                    break;
                case StepToken.Integer:
                case StepToken.Float:
                case StepToken.String:
                case StepToken.Boolean:
                case StepToken.Enumeration:
                case StepToken.Overridden:
                case StepToken.Null:
                case StepToken.Undefined:
                case StepToken.Date:
                    // a value is being written
                    token = 7;
                    break;
            }
            
            
            
            // gets new state based on the current state and what is being written
            State newState = stateArray[token][(int)_currentState];

            /*logger.Debug(String.Format(CultureInfo.InvariantCulture,
                                       "From a Current State, {0}, and attempting to write a token of {1} has resulted in a state of {2}",
                                       _currentState.ToString(), tokenBeingWritten.ToString(), newState.ToString()));
            */
            if (newState == State.Error)
                throw new StepWriterException(String.Format(CultureInfo.InvariantCulture,
                                                            "Token {0} in state {1} would result in an invalid Step object.",
                                                            tokenBeingWritten.ToString(),
                                                            _currentState.ToString()));

            if ((_currentState == State.Entity || _currentState == State.Array) && tokenBeingWritten != StepToken.Comment)
            {
                WriteValueDelimiter();
            }

            WriteState writeState = WriteState;

            _currentState = newState;
        }
        
        /// <summary>
        /// Writes a string value to the stream
        /// </summary>
        /// <param name="value"></param>
        public void WriteValue(string value){
        	logger.Debug(String.Format("WriteValue( \"{0}\" ); //string; _currentState : {1}; _top : {2};", value, _currentState, _top));
            AutoComplete(StepToken.String);
            //if(String.IsNullOrEmpty(value))
                //WriteNull();
            //else
                WriteEscapedString(value);
        }
        
        /// <summary>
        /// Writes an Int16 value to the stream
        /// </summary>
        /// <param name="value"></param>
        public void WriteValue(System.Int16 value){
        	logger.Debug(String.Format("WriteValue( {0} ); //System.Int16; _currentState : {1}; _top : {2};", value, _currentState, _top));
            AutoComplete(StepToken.Integer);
            _writer.Write(value.ToString(CultureInfo.InvariantCulture));
        }
        
        /// <summary>
        /// Writes an Int32 value to the stream
        /// </summary>
        /// <param name="value"></param>
        public void WriteValue(System.Int32 value){
        	logger.Debug(String.Format("WriteValue( {0} ); //System.Int32; _currentState : {1}; _top : {2};", value, _currentState, _top));
            AutoComplete(StepToken.Integer);
            _writer.Write(value.ToString(CultureInfo.InvariantCulture));
        }
        
        /// <summary>
        /// Writes an Int64 value to the stream
        /// </summary>
        /// <param name="value"></param>
        public void WriteValue(System.Int64 value){
        	logger.Debug(String.Format("WriteValue( {0} ); //System.Int64; _currentState : {1}; _top : {2};", value, _currentState, _top));
            AutoComplete(StepToken.Integer);
            _writer.Write(value.ToString(CultureInfo.InvariantCulture));
        }
        
        /// <summary>
        /// Writes a double value to the stream
        /// </summary>
        /// <param name="value"></param>
        public void WriteValue(double value){
        	logger.Debug(String.Format("WriteValue( {0} ); //double; _currentState : {1}; _top : {2};", value, _currentState, _top));
            AutoComplete(StepToken.Float);
            
            //HACK ensure floating point values which are also integers have a trailing period
            string output = value.ToString(CultureInfo.InvariantCulture);
            if(output.IndexOf('.') == -1 && output.IndexOfAny(new char[]{'e','E'}) == -1){
    			output += ".";
    		}
            _writer.Write(output); //TODO we may want a custom IFormatProvider here.
        }
        
        /// <summary>
        /// Writes the null character to the stream
        /// </summary>
        public void WriteNull(){
        	logger.Debug(String.Format("WriteNull(); //_currentState : {0}; _top : {1};", _currentState, _top));
            AutoComplete(StepToken.Null);
            _writer.Write("$");
        }
        
        /// <summary>
        /// Writes the character denoting an overridden property to the stream
        /// </summary>
        public void WriteOverridden(){
        	logger.Debug(String.Format("WriteOverridden(); //_currentState : {0}; _top : {1};", _currentState, _top));
            AutoComplete(StepToken.Overridden);
            _writer.Write("*");
        }
        
        /// <summary>
        /// Writes a boolean value to the stream
        /// </summary>
        /// <param name="value"></param>
        public void WriteBool(bool value){
        	logger.Debug(String.Format("WriteBool( {0} ); //_currentState : {1}; _top : {2};", value, _currentState, _top));
            AutoComplete(StepToken.Boolean);
            _writer.Write(".");
            if(value){
            	_writer.Write("T");
            }else{
            	_writer.Write("F");
            }
            _writer.Write(".");
        }
        
        /// <summary>
        /// Writes an enumeration to the stream
        /// </summary>
        /// <param name="value"></param>
        public void WriteEnum(string value){
        	logger.Debug(String.Format("WriteEnum( {0} ); //_currentState : {1}; _top : {2};", value, _currentState, _top));
            if(string.IsNullOrEmpty(value)) throw new ArgumentNullException("value");
            AutoComplete(StepToken.Enumeration);
            _writer.Write(".");
            _writer.Write(value);
            _writer.Write(".");
        }
        
        /// <summary>
        /// Writes a line reference to the stream
        /// </summary>
        /// <param name="lineReference"></param>
        public void WriteLineReference(int lineReference){
        	logger.Debug(String.Format("WriteLineReference( {0} ); //_currentState : {1}; _top : {2};", lineReference, _currentState, _top));
            AutoComplete(StepToken.LineReference);
            _writer.Write("#");
            _writer.Write(lineReference);
        }
        
        /// <summary>
        /// Writes a string, with the correct escaped characters, to the string
        /// </summary>
        /// <param name="value"></param>
        public void WriteEscapedString(string value){
        	logger.Debug(String.Format("WriteEscapedString( \"{0}\" ); //_currentState : {1}; _top : {2};", value, _currentState, _top));
            char delimiter = '\''; //TODO refactor this out, so it can be set externally
            
            _writer.Write(delimiter);
            
            if(!String.IsNullOrEmpty(value)){
                int lastWritePosition = 0;
                int skipped = 0;
                char[] chars = null;
                for(int i = 0; i < value.Length; i++){
                    char c = value[i];
                    string escapedValue;
                    switch(c){
                            //TODO add cases as necessary.
                            
                        case '\'':
                            // only escape if this charater is being used as the delimiter
                            escapedValue = (delimiter == '\'') ? @"\'" : null;
                            break;
                        default:
                            escapedValue = (c <= '\u001f') ? StringUtils.ToCharAsUnicode(c) : null;
                            break;
                    }
                    
                    if (escapedValue != null)
                    {
                        if (chars == null)
                            chars = value.ToCharArray();

                        // write skipped text
                        if (skipped > 0)
                        {
                            _writer.Write(chars, lastWritePosition, skipped);
                            skipped = 0;
                        }

                        // write escaped value and note position
                        _writer.Write(escapedValue);
                        lastWritePosition = i + 1;
                    }
                    else
                    {
                        skipped++;
                    }
                }
                // write any remaining skipped text
                if (skipped > 0)
                {
                    if (lastWritePosition == 0)
                        _writer.Write(value);
                    else
                        _writer.Write(chars, lastWritePosition, skipped);
                }
            }
            
            _writer.Write(delimiter);
        }
    }
}
