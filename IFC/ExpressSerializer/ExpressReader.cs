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
using System.IO;
using System.Globalization;
using System.Collections.Generic;

using IfcDotNet.ExpressSerializer.Utilities;

using log4net;

namespace IfcDotNet.ExpressSerializer
{
    /// <summary>
    /// IfcExpressReader reads Express files and tokenizes them
    /// </summary>
    public class ExpressReader : IDisposable
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(ExpressReader));
        
        protected enum State{
            Start,
            ExpressStart,
            Express,
            SectionStart,
            Section,
            EntityName,
            EntityStart,
            Entity,
            PostValue,
            ArrayStart,
            Array,
            //ArrayEnd,
            //EntityEnd,
            //CommentStart,
            //Comment,
            //CommentEnd,
            //SectionEnd,
            //ExpressEnd,
            Complete,
            Closed,
            //Error
        }
        
        #region Constants
        private const string CarriageReturnLineFeed = "\r\n";
        private const string Empty = "";
        private const char CarriageReturn = '\r';
        private const char LineFeed = '\n';
        private const char Tab = '\t';
        #endregion
        
        #region Private Members
        private ExpressToken _token;
        private State _currentState = State.Start;
        private bool _end = false;
        private char? _lastChar;
        private int _currentLinePosition;
        private int _currentLineNumber;
        private object _value;
        private int _top;
        private ExpressTokenType _currentTypeContext;
        private Type _valueType;
        private char _quoteChar;
        
        private readonly StringBuffer _buffer;
        
        /// <summary>
        /// The underlying reader
        /// </summary>
        TextReader _reader;
        
        private readonly IList<ExpressTokenType> _stack;
        #endregion
        
        #region Properties
        protected State CurrentState{
            get{ return _currentState; }
        }
        
        /// <summary>
        /// Gets the type of the current Express token.
        /// </summary>
        public virtual ExpressToken TokenType
        {
            get { return _token; }
        }
        
        /// <summary>
        /// Gets the text value of the current Express token.
        /// </summary>
        public virtual object Value
        {
            get { return _value; }
        }
        
        /// <summary>
        /// Gets the quotation mark character used to enclose the value of a string.
        /// </summary>
        public char QuoteChar
        {
            get { return _quoteChar; }
            protected internal set { _quoteChar = value; }
        }
        
        /// <summary>
        /// Gets The Common Language Runtime (CLR) type for the current Json token.
        /// </summary>
        public virtual Type ValueType
        {
            get { return _valueType; }
        }
        
        /// <summary>
        /// Gets the depth of the current token in the JSON document.
        /// </summary>
        /// <value>The depth of the current token in the JSON document.</value>
        public virtual int Depth
        {
            get
            {
                int depth = _top - 1;
                if (IsStartToken(TokenType))
                    return depth - 1;
                else
                    return depth;
            }
        }
        #endregion
        
        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="reader"></param>
        public ExpressReader(TextReader reader)
        {
            if(reader == null)
                throw new ArgumentNullException("reader");
            _reader = reader;
            _currentState = State.Start;
            _stack = new List<ExpressTokenType>();
            _buffer = new StringBuffer(4096);
            Push(ExpressTokenType.None);
        }
        
        private void Push(ExpressTokenType value)
        {
            logger.Debug("Pushing to stack.  Current context type is " + value.ToString() );
            _stack.Add(value);
            _top++;
            _currentTypeContext = value;
        }

        private ExpressTokenType Pop()
        {
            
            ExpressTokenType value = Peek();
            logger.Debug("Pop : " + value.ToString());
            _stack.RemoveAt(_stack.Count - 1);
            _top--;
            _currentTypeContext = _stack[_top - 1];

            return value;
        }

        private ExpressTokenType Peek()
        {
            return _currentTypeContext;
        }
        
        public bool Read(){
            logger.Debug("Read()");
            while(true){
                char currentChar;
                if (_lastChar != null)
                {
                    currentChar = _lastChar.Value;
                    _lastChar = null;
                }
                else
                {
                    currentChar = MoveNext();
                }

                if (currentChar == '\0' && _end)
                    return false;
                
                logger.Debug( "Current State : " + this.CurrentState );
                
                switch(this.CurrentState){
                    case State.Start:
                        return ParseIsoDefinition(currentChar );
                    case State.Express:
                    case State.ExpressStart:
                    case State.Section:
                    case State.SectionStart:
                    case State.Entity:
                    case State.EntityStart:
                    case State.Array:
                    case State.ArrayStart:
                        return ParseValue(currentChar);
                    case State.EntityName:
                        return ParseEntity( currentChar );
                    case State.PostValue:
                        // returns true if it hits
                        // end of object or array
                        if (ParsePostValue(currentChar))
                            return true;
                        break;
                        //case State.CommentStart:
                        //    return ParseComment(currentChar);
                    case State.Complete:
                        break;
                    case State.Closed:
                        break;
                        //case State.Error:
                        //    break;
                    default:
                        throw CreateExpressReaderException("Unexpected State : {0}. Line {1}, position {2}", CurrentState, _currentLineNumber, _currentLinePosition);
                }
            }
        }
        
        private bool ParseValue( char currentChar ){
            logger.Debug("ParseValue");
            do
            {
                logger.Debug("Current char held by ParseValue is : " + currentChar.ToString() );
                switch (currentChar)
                {
                    case '\'':
                        ParseString(currentChar);
                        return true;
                    case '$':
                        SetToken(ExpressToken.Null );
                        return true;
                    case '-':
                        ParseNumber(currentChar);
                        return true;
                    case '/':
                        ParseComment(currentChar);
                        return true;
                    case '#': //FIXME may be in a function or at the start of a line
                        ParseLineIdentity(currentChar);
                        return true;
                    case '(':
                        SetToken(ExpressToken.StartArray);
                        return true;
                    case '*':
                        SetToken(ExpressToken.Overridden);
                        return true;
                    case 'D':
                    case 'H':
                        ParseSectionName(currentChar);  //HACK
                        return true;
                    case 'F':
                    case 'S':
                        ParseEntityName( currentChar ); //HACK
                        return true;
                    case 'E':
                        ParseEndSection( currentChar ); //HACK
                        return true;
                    case ')':
                        if(Peek() == ExpressTokenType.Entity )
                            SetToken(ExpressToken.EndEntity);
                        else
                            SetToken(ExpressToken.EndArray);
                        return true;
                    case '=':
                        SetToken(ExpressToken.Operator, "=");
                        return true;
                    case ',':
                        SetToken(ExpressToken.Undefined);
                        return true;
                    case ';':
                        SetToken(ExpressToken.EndLine);
                        return true;
                    case '.':
                        ParseEnum(currentChar);
                        return true;
                    case ' ':
                    case ExpressReader.Tab:
                    case ExpressReader.LineFeed:
                    case ExpressReader.CarriageReturn:
                        // eat
                        break;
                    default:
                        if (char.IsWhiteSpace(currentChar))
                        {
                            // eat
                        }
                        else if (char.IsNumber(currentChar) || currentChar == '-')
                        {
                            ParseNumber(currentChar);
                            return true;
                        }
                        else if (char.IsLetter(currentChar))//HACK assumes that any value with a letter is an entity name
                        {
                            ParseEntityName(currentChar);
                            return true;
                        }
                        else
                        {
                            throw CreateExpressReaderException("Unexpected character encountered while parsing value: {0}. Line {1}, position {2}.", currentChar, _currentLineNumber, _currentLinePosition);
                        }
                        break;
                }
            } while ((currentChar = MoveNext()) != '\0' || !_end);

            return false;
        }
        
        private bool ParsePostValue( char currentChar ){
            
            do
            {
                switch (currentChar)
                {
                    case ')':
                        SetToken(GetCloseTokenForType(Peek()));
                        return true;
                    case '/':
                        ParseComment(currentChar);
                        return true;
                    case ',':
                    case ';':
                        // finished parsing
                        SetStateBasedOnCurrent();
                        return false;
                    case ' ':
                    case ExpressReader.Tab:
                    case ExpressReader.LineFeed:
                    case ExpressReader.CarriageReturn:
                        // eat
                        break;
                    default:
                        if (char.IsWhiteSpace(currentChar))
                        {
                            // eat
                        }
                        else
                        {
                            throw CreateExpressReaderException("After parsing a value an unexpected character was encountered: {0}. Line {1}, position {2}.", currentChar, _currentLineNumber, _currentLinePosition);
                        }
                        break;
                }
            } while ((currentChar = MoveNext()) != '\0' || !_end);

            return false;
        }
        
        private bool ParseComment( char currentChar ){
            if( currentChar != '/' )
                throw CreateExpressReaderException("A comment should start with a backslash, /");

            currentChar = MoveNext();

            if (currentChar == '*')
            {
                while ((currentChar = MoveNext()) != '\0' || !_end)
                {
                    if (currentChar == '*')
                    {
                        if ((currentChar = MoveNext()) != '\0' || !_end)
                        {
                            if (currentChar == '/')
                            {
                                break;
                            }
                            else
                            {
                                _buffer.Append('*');
                                _buffer.Append(currentChar);
                            }
                        }
                    }
                    else
                    {
                        _buffer.Append(currentChar);
                    }
                }
            }
            else
            {
                throw CreateExpressReaderException("Error parsing comment. Expected: *. Line {0}, position {1}.", _currentLineNumber, _currentLinePosition);
            }

            SetToken(ExpressToken.Comment, _buffer.ToString());

            _buffer.Position = 0;
            return true;
        }
        
        private void ParseEnum( char quote ){
            logger.Debug("ParseEnum");
            if( quote != '.')
                throw CreateExpressReaderException("Enumerations should be preceded and succeeded by a period, .");
            ReadStringIntoBuffer( quote );
            
            string text = _buffer.ToString();
            
            //FIXME is it possible in ISO-10303-21
            //that an enumeration value might be 'TRUE' or 'FALSE'
            //and therefore it will not necessarily be a boolean??
            if("TRUE".Equals( text.ToUpper() ) )
                SetToken(ExpressToken.Boolean, true);
            else if("FALSE".Equals( text.ToUpper() ) )
                SetToken(ExpressToken.Boolean, false);
            else
                SetToken(ExpressToken.Enumeration, text);
            
            _buffer.Position = 0;
        }
        
        private void ParseString( char quote ){
            ReadStringIntoBuffer(quote);
            string text = _buffer.ToString();
            _buffer.Position = 0;
            
            //HACK parse a date string
            if( text.Length == 19 && text[4] == '-' && text[7] == '-' && text[10] == 'T' && text[13] == ':' && text[16] == ':'){
                DateTime result;
                if(DateTime.TryParse(text, out result )){
                    SetToken(ExpressToken.Date, result );
                    return;
                }
            }
            SetToken(ExpressToken.String, text);
        }
        
        private bool ParseNull( char currentChar ){
            logger.Error("ParseNull is not implemented");
            throw new NotImplementedException("ParseNull");
        }
        
        private bool ParseIsoDefinition( char currentChar ){
            logger.Debug("ParseIsoDefinition");
            if(currentChar != 'I')
                throw CreateExpressReaderException("valid express files should begin with 'ISO_10303_21;'");
            
            currentChar = ParseUnquotedProperty( currentChar );
            
            string isoDef = _buffer.ToString();
            if( isoDef != "ISO-10303-21")
                throw CreateExpressReaderException("ISO declaration should be 'ISO-10303-21', but is instead {0}", isoDef );
            if(currentChar != ';')//FIXME should we allow for whitespace between the definition and the semicolon?
                throw CreateExpressReaderException("expect a semi-colon, ;, after the ISO_10303_21 declaration");
            
            SetToken(ExpressToken.StartExpress, isoDef);
            _buffer.Position = 0;
            
            return true;
        }
        
        private bool ParseNumber( char currentChar ){
            logger.Debug("ParseNumber");

            do
            {
                if (char.IsWhiteSpace(currentChar) || currentChar == ';' || currentChar == '(' || currentChar == '=' || currentChar == ',' || currentChar == ')') //FIXME move into a function
                {
                    break;
                }
                else if (char.IsDigit(currentChar)|| currentChar == '.' || currentChar == '-' || currentChar == 'E')
                {
                    _buffer.Append(currentChar);
                    if(PeekNext() == '(' || PeekNext() == ')' || PeekNext() == ',')//HACK
                        break;
                }
                else
                {
                    throw CreateExpressReaderException("Invalid identifier character: {0}. Line {1}, position {2}.", currentChar, _currentLineNumber, _currentLinePosition);
                }
            } while ((currentChar = MoveNext()) != '\0' || !_end);
            
            string number = _buffer.ToString();
            if(string.IsNullOrEmpty(number))
                throw CreateExpressReaderException("Tried to read a number, but it is null");
            
            double numberValue = 0;
            try{
                numberValue = Double.Parse(number);
            }catch(FormatException fe){
                throw CreateExpressReaderException("Failed to format number, {0}, which failed due to : {1}", number, fe.Message);
            }catch(OverflowException oe){
                throw CreateExpressReaderException("Tried to parse number, {0}, but an overflow exception was thrown : {1}", number, oe.Message);
            }
            
            if(number.IndexOf('.') != -1)
                SetToken(ExpressToken.Float, numberValue);
            else
                SetToken(ExpressToken.Integer, (int)numberValue);
            
            _buffer.Position = 0;
            return true;
        }
        
        private bool ParseLineIdentity( char currentChar ){
            currentChar = ParseUnquotedProperty( currentChar );
            
            string sectionName = _buffer.ToString();
            
            if(Peek() == ExpressTokenType.Entity || Peek() == ExpressTokenType.Array)
                SetToken(ExpressToken.LineReference, sectionName );
            else
                SetToken(ExpressToken.LineIdentifier, sectionName);
            _buffer.Position = 0;
            
            return true;
        }
        
        private bool ParseEntity( char currentChar ){
            logger.Debug("ParseValue");
            do
            {
                switch (currentChar)
                {
                    case '/':
                        ParseComment(currentChar);
                        return true;
                    case '(':
                        SetToken(ExpressToken.StartEntity);
                        return true;
                    case ' ':
                    case ExpressReader.Tab:
                    case ExpressReader.LineFeed:
                    case ExpressReader.CarriageReturn:
                        // eat
                        break;
                    default:
                        if (char.IsWhiteSpace(currentChar))
                        {
                            // eat
                        }
                        else
                        {
                            throw CreateExpressReaderException("Unexpected character encountered while parsing value: {0}. Line {1}, position {2}.", currentChar, _currentLineNumber, _currentLinePosition);
                        }
                        break;
                }
            } while ((currentChar = MoveNext()) != '\0' || !_end);

            return false;
        }
        
        private bool ParseEntityName( char currentChar ){
            logger.Debug("ParseEntityName");
            
            currentChar = ParseUnquotedProperty( currentChar );
            
            string sectionName = _buffer.ToString();
            
            SetToken(ExpressToken.EntityName, _buffer.ToString());
            _buffer.Position = 0;
            
            return true;
        }
        
        private bool ParseSectionName( char currentChar ){
            logger.Debug("ParseSectionName");
            if(currentChar != 'H'){
                if(currentChar != 'D')
                    throw CreateExpressReaderException("valid express files can only have sections 'HEADER' or 'DATA'");
            }
            
            currentChar = ParseUnquotedProperty( currentChar );
            
            string sectionName = _buffer.ToString();
            
            if(sectionName != "HEADER"){
                if(sectionName != "DATA")
                    throw CreateExpressReaderException("expect the section name to be 'HEADER' or 'DATA', but was instead {0}", sectionName);
            }
            if(currentChar != ';')
                throw CreateExpressReaderException("expect a semi-colon, ;, after the section declaration");
            
            SetToken(ExpressToken.StartSection, _buffer.ToString());
            _buffer.Position = 0;
            
            return true;
        }
        
        private bool ParseEndSection( char currentChar ){
            logger.Debug("ParseEndSection");
            if(currentChar != 'E'){
                throw CreateExpressReaderException("Sections can only be ended by calling ENDSEC;");
            }
            
            currentChar = ParseUnquotedProperty( currentChar );
            
            if(currentChar != ';')
                throw CreateExpressReaderException("expect a semi-colon, ;, after the section end");
            
            string text = _buffer.ToString();
            if("ENDSEC".Equals(text.ToUpper()))
                SetToken(ExpressToken.EndSection, text);
            else if("END-ISO-10303-21".Equals(text.ToUpper()))
                SetToken(ExpressToken.EndExpress, text);
            else
                throw CreateExpressReaderException("A section should be ended by ENDSEC or END-ISO-10303-21, and not {0}", text);
            
            _buffer.Position = 0;
            return true;
        }
        
        private bool ValidIdentifierChar(char value)
        {
            return (char.IsLetterOrDigit(value) || value == '_' || value == '$' || value == '-');
        }
        
        /// <summary>
        /// Parses a property name until it reaches whitespace, a semicolon or an opening parenthesis
        /// </summary>
        /// <param name="firstChar"></param>
        /// <returns></returns>
        private char ParseUnquotedProperty(char firstChar)
        {
            logger.Debug("ParseUnquotedProperty");
            _buffer.Append(firstChar);

            char currentChar;

            while ((currentChar = MoveNext()) != '\0' || !_end)
            {
                if (char.IsWhiteSpace(currentChar) || currentChar == ';' || currentChar == '(' || currentChar == '=' || currentChar == ',' || currentChar == ')') //FIXME move into a function
                {
                    return currentChar;
                }
                else if (ValidIdentifierChar(currentChar))
                {
                    _buffer.Append(currentChar);
                    if(PeekNext() == '(' || PeekNext() == ')') //HACK
                        return currentChar;
                }
                else
                {
                    throw CreateExpressReaderException("Invalid identifier character: {0}. Line {1}, position {2}.", currentChar, _currentLineNumber, _currentLinePosition);
                }
            }

            throw CreateExpressReaderException("Unexpected end when parsing unquoted property name. Line {0}, position {1}.", _currentLineNumber, _currentLinePosition);
        }
        
        private void ReadStringIntoBuffer(char quote)
        {
            while (true)
            {
                char currentChar = MoveNext();

                switch (currentChar)
                {
                    case '\0':
                        if (_end)
                            throw CreateExpressReaderException("Unterminated string. Expected delimiter: {0}. Line {1}, position {2}.", quote, _currentLineNumber, _currentLinePosition);

                        _buffer.Append('\0');
                        break;
                    case '\\':
                        if ((currentChar = MoveNext()) != '\0' || !_end)
                        {
                            switch (currentChar)
                            {
                                case 'b':
                                    _buffer.Append('\b');
                                    break;
                                case 't':
                                    _buffer.Append('\t');
                                    break;
                                case 'n':
                                    _buffer.Append('\n');
                                    break;
                                case 'f':
                                    _buffer.Append('\f');
                                    break;
                                case 'r':
                                    _buffer.Append('\r');
                                    break;
                                case '\\':
                                    _buffer.Append('\\');
                                    break;
                                case '"':
                                case '\'':
                                case '/':
                                    _buffer.Append(currentChar);
                                    break;
                                case 'u':
                                    char[] hexValues = new char[4];
                                    for (int i = 0; i < hexValues.Length; i++)
                                    {
                                        if ((currentChar = MoveNext()) != '\0' || !_end)
                                            hexValues[i] = currentChar;
                                        else
                                            throw CreateExpressReaderException("Unexpected end while parsing unicode character. Line {0}, position {1}.", _currentLineNumber, _currentLinePosition);
                                    }

                                    char hexChar = Convert.ToChar(int.Parse(new string(hexValues), NumberStyles.HexNumber, NumberFormatInfo.InvariantInfo));
                                    _buffer.Append(hexChar);
                                    break;
                                default:
                                    throw CreateExpressReaderException("Bad escape sequence: {0}. Line {1}, position {2}.", @"\" + currentChar, _currentLineNumber, _currentLinePosition);
                            }
                        }
                        else
                        {
                            throw CreateExpressReaderException("Unterminated string. Expected delimiter: {0}. Line {1}, position {2}.", quote, _currentLineNumber, _currentLinePosition);
                        }
                        break;
                    case '"':
                    case '\'':
                    case '.'://HACK added period, so that enumeration values can be treated as strings
                        if (currentChar == quote)
                        {
                            return;
                        }
                        else
                        {
                            _buffer.Append(currentChar);
                        }
                        break;
                    default:
                        _buffer.Append(currentChar);
                        break;
                }
            }
        }
        
        /// <summary>
        /// Skips the children of the current token.
        /// </summary>
        public void Skip()
        {
            if (IsStartToken(TokenType))
            {
                int depth = Depth;

                while (Read() && (depth < Depth))
                {
                }
            }
        }
        
        /// <summary>
        /// Sets the current token.
        /// </summary>
        /// <param name="newToken">The new token.</param>
        protected void SetToken(ExpressToken newToken)
        {
            SetToken(newToken, null);
        }

        /// <summary>
        /// Sets the current token and value.
        /// </summary>
        /// <param name="newToken">The new token.</param>
        /// <param name="value">The value.</param>
        protected virtual void SetToken(ExpressToken newToken, object value)
        {
            logger.Debug("Setting token : " + newToken.ToString() );
            _token = newToken;
            
            switch (newToken)
            {
                case ExpressToken.StartExpress:
                    _currentState = State.ExpressStart;
                    Push(ExpressTokenType.Express);
                    break;
                case ExpressToken.StartSection:
                    _currentState = State.SectionStart;
                    Push(ExpressTokenType.Section);
                    break;
                case ExpressToken.StartEntity:
                    _currentState = State.EntityStart;
                    Push(ExpressTokenType.Entity);
                    break;
                case ExpressToken.StartArray:
                    _currentState = State.ArrayStart;
                    Push(ExpressTokenType.Array);
                    break;
                case ExpressToken.EndExpress:
                    ValidateEnd(ExpressToken.EndExpress);
                    _currentState = State.Complete;
                    break;
                case ExpressToken.EndSection:
                    ValidateEnd(ExpressToken.EndSection);
                    _currentState = State.Express; //FIXME should this be State.PostValue
                    break;
                case ExpressToken.EndEntity:
                    ValidateEnd(ExpressToken.EndEntity);
                    _currentState = State.PostValue;
                    break;
                case ExpressToken.EndArray:
                    ValidateEnd(ExpressToken.EndArray);
                    _currentState = State.PostValue;
                    break;
                case ExpressToken.EntityName:
                    _currentState = State.EntityName;
                    break;
                case ExpressToken.Undefined:
                case ExpressToken.Integer:
                case ExpressToken.Float:
                case ExpressToken.Boolean:
                case ExpressToken.Null:
                case ExpressToken.Date:
                case ExpressToken.String:
                case ExpressToken.Enumeration:
                case ExpressToken.Overridden:
                    _currentState = State.PostValue;
                    break;
            }

            //ExpressTokenType current = Peek();
            //if (current == ExpressTokenType.Entity && _currentState == State.PostValue)
            //    Pop();

            if (value != null)
            {
                _value = value;
                _valueType = value.GetType();
            }
            else
            {
                _value = null;
                _valueType = null;
            }
        }
        
        /// <summary>
        /// Sets the state based on current token type.
        /// </summary>
        protected void SetStateBasedOnCurrent()
        {
            ExpressTokenType currentObject = Peek();

            switch (currentObject)
            {
                case ExpressTokenType.Express:
                    _currentState = State.Express;
                    break;
                case ExpressTokenType.Section:
                    _currentState = State.Section;
                    break;
                case ExpressTokenType.Entity:
                    _currentState = State.Entity;
                    break;
                case ExpressTokenType.Array:
                    _currentState = State.Array;
                    break;
                case ExpressTokenType.None:
                    _currentState = State.Complete;
                    break;
                default:
                    throw CreateExpressReaderException("While setting the reader state back to current object an unexpected ExpressType was encountered: {0}", currentObject);
            }
            logger.Debug("State has been set based on current, and is now " + _currentState.ToString() );
        }
        
        /// <summary>
        /// Moves the reader forward one position.
        /// The method handles line breaks and keeps track of the line number and position within that line.
        /// </summary>
        /// <returns></returns>
        private char MoveNext(){
            int value = _reader.Read();
            switch(value){
                case -1:
                    _end = true;
                    return '\0';
                case CarriageReturn:
                    if (_reader.Peek() == LineFeed)
                        _reader.Read();

                    _currentLineNumber++;
                    _currentLinePosition = 0;
                    break;
                case LineFeed:
                    _currentLineNumber++;
                    _currentLinePosition = 0;
                    break;
                default:
                    _currentLinePosition++;
                    break;
            }
            logger.Debug(String.Format(CultureInfo.InvariantCulture, "MoveNext : {0}", (char)value));
            return (char)value;
        }
        
        private bool HasNext()
        {
            return (_reader.Peek() != -1);
        }

        private int PeekNext()
        {
            return _reader.Peek();
        }
        
        internal static bool IsStartToken(ExpressToken token)
        {
            switch (token)
            {
                case ExpressToken.StartSection:
                case ExpressToken.StartEntity:
                case ExpressToken.StartArray:
                case ExpressToken.LineIdentifier:
                case ExpressToken.EntityName:
                    return true;
                case ExpressToken.None:
                case ExpressToken.Comment:
                case ExpressToken.Integer:
                case ExpressToken.Float:
                case ExpressToken.String:
                case ExpressToken.Boolean:
                case ExpressToken.Null:
                case ExpressToken.Undefined:
                case ExpressToken.EndEntity:
                case ExpressToken.EndArray:
                case ExpressToken.Date:
                case ExpressToken.EndSection:
                    return false;
                default:
                    throw new ArgumentOutOfRangeException("token", token, "Unexpected JsonToken value.");
            }
        }
        
        private void ValidateEnd(ExpressToken endToken)
        {
            ExpressTokenType currentObject = Pop();

            if (GetTypeForCloseToken(endToken) != currentObject)
                throw CreateExpressReaderException("ExpressToken {0} is not valid for closing ExpressType {1}.", endToken, currentObject);
        }
        
        private ExpressTokenType GetTypeForCloseToken(ExpressToken token)
        {
            switch (token)
            {
                case ExpressToken.EndExpress:
                    return ExpressTokenType.Express;
                case ExpressToken.EndSection:
                    return ExpressTokenType.Section;
                case ExpressToken.EndEntity:
                    return ExpressTokenType.Entity;
                case ExpressToken.EndArray:
                    return ExpressTokenType.Array;
                default:
                    throw CreateExpressReaderException("Not a valid close JsonToken: {0}", token);
            }
        }
        
        private ExpressToken GetCloseTokenForType(ExpressTokenType type){
            switch(type){
                case ExpressTokenType.Express:
                    return ExpressToken.EndExpress;
                case ExpressTokenType.Section:
                    return ExpressToken.EndSection;
                case ExpressTokenType.Entity:
                    return ExpressToken.EndEntity;
                case ExpressTokenType.Array:
                    return ExpressToken.EndArray;
                default:
                    throw CreateExpressReaderException("Not an ExpressTokenType which can be closed");
            }
        }
        
        private ExpressReaderException CreateExpressReaderException(string format, params object[] args){
            string message = String.Format(CultureInfo.InvariantCulture, format, args);
            logger.Error(message);
            return new ExpressReaderException(message, null, _currentLineNumber, _currentLinePosition);
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
        protected virtual void Dispose(bool disposing)
        {
            if (_currentState != State.Closed && disposing)
                Close();
        }
        
        /// <summary>
        /// Changes the <see cref="State"/> to Closed.
        /// </summary>
        public virtual void Close()
        {
            _currentState = State.Closed;
            _token = ExpressToken.None;
            _value = null;
            _valueType = null;
        }
    }
}
