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
	public partial class doublewrapper : IEquatable<doublewrapper>, IEquatable<double>{
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
	#endregion
	

	#region Intermediate classes
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
		
		/// <summary>
		/// Implicitly converts an IfcDirection to an IfcAxis2Placement2DRefDirection
		/// </summary>
		/// <param name="dir"></param>
		/// <returns></returns>
		public static implicit operator IfcAxis2Placement2DRefDirection(IfcDirection dir){
			return new IfcAxis2Placement2DRefDirection(dir);
		}
		
		/// <summary>
		/// Explicitly convers an IfcAxis2Placement2DRefDirection to an IfcDirection
		/// </summary>
		/// <param name="dir"></param>
		/// <returns></returns>
		public static explicit operator IfcDirection(IfcAxis2Placement2DRefDirection dir){
			return dir.Item;
		}
	}
	
	public partial class IfcBSplineCurveControlPointsList{
	    /// <summary>
	    /// Indexes the underlying IfcCartesianPoint array
	    /// </summary>
	    public IfcCartesianPoint this[int index]{
	        get{
	            if(this.IfcCartesianPoint == null)
	                this.IfcCartesianPoint = new IfcCartesianPoint[0];
	            return this.IfcCartesianPoint[index];
	        }
	        set{
	            if(this.IfcCartesianPoint == null)
	                this.IfcCartesianPoint = new IfcCartesianPoint[0];
	            this.IfcCartesianPoint[index] = value;
	        }
	    }
	}
	public partial class IfcCartesianTransformationOperatorAxis1{
	    /// <summary>
	    /// Implicitly converts an IfcDirection to an IfcCartesianTransformationOperatorAxis1
	    /// </summary>
	    /// <param name="a"></param>
	    /// <returns></returns>
	    public static implicit operator IfcCartesianTransformationOperatorAxis1(IfcDirection a){
	        IfcCartesianTransformationOperatorAxis1 op = new IfcCartesianTransformationOperatorAxis1();
	        op.Item = a;
	        return op;
	    }
	}
	public partial class IfcCartesianTransformationOperatorAxis2{
	    /// <summary>
	    /// Implicitly converts an IfcDirection to an IfcCartesianTransformationOperatorAxis2
	    /// </summary>
	    /// <param name="a"></param>
	    /// <returns></returns>
	    public static implicit operator IfcCartesianTransformationOperatorAxis2(IfcDirection a){
	        IfcCartesianTransformationOperatorAxis2 op = new IfcCartesianTransformationOperatorAxis2();
	        op.Item = a;
	        return op;
	    }
	}
	
	public partial class IfcCartesianTransformationOperatorLocalOrigin{
	    /// <summary>
	    /// Implicitly converts an IfcCartesianPoint to an IfcCartesianTransformationOperatorLocalOrigin
	    /// </summary>
	    /// <param name="pnt"></param>
	    /// <returns></returns>
	    public static implicit operator IfcCartesianTransformationOperatorLocalOrigin(IfcCartesianPoint pnt){
	        IfcCartesianTransformationOperatorLocalOrigin op = new IfcCartesianTransformationOperatorLocalOrigin();
	        op.Item = pnt;
	        return op;
	    }
	}
	public partial class IfcCompositeCurveSegments{
	    /// <summary>
	    /// Constructor accepting a predefined capacity for the underlying array
	    /// </summary>
	    /// <param name="capacity"></param>
	    public IfcCompositeCurveSegments(int capacity){
	        this.IfcCompositeCurveSegment = new IfcCompositeCurveSegment[capacity];
	    }
	    
	    /// <summary>
	    /// Constructor providing the underlying array
	    /// </summary>
	    /// <param name="list"></param>
	    public IfcCompositeCurveSegments(IfcCompositeCurveSegment[] list){
	        this.IfcCompositeCurveSegment = list;
	    }
	    
	    /// <summary>
	    /// Provides index access to the underlying array
	    /// </summary>
	    public IfcCompositeCurveSegment this[int index]{
	        get{
	            if(this.IfcCompositeCurveSegment == null)
	                this.IfcCompositeCurveSegment = new IfcCompositeCurveSegment[0];
	            return this.IfcCompositeCurveSegment[index];
	        }
	        set{
	            if(this.IfcCompositeCurveSegment == null)
	                this.IfcCompositeCurveSegment = new IfcCompositeCurveSegment[0];
	            this.IfcCompositeCurveSegment[index] = value;
	        }
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
	    public IfcDirectionDirectionRatios(int capacity){
	        this.doublewrapper = new doublewrapper[capacity];
	    }
	    
	    /// <summary>
	    /// Constructor accepting an array to be used for the underlying array
	    /// </summary>
	    /// <param name="list"></param>
	    public IfcDirectionDirectionRatios(doublewrapper[] list){
	        this.doublewrapper = list;
	    }
	    
	    /// <summary>
	    /// Provides access to index the underlying array
	    /// </summary>
		public doublewrapper this[int index]{
			get{
				if(this.doublewrapper == null)
					this.doublewrapper = new doublewrapper[0];
				return this.doublewrapper[index];
	        }
			set{ if(this.doublewrapper == null)
					this.doublewrapper = new doublewrapper[0];
				this.doublewrapper[index] = value; 
	        }
		}
		
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
	
	public partial class IfcGeometricSetElements{
	    /// <summary>
	    /// Provides index access to the underlying array
	    /// </summary>
	    public IfcGeometricSetSelect this[int index]{
	        get{
	            if(this.Items == null)
	                this.Items = new IfcGeometricRepresentationItem[0];
	            return this.Items[index] as IfcGeometricSetSelect;
	        }
	        set{
	            if(this.Items == null)
	                this.Items = new IfcGeometricRepresentationItem[0];
	            this.Items[index] = value as IfcGeometricRepresentationItem;
	        }
	    }
	}
	
	public partial class IfcLineDir{
	    /// <summary>
	    /// Implicitly converts an IfcVector to an IfcLineDir
	    /// </summary>
	    /// <param name="vec"></param>
	    /// <returns></returns>
	    public static implicit operator IfcLineDir(IfcVector vec){
	        IfcLineDir dir = new IfcLineDir();
	        dir.Item = vec;
	        return dir;
	    }
	}
	
	public partial class IfcLinePnt{
	    /// <summary>
	    /// Implicitly converts an IfcCartesianPoint to an IfcLinePnt
	    /// </summary>
	    /// <param name="pnt"></param>
	    /// <returns></returns>
	    public static implicit operator IfcLinePnt(IfcCartesianPoint pnt){
	        IfcLinePnt l = new IfcLinePnt();
	        l.Item = pnt;
	        return l;
	    }
	}
	
	public partial class IfcMaterialLayerSetMaterialLayers{
	    /// <summary>
	    /// Allows index access to the underlying array
	    /// </summary>
	    public IfcMaterialLayer this[int index]{
	        get{
	            if(this.IfcMaterialLayer == null)
	                this.IfcMaterialLayer = new IfcMaterialLayer[0];
	            return this.IfcMaterialLayer[index];
	        }
	        set{
	            if(this.IfcMaterialLayer == null)
	                this.IfcMaterialLayer = new IfcMaterialLayer[0];
	            this.IfcMaterialLayer[index] = value;
	        }
	    }
	}
	
	public partial class IfcPolylinePoints{
	    /// <summary>
	    /// Allows index access to the underlying array
	    /// </summary>
	    public IfcCartesianPoint this[int index]{
	        get{ if(this.IfcCartesianPoint == null)
	                this.IfcCartesianPoint = new IfcCartesianPoint[0];
	            return this.IfcCartesianPoint[index];
	        }
	        set{
	            if(this.IfcCartesianPoint == null)
	                this.IfcCartesianPoint = new IfcCartesianPoint[0];
	            this.IfcCartesianPoint[index] = value;
	        }
	    }
	}
	
	public partial class IfcTableRows{
	    /// <summary>
	    /// Allows index access to the underlying array
	    /// </summary>
	    public IfcTableRow this[int index]{
	        get{ if(this.IfcTableRow == null)
	                this.IfcTableRow = new IfcTableRow[0];
	            return this.IfcTableRow[index];
	        }
	        set{ if(this.IfcTableRow == null)
	                this.IfcTableRow = new IfcTableRow[0];
	            this.IfcTableRow[index] = value;
	        }
	    }
	}
	
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
		
		/// <summary>
		/// Implicitly converts an IfcDirection to an IfcVectorOrientation
		/// </summary>
		/// <param name="dir"></param>
		/// <returns></returns>
		public static implicit operator IfcVectorOrientation(IfcDirection dir){
			return new IfcVectorOrientation(dir);
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
		/// Constructor allowing the underlying data to be given
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		public IfcCartesianPoint(double x, double y){
		    this.Coordinates = new double[]{x, y};
		}
		/// <summary>
		/// Constructor for a 2D coordinate
		/// </summary>
		/// <param name="id"></param>
		/// <param name="x"></param>
		/// <param name="y"></param>
		public IfcCartesianPoint(string id, double x, double y){
			this.entityid = id;
			this.Coordinates = new double[]{x, y};
		}
		
		/// <summary>
		/// Constructor allowing the underlying data to be given
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="z"></param>
		public IfcCartesianPoint(double x, double y, double z){
		    this.Coordinates = new double[]{x, y, z};
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
			this.Coordinates = new double[]{x, y, z};
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
			this.DirectionRatios = new double[]{x, y};
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		public IfcDirection(doublewrapper x, doublewrapper y){
		    this.DirectionRatios = new doublewrapper[]{x, y};
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="z"></param>
		public IfcDirection(double x, double y, double z){
			this.DirectionRatios = new double[]{x, y, z};
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="z"></param>
		public IfcDirection(doublewrapper x, doublewrapper y, doublewrapper z){
		    this.DirectionRatios = new doublewrapper[]{x, y, z};
		}
		
		/// <summary>
		/// Constructor for 2D directions
		/// </summary>
		/// <param name="id"></param>
		/// <param name="x"></param>
		/// <param name="y"></param>
		public IfcDirection(string id, double x, double y){
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
		public IfcDirection(string id, double x, double y, double z){
			this.entityid = id;
			this.DirectionRatios = new double[]{x, y, z};
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
	        this.Pnt = point;
	        this.Dir = direction;
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
			this.Orientation = orient;
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
