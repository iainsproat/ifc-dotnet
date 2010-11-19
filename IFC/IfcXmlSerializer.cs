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
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Globalization;

using log4net;

namespace IfcDotNet
{
    /// <summary>
    /// IfcXmlSerializer will serialize or deserialize valid IfcXml.
    /// </summary>
    public class IfcXmlSerializer
    {
        /// <summary>
        /// logger
        /// </summary>
        private static readonly ILog logger = LogManager.GetLogger(typeof(IfcXmlSerializer));
        
        /// <summary>
        /// 
        /// </summary>
        private XmlSerializer serializer;
        
        /// <summary>
        /// .ctor
        /// </summary>
        public IfcXmlSerializer()
        {
            serializer = new XmlSerializer(typeof(iso_10303_28));
        }

        /// <summary>
        /// Deserialize an IfcXml file to an iso_10303_28 object
        /// </summary>
        /// <param name="reader"></param>
        /// <exception cref="XmlException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <returns></returns>
        public iso_10303_28 Deserialize(TextReader reader){
            if(reader == null)
                throw new ArgumentNullException("reader");
            
            iso_10303_28 deserializedUos;
            IfcXmlValidator validator;
            try{
                validator = new IfcXmlValidator( reader );
                
                deserializedUos = (iso_10303_28)serializer.Deserialize( validator.Reader );
                
            }catch(XmlException xe){
                logger.Error( String.Format(CultureInfo.InvariantCulture, "Deserialize failed: {0}", xe.Message) );
                throw;
            }catch(InvalidOperationException ioe){
                logger.Error(String.Format(CultureInfo.InvariantCulture, "Deserialize failed: {0}", ioe.Message));
                throw;
            }finally{
                reader.Close();
            }
            
            if(!validator.IsValid){
                StringBuilder warningBuilder = new StringBuilder();
                foreach(String warning in validator.Messages){
                    warningBuilder.AppendLine( warning );
                }
                throw new XmlException(String.Format(CultureInfo.InvariantCulture, "Not valid IfcXml: {0}", warningBuilder.ToString()));
                
            }
            return deserializedUos;
        }
        
        /// <summary>
        /// Writes the passed object as IfcXml.
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="iso10303"></param>
        public void Serialize(TextWriter writer, iso_10303_28 iso10303){
            if(writer == null)
                throw new ArgumentNullException("writer");
            if(iso10303 == null)
                throw new ArgumentNullException("iso10303");
            //TODO validate that iso10303 is valid IFC
            
            serializer.Serialize(writer, iso10303, getIfcNamespaces());
            writer.Close();
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="iso10303"></param>
        /// <returns></returns>
        public string SerializeToString( iso_10303_28 iso10303 ){
            if( iso10303 == null )
                throw new ArgumentNullException("iso10303");
            
            StringBuilder sb = new StringBuilder();
            StringWriter writer = new StringWriter( sb, CultureInfo.InvariantCulture );
            Serialize(writer, iso10303);
            return sb.ToString();
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static XmlSerializerNamespaces getIfcNamespaces()
        {
            XmlSerializerNamespaces xsn = new XmlSerializerNamespaces();
            xsn.Add("xsi",@"http://www.w3.org/2001/XMLSchema-instance");
            xsn.Add("ex",@"urn:iso.org:standard:10303:part(28):version(2):xmlschema:common");
            xsn.Add("xlink",@"http://www.w3.org/1999/xlink");
            xsn.Add("",@"http://www.iai-tech.org/ifcXML/IFC2x3/FINAL");
            return xsn;
        }
    }
}
