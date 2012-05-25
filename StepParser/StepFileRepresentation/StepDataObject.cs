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
using System.Collections.Generic;

namespace StepParser.StepFileRepresentation
{
	/// <summary>
	/// An ExpressDataObject is a representation of an IFC entity as entered in an IFC file.
	/// </summary>
	public class StepDataObject{
		private string _name;
		private IList<StepValue> _properties = new List<StepValue>();
		
		/// <summary>
		/// The Step entity has a name, which represents an IFC Entity and a class in the object model
		/// </summary>
		public string ObjectName{
			get{return this._name;}
			set{ this._name = value;}
		}
		
		/// <summary>
		/// The properties as given in the STEP format.
		/// </summary>
		public IList<StepValue> Properties{
			get{ return this._properties; }
		}
		
		public StepDataObject(){}
		
		public StepDataObject(string objName, params StepValue[] values){
			this._name = objName;
			if(values == null)
				return;
			foreach(StepValue val in values){
				this._properties.Add(val);
			}
		}
		
		
		public override string ToString()
        {
		   string props = String.Empty;
		   for(int i = 0; i < this._properties.Count; i++){
		       props += this._properties[i].ToString();
		       if(i < this.Properties.Count-1)
		           props += ", ";
		   }
		    return string.Format("[StepDataObject Name={0}, Properties=[{1}]]", _name, props);
        }

	}
}
