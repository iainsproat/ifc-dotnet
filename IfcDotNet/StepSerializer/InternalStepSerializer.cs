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

 */
#endregion
using System;
using System.Globalization;
using System.Collections.Generic;

namespace IfcDotNet.StepSerializer
{
    /// <summary>
    /// Description of InternalStepSerializer.
    /// </summary>
    internal class InternalStepSerializer
    {
        public InternalStepSerializer()
        {
        }
        
        public void Serialize(StepWriter writer, StepFile step){ //FIXME what about header information etc.?
            if(writer == null) throw new ArgumentNullException("writer");
            if(step == null) throw new ArgumentNullException("step");
            
            writer.WriteStartStep();
            SerializeHeader(writer, step.Header);
            SerializeData(writer, step.Data);
            writer.WriteEndStep();
        }
        
        private void SerializeHeader(StepWriter writer, IList<StepDataObject> header){
            if(writer == null) throw new ArgumentNullException("writer");
            if(header == null) throw new ArgumentNullException("header");
            
            writer.WriteStartHeader();
            foreach(StepDataObject sdo in header)
                SerializeObject( writer, sdo );
            writer.WriteEndSection();
        }
        
        private void SerializeData(StepWriter writer, IDictionary<int, StepDataObject> data){
            if(writer == null) throw new ArgumentNullException("writer");
            if(data == null) throw new ArgumentNullException("data");
            writer.WriteStartData();
            foreach(KeyValuePair<int, StepDataObject> kvp in data){
                SerializeEntity( writer, kvp.Key, kvp.Value );
            }
            writer.WriteEndSection();
        }
        
        private void SerializeEntity( StepWriter writer, int entityId, StepDataObject sdo ){
            if( writer == null ) throw new ArgumentNullException("writer");
            if( sdo == null ) throw new ArgumentNullException( "sdo" );
            writer.WriteLineIdentifier( entityId );
            SerializeObject( writer, sdo );
            writer.WriteEndLine();
        }
        
        private void SerializeObject(StepWriter writer, StepDataObject sdo ){
            if( writer == null ) throw new ArgumentNullException("writer");
            if(sdo == null ) throw new ArgumentNullException("sdo");
            if(String.IsNullOrEmpty( sdo.ObjectName )) throw new ArgumentNullException("sdo.ObjectName");
            writer.WriteObjectName( sdo.ObjectName );
            writer.WriteStartObject();
            foreach(StepValue sv in sdo.Properties){
                SerializeProperty(writer, sv);
            }
            writer.WriteEndObject();
        }
        
        private void SerializeProperty( StepWriter writer, StepValue sv){
            if(writer == null) throw new ArgumentNullException("writer");
            switch(sv.Token){
                case StepToken.StartArray:
                    //TODO assert that sv.ValueType.Equals(typeof(IList<StepValue>)
                    SerializeArray( writer, sv.Value as List<StepValue> );
                    break;
                case StepToken.String:
                    writer.WriteValue((string)sv.Value);
                    break;
                case StepToken.Date:
                    writer.WriteValue(((DateTime)sv.Value).ToString("yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture));
                    break;
                default:
                    throw new NotImplementedException(String.Format(CultureInfo.InvariantCulture,
                                                                    "SerializeProperty(StepValue) cannot, yet, handle token {0}",
                                                                    sv.Token.ToString()));
            }
        }
        
        private void SerializeArray( StepWriter writer, IList<StepValue> items ){
            if(writer == null) throw new ArgumentNullException("writer");
            if(items == null ) throw new ArgumentNullException("items");
            writer.WriteStartArray();
            foreach(StepValue sv in items){
                SerializeProperty( writer, sv );
            }
            writer.WriteEndArray();
        }
    }
}
