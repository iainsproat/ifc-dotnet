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

namespace IfcDotNet.Schema
{
		/// <summary>
	/// A ValueType where the wrapped value is of type double
	/// </summary>
	public abstract class DoubleValueType<K> : ValueType<double, K> where K : DoubleValueType<K>, new()
	{
		/// <summary>
		/// Unary + operator
		/// </summary>
		/// <param name="a"></param>
		/// <returns></returns>
		public static DoubleValueType<K> operator +(DoubleValueType<K> a){
			if(a == null)
				return null;
			a.Value = +a.Value;
			return a;
		}
		
		/// <summary>
		/// Inverts the value of the DoubleValueType
		/// </summary>
		/// <param name="a"></param>
		/// <returns></returns>
		public static DoubleValueType<K> operator -(DoubleValueType<K> a){
			if(a == null)
				return null;
			a.Value = -a.Value;
			return a;
		}
		
		/// <summary>
		/// Increments the DoubleValueType
		/// </summary>
		/// <param name="a"></param>
		/// <returns></returns>
		public static DoubleValueType<K> operator ++(DoubleValueType<K> a){
			if(a == null)
				return null;
			a.Value++;
			return a;
		}
		
		/// <summary>
		/// Decrements the DoubleValueType
		/// </summary>
		/// <param name="a"></param>
		/// <returns></returns>
		public static DoubleValueType<K> operator --(DoubleValueType<K> a){
			if(a == null)
				return null;
			a.Value--;
			return a;
		}
		
		/// <summary>
		/// Adds two DoubleValueType together
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static DoubleValueType<K> operator +(DoubleValueType<K> a, DoubleValueType<K> b){
			if(a == null || b == null)
				return null;
			K k = new K();
			k.Value = a.Value + b.Value;
			return k;
		}
		
		/// <summary>
		/// Subtracts one DoubleValueType from the other
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static DoubleValueType<K> operator -(DoubleValueType<K> a, DoubleValueType<K> b){
			if(a == null || b == null)
				return null;
			K k = new K();
			k.Value = a.Value - b.Value;
			return k;
		}
		
		/// <summary>
		/// Multiplies two DoubleValueType together
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static DoubleValueType<K> operator *(DoubleValueType<K> a, DoubleValueType<K> b){
			if(a == null || b == null)
				return null;
			K k = new K();
			k.Value = a.Value * b.Value;
			return k;
		}
		
		/// <summary>
		/// Divides one DoubleValueType by the other
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static DoubleValueType<K> operator /(DoubleValueType<K> a, DoubleValueType<K> b){
			if(a == null || b == null)
				return null;
			K k = new K();
			k.Value = a.Value / b.Value;
			return k;
		}
		
		/// <summary>
		/// Calculates the modulo of one DoubleValueType by the other
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static DoubleValueType<K> operator %(DoubleValueType<K> a, DoubleValueType<K> b){
			if(a == null || b == null)
				return null;
			K k = new K();
			k.Value = a.Value % b.Value;
			return k;
		}
		
		/// <summary>
		/// Determines whether one DoubleValueType is less than the other
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static bool operator <(DoubleValueType<K> a, DoubleValueType<K> b){
			if(a == null || b == null)
				return false;
			return a.Value < b.Value;
		}
		
		/// <summary>
		/// Determines whether one DoubleValueType is greater than the other
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static bool operator >(DoubleValueType<K> a, DoubleValueType<K> b){
			if(a == null || b == null)
				return false;
			return a.Value > b.Value;
		}
		
		/// <summary>
		/// Determines whether one DoubleValueType is less than or equal the other
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static bool operator <=(DoubleValueType<K> a, DoubleValueType<K> b){
			if(a == null || b == null)
				return false;
			return a.Value <= b.Value;
		}
		
		/// <summary>
		/// Determines whether one DoubleValueType is greater than or equal the other
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static bool operator >=(DoubleValueType<K> a, DoubleValueType<K> b){
			if(a == null || b == null)
				return false;
			return a.Value >= b.Value;
		}
		
		/// <summary>
		/// Adds a DoubleValueType and double together
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static DoubleValueType<K> operator +(DoubleValueType<K> a, double b){
			if(a == null)
				return null;
			K k = new K();
			k.Value = a.Value + b;
			return k;
		}
		
