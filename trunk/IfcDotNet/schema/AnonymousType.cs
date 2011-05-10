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
using System.Collections.Generic;
using System.Xml.Serialization;
using IfcDotNet.Schema;

namespace IfcDotNet.Schema
{
	/// <summary>
	/// An XSD generated type which wraps the actual underlying type required by IFC
	/// </summary>
	public interface IAnonymousType<T, K> where K : IAnonymousType<T, K>, new()
	{
		/// <summary>
		/// The underlying item required by IFC
		/// </summary>
		T Item{ get; set; }
	}
	
	/// <summary>
	/// 
	/// </summary>
	public abstract class AnonymousType<T, K> : IAnonymousType<T, K> where K : AnonymousType<T, K>, new()
	{
		/// <summary>
		/// Underlying item
		/// </summary>
		[XmlIgnore()]
		public abstract T Item{ get; set; }
		
		/// <summary>
		/// Explicit operator casts AnonymousType to T
		/// </summary>
		/// <param name="vt"></param>
		/// <returns></returns>
		public static explicit operator T(AnonymousType<T, K> vt){
			if(vt == null)
				return default(T);
			return vt.Item;
		}
		
		/// <summary>
		/// Implicit operator casts T to AnonymousType
		/// </summary>
		/// <param name="b"></param>
		/// <returns></returns>
		public static implicit operator AnonymousType<T, K>(T b){
			K vt = new K();
			vt.Item = b;
			return vt;
		}
	}
	
	
	

	

	
	
}
