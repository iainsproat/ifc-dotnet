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
using System.Xml.Serialization;

namespace IfcDotNet.Schema
{
	public partial class iso_10303{
		/// <summary>
		/// Assists the XmlSerializer in choosing the correct schema to work with
		/// </summary>
		public enum uosChoice
		{
			/// <summary>
			/// Uos uses the IFC schema
			/// </summary>
			[XmlEnum("http://www.iai-tech.org/ifcXML/IFC2x3/FINAL:uos")]
			uos,
			/// <summary>
			/// uos1 uses the STEP schema
			/// </summary>
			[XmlEnum("urn:iso.org:standard:10303:part(28):version(2):xmlschema:common:uos")]
			uos1
		}

		/// <summary>
		/// A helper variable to assist the XmlSerializer in choosing the correct Xml schema to work with
		/// </summary>
		[XmlIgnore()]
		public uosChoice uosChoiceField;
	}
	
	#region STEP data type wrappers
	
	/// <summary>
	/// Wraps HexaDecimal values used in STEP files
	/// </summary>
	public partial class hexBinary {
		/// <summary>
		/// Operator for explicitly converting hexBinary to a byte array
		/// </summary>
		/// <param name="wrap"></param>
		/// <returns></returns>
		public static explicit operator byte[](hexBinary wrap){
			return wrap.Value;
		}
		
		/// <summary>
		/// Operator for implicitly converting a byte array to hexBinary
		/// </summary>
		/// <param name="d"></param>
		/// <returns></returns>
		public static implicit operator hexBinary(byte[] d){
			hexBinary wrap = new hexBinary();
			wrap.Value = d;
			return wrap;
		}
	}
	
	/// <summary>
	/// Wraps Long values used in STEP files
	/// </summary>
	public partial class longwrapper{
		/// <summary>
		/// Operator for casting longwrapper values to long
		/// </summary>
		/// <param name="len"></param>
		/// <returns></returns>
		public static explicit operator long(longwrapper len){
			return len.Value;
		}
		
		/// <summary>
		/// Operator casts long values to longwrapper
		/// </summary>
		/// <param name="d"></param>
		/// <returns></returns>
		public static implicit operator longwrapper(long d){
			longwrapper len = new longwrapper();
			len.Value = d;
			return len;
		}
	}
	
	/// <summary>
	/// Wraps double value used in STEP data
	/// </summary>
	public partial class doublewrapper{
		/// <summary>
		/// Operator casts doublewrapper values to double
		/// </summary>
		/// <param name="wrap"></param>
		/// <returns></returns>
		public static explicit operator double(doublewrapper wrap){
			return wrap.Value;
		}
		
		/// <summary>
		/// Operator casts double values to doublewrapper
		/// </summary>
		/// <param name="d"></param>
		/// <returns></returns>
		public static implicit operator doublewrapper(double d){
			doublewrapper wrap = new doublewrapper();
			wrap.Value = d;
			return wrap;
		}
		
