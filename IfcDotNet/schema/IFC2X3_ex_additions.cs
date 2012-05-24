#region License
/*

Copyright 2010, 2011, Iain Sproat
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
	public partial class longwrapper : ValueType<long, longwrapper>{
	
		/// <summary>
		/// Operator casts longwrapper values to long
		/// </summary>
		/// <param name="wrap"></param>
		/// <returns></returns>
		public static explicit operator long(longwrapper wrap){
			if(wrap == null)
				return 0;
			return wrap.Value;
		}
		
		/// <summary>
		/// Operator casts long values to longwrapper
		/// </summary>
		/// <param name="l"></param>
		/// <returns></returns>
		public static implicit operator longwrapper(long l){
			longwrapper wrap = new longwrapper();
			wrap.Value = l;
			return wrap;
		}
	
	}
	
	/// <summary>
	/// Wraps double value used in STEP data
	/// </summary>
	public partial class doublewrapper : ValueType<double, doublewrapper>, IEquatable<doublewrapper>, IEquatable<double>{
		/// <summary>
		/// default constructor, required for serialization
		/// </summary>
		public doublewrapper(){}
		
		/// <summary>
		/// constructor
		/// </summary>
		/// <param name="value"></param>
		public doublewrapper(double value){
			this.Value = value;
		}
		
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
		
		#region Equals and GetHashCode implementation
		/// <summary>
		/// Determines whether two objects are equal
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public override bool Equals(object obj)
		{
			doublewrapper other = obj as doublewrapper;
			return this.Equals(other);
		}
		
		/// <summary>
		/// Determines whether the doublewrapper is equal to the value of this
		/// </summary>
		/// <param name="other"></param>
		/// <returns></returns>
		public bool Equals(doublewrapper other){
			if(other == null) return false;
			return this.Value.Equals(other.Value);
		}
		
		/// <summary>
		/// Determines whether the double is equal to the value of this
		/// </summary>
		/// <param name="other"></param>
		/// <returns></returns>
		public bool Equals(double other){
			return this.Value.Equals(other);
		}
		
		/// <summary>
		/// Calculates the unique hashcode
		/// </summary>
		/// <returns></returns>
		public override int GetHashCode()
		{
			int hashCode = 4;
			hashCode += this.Value.GetHashCode();
			return hashCode;
		}
		
		/// <summary>
		/// Equality operator
		/// </summary>
		/// <param name="lhs"></param>
		/// <param name="rhs"></param>
		/// <returns></returns>
		public static bool operator ==(doublewrapper lhs, doublewrapper rhs)
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
		public static bool operator !=(doublewrapper lhs, doublewrapper rhs)
		{
			return !(lhs == rhs);
		}
		#endregion

		/// <summary>
		/// Adds two doublewrappers together
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static doublewrapper operator +(doublewrapper a, doublewrapper b){
			if(a == null) throw new ArgumentNullException("a");
			if(b == null) throw new ArgumentNullException("b");
			return new doublewrapper(a.Value + b.Value);
		}
		
		/// <summary>
		/// Subtracts one doublewrapper from the other
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static doublewrapper operator -(doublewrapper a, doublewrapper b){
			if(a == null) throw new ArgumentNullException("a");
			if(b == null) throw new ArgumentNullException("b");
			return new doublewrapper(a.Value - b.Value);
		}

		/// <summary>
		/// Multiplies two doublewrappers together
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static doublewrapper operator *(doublewrapper a, doublewrapper b){
			if(a == null) throw new ArgumentNullException("a");
			if(b == null) throw new ArgumentNullException("b");
			return new doublewrapper(a.Value * b.Value);
		}
		
		/// <summary>
		/// Divides one doublewrapper by the other
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static doublewrapper operator /(doublewrapper a, doublewrapper b){
			if(a == null) throw new ArgumentNullException("a");
			if(b == null) throw new ArgumentNullException("b");
			return new doublewrapper(a.Value / b.Value);
		}
		
		/// <summary>
		/// Determines whether one doublewrapper is less than the other
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static bool operator <(doublewrapper a, doublewrapper b){
			if(a == null) throw new ArgumentNullException("a");
			if(b == null) throw new ArgumentNullException("b");
			return a.Value < b.Value;
		}
		
		/// <summary>
		/// Determine whether one doublewrapper is greater than the other
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static bool operator >(doublewrapper a, doublewrapper b){
			if(a == null) throw new ArgumentNullException("a");
			if(b == null) throw new ArgumentNullException("b");
			return a.Value > b.Value;
		}
		
		/// <summary>
		/// Determine whether one doublewrapper is equal or less than the other
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static bool operator <=(doublewrapper a, doublewrapper b){
			if(a == null) throw new ArgumentNullException("a");
			if(b == null) throw new ArgumentNullException("b");
			return a.Value <= b.Value;
		}
		
		/// <summary>
		/// Determine whether one doublewrapper is equal or greater than the other
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static bool operator >=(doublewrapper a, doublewrapper b){
			if(a == null) throw new ArgumentNullException("a");
			if(b == null) throw new ArgumentNullException("b");
			return a.Value >= b.Value;
		}
		
		/// <summary>
		/// Inverts the sign of a doublewrapper
		/// </summary>
		/// <param name="a"></param>
		/// <returns></returns>
		public static doublewrapper operator -(doublewrapper a){
			if(a == null) throw new ArgumentNullException("a");
			return -a.Value;
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
	
	//computer generated
	public partial class hexBinary : ValueType<byte[], hexBinary>{}
	public partial class base64Binary : ValueType<Byte[], base64Binary>{}
	public partial class logicalwrapper : ValueType<IfcLogical, logicalwrapper>{}
	public partial class booleanwrapper : ValueType<Boolean, booleanwrapper>{}
	public partial class decimalwrapper : ValueType<Decimal, decimalwrapper>{}
	public partial class integerwrapper : ValueType<String, integerwrapper>{}
	public partial class stringwrapper : ValueType<String, stringwrapper>{}
	#endregion
	

	#region Additional Constructors
	public partial class IfcAxis2Placement2DRefDirection{
		/// <summary>
		/// Default constructor for serialization
		/// </summary>
		public IfcAxis2Placement2DRefDirection(){}
		
		/// <summary>
		/// Constructor from an IfcDirection
		/// </summary>
		/// <param name="dir"></param>
		public IfcAxis2Placement2DRefDirection(IfcDirection dir){
			this.Item = dir;
		}
	}
	
	public partial class IfcCompositeCurveSegments{
		/// <summary>
		/// Constructor accepting a predefined capacity for the underlying array
		/// </summary>
		/// <param name="capacity"></param>
		public IfcCompositeCurveSegments(int capacity){
			this.Items = new IfcCompositeCurveSegment[capacity];
		}
		
		/// <summary>
		/// Constructor providing the underlying array
		/// </summary>
		/// <param name="list"></param>
		public IfcCompositeCurveSegments(IfcCompositeCurveSegment[] list){
			this.Items = list;
		}
	}
	
	/// <summary>
	/// Intermediate class wrapping the DirectionRatios property of IfcDirection
	/// </summary>
	public partial class IfcDirectionDirectionRatios{
		/// <summary>
		/// Constructor giving capacity for the underlying array
		/// </summary>
		/// <param name="capacity"></param>
		public IfcDirectionDirectionRatios(int capacity) : this(){
			this.Items = new doublewrapper[capacity];
		}
		
		/// <summary>
		/// Constructor accepting an array to be used for the underlying array
		/// </summary>
		/// <param name="list"></param>
		public IfcDirectionDirectionRatios(doublewrapper[] list) : this(){
			this.Items = list;
		}
		
		/// <summary>
		/// Constructor. Clones an array of double
		/// </summary>
		/// <param name="list"></param>
		public IfcDirectionDirectionRatios(double[] list) : this(){
			if(list == null)
				return;
			this.Items = new doublewrapper[list.Length];
			for(int i = 0; i < list.Length; i++){
				this.Items[i] = list[i];
			}
		}
		
		/// <summary>
		/// Constructor. Clones a list of double
		/// </summary>
		/// <param name="list"></param>
		public IfcDirectionDirectionRatios(IList<double> list) : this(){
			if(list == null)
				return;
			this.Items = new doublewrapper[list.Count];
			for(int i = 0; i < list.Count; i++){
				this.Items[i] = list[i];
			}
		}
	}
	
	/* TODO find a way to use IfcGeometricSetSelect rather than IfcGeometricRepresentationItem
    public partial class IfcGeometricSetElements{
	    /// <summary>
	    /// 
	    /// </summary>
	    [XmlIgnore()]
	    public override IfcGeometricSetSelect[] Items{
	        get{
	            if(this.IfcGeometricRepresentationItems == null)
	                return null;
	            IfcGeometricSetSelect[] array = new IfcGeometricSetSelect[this.IfcGeometricRepresentationItems.Length];
	            for(int i = 0; i < this.IfcGeometricRepresentationItems.Length; i++){
	                IfcGeometricSetSelect sel = (IfcGeometricSetSelect)this.IfcGeometricRepresentationItems[i];
	                array[i] = sel;
	            }
	            return array;
	        }
	        set{
	            throw new NotImplementedException();
	        }
	    }
	}*/
	
	public partial class IfcVectorOrientation{
		/// <summary>
		/// Default constructor, used for serialization
		/// </summary>
		public IfcVectorOrientation(){}
		
		/// <summary>
		/// Constructor allowing the underlying item to be given
		/// </summary>
		/// <param name="dir"></param>
		public IfcVectorOrientation(IfcDirection dir){
			this.Item = dir;
		}
		
		/// <summary>
		/// Constructor allowing the underlying item data to be given
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		public IfcVectorOrientation(double x, double y){
			this.Item = new IfcDirection(x, y);
		}
		
		/// <summary>
		/// Constructor allowing the underlying item data to be given
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="z"></param>
		public IfcVectorOrientation(double x, double y, double z){
			this.Item = new IfcDirection(x, y, z);
		}
	}

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
		/// Constructor allowing the underlying data to be given
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		public IfcCartesianPoint(double x, double y){
		    this.Coordinates = (IfcCartesianPointCoordinates)(new double[]{x, y});
		}
		/// <summary>
		/// Constructor for a 2D coordinate
		/// </summary>
		/// <param name="id"></param>
		/// <param name="x"></param>
		/// <param name="y"></param>
		public IfcCartesianPoint(string id, double x, double y){
			this.entityid = id;
			this.Coordinates = (IfcCartesianPointCoordinates)(new double[]{x, y});
		}
		
		/// <summary>
		/// Constructor allowing the underlying data to be given
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="z"></param>
		public IfcCartesianPoint(double x, double y, double z){
		    this.Coordinates = (IfcCartesianPointCoordinates)(new double[]{x, y, z});
		}
		
		/// <summary>
		/// Constructor for a 3D coordinate
		/// </summary>
		/// <param name="id"></param>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="z"></param>
		public IfcCartesianPoint(string id, double x, double y, double z){
			this.entityid = id;
			this.Coordinates = (IfcCartesianPointCoordinates)(new double[]{x, y, z});
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
	
	public partial class IfcDirection{
		/// <summary>
		/// Parameterless constructor for serialization
		/// </summary>
		public IfcDirection(){}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		public IfcDirection(double x, double y){
			this.DirectionRatios = new IfcDirectionDirectionRatios( new doublewrapper[]{x, y});
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		public IfcDirection(doublewrapper x, doublewrapper y){
			this.DirectionRatios = new IfcDirectionDirectionRatios( new doublewrapper[]{x, y});
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="z"></param>
		public IfcDirection(double x, double y, double z){
			this.DirectionRatios = new IfcDirectionDirectionRatios( new doublewrapper[]{x, y, z});
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="z"></param>
		public IfcDirection(doublewrapper x, doublewrapper y, doublewrapper z){
			this.DirectionRatios = new IfcDirectionDirectionRatios( new doublewrapper[]{x, y, z});
		}
		
		/// <summary>
		/// Constructor for 2D directions
		/// </summary>
		/// <param name="id"></param>
		/// <param name="x"></param>
		/// <param name="y"></param>
		public IfcDirection(string id, double x, double y){
			this.entityid = id;
			this.DirectionRatios = new IfcDirectionDirectionRatios( new doublewrapper[]{x, y});
		}
		
		/// <summary>
		/// Constructor for 3D directions
		/// </summary>
		/// <param name="id"></param>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="z"></param>
		public IfcDirection(string id, double x, double y, double z){
			this.entityid = id;
			this.DirectionRatios = new IfcDirectionDirectionRatios(new doublewrapper[]{x, y, z});
		}
	}
	
	public partial class IfcLine{
		/// <summary>
		/// Parameterless constructor for serialization
		/// </summary>
		public IfcLine(){}
		
		/// <summary>
		/// Constructor accepting underlying data
		/// </summary>
		/// <param name="point"></param>
		/// <param name="direction"></param>
		public IfcLine(IfcCartesianPoint point, IfcVector direction){
			if(point == null) throw new ArgumentNullException("point");
			if(direction == null) throw new ArgumentNullException("direction");
			this.Pnt = (IfcLinePnt)point;
			this.Dir = (IfcLineDir)direction;
		}
	}
	
	public partial class IfcVector
	{
		/// <summary>
		/// Parameterless constructor for serialization
		/// </summary>
		public IfcVector(){}
		
		/// <summary>
		/// Constructor allowing the underlying data to be given
		/// </summary>
		/// <param name="orient"></param>
		/// <param name="mag"></param>
		public IfcVector(IfcDirection orient, double mag){
		    this.Orientation = (IfcVectorOrientation)orient;
			this.Magnitude = mag;
		}
		
		/// <summary>
		/// Constructor allowing the underlying item data to be given
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		public IfcVector(double x, double y){
			this.Orientation = new IfcVectorOrientation(x, y);
		}
		
		/// <summary>
		/// Constructor allowing the underlying item data to be given
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="z"></param>
		public IfcVector(double x, double y, double z){
			this.Orientation = new IfcVectorOrientation(x, y, z);
		}
	}
	#endregion
}
