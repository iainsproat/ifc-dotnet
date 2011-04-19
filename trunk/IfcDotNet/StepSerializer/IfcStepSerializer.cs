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
using System.Xml.Serialization;
using System.Globalization;
using System.Collections.Generic;
using System.Reflection;
using IfcDotNet.Schema;
using IfcDotNet.StepSerializer;
using IfcDotNet.StepSerializer.Utilities;
using log4net;

namespace IfcDotNet.StepSerializer
{
    /// <summary>
    /// Reads IFC data in STEP (10303 part 21) format.
    /// </summary>
    public class IfcStepSerializer
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(IfcStepSerializer));
        
        private InternalStepDeserializer _internalDeserializer;
        private InternalStepSerializer _internalSerializer;
        private StepBinder _binder;
        
        /// <summary>
        /// Default constructor
        /// </summary>
        public IfcStepSerializer(){
        }
        
        /// <summary>
        /// Serializes an iso_10303 object into STEP format, writing the data to the given StepWriter
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="iso10303"></param>
        public void Serialize(StepWriter writer, iso_10303 iso10303){
            if(writer == null)
                throw new ArgumentNullException("writer");
            if(iso10303 == null)
                throw new ArgumentNullException("iso10303");
            
            this._internalSerializer = new InternalStepSerializer();
            
            //TODO
            //Convert from iso_10303 to StepDataObjects
            StepFile stepFile = this._binder.Extract( iso10303 );
            //use the InternalStepSerializer to write StepDataObjects to the StepWriter
            this._internalSerializer.Serialize(writer, stepFile );
        }
        
        /// <summary>
        /// Deserializes STEP format data by reading stream provided by a StreamReader.  An iso_10303 object is returned.
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public iso_10303 Deserialize(StepReader reader)
        {
            if( reader == null )
                throw new ArgumentNullException( "reader" );
            
            this._internalDeserializer = new InternalStepDeserializer();
            this._binder = new StepBinder();
            StepFile stepFile = this._internalDeserializer.Deserialize(reader);
            
            iso_10303 iso10303 = this._binder.Bind( stepFile );
            return iso10303;
        }
        

        
    }
}