		/// <summary>
		/// Expresses the doublewrapper as the value string
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return this.Value.ToString();
		}

	}
	
	/// <summary>
	/// Wraps base64 binary values used in STEP data
	/// </summary>
	public partial class base64Binary{
		/// <summary>
		/// Operator casts base64Binary values to byte arrays
		/// </summary>
		/// <param name="wrap"></param>
		/// <returns></returns>
		public static explicit operator byte[](base64Binary wrap){
			return wrap.Value;
		}
		
		/// <summary>
		/// Operator casts byte arrays to base64Binary values
		/// </summary>
		/// <param name="d"></param>
		/// <returns></returns>
		public static implicit operator base64Binary(byte[] d){
			base64Binary wrap = new base64Binary();
			wrap.Value = d;
			return wrap;
		}
	}
	
	/// <summary>
	/// Wrapper for logical values used in STEP data
	/// </summary>
	public partial class logicalwrapper{
		/// <summary>
		/// Operator cast logicalwrapper values to IfcLogical
		/// </summary>
		/// <param name="len"></param>
		/// <returns></returns>
		public static explicit operator IfcLogical(logicalwrapper len){
			return len.Value;
		}
		
		/// <summary>
		/// Operator casts IfcLogical values to logicalwrapper
		/// </summary>
		/// <param name="d"></param>
		/// <returns></returns>
		public static implicit operator logicalwrapper(IfcLogical d){
			logicalwrapper len = new logicalwrapper();
			len.Value = d;
			return len;
		}
	}
	
	/// <summary>
	/// Wrapper for boolean values used in STEP data
	/// </summary>
	public partial class booleanwrapper{
		/// <summary>
		/// Operator casts booleanwrapper values to Boolean
		/// </summary>
		/// <param name="wrap"></param>
		/// <returns></returns>
		public static explicit operator bool(booleanwrapper wrap){
			return wrap.Value;
		}
		
		/// <summary>
		/// Operator casts Boolean values to BooleanWrapper
		/// </summary>
		/// <param name="d"></param>
		/// <returns></returns>
		public static implicit operator booleanwrapper(bool d){
			booleanwrapper wrap = new booleanwrapper();
			wrap.Value = d;
			return wrap;
		}
	}
	
	public partial class decimalwrapper{
		public static explicit operator decimal(decimalwrapper wrap){
			return wrap.Value;
		}
		
		public static implicit operator decimalwrapper(decimal d){
			decimalwrapper wrap = new decimalwrapper();
			wrap.Value = d;
			return wrap;
		}
	}
	
	public partial class integerwrapper{
		public static explicit operator string(integerwrapper len){
			return len.Value;
		}
		
		public static implicit operator integerwrapper(string d){
			integerwrapper len = new integerwrapper();
			len.Value = d;
			return len;
		}
	}
	
	public partial class stringwrapper{
		public static explicit operator string(stringwrapper len){
			return len.Value;
		}
		
		public static implicit operator stringwrapper(string d){
			stringwrapper len = new stringwrapper();
			len.Value = d;
			return len;
		}
	}
	#endregion
	
	#region IFC defined types
	/// <summary>
	/// Definition from ISO/CD 10303-41:1992: A length measure is the value of a distance.
	/// </summary>
	public partial class IfcLengthMeasure1{
		/// <summary>
		/// Helper method for casting an IfcLengthMeasure to a double
		/// </summary>
		/// <param name="len"></param>
		/// <returns></returns>
		public static explicit operator double(IfcLengthMeasure1 len){
			if(len ==null)
				return 0;
			return len.Value;
		}
		
		/// <summary>
		/// Helper operator for casting a double to an IfcLengthMeasure.
		/// </summary>
		/// <param name="d"></param>
		/// <returns></returns>
		public static implicit operator IfcLengthMeasure1(double d){
			IfcLengthMeasure1 len = new IfcLengthMeasure1();
			len.Value = d;
			return len;
		}
	}
	
	/// <summary>
	/// Definition from ISO/CD 10303-41:1992: A plane angle measure is the value of an angle in a plane.
	/// </summary>
	public partial class IfcPlaneAngleMeasure1{
		/// <summary>
		/// A helper method for casting from an IfcPlaneAngleMeasure to a double
		/// </summary>
		/// <param name="pln"></param>
		/// <returns></returns>
		public static explicit operator double(IfcPlaneAngleMeasure1 pln){
			if(pln == null)
				return 0;
			return pln.Value;
		}
		
		/// <summary>
		/// A helper method for casting from a double to an IfcPlaneAngleMeasure
		/// </summary>
		/// <param name="d"></param>
		/// <returns></returns>
		public static implicit operator IfcPlaneAngleMeasure1(double d){
			IfcPlaneAngleMeasure1 pln = new IfcPlaneAngleMeasure1();
			pln.Value = d;
			return pln;
		}
	}
	#endregion
	
	#region Intermediate classes
	/// <summary>
	/// Intermediate class wrapping the DirectionRatios property of IfcDirection
	/// </summary>
	public partial class IfcDirectionDirectionRatios{
		public static explicit operator doublewrapper[](IfcDirectionDirectionRatios rat){
			return rat.doublewrapper;
		}
		
		public static implicit operator IfcDirectionDirectionRatios(doublewrapper[] d){
			IfcDirectionDirectionRatios dir = new IfcDirectionDirectionRatios();
			dir.doublewrapper = d;
			return dir;
		}
		
		public static explicit operator double[](IfcDirectionDirectionRatios rat){
			if(rat == null)
				return null;
			if(rat.doublewrapper == null)
				return null;
			double[] dub = new double[rat.doublewrapper.Length];
			for(int i = 0; i < rat.doublewrapper.Length; i++){
				dub[i] = (double)rat.doublewrapper[i];
			}
			return dub;
		}
		
		public static implicit operator IfcDirectionDirectionRatios(double[] d){
			IfcDirectionDirectionRatios dir = new IfcDirectionDirectionRatios();
			dir.doublewrapper = new doublewrapper[d.Length];
			for(int i = 0; i < d.Length; i++){
				dir.doublewrapper[i] = d[i];
			}
			return dir;
		}
	}
	
	public partial class IfcCartesianPointCoordinates{
		public static explicit operator IfcLengthMeasure1[](IfcCartesianPointCoordinates rat){
			return rat.IfcLengthMeasure;
		}
		
		public static implicit operator IfcCartesianPointCoordinates(IfcLengthMeasure1[] d){
			IfcCartesianPointCoordinates coord = new IfcCartesianPointCoordinates();
			coord.IfcLengthMeasure = d;
			return coord;
		}
		
		public static explicit operator double[](IfcCartesianPointCoordinates len){
			if(len == null )
				return null;
			if(len.IfcLengthMeasure == null)
				return null;
			double[] d = new double[len.IfcLengthMeasure.Length];
			for(int i = 0; i < len.IfcLengthMeasure.Length; i++){
				d[i] = (double)len.IfcLengthMeasure[i];
			}
			return d;
		}
		
		public static implicit operator IfcCartesianPointCoordinates(double[] d){
			if(d == null)
				return null;
			IfcCartesianPointCoordinates len = new IfcCartesianPointCoordinates();
			len.IfcLengthMeasure = new IfcLengthMeasure1[d.Length];
			for(int i = 0; i < d.Length; i++){
				len.IfcLengthMeasure[i] = d[i];
			}
			return len;
		}
	}
	#endregion
	
	#region Derived Properties
	public partial class IfcSIUnit{
		
		/// <summary>
		/// The dimensional exponents of SI units are derived by function IfcDimensionsForSiUnit.
		/// </summary>
		[StepProperty(Order=0,Overridden=true)]
		public override IfcNamedUnitDimensions Dimensions{
			get{ return null; }
			set{ //do nothing
			}
		}
	}
	#endregion
	
	#region Additional Constructors
	public partial class IfcDimensionalExponents{
		public IfcDimensionalExponents(int lengthExponent, int massExponent, int timeExponent, int electricCurrentExponent,
		                               int thermodynamicTemperatureExponent, int amountOfSubstanceExponent, int luminousIntensityExponent){
			this.lengthExponentField = lengthExponent;
			this.massExponentField = massExponent;
			this.timeExponentField = timeExponent;
			this.electricCurrentExponentField = electricCurrentExponent;
			this.thermodynamicTemperatureExponentField = thermodynamicTemperatureExponent;
			this.amountOfSubstanceExponentField = amountOfSubstanceExponent;
			this.luminousIntensityExponentField = luminousIntensityExponent;
		}
		
		public override string ToString()
		{
			return string.Format(System.Globalization.CultureInfo.InvariantCulture, 
			                    "({0},{1},{2},{3},{4},{5},{6},{7})",
			                   this.lengthExponentField,
			                   this.massExponentField,
			                   this.timeExponentField,
			                   this.electricCurrentExponentField,
			                   this.thermodynamicTemperatureExponentField,
			                   this.amountOfSubstanceExponentField,
			                   this.luminousIntensityExponentField);
		}

	}
	#endregion
}
