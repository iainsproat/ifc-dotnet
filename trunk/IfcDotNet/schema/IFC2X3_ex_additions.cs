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
			if(wrap == null)
				return null;
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
			if(len == null)
				return 0;
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
			if(wrap == null)
				return 0;
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
			if(wrap == null)
				return null;
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
			if(len == null)
				return IfcLogical.unknown;
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
			if(wrap == null)
				return false;
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
	
	/// <summary>
	/// Wrapper for decimal values used in STEP data
	/// </summary>
	public partial class decimalwrapper{
		/// <summary>
		/// Operator casts decimalwrapper values to decimal
		/// </summary>
		/// <param name="wrap"></param>
		/// <returns></returns>
		public static explicit operator decimal(decimalwrapper wrap){
			if(wrap == null)
				return 0;
			return wrap.Value;
		}
		
		/// <summary>
		/// Operator casts decimal values to decimalwrapper
		/// </summary>
		/// <param name="d"></param>
		/// <returns></returns>
		public static implicit operator decimalwrapper(decimal d){
			decimalwrapper wrap = new decimalwrapper();
			wrap.Value = d;
			return wrap;
		}
	}
	
	/// <summary>
	/// Wrapper for integer values used in STEP data
	/// </summary>
	public partial class integerwrapper{
		/// <summary>
		/// Operator casts integerwrapper values to integer
		/// </summary>
		/// <param name="len"></param>
		/// <returns></returns>
		public static explicit operator string(integerwrapper len){
			if(len == null)
				return string.Empty;
			return len.Value;
		}
		
		/// <summary>
		/// Operators casts integer values to integerwrapper
		/// </summary>
		/// <param name="d"></param>
		/// <returns></returns>
		public static implicit operator integerwrapper(string d){
			integerwrapper len = new integerwrapper();
			len.Value = d;
			return len;
		}
	}
	
	/// <summary>
	/// Wrapper for string values used in STEP data
	/// </summary>
	public partial class stringwrapper{
		/// <summary>
		/// Operator casts stringwrapper values to string
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public static explicit operator string(stringwrapper str){
			if(str == null)
				return String.Empty;
			return str.Value;
		}
		
		
		/// <summary>
		/// Operator casts string values to stringwrapper
		/// </summary>
		/// <param name="d"></param>
		/// <returns></returns>
		public static implicit operator stringwrapper(string d){
			stringwrapper len = new stringwrapper();
			len.Value = d;
			return len;
		}
	}
	#endregion
	

	#region Intermediate classes
	/// <summary>
	/// Intermediate class wrapping the DirectionRatios property of IfcDirection
	/// </summary>
	public partial class IfcDirectionDirectionRatios{
		/// <summary>
		/// Operator casts an IfcDirectionDirectionRatios to a doublewrapper array
		/// </summary>
		/// <param name="rat"></param>
		/// <returns></returns>
		public static explicit operator doublewrapper[](IfcDirectionDirectionRatios rat){
			if(rat == null)
				return null;
			return rat.doublewrapper;
		}
		
		/// <summary>
		/// Operator casts a doublewrapper array to an IfcDirectionDirectionRatios
		/// </summary>
		/// <param name="d"></param>
		/// <returns></returns>
		public static implicit operator IfcDirectionDirectionRatios(doublewrapper[] d){
			IfcDirectionDirectionRatios dir = new IfcDirectionDirectionRatios();
			dir.doublewrapper = d;
			return dir;
		}
		
		/// <summary>
		/// Operator casts an IfcDirectionDirectionRatios to a double array
		/// </summary>
		/// <param name="rat"></param>
		/// <returns></returns>
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
		
		/// <summary>
		/// Operator casts a double array to an IfcDirectionDirectionRatios
		/// </summary>
		/// <param name="d"></param>
		/// <returns></returns>
		public static implicit operator IfcDirectionDirectionRatios(double[] d){
			IfcDirectionDirectionRatios dir = new IfcDirectionDirectionRatios();
			if(d == null)
				return dir;
			dir.doublewrapper = new doublewrapper[d.Length];
			for(int i = 0; i < d.Length; i++){
				dir.doublewrapper[i] = d[i];
			}
			return dir;
		}
	}
	
	public partial class IfcCartesianPointCoordinates{
		
		/// <summary>
		/// Operator casts an IfcCartesianPointCoordinates to an IfcLengthMeasureArray
		/// </summary>
		/// <param name="rat"></param>
		/// <returns></returns>
		public static explicit operator IfcLengthMeasure1[](IfcCartesianPointCoordinates rat){
			if(rat == null)
				return null;
			return rat.IfcLengthMeasure;
		}
		
		/// <summary>
		/// Operator casts an IfcLengthMeasure1 array to an IfcCartesianPointCoordinates
		/// </summary>
		/// <param name="d"></param>
		/// <returns></returns>
		public static implicit operator IfcCartesianPointCoordinates(IfcLengthMeasure1[] d){
			IfcCartesianPointCoordinates coord = new IfcCartesianPointCoordinates();
			coord.IfcLengthMeasure = d;
			return coord;
		}
		
		/// <summary>
		/// Operator casts an IfcCartesianPointCoordinates to a double array
		/// </summary>
		/// <param name="len"></param>
		/// <returns></returns>
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
		
		/// <summary>
		/// Operator casts a double array to an IfcCartesianPointCoordinates
		/// </summary>
		/// <param name="d"></param>
		/// <returns></returns>
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
	/// <summary>
	/// Definition from ISO/CD 10303-42:1992: 
	/// A point defined by its coordinates in a two or three dimensional rectangular Cartesian coordinate system, 
	/// or in a two dimensional parameter space.
	/// The entity is defined in a two or three dimensional space.
	/// </summary>
	public partial class IfcCartesianPoint{
		/// <summary>
		/// Parameterless constructor for serialization
		/// </summary>
		public IfcCartesianPoint(){}
		
		/// <summary>
		/// Constructor for a 2D coordinate
		/// </summary>
		/// <param name="id"></param>
		/// <param name="x"></param>
		/// <param name="y"></param>
		public IfcCartesianPoint(string id, int x, int y){
			this.entityid = id;
			this.Coordinates = new double[]{x, y};
		}
		
		/// <summary>
		/// Constructor for a 3D coordinate
		/// </summary>
		/// <param name="id"></param>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="z"></param>
		public IfcCartesianPoint(string id, int x, int y, int z){
			this.entityid = id;
			this.Coordinates = new double[]{x, y, z};
		}
		
		/// <summary>
		/// The space dimensionality of this class, determined by the number of coordinates in the List of Coordinates.
		/// </summary>
		[XmlIgnore()]
		public IfcDimensionCount1 Dim{
			get{ return this.Coordinates.IfcLengthMeasure.Length; }
		}
	}
	
	public partial class IfcDirection{
		/// <summary>
		/// Parameterless constructor for serialization
		/// </summary>
		public IfcDirection(){}
		
		/// <summary>
		/// Constructor for 2D directions
		/// </summary>
		/// <param name="id"></param>
		/// <param name="x"></param>
		/// <param name="y"></param>
		public IfcDirection(string id, int x, int y){
			this.entityid = id;
			this.DirectionRatios = new double[]{x, y};
		}
		
		/// <summary>
		/// Constructor for 3D directions
		/// </summary>
		/// <param name="id"></param>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="z"></param>
		public IfcDirection(string id, int x, int y, int z){
			this.entityid = id;
			this.DirectionRatios = new double[]{x, y, z};
		}
		
		/// <summary>
		/// The space dimensionality of this class, defined by the number of real in the list of DirectionRatios.
		/// </summary>
		[XmlIgnore()]
		public IfcDimensionCount1 Dim{
			get{ return this.DirectionRatios.doublewrapper.Length; }
		}
	}
	
	public partial class IfcDimensionalExponents
	{
		/// <summary>
		/// Parameterless constructor for serialization
		/// </summary>
		public IfcDimensionalExponents(){}
		
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="lengthExponent"></param>
		/// <param name="massExponent"></param>
		/// <param name="timeExponent"></param>
		/// <param name="electricCurrentExponent"></param>
		/// <param name="thermodynamicTemperatureExponent"></param>
		/// <param name="amountOfSubstanceExponent"></param>
		/// <param name="luminousIntensityExponent"></param>
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
		
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
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
