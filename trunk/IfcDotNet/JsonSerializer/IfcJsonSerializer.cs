#region License
/*

Copyright 2011, Iain Sproat
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
using IfcDotNet.Schema;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Converters;

namespace IfcDotNet.JsonSerializer
{
	/// <summary>
	/// Serializes json data to iso10303 data
	/// </summary>
	public class IfcJsonSerializer : IfcSerializer
	{
		Newtonsoft.Json.JsonSerializer serializer;
		XmlContractResolver contractResolver;
		
		/// <summary>
		/// Default constructor
		/// </summary>
		public IfcJsonSerializer()
		{
			serializer = new Newtonsoft.Json.JsonSerializer();
			contractResolver = new XmlContractResolver();
			
			
			contractResolver.SuppressAnonymousTypes = true;
			serializer.ContractResolver = contractResolver;
			serializer.TypeNameHandling = TypeNameHandling.Objects;
			
		}
		
		/// <summary>
		/// Serializes an iso_10303 object to a TextWriter in Json format.
		/// </summary>
		/// <param name="writer"></param>
		/// <param name="iso10303"></param>
		public void Serialize(TextWriter writer, iso_10303 iso10303){
			
			serializer.Serialize(writer, iso10303);
		}
		
		/// <summary>
		/// Deserializes Json format data, as read in the TextReader, to an iso_10303 object
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public iso_10303 Deserialize(TextReader reader){
			JsonReader jsonReader = new JsonTextReader( reader );
			return serializer.Deserialize<iso_10303>( jsonReader );
		}
	}
}
