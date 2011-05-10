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

namespace IfcDotNet.Schema
{
	/// <summary>
	/// An XSD generated type which wraps the actual underlying type required by IFC
	/// </summary>
	public interface IArrayWrapper<T, K> where K : IArrayWrapper<T, K>, new()
	{
		/// <summary>
		/// wrapped array
		/// </summary>
		T[] Items{ get; set; }
		
		/// <summary>
		/// length of wrapped array
		/// </summary>
		int Length{ get; }
	}
	
	/// <summary>
	/// 
	/// </summary>
	public abstract class ArrayWrapper<T, K> : IArrayWrapper<T, K> where K : ArrayWrapper<T, K>, new()
	{
		/// <summary>
		/// wrapped array
		/// </summary>
		[XmlIgnore()]
		public abstract T[] Items{ get; set; }
		
		/// <summary>
		/// Length of the wrapped array
		/// </summary>
		[XmlIgnore()]
		public int Length{
			get{ CheckForNullWrappedArray();
				return this.Items.Length; }
		}
		
		/// <summary>
		/// Explicit operator casts ValueType to T[]
		/// </summary>
		/// <param name="vtaw"></param>
		/// <returns></returns>
		public static explicit operator T[](ArrayWrapper<T, K> vtaw){
			if(vtaw == null)
				return null;
			return vtaw.Items;
		}
		
		/// <summary>
		/// Implicit operator casts T[] to ValueType
		/// </summary>
		/// <param name="b"></param>
		/// <returns></returns>
		public static implicit operator ArrayWrapper<T, K>(T[] b){
			K vt = new K();
			vt.Items = b;
			return vt;
		}
		
		/// <summary>
		/// Explicit operator casts ValueType to List of T
		/// </summary>
		/// <param name="vtaw"></param>
		/// <returns></returns>
		public static explicit operator List<T>(ArrayWrapper<T, K> vtaw){
			if(vtaw == null)
				return null;
			if(vtaw.Items == null)
				return new List<T>(0);
			return new List<T>(vtaw.Items);
		}
		
		/// <summary>
		/// Implicit operator casts a List of T to ValueType
		/// </summary>
		/// <param name="b"></param>
		/// <returns></returns>
		public static implicit operator ArrayWrapper<T, K>(List<T> b){
			K aw = new K();
			if(b != null)
				aw.Items = b.ToArray();
			return aw;
		}
		
		/// <summary>
		/// Allows indexing of the ArrayWrapper
		/// </summary>
		public T this[int index]{
			get{CheckForNullWrappedArray();
				return this.Items[index];
			}
			set{CheckForNullWrappedArray();
				this.Items[index] = value;
			}
		}
		
		/// <summary>
		/// Creates a zero length wrapped array if it does not already exist
		/// </summary>
		protected void CheckForNullWrappedArray(){
			if(this.Items == null)
				this.Items = new T[0];
		}
	}
	
	/// <summary>
	/// generic parameter M is the underlying type of the ValueType
	/// generic parameter T is the ValueType, the underlying type of the ArrayWrapper
	/// generic parameter K is the implementing class
	/// </summary>
	public interface IValueTypeArrayWrapper<M, T, K> : IArrayWrapper<T, K> where T : IValueType<M, T>, new() where K : IValueTypeArrayWrapper<M, T, K>, new()
	{
		
	}
	
	/// <summary>
	/// generic parameter M is the underlying type of the ValueType
	/// generic parameter T is the ValueType, the underlying type of the ArrayWrapper
	/// generic parameter K is the implementing class
	/// </summary>
	public abstract class ValueTypeArrayWrapper<M, T, K> : ArrayWrapper<T, K>, IValueTypeArrayWrapper<M, T, K>  where T : IValueType<M, T>, new() where K : ValueTypeArrayWrapper<M, T, K>, new()
	{
		/// <summary>
		/// Explicit operator casts ValueType to T[]
		/// </summary>
		/// <param name="vtaw"></param>
		/// <returns></returns>
		public static explicit operator M[](ValueTypeArrayWrapper<M, T, K> vtaw){
			if(vtaw == null)
				return null;
			if(vtaw.Items == null)
				return new M[0];
			M[] m = new M[vtaw.Length];
			for(int i = 0; i < vtaw.Length; i++){
				m[i] = vtaw[i].Value;
			}
			return m;
		}
		
		/// <summary>
		/// Implicit operator casts T[] to ValueType
		/// </summary>
		/// <param name="b"></param>
		/// <returns></returns>
		public static implicit operator ValueTypeArrayWrapper<M, T, K>(M[] b){
			K vtaw = new K();
			if(b == null){
				vtaw.Items = new T[0];
				return vtaw;
			}
			vtaw.Items = new T[b.Length];
			for(int i = 0; i < b.Length; i++){
				vtaw.Items[i] = new T();
				vtaw.Items[i].Value = b[i];
			}
			return vtaw;
		}
	}
}