		/// <summary>
		/// Subtracts a double from a DoubleValueType
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static DoubleValueType<K> operator -(DoubleValueType<K> a, double b){
			if(a == null)
				return null;
			K k = new K();
			k.Value = a.Value - b;
			return k;
		}
		
		/// <summary>
		/// Multiplies a DoubleValueType by a double
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static DoubleValueType<K> operator *(DoubleValueType<K> a, double b){
			if(a == null)
				return null;
			K k = new K();
			k.Value = a.Value * b;
			return k;
		}
		
		/// <summary>
		/// Divides a DoubleValueType by a double
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static DoubleValueType<K> operator /(DoubleValueType<K> a, double b){
			if(a == null)
				return null;
			K k = new K();
			k.Value = a.Value / b;
			return k;
		}
		
		/// <summary>
		/// Calculates the modulo of a DoubleValueType divided by a double
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static DoubleValueType<K> operator %(DoubleValueType<K> a, double b){
			if(a == null)
				return null;
			K k = new K();
			k.Value = a.Value % b;
			return k;
		}
		
		/// <summary>
		/// Determines whether a DoubleValueType is less than a double
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static bool operator <(DoubleValueType<K> a, double b){
			if(a == null)
				return false;
			return a.Value < b;
		}
		
		/// <summary>
		/// Determines whether a DoubleValueType is greater than a double
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static bool operator >(DoubleValueType<K> a, double b){
			if(a == null)
				return false;
			return a.Value > b;
		}
		
		/// <summary>
		/// Determines whether a DoubleValueType is less than or equal a double
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static bool operator <=(DoubleValueType<K> a, double b){
			if(a == null)
				return false;
			return a.Value <= b;
		}
		
		/// <summary>
		/// Determines whether a DoubleValueType is greater than or equal a double
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static bool operator >=(DoubleValueType<K> a, double b){
			if(a == null)
				return false;
			return a.Value >= b;
		}
		
		/// <summary>
		/// Adds a DoubleValueType and double together
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static DoubleValueType<K> operator +(double a, DoubleValueType<K> b){
			if(b == null)
				return null;
			K k = new K();
			k.Value = a + b.Value;
			return k;
		}
		
		/// <summary>
		/// Subtracts a DoubleValueType from a double
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static DoubleValueType<K> operator -(double a, DoubleValueType<K> b){
			if(b == null)
				return null;
			K k = new K();
			k.Value = a - b.Value;
			return k;
		}
		
		/// <summary>
		/// Multiplies a DoubleValueType and a double together
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static DoubleValueType<K> operator *(double a, DoubleValueType<K> b){
			if(b == null)
				return null;
			K k = new K();
			k.Value = a * b.Value;
			return k;
		}
		
		/// <summary>
		/// Divides a double by a DoubleValueType
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static DoubleValueType<K> operator /(double a, DoubleValueType<K> b){
			if(b == null)
				return null;
			K k = new K();
			k.Value = a / b.Value;
			return k;
		}
		
		/// <summary>
		/// Calculates the modulo of a double divided by a DoubleValueType
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static DoubleValueType<K> operator %(double a, DoubleValueType<K> b){
			if(b == null)
				return null;
			K k = new K();
			k.Value = a % b.Value;
			return k;
		}
		
		/// <summary>
		/// Determines whether a double is less than a DoubleValueType
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static bool operator <(double a, DoubleValueType<K> b){
			if(b == null)
				return false;
			return a < b.Value;
		}
		
		/// <summary>
		/// Determines whether a double is greater than a DoubleValueType
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static bool operator >(double a, DoubleValueType<K> b){
			if(b == null)
				return false;
			return a > b.Value;
		}
		
		/// <summary>
		/// Determines whether a double is less than or equal to a DoubleValueType
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static bool operator <=(double a, DoubleValueType<K> b){
			if(b == null)
				return false;
			return a <= b.Value;
		}
		
		/// <summary>
		/// Determines whether a double is greater than or equal to a DoubleValueType
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static bool operator >=(double a, DoubleValueType<K> b){
			if(b == null)
				return false;
			return a >= b.Value;
		}
	}
}

