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
using System.Globalization;
using System.Xml.Serialization;

using log4net;

namespace IfcDotNet.Schema
{
	/// <summary>
	/// A non-generic interface for easy identification
	/// of Types which derive from IValueType.
	/// </summary>
	public interface IValueTypeBase{
		
	}
	/// <summary>
	/// An XSD generated type which wraps the actual underlying type required by IFC
	/// </summary>
	public interface IValueType<T, K> : IValueTypeBase where K : IValueType<T, K>, new()
	{
		/// <summary>
		/// Underlying value of this data type
		/// </summary>
		T Value{ get; set; }
		
		/// <summary>
		/// Handles the conversion of the Value to a String.
		/// Used in XmlSerialization
		/// </summary>
		string ValueAsString{ get; set; }
	}
	
	/// <summary>
	/// An XSD generate type which wraps the actual underlying type required by IFC
	/// </summary>
	public abstract class ValueType<T, K> : IValueType<T, K>, IEquatable<ValueType<T, K>> where K : ValueType<T, K>, new()
	{
		private static ILog logger = LogManager.GetLogger(typeof(ValueType<T, K>));
		/// <summary>
		/// Underlying value
		/// </summary>
		[XmlIgnore()]
		public abstract T Value{ get; set; }
		
		/// <summary>
		/// Handles the conversion of the Value to string
		/// </summary>
		[XmlText()]
		public string ValueAsString{
			get{
				return this.ToString();
			}
			set{
				/*logger.Debug(String.Format(CultureInfo.InvariantCulture,
				                           "Setting ValueAsString. Attempting to convert value \"{0}\" to type of {1}",
				                           value, typeof(T).FullName));
				*/
				this.Value = (T)Convert.ChangeType(value, typeof(T), null);
			}
		}
		
		#region Equals and GetHashCode implementation
		/// <summary>
		/// Determines whether the provided object is equal to this ValueType
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public override bool Equals(object obj)
		{
			ValueType<T,K> other = obj as ValueType<T, K>;
			if (other == null)
				return false;
			return this.Equals(other);
		}
		
		/// <summary>
		/// Determines whether the provided ValueType is equal to this ValueType
		/// </summary>
		/// <param name="other"></param>
		/// <returns></returns>
		public bool Equals(ValueType<T, K> other){
			if(other == null)
				return false;
			return this.Value.Equals(other.Value);
		}
		
		/// <summary>
		/// The hashcode of the ValueType
		/// </summary>
		/// <returns></returns>
		public override int GetHashCode()
		{
			int hashCode = 44;
			hashCode += this.Value.GetHashCode();
			return hashCode;
		}
		
		/// <summary>
		/// Equality operator
		/// </summary>
		/// <param name="lhs"></param>
		/// <param name="rhs"></param>
		/// <returns></returns>
		public static bool operator ==(ValueType<T,K> lhs, ValueType<T,K> rhs)
		{
			if (ReferenceEquals(lhs, rhs))
				return true;
			if (ReferenceEquals(lhs, null) || ReferenceEquals(rhs, null))
				return false;
			return lhs.Equals(rhs);
		}
		
		/// <summary>
		/// Inequality operator
		/// </summary>
		/// <param name="lhs"></param>
		/// <param name="rhs"></param>
		/// <returns></returns>
		public static bool operator !=(ValueType<T,K> lhs, ValueType<T,K> rhs)
		{
			return !(lhs == rhs);
		}
		#endregion

		/// <summary>
		/// NOT TO BE USED. REQUIRED FOR SIMPLEELEMENT HACK IN XMLSERIALIZATION
		/// </summary>
		public object _dummy; //HACK
		
		/// <summary>
		/// Converts this type to a string
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return this.Value.ToString();
		}

		
		/// <summary>
		/// Explicit operator casts ValueType to T
		/// </summary>
		/// <param name="vt"></param>
		/// <returns></returns>
		public static explicit operator T(ValueType<T, K> vt){
			if(vt == null)
				return default(T);
			return vt.Value;
		}
		
		/// <summary>
		/// Implicit operator casts T to ValueType
		/// </summary>
		/// <param name="b"></param>
		/// <returns></returns>
		public static implicit operator ValueType<T, K>(T b){
			K vt = new K();
			vt.Value = b;
			return vt;
		}
	}
}
