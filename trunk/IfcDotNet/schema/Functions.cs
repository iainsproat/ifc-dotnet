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
	/// Functions is a class to hold all functions defined in the IFC specification
	/// </summary>
	public class Functions
	{
		/// <summary>
		/// Definition from ISO/CD 10303-41:1992: The function returns the dimensional exponents of the given SI-unit.
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public static IfcDimensionalExponents IfcDimensionsForSiUnit(IfcSIUnitName name){
			
			switch(name){
				case IfcSIUnitName.metre:
					return new IfcDimensionalExponents(1, 0, 0, 0, 0, 0, 0);
				case IfcSIUnitName.square_metre:
					return new IfcDimensionalExponents(2, 0, 0, 0, 0, 0, 0);
				case IfcSIUnitName.cubic_metre:
					return new IfcDimensionalExponents(3, 0, 0, 0, 0, 0, 0);
				case IfcSIUnitName.gram:
					return new IfcDimensionalExponents(0, 1, 0, 0, 0, 0, 0);
				case IfcSIUnitName.second:
					return new IfcDimensionalExponents(0, 0, 1, 0, 0, 0, 0);
				case IfcSIUnitName.ampere:
					return new IfcDimensionalExponents(0, 0, 0, 1, 0, 0, 0);
				case IfcSIUnitName.kelvin:
					return new IfcDimensionalExponents(0, 0, 0, 0, 1, 0, 0);
				case IfcSIUnitName.mole:
					return new IfcDimensionalExponents(0, 0, 0, 0, 0, 1, 0);
				case IfcSIUnitName.candela:
					return new IfcDimensionalExponents(0, 0, 0, 0, 0, 0, 1);
				case IfcSIUnitName.radian:
					return new IfcDimensionalExponents(0, 0, 0, 0, 0, 0, 0);
				case IfcSIUnitName.steradian:
					return new IfcDimensionalExponents(0, 0, 0, 0, 0, 0, 0);
				case IfcSIUnitName.hertz:
					return new IfcDimensionalExponents(0, 0, -1, 0, 0, 0, 0);
				case IfcSIUnitName.newton:
					return new IfcDimensionalExponents(1, 1, -2, 0, 0, 0, 0);
				case IfcSIUnitName.pascal:
					return new IfcDimensionalExponents(-1, 1, -2, 0, 0, 0, 0);
				case IfcSIUnitName.joule:
					return new IfcDimensionalExponents(2, 1, -2, 0, 0, 0, 0);
				case IfcSIUnitName.watt:
					return new IfcDimensionalExponents(2, 1, -3, 0, 0, 0, 0);
				case IfcSIUnitName.coulomb:
					return new IfcDimensionalExponents(0, 0, 1, 1, 0, 0, 0);
				case IfcSIUnitName.volt:
					return new IfcDimensionalExponents(2, 1, -3, -1, 0, 0, 0);
				case IfcSIUnitName.farad:
					return new IfcDimensionalExponents(-2, -1, 4, 1, 0, 0, 0);
				case IfcSIUnitName.ohm:
					return new IfcDimensionalExponents(2, 1, -3, -2, 0, 0, 0);
				case IfcSIUnitName.siemens:
					return new IfcDimensionalExponents(-2, -1, 3, 2, 0, 0, 0);
				case IfcSIUnitName.weber:
					return new IfcDimensionalExponents(2, 1, -2, -1, 0, 0, 0);
				case IfcSIUnitName.tesla:
					return new IfcDimensionalExponents(0, 1, -2, -1, 0, 0, 0);
				case IfcSIUnitName.henry:
					return new IfcDimensionalExponents(2, 1, -2, -2, 0, 0, 0);
				case IfcSIUnitName.degree_celsius:
					return new IfcDimensionalExponents(0, 0, 0, 0, 1, 0, 0);
				case IfcSIUnitName.lumen:
					return new IfcDimensionalExponents(0, 0, 0, 0, 0, 0, 1);
				case IfcSIUnitName.lux:
					return new IfcDimensionalExponents(-2, 0, 0, 0, 0, 0, 1);
				case IfcSIUnitName.becquerel:
					return new IfcDimensionalExponents(0, 0, -1, 0, 0, 0, 0);
				case IfcSIUnitName.gray:
					return new IfcDimensionalExponents(2, 0, -2, 0, 0, 0, 0);
				case IfcSIUnitName.sievert:
					return new IfcDimensionalExponents(2, 0, -2, 0, 0, 0, 0);
				default:
					return new IfcDimensionalExponents(0, 0, 0, 0, 0, 0, 0);
			}
			
		}
	}
}
