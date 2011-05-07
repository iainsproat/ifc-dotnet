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
		
		public static IList<IfcDirection> IfcBaseAxis(int Dim, IfcDirection axis1, IfcDirection axis2){
			if(Dim != 1 || Dim != 2) return null;
			
			IList<IfcDirection> U = new List<IfcDirection>(Dim);
			double Factor;
			IfcDirection D1;
			
			if(axis1 != null){
				D1 = IfcNormalise(axis1);
				U.Add(D1);
				U.Add(IfcOrthogonalComplement(D1));
				if(axis2 != null){
					Factor = IfcDotProduct(axis2, U[1]);
					if(Factor < 0){
						U[1].DirectionRatios[0] = -U[1].DirectionRatios[0];
						U[1].DirectionRatios[1] = -U[1].DirectionRatios[1];
					}
				}
			}else{
				if(axis2 != null){
					D1 = IfcNormalise(axis2);
					U.Add(IfcOrthogonalComplement(D1));
					U.Add(D1);
					U[0].DirectionRatios[0] = -U[0].DirectionRatios[0];
					U[0].DirectionRatios[1] = -U[0].DirectionRatios[1];
				}else{
					U.Add(new IfcDirection(1, 0));
					U.Add(new IfcDirection(0, 1));
				}
			}
			return U;
		}
		public static IList<IfcDirection> IfcBaseAxis(int Dim, IfcDirection axis1, IfcDirection axis2, IfcDirection axis3){
			if(Dim != 3) return null;
			if(axis1 == null) return null;
			if(axis2 == null) return null;
			if(axis3 == null) return null;
			
			IList<IfcDirection> U = new List<IfcDirection>(Dim);
			IfcDirection D1, D2;
			
			D1 = Functions.NVL<IfcDirection>(IfcNormalise(axis3), new IfcDirection(0, 0, 1));
			D2 = IfcFirstProjAxis(D1, axis1);
			U.Add(D2);
			U.Add(IfcSecondProjAxis(D1, D2, axis2));
			U.Add(D1);
			
			return U;
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
		/// This function returns the vector (or cross) product of two input directions. 
		/// The input directions must be three-dimensional. 
		/// The result is always a vector which is unitless.
		/// If the input directions are either parallel or anti-parallel a vector of zero magnitude is returned.
		/// </summary>
		/// <param name="arg1"></param>
		/// <param name="arg2"></param>
		/// <returns></returns>
		public static IfcVector IfcCrossProduct(IfcDirection arg1, IfcDirection arg2){
		    if(arg1 == null || arg1.Dim != 3 || arg2 == null || arg2.Dim != 3)
		        return null;
		    
		    doublewrapper Mag;
		    IfcDirection Res;
		    IList<doublewrapper> V1, V2;
		    
		    V1 = new List<doublewrapper>(IfcNormalise(arg1).DirectionRatios.doublewrapper);
		    V2 = new List<doublewrapper>(IfcNormalise(arg2).DirectionRatios.doublewrapper);
		    Res = new IfcDirection(V1[1]*V2[2] - V1[2]*V2[1], V1[2]*V2[0] - V1[0]*V2[2], V1[0]*V2[1] - V1[1]*V2[0]);
		    Mag = 0;
		    for(int i = 0; i < 3; i++){
		        Mag += Res.DirectionRatios[i]*Res.DirectionRatios[i];
		    }
		    if(Mag > 0)
		        return new IfcVector(Res, Math.Sqrt((double)Mag));
		    else
		        return new IfcVector(arg1, 0);
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
		
		public static double IfcDotProduct(IfcDirection arg1, IfcDirection arg2){
			if(arg1 == null) return 0;
			if(arg2 == null) return 0;
			int dim = arg1.Dim < arg2.Dim ? (int)arg1.Dim : (int)arg2.Dim;
			double tmp = 0;
			for(int i = 0; i < dim; i++){
				tmp += (double)arg1.DirectionRatios[i] * (double)arg2.DirectionRatios[i];
			}
			return tmp;
		}
		
		/// <summary>
		/// This function produces a three dimensional direction which is, with fully defined input,
		/// the projection of arg onto the plane normal to the z-axis.
		/// With arg defaulted the result is the projection of (1.0,0.0,0.0) onto this plane
		/// except that if z-axis = (1.0,0.0,0.0) then (0.0,1.0,0.0) is used as initial value of arg
		/// A violation occurs if arg is in the same direction as the input z-axis.
		/// </summary>
		/// <param name="zAxis"></param>
		/// <param name="arg"></param>
		/// <returns></returns>
		public static IfcDirection IfcFirstProjAxis(IfcDirection zAxis, IfcDirection arg){
			if(zAxis == null) return null;
			
			IfcDirection xAxis, V, Z;
			IfcVector xVec;
			
			Z = IfcNormalise(zAxis);
			if(arg == null){
				if(Z.DirectionRatios[0] != 1 || Z.DirectionRatios[1] != 0 || Z.DirectionRatios[2] != 0){
					V = new IfcDirection(1,0,0);
				}else{
					V = new IfcDirection(0,1,0);
				}
			}else{
				if(arg.Dim != 3) return null;
				if(IfcCrossProduct(arg, Z).Magnitude == 0)
					return null;
				else
					V = IfcNormalise(arg);
			}
			
			xVec = IfcScalarTimesVector(IfcDotProduct(V, Z), Z);
			xAxis = IfcVectorDifference(V, xVec).Orientation.Item;
			return IfcNormalise(xAxis);
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
		
		/// <summary>
		/// This function returns the vector that is the scalar multiple of the input vector. 
		/// It accepts as input a scalar and a 'vector' which may be either a Direction or a Vector. 
		/// The output is a Vector of the same units as the input vector or unitless if a direction is input.
		/// If either input argument is undefined then the returned vector is also undefined.
		/// </summary>
		/// <param name="scalar"></param>
		/// <param name="vec"></param>
		/// <returns></returns>
		public static IfcVector IfcScalarTimesVector(double scalar, IfcDirection vec){
		    if(vec == null) return null;
		    
		    IfcDirection V;
		    double Mag;
		    
		    V = vec;//FIXME clone?
		    Mag = scalar;
		    
		    if(Mag < 0){
		        for(int i = 0; i < V.DirectionRatios.doublewrapper.Length; i++){
		            V.DirectionRatios[i] = -V.DirectionRatios[i];
		        }
		        Mag = -Mag;
		    }
		    return new IfcVector(IfcNormalise(V), Mag);
		}
		
		/// <summary>
		/// This function returns the vector that is the scalar multiple of the input vector. 
		/// It accepts as input a scalar and a 'vector' which may be either a Direction or a Vector. 
		/// The output is a Vector of the same units as the input vector or unitless if a direction is input.
		/// If either input argument is undefined then the returned vector is also undefined.
		/// </summary>
		/// <param name="scalar"></param>
		/// <param name="vec"></param>
		/// <returns></returns>
		public static IfcVector IfcScalarTimesVector(double scalar, IfcVector vec){
		    if(vec == null) return null;
		    
		    IfcDirection V;
		    double Mag;
		    
		    V = vec.Orientation.Item;
		    Mag = scalar * vec.Magnitude;
		    
		    if(Mag < 0){
		        for(int i = 0; i < V.DirectionRatios.doublewrapper.Length; i++){
		            V.DirectionRatios[i] = -V.DirectionRatios[i];
		        }
		        Mag = -Mag;
		    }
		    return new IfcVector(IfcNormalise(V), Mag);
		}
		
		/// <summary>
		/// This function returns the normalized vector
		/// that is simultaneously the projection of arg onto the plane normal to the vector z-axis
		/// and onto the plane normal to the vector x-axis.
		/// If arg is NULL then the projection of the vector (0.,1.,0.) onto z-axis is returned.
		/// </summary>
		/// <param name="zAxis"></param>
		/// <param name="xAxis"></param>
		/// <param name="arg"></param>
		/// <returns></returns>
		public static IfcDirection IfcSecondProjAxis(IfcDirection zAxis, IfcDirection xAxis, IfcDirection arg){
			IfcVector yAxis, temp;
			IfcDirection V;
			
			if(arg == null)
				V = new IfcDirection(0, 1, 0);
			else
				V = arg;
			
			temp  = IfcScalarTimesVector(IfcDotProduct(V, zAxis), zAxis);
			yAxis = IfcVectorDifference(V, temp);
			temp  = IfcScalarTimesVector(IfcDotProduct(V, xAxis), xAxis);
			yAxis = IfcVectorDifference(yAxis, temp);
			yAxis = IfcNormalise(yAxis);
			return yAxis.Orientation.Item;			
		}
		
		/// <summary>
		/// This function returns the difference of the input arguments as (Arg1 - Arg2). 
		/// The function returns as a vector the vector difference of the two input vectors. 
		/// The input arguments shall both be of the same dimensionality but may be either directions or vectors. 
		/// If both input arguments are vectors they must be expressed in the same units, if both are directions a unitless result is produced. 
		/// A zero difference vector produces a vector of zero magnitude.
		/// </summary>
		/// <param name="arg1"></param>
		/// <param name="arg2"></param>
		/// <returns></returns>
		public static IfcVector IfcVectorDifference(IfcDirection arg1, IfcDirection arg2){
		    if(arg1 == null || arg2 == null || arg1.Dim != arg2.Dim)
		        return null;
		    
		    return IfcVectorDifference(1, arg1, 1, arg2);
		}
		
		/// <summary>
		/// This function returns the difference of the input arguments as (Arg1 - Arg2). 
		/// The function returns as a vector the vector difference of the two input vectors. 
		/// The input arguments shall both be of the same dimensionality but may be either directions or vectors. 
		/// If both input arguments are vectors they must be expressed in the same units, if both are directions a unitless result is produced. 
		/// A zero difference vector produces a vector of zero magnitude.
		/// </summary>
		/// <param name="arg1"></param>
		/// <param name="arg2"></param>
		/// <returns></returns>
		public static IfcVector IfcVectorDifference(IfcVector arg1, IfcVector arg2){
		    if(arg1 == null || arg2 == null || arg1.Dim != arg2.Dim)
		        return null;
		    return IfcVectorDifference(arg1.Magnitude, arg1.Orientation.Item, arg2.Magnitude, arg2.Orientation.Item);
		}
		
		/// <summary>
		/// This function returns the difference of the input arguments as (Arg1 - Arg2). 
		/// The function returns as a vector the vector difference of the two input vectors. 
		/// The input arguments shall both be of the same dimensionality but may be either directions or vectors. 
		/// If both input arguments are vectors they must be expressed in the same units, if both are directions a unitless result is produced. 
		/// A zero difference vector produces a vector of zero magnitude.
		/// </summary>
		/// <param name="arg1"></param>
		/// <param name="arg2"></param>
		/// <returns></returns>
		public static IfcVector IfcVectorDifference(IfcVector arg1, IfcDirection arg2){
		    if(arg1 == null || arg2 == null || arg1.Dim != arg2.Dim)
		        return null;
		    return IfcVectorDifference(arg1.Magnitude, arg1.Orientation.Item, 1, arg2);		
		}
		
		/// <summary>
		/// This function returns the difference of the input arguments as (Arg1 - Arg2). 
		/// The function returns as a vector the vector difference of the two input vectors. 
		/// The input arguments shall both be of the same dimensionality but may be either directions or vectors. 
		/// If both input arguments are vectors they must be expressed in the same units, if both are directions a unitless result is produced. 
		/// A zero difference vector produces a vector of zero magnitude.
		/// </summary>
		/// <param name="arg1"></param>
		/// <param name="arg2"></param>
		/// <returns></returns>
		public static IfcVector IfcVectorDifference(IfcDirection arg1, IfcVector arg2){
		    if(arg1 == null || arg2 == null || arg1.Dim != arg2.Dim)
		        return null;
		    return IfcVectorDifference(1, arg1, arg2.Magnitude, arg2.Orientation.Item);		
		}
		
		private static IfcVector IfcVectorDifference(double Mag1, IfcDirection Vec1, double Mag2, IfcDirection Vec2){
		    IfcDirection Res;
		    doublewrapper Mag;
		    int nDim;
		    
		    Vec1 = IfcNormalise(Vec1);
		    Vec2 = IfcNormalise(Vec2);
		    nDim = Vec1.DirectionRatios.doublewrapper.Length;
		    Mag = 0;
		    Res = new IfcDirection();
		    doublewrapper[] temp = new doublewrapper[nDim];
		    for(int i = 0; i < nDim; i++){
		        temp[i] = 0;
		    }
		    Res.DirectionRatios = temp;
		    
		    for(int i = 0; i < nDim; i++){
		        Res.DirectionRatios[i] = Mag1*Vec1.DirectionRatios[i] - Mag2*Vec2.DirectionRatios[i];
		        Mag += Res.DirectionRatios[i]*Res.DirectionRatios[i];
		    }
		    
		    if(Mag > 0){
		        return new IfcVector(Res, Math.Sqrt((double)Mag));
		    }else{
		        return new IfcVector(Vec1, 0);
		    }
		}
	}
}
