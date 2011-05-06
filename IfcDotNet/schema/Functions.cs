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

namespace IfcDotNet.Schema
{
	/// <summary>
	/// Functions is a class to hold all functions defined in the IFC specification
	/// </summary>
	public class Functions
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="arg1"></param>
		/// <param name="arg2"></param>
		/// <returns>arg1 is returned unless arg1 is null, otherwise arg2</returns>
		public static object NVL(object arg1, object arg2){
			return NVL<object, object, object>(arg1, arg2);
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="arg1"></param>
		/// <param name="arg2"></param>
		/// <returns>arg1 is returned unless arg1 is null, otherwise arg2</returns>
		public static T NVL<T>(T arg1, T arg2) where T : class
		{
			return NVL<T, T, T>(arg1, arg2);
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="arg1"></param>
		/// <param name="arg2"></param>
		/// <returns>arg1 is returned unless arg1 is null, otherwise arg2</returns>
		public static K NVL<K, L, M>(L arg1, M arg2) where L : class, K where M : class, K
		{
			if(arg1 != null)
				return arg1;
			return arg2;
		}
		
		/// <summary>
		/// This function returns two orthogonal directions. u[1] is in the direction of ref_direction and u[2] is perpendicular to u[1].
		/// A default value of (1.0,0.0,0.0) is supplied for ref_direction if the input data is incomplete.
		/// </summary>
		/// <param name="refDirection"></param>
		/// <returns></returns>
		public static IList<IfcDirection> IfcBuild2Axes(IfcDirection refDirection){
			IfcDirection D = NVL<IfcDirection>(IfcNormalise(refDirection), new IfcDirection(1, 0));
			IList<IfcDirection> axes = new List<IfcDirection>(2);
			axes.Add(D);
			axes.Add(IfcOrthogonalComplement(D));
			return axes;
		}
		
		
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
		
		/// <summary>
		/// This function returns a vector whose components are normalized to have a sum of squares of 1.0.
		/// If the input argument is not defined or of zero length then null is returned.
		/// </summary>
		/// <param name="arg"></param>
		/// <returns></returns>
		public static IfcVector IfcNormalise(IfcVector arg){
			if(arg == null) throw new ArgumentNullException("arg");
			IfcDimensionCount1 Ndim;
			IfcDirection V = new IfcDirection(1, 0);
			IfcVector Vec = new IfcVector( new IfcDirection(1, 0), 1);
			doublewrapper Mag;
			
			Ndim = arg.Dim;
			V.DirectionRatios = arg.Orientation.Item.DirectionRatios;
			Vec.Magnitude = arg.Magnitude;
			Vec.Orientation = V;
			if(arg.Magnitude == 0)
				return null;
			Vec.Magnitude = 1;
			
			Mag = 0;
			for(int i =0; i < Ndim; i++){
				Mag += V.DirectionRatios[i] * V.DirectionRatios[i];
			}
			
			if(Mag <= 0)
				return null;
			
			Mag = Math.Sqrt((double)Mag);
			for(int i = 0; i < Ndim; i++){
				V.DirectionRatios[i] /= Mag;
			}
			
			Vec.Orientation = V;
			return Vec;
		}
		
		/// <summary>
		/// This function returns a direction whose components are normalized to have a sum of squares of 1.0.
		/// If the input argument is not defined or of zero length then null is returned.
		/// </summary>
		/// <param name="arg"></param>
		/// <returns></returns>
		public static IfcDirection IfcNormalise(IfcDirection arg){
			if(arg == null) throw new ArgumentNullException("arg");
			IfcDimensionCount1 Ndim;
			IfcDirection V = new IfcDirection(1, 0);
			doublewrapper Mag;

			Ndim = arg.Dim;
			V.DirectionRatios = arg.DirectionRatios;
			
			Mag = 0;
			for(int i =0; i < Ndim; i++){
				Mag += V.DirectionRatios[i] * V.DirectionRatios[i];
			}
			
			if(Mag <= 0)
				return null;
			
			Mag = Math.Sqrt((double)Mag);
			for(int i = 0; i < Ndim; i++){
				V.DirectionRatios[i] /= Mag;
			}
			
			return V;
		}
		
		/// <summary>
		/// This function returns a direction which is the orthogonal complement of the input direction.
		/// The input direction must be a two-dimensional direction and the result is a vector of the same type and perpendicular to the input vector.
		/// </summary>
		/// <param name="Vec"></param>
		/// <returns>Null if the parameter Vec is null, or if Vec is not of 2 dimensions.</returns>
		public static IfcDirection IfcOrthogonalComplement(IfcDirection Vec){
			if(Vec == null || Vec.Dim != 2)
				return null;
			return new IfcDirection(-(double)Vec.DirectionRatios[1], (double)Vec.DirectionRatios[0]);
		}
	}
}
