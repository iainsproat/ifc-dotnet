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
using System.Xml.Serialization;

#pragma warning disable 1591
namespace IfcDotNet.Schema
{
    public partial class IfcAbsorbedDoseMeasure1 : DoubleValueType<IfcAbsorbedDoseMeasure1>{
        public static explicit operator double(IfcAbsorbedDoseMeasure1 msr){
            if(msr == null)
                return 0;
            return msr.Value;
        }
        
        public static implicit operator IfcAbsorbedDoseMeasure1(double d){
            IfcAbsorbedDoseMeasure1 msr = new IfcAbsorbedDoseMeasure1();
            msr.Value = d;
            return msr;
        }
    }
    public partial class IfcAccelerationMeasure1 : DoubleValueType<IfcAccelerationMeasure1>{
        public static explicit operator double(IfcAccelerationMeasure1 msr){
            if(msr == null)
                return 0;
            return msr.Value;
        }
        
        public static implicit operator IfcAccelerationMeasure1(double d){
            IfcAccelerationMeasure1 msr = new IfcAccelerationMeasure1();
            msr.Value = d;
            return msr;
        }
    }
    public partial class IfcAmountOfSubstanceMeasure1 : DoubleValueType<IfcAmountOfSubstanceMeasure1>{
        public static explicit operator double(IfcAmountOfSubstanceMeasure1 msr){
            if(msr == null)
                return 0;
            return msr.Value;
        }
        
        public static implicit operator IfcAmountOfSubstanceMeasure1(double d){
            IfcAmountOfSubstanceMeasure1 msr = new IfcAmountOfSubstanceMeasure1();
            msr.Value = d;
            return msr;
        }
    }
    public partial class IfcAngularVelocityMeasure1 : DoubleValueType<IfcAngularVelocityMeasure1>{
        public static explicit operator double(IfcAngularVelocityMeasure1 msr){
            if(msr == null)
                return 0;
            return msr.Value;
        }
        
        public static implicit operator IfcAngularVelocityMeasure1(double d){
            IfcAngularVelocityMeasure1 msr = new IfcAngularVelocityMeasure1();
            msr.Value = d;
            return msr;
        }
    }
    public partial class IfcAreaMeasure1 : DoubleValueType<IfcAreaMeasure1>{
        public static explicit operator double(IfcAreaMeasure1 msr){
            if(msr == null)
                return 0;
            return msr.Value;
        }
        
        public static implicit operator IfcAreaMeasure1(double d){
            IfcAreaMeasure1 msr = new IfcAreaMeasure1();
            msr.Value = d;
            return msr;
        }
    }
    public partial class IfcBoolean1 : ValueType<bool, IfcBoolean1>{
		public static explicit operator bool(IfcBoolean1 b){
			if(b == null)
				return false;
			return b.Value;
		}
		public static implicit operator IfcBoolean1(bool b){
			IfcBoolean1 msr = new IfcBoolean1();
			msr.Value = b;
			return msr;
		}
	}
    public partial class IfcBoxAlignment1 : ValueType<string, IfcBoxAlignment1>{
		public static explicit operator string(IfcBoxAlignment1 box){
			if(box == null)
				return string.Empty;
			return box.Value;
		}
		public static implicit operator IfcBoxAlignment1(string s){
			IfcBoxAlignment1 box = new IfcBoxAlignment1();
			box.Value = s;
			return box;
		}
	}
    /*
	 //TODO New Type in IFC2x4.
	public partial class IfcCardinalPointReference : ValueType<string, IfcCardinalPointReference>{
	}
     */
    public partial class IfcComplexNumber1 {
        public static explicit operator doublewrapper[](IfcComplexNumber1 msr){
            if(msr == null)
                return null;
            return msr.Items;
        }
        
        public static implicit operator IfcComplexNumber1(doublewrapper[] d){
            IfcComplexNumber1 msr = new IfcComplexNumber1();
            msr.Items = d;
            return msr;
        }
        
        public static explicit operator double[](IfcComplexNumber1 cnr){
            if(cnr == null)
                return null;
            if(cnr.Items == null)
                return null;
            double[] dw = new double[cnr.Length];
            for(int i = 0; i < cnr.Length; i++){
                dw[i] = (double)cnr[i];
            }
            return dw;
        }
        
        public static implicit operator IfcComplexNumber1(double[] d){
            IfcComplexNumber1 cnr = new IfcComplexNumber1();
            if(d == null)
                return cnr;
            cnr.Items = new doublewrapper[d.Length];
            for(int i = 0; i < d.Length; i++){
                cnr[i] = d[i];
            }
            return cnr;
        }
    }
    
    public partial class IfcCompoundPlaneAngleMeasure1{
        public static explicit operator longwrapper[](IfcCompoundPlaneAngleMeasure1 msr){
            if(msr == null)
                return null;
            return msr.Items;
        }
        
        public static implicit operator IfcCompoundPlaneAngleMeasure1(longwrapper[] b){
            IfcCompoundPlaneAngleMeasure1 msr = new IfcCompoundPlaneAngleMeasure1();
            msr.Items = b;
            return msr;
        }
        
        public static explicit operator long[](IfcCompoundPlaneAngleMeasure1 msr){
            if(msr == null)
                return null;
            if(msr.Items == null)
                return null;
            long[] d = new long[msr.Length];
            for(int i = 0; i < msr.Length; i++){
                d[i] = (long)msr[i];
            }
            return d;
        }
        
        public static implicit operator IfcCompoundPlaneAngleMeasure1(long[] d){
            IfcCompoundPlaneAngleMeasure1 msr = new IfcCompoundPlaneAngleMeasure1();
            if(d == null)
                return msr;
            msr.Items = new longwrapper[d.Length];
            for(int i = 0; i < d.Length; i++){
                msr[i] = (longwrapper)d[i];
            }
            return msr;
        }
    }
    public partial class IfcContextDependentMeasure1 : DoubleValueType<IfcContextDependentMeasure1>{
        public static explicit operator double(IfcContextDependentMeasure1 msr){
            if(msr == null)
                return 0;
            return msr.Value;
        }
        
        public static implicit operator IfcContextDependentMeasure1(double d){
            IfcContextDependentMeasure1 msr = new IfcContextDependentMeasure1();
            msr.Value = d;
            return msr;
        }
    }
    public partial class IfcCountMeasure1 : DoubleValueType<IfcCountMeasure1>{
        public static explicit operator double(IfcCountMeasure1 msr){
            if(msr == null)
                return 0;
            return msr.Value;
        }
        
        public static implicit operator IfcCountMeasure1(double d){
            IfcCountMeasure1 msr = new IfcCountMeasure1();
            msr.Value = d;
            return msr;
        }
    }
    public partial class IfcCurvatureMeasure1 : DoubleValueType<IfcCurvatureMeasure1>{
        public static explicit operator double(IfcCurvatureMeasure1 msr){
            if(msr == null)
                return 0;
            return msr.Value;
        }
        
        public static implicit operator IfcCurvatureMeasure1(double d){
            IfcCurvatureMeasure1 msr = new IfcCurvatureMeasure1();
            msr.Value = d;
            return msr;
        }
    }
    /*
	//TODO New type in IFC Release 2x4
	public partial class IfcDate{
		public static explicit operator double(IfcDate msr){
			if(msr == null)
				return 0;
			
			return msr.Value;
		}
		
		public static implicit operator IfcDate1(double d){
			IfcDate1 msr = new IfcDate1();
			msr.Value = d;
			return msr;
		}
	}
     */
    /*
	//TODO New type in IFC Release 2x4
	public partial class IfcDateTime{
		public static explicit operator double(IfcDateTime msr){
			if(msr == null)
				return 0;
			
			return msr.Value;
		}
		
		public static implicit operator IfcDateTime(double d){
			IfcDateTime msr = new IfcDateTime();
			msr.Value = d;
			return msr;
		}
	}
     */
    
    public partial class IfcDayInMonthNumber1 : ValueType<long, IfcDayInMonthNumber1>{
        public static explicit operator long(IfcDayInMonthNumber1 msr){
            if(msr == null)
                return 0;
            return msr.Value;
        }
        
        public static implicit operator IfcDayInMonthNumber1(long d){
            IfcDayInMonthNumber1 msr = new IfcDayInMonthNumber1();
            msr.Value = d;
            return msr;
        }
    }
    
    /*
	 //TODO New type in IFC2x4.
	public partial class IfcDayInWeekNumber1{
		public static explicit operator long(IfcDayInWeekNumber1 msr){
			if(msr == null)
				return 0;
			return msr.Value;
		}
		
		public static implicit operator IfcDayInWeekNumber1(long d){
			IfcDayInMonthNumber1 msr = new IfcDayInWeekNumber1();
			msr.Value = d;
			return msr;
		}
	}
     */
    
    public partial class IfcDescriptiveMeasure1 : ValueType<string, IfcDescriptiveMeasure1>{
        public static explicit operator string(IfcDescriptiveMeasure1 msr){
            if(msr == null)
                return string.Empty;
            return msr.Value;
        }
        
        public static implicit operator IfcDescriptiveMeasure1(string d){
            IfcDescriptiveMeasure1 msr = new IfcDescriptiveMeasure1();
            msr.Value = d;
            return msr;
        }
    }
    
    /// <summary>
    /// Definition from ISO/CD 10303-42:1992: A dimension count is a positive integer used to define the coordinate space dimensionality.
    /// </summary>
    public partial class IfcDimensionCount1 : ValueType<long, IfcDimensionCount1>
    {
        public IfcDimensionCount1(){}
        public IfcDimensionCount1(int i){
            this.Value = (long)i;
        }
        public IfcDimensionCount1(long l){
            this.Value = l;
        }
        
        //FIXME remove the below methods by implementing and inheriting from LongValueType
        /// <summary>
        /// A helper method for casting from an integer value to an IfcDimensionCount1
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static implicit operator IfcDimensionCount1(int i){
            return new IfcDimensionCount1(i);
        }
        
        public static bool operator <(IfcDimensionCount1 a, IfcDimensionCount1 b){
            if(a == null) throw new ArgumentNullException("a");
            if(b == null) throw new ArgumentNullException("b");
            return a.Value < b.Value;
        }
        
        public static bool operator >(IfcDimensionCount1 a, IfcDimensionCount1 b){
            if(a == null) throw new ArgumentNullException("a");
            if(b == null) throw new ArgumentNullException("b");
            return a.Value > b.Value;
        }

    }
    
    public partial class IfcDoseEquivalentMeasure1 : DoubleValueType<IfcDoseEquivalentMeasure1>{
        public static explicit operator double(IfcDoseEquivalentMeasure1 msr){
            if(msr == null)
                return 0;
            return msr.Value;
        }
        
        public static implicit operator IfcDoseEquivalentMeasure1(double d){
            IfcDoseEquivalentMeasure1 msr = new IfcDoseEquivalentMeasure1();
            msr.Value = d;
            return msr;
        }
    }
    
    /*
	//TODO New type in IFC Release 2x4
	public partial class IfcDuration1 : DoubleValueType<IfcDuration1>{
		public static explicit operator double(IfcDuration1 msr){
			if(msr == null)
				return 0;
			return msr.Value;
		}
		
		public static implicit operator IfcDuration1(double d){
			IfcDuration1 msr = new IfcDuration1();
			msr.Value = d;
			return msr;
		}
	}
     */
    
    public partial class IfcDynamicViscosityMeasure1 : DoubleValueType<IfcDynamicViscosityMeasure1>{
        public static explicit operator double(IfcDynamicViscosityMeasure1 msr){
            if(msr == null)
                return 0;
            return msr.Value;
        }
        
        public static implicit operator IfcDynamicViscosityMeasure1(double d){
            IfcDynamicViscosityMeasure1 msr = new IfcDynamicViscosityMeasure1();
            msr.Value = d;
            return msr;
        }
    }
    public partial class IfcElectricCapacitanceMeasure1 : DoubleValueType<IfcElectricCapacitanceMeasure1>{
        public static explicit operator double(IfcElectricCapacitanceMeasure1 msr){
            if(msr == null)
                return 0;
            return msr.Value;
        }
        
        public static implicit operator IfcElectricCapacitanceMeasure1(double d){
            IfcElectricCapacitanceMeasure1 msr = new IfcElectricCapacitanceMeasure1();
            msr.Value = d;
            return msr;
        }
    }
    public partial class IfcElectricChargeMeasure1 : DoubleValueType<IfcElectricChargeMeasure1>{
        public static explicit operator double(IfcElectricChargeMeasure1 msr){
            if(msr == null)
                return 0;
            return msr.Value;
        }
        
        public static implicit operator IfcElectricChargeMeasure1(double d){
            IfcElectricChargeMeasure1 msr = new IfcElectricChargeMeasure1();
            msr.Value = d;
            return msr;
        }
    }
    public partial class IfcElectricConductanceMeasure1 : DoubleValueType<IfcElectricConductanceMeasure1>{
        public static explicit operator double(IfcElectricConductanceMeasure1 msr){
            if(msr == null)
                return 0;
            return msr.Value;
        }
        
        public static implicit operator IfcElectricConductanceMeasure1(double d){
            IfcElectricConductanceMeasure1 msr = new IfcElectricConductanceMeasure1();
            msr.Value = d;
            return msr;
        }
    }
    public partial class IfcElectricCurrentMeasure1 : DoubleValueType<IfcElectricCurrentMeasure1>{
        public static explicit operator double(IfcElectricCurrentMeasure1 msr){
            if(msr == null)
                return 0;
            return msr.Value;
        }
        
        public static implicit operator IfcElectricCurrentMeasure1(double d){
            IfcElectricCurrentMeasure1 msr = new IfcElectricCurrentMeasure1();
            msr.Value = d;
            return msr;
        }
    }
    public partial class IfcElectricResistanceMeasure1 : DoubleValueType<IfcElectricResistanceMeasure1>{
        public static explicit operator double(IfcElectricResistanceMeasure1 msr){
            if(msr == null)
                return 0;
            return msr.Value;
        }
        
        public static implicit operator IfcElectricResistanceMeasure1(double d){
            IfcElectricResistanceMeasure1 msr = new IfcElectricResistanceMeasure1();
            msr.Value = d;
            return msr;
        }
    }
    public partial class IfcElectricVoltageMeasure1 : DoubleValueType<IfcElectricVoltageMeasure1>{
        public static explicit operator double(IfcElectricVoltageMeasure1 msr){
            if(msr == null)
                return 0;
            return msr.Value;
        }
        
        public static implicit operator IfcElectricVoltageMeasure1(double d){
            IfcElectricVoltageMeasure1 msr = new IfcElectricVoltageMeasure1();
            msr.Value = d;
            return msr;
        }
    }
    public partial class IfcEnergyMeasure1 : DoubleValueType<IfcEnergyMeasure1>{
        public static explicit operator double(IfcEnergyMeasure1 msr){
            if(msr == null)
                return 0;
            return msr.Value;
        }
        
        public static implicit operator IfcEnergyMeasure1(double d){
            IfcEnergyMeasure1 msr = new IfcEnergyMeasure1();
            msr.Value = d;
            return msr;
        }
    }
    public partial class IfcFontStyle1 : ValueType<string, IfcFontStyle1>{
        public static explicit operator string(IfcFontStyle1 msr){
            if(msr == null)
                return string.Empty;
            return msr.Value;
        }
        
        public static implicit operator IfcFontStyle1(string d){
            IfcFontStyle1 msr = new IfcFontStyle1();
            msr.Value = d;
            return msr;
        }
    }
    public partial class IfcFontVariant1 : ValueType<string, IfcFontVariant1>{
        public static explicit operator string(IfcFontVariant1 msr){
            if(msr == null)
                return string.Empty;
            return msr.Value;
        }
        
        public static implicit operator IfcFontVariant1(string d){
            IfcFontVariant1 msr = new IfcFontVariant1();
            msr.Value = d;
            return msr;
        }
    }
    public partial class IfcFontWeight1 : ValueType<string, IfcFontWeight1>{
        public static explicit operator string(IfcFontWeight1 msr){
            if(msr == null)
                return string.Empty;
            return msr.Value;
        }
        
        public static implicit operator IfcFontWeight1(string d){
            IfcFontWeight1 msr = new IfcFontWeight1();
            msr.Value = d;
            return msr;
        }
    }
    public partial class IfcForceMeasure1 : DoubleValueType<IfcForceMeasure1>{
        public static explicit operator double(IfcForceMeasure1 msr){
            if(msr == null)
                return 0;
            return msr.Value;
        }
        
        public static implicit operator IfcForceMeasure1(double d){
            IfcForceMeasure1 msr = new IfcForceMeasure1();
            msr.Value = d;
            return msr;
        }
    }
    public partial class IfcFrequencyMeasure1 : DoubleValueType<IfcFrequencyMeasure1>{
        public static explicit operator double(IfcFrequencyMeasure1 msr){
            if(msr == null)
                return 0;
            return msr.Value;
        }
        
        public static implicit operator IfcFrequencyMeasure1(double d){
            IfcFrequencyMeasure1 msr = new IfcFrequencyMeasure1();
            msr.Value = d;
            return msr;
        }
    }
    public partial class IfcGloballyUniqueId1 : ValueType<string, IfcGloballyUniqueId1>{
        public static explicit operator string(IfcGloballyUniqueId1 msr){
            if(msr == null)
                return string.Empty;
            return msr.Value;
        }
        
        public static implicit operator IfcGloballyUniqueId1(string d){
            IfcGloballyUniqueId1 msr = new IfcGloballyUniqueId1();
            msr.Value = d;
            return msr;
        }
    }
    public partial class IfcHeatFluxDensityMeasure1 : DoubleValueType<IfcHeatFluxDensityMeasure1>{
        public static explicit operator double(IfcHeatFluxDensityMeasure1 msr){
            if(msr == null)
                return 0;
            return msr.Value;
        }
        
        public static implicit operator IfcHeatFluxDensityMeasure1(double d){
            IfcHeatFluxDensityMeasure1 msr = new IfcHeatFluxDensityMeasure1();
            msr.Value = d;
            return msr;
        }
    }
    public partial class IfcHeatingValueMeasure1 : DoubleValueType<IfcHeatingValueMeasure1>{
        public static explicit operator double(IfcHeatingValueMeasure1 msr){
            if(msr == null)
                return 0;
            return msr.Value;
        }
        
        public static implicit operator IfcHeatingValueMeasure1(double d){
            IfcHeatingValueMeasure1 msr = new IfcHeatingValueMeasure1();
            msr.Value = d;
            return msr;
        }
    }
    public partial class IfcIdentifier1 : ValueType<string, IfcIdentifier1>{
        public static explicit operator string(IfcIdentifier1 msr){
            if(msr == null)
                return string.Empty;
            return msr.Value;
        }
        
        public static implicit operator IfcIdentifier1(string d){
            IfcIdentifier1 msr = new IfcIdentifier1();
            msr.Value = d;
            return msr;
        }
    }
	public partial class IfcIlluminanceMeasure1 : DoubleValueType<IfcIlluminanceMeasure1>{
        public static explicit operator double(IfcIlluminanceMeasure1 msr){
            if(msr == null)
                return 0;
            return msr.Value;
        }
        
        public static implicit operator IfcIlluminanceMeasure1(double d){
            IfcIlluminanceMeasure1 msr = new IfcIlluminanceMeasure1();
            msr.Value = d;
            return msr;
        }
    }
    public partial class IfcInductanceMeasure1 : DoubleValueType<IfcInductanceMeasure1>{
        public static explicit operator double(IfcInductanceMeasure1 msr){
            if(msr == null)
                return 0;
            return msr.Value;
        }
        
        public static implicit operator IfcInductanceMeasure1(double d){
            IfcInductanceMeasure1 msr = new IfcInductanceMeasure1();
            msr.Value = d;
            return msr;
        }
    }
    public partial class IfcInteger1 : ValueType<Int64, IfcInteger1>{
        public static explicit operator Int64(IfcInteger1 msr){
            if(msr == null)
                return 0;
            return msr.Value;
        }
        
        public static implicit operator IfcInteger1(Int64 d){
            IfcInteger1 msr = new IfcInteger1();
            msr.Value = d;
            return msr;
        }
    }
    public partial class IfcIntegerCountRateMeasure1 : ValueType<Int64, IfcIntegerCountRateMeasure1>{
        public static explicit operator Int64(IfcIntegerCountRateMeasure1 msr){
            if(msr == null)
                return 0;
            return msr.Value;
        }
        
        public static implicit operator IfcIntegerCountRateMeasure1(Int64 d){
            IfcIntegerCountRateMeasure1 msr = new IfcIntegerCountRateMeasure1();
            msr.Value = d;
            return msr;
        }
    }
	public partial class IfcIonConcentrationMeasure1 : DoubleValueType<IfcIonConcentrationMeasure1>{
        public static explicit operator double(IfcIonConcentrationMeasure1 msr){
            if(msr == null)
                return 0;
            return msr.Value;
        }
        
        public static implicit operator IfcIonConcentrationMeasure1(double d){
            IfcIonConcentrationMeasure1 msr = new IfcIonConcentrationMeasure1();
            msr.Value = d;
            return msr;
        }
    }
    public partial class IfcIsothermalMoistureCapacityMeasure1 : DoubleValueType<IfcIsothermalMoistureCapacityMeasure1>{
        public static explicit operator double(IfcIsothermalMoistureCapacityMeasure1 msr){
            if(msr == null)
                return 0;
            return msr.Value;
        }
        
        public static implicit operator IfcIsothermalMoistureCapacityMeasure1(double d){
            IfcIsothermalMoistureCapacityMeasure1 msr = new IfcIsothermalMoistureCapacityMeasure1();
            msr.Value = d;
            return msr;
        }
    }
    public partial class IfcKinematicViscosityMeasure1 : DoubleValueType<IfcKinematicViscosityMeasure1>{
        public static explicit operator double(IfcKinematicViscosityMeasure1 msr){
            if(msr == null)
                return 0;
            return msr.Value;
        }
        
        public static implicit operator IfcKinematicViscosityMeasure1(double d){
            IfcKinematicViscosityMeasure1 msr = new IfcKinematicViscosityMeasure1();
            msr.Value = d;
            return msr;
        }
    }
    
    public partial class IfcLabel1 : ValueType<string, IfcLabel1>{
        public static explicit operator string(IfcLabel1 msr){
            if(msr == null)
                return string.Empty;
            return msr.Value;
        }
        
        public static implicit operator IfcLabel1(string d){
            IfcLabel1 msr = new IfcLabel1();
            msr.Value = d;
            return msr;
        }
    }
    /*
	//TODO New defined datatype in IFC2x4.
	public partial class IfcLanguageId1 : ValueType<string,IfcLanguageId1>{
		public static explicit operator string(IfcLanguageId1 msr){
			if(msr == null)
				return string.Empty;
			return msr.Value;
		}
		
		public static implicit operator IfcLanguageId1(string d){
			IfcLanguageId1 msr = new IfcLanguageId1();
			msr.Value = d;
			return msr;
		}
	}
     */    
    
    /// <summary>
    /// Definition from ISO/CD 10303-41:1992: A length measure is the value of a distance.
    /// </summary>
    public partial class IfcLengthMeasure1 : ValueType<double, IfcLengthMeasure1>
    {
        public IfcLengthMeasure1(){}
        public IfcLengthMeasure1(double value){
            this.Value = value;
        }
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
        
        public static IfcLengthMeasure1 operator +(IfcLengthMeasure1 a, IfcLengthMeasure1 b){
            return new IfcLengthMeasure1(a.Value + b.Value);
        }
    }       
    
    public partial class IfcLinearForceMeasure1 : DoubleValueType<IfcLinearForceMeasure1>{
        public static explicit operator double(IfcLinearForceMeasure1 msr){
            if(msr == null)
                return 0;
            return msr.Value;
        }
        
        public static implicit operator IfcLinearForceMeasure1(double d){
            IfcLinearForceMeasure1 msr = new IfcLinearForceMeasure1();
            msr.Value = d;
            return msr;
        }
    }        
    
    public partial class IfcLinearMomentMeasure1 : DoubleValueType<IfcLinearMomentMeasure1>{
        public static explicit operator double(IfcLinearMomentMeasure1 msr){
            if(msr == null)
                return 0;
            return msr.Value;
        }
        
        public static implicit operator IfcLinearMomentMeasure1(double d){
            IfcLinearMomentMeasure1 msr = new IfcLinearMomentMeasure1();
            msr.Value = d;
            return msr;
        }
    }
    
    public partial class IfcLinearStiffnessMeasure1 : DoubleValueType<IfcLinearStiffnessMeasure1>{
        public static explicit operator double(IfcLinearStiffnessMeasure1 msr){
            if(msr == null)
                return 0;
            return msr.Value;
        }
        
        public static implicit operator IfcLinearStiffnessMeasure1(double d){
            IfcLinearStiffnessMeasure1 msr = new IfcLinearStiffnessMeasure1();
            msr.Value = d;
            return msr;
        }
    }
    
    public partial class IfcLinearVelocityMeasure1 : DoubleValueType<IfcLinearVelocityMeasure1>{
        public static explicit operator double(IfcLinearVelocityMeasure1 msr){
            if(msr == null)
                return 0;
            return msr.Value;
        }
        
        public static implicit operator IfcLinearVelocityMeasure1(double d){
            IfcLinearVelocityMeasure1 msr = new IfcLinearVelocityMeasure1();
            msr.Value = d;
            return msr;
        }
    }
    
    public partial class IfcLogical1 : ValueType<IfcLogical, IfcLogical1>{
        public static explicit operator IfcLogical(IfcLogical1 msr){
            if(msr == null)
                return IfcLogical.unknown;
            return msr.Value;
        }
        
        public static implicit operator IfcLogical1(IfcLogical d){
            IfcLogical1 msr = new IfcLogical1();
            msr.Value = d;
            return msr;
        }
    }
    
    public partial class IfcLuminousFluxMeasure1 : DoubleValueType<IfcLuminousFluxMeasure1>{
        public static explicit operator double(IfcLuminousFluxMeasure1 msr){
            if(msr == null)
                return 0;
            return msr.Value;
        }
        
        public static implicit operator IfcLuminousFluxMeasure1(double d){
            IfcLuminousFluxMeasure1 msr = new IfcLuminousFluxMeasure1();
            msr.Value = d;
            return msr;
        }
    }
    
    public partial class IfcLuminousIntensityDistributionMeasure1 : DoubleValueType<IfcLuminousIntensityDistributionMeasure1>{
        public static explicit operator double(IfcLuminousIntensityDistributionMeasure1 msr){
            if(msr == null)
                return 0;
            return msr.Value;
        }
        
        public static implicit operator IfcLuminousIntensityDistributionMeasure1(double d){
            IfcLuminousIntensityDistributionMeasure1 msr = new IfcLuminousIntensityDistributionMeasure1();
            msr.Value = d;
            return msr;
        }
    }
    
    public partial class IfcLuminousIntensityMeasure1 : DoubleValueType<IfcLuminousIntensityMeasure1>{
        public static explicit operator double(IfcLuminousIntensityMeasure1 msr){
            if(msr == null)
                return 0;
            return msr.Value;
        }
        
        public static implicit operator IfcLuminousIntensityMeasure1(double d){
            IfcLuminousIntensityMeasure1 msr = new IfcLuminousIntensityMeasure1();
            msr.Value = d;
            return msr;
        }
    }
    
    public partial class IfcMagneticFluxDensityMeasure1 : DoubleValueType<IfcMagneticFluxDensityMeasure1>{
        public static explicit operator double(IfcMagneticFluxDensityMeasure1 msr){
            if(msr == null)
                return 0;
            return msr.Value;
        }
        
        public static implicit operator IfcMagneticFluxDensityMeasure1(double d){
            IfcMagneticFluxDensityMeasure1 msr = new IfcMagneticFluxDensityMeasure1();
            msr.Value = d;
            return msr;
        }
    }
    
    public partial class IfcMagneticFluxMeasure1 : DoubleValueType<IfcMagneticFluxMeasure1>{
        public static explicit operator double(IfcMagneticFluxMeasure1 msr){
            if(msr == null)
                return 0;
            return msr.Value;
        }
        
        public static implicit operator IfcMagneticFluxMeasure1(double d){
            IfcMagneticFluxMeasure1 msr = new IfcMagneticFluxMeasure1();
            msr.Value = d;
            return msr;
        }
    }
    
    public partial class IfcMassDensityMeasure1 : DoubleValueType<IfcMassDensityMeasure1>{
        public static explicit operator double(IfcMassDensityMeasure1 msr){
            if(msr == null)
                return 0;
            return msr.Value;
        }
        
        public static implicit operator IfcMassDensityMeasure1(double d){
            IfcMassDensityMeasure1 msr = new IfcMassDensityMeasure1();
            msr.Value = d;
            return msr;
        }
    }
    
    public partial class IfcMassFlowRateMeasure1 : DoubleValueType<IfcMassFlowRateMeasure1>{
        public static explicit operator double(IfcMassFlowRateMeasure1 msr){
            if(msr == null)
                return 0;
            return msr.Value;
        }
        
        public static implicit operator IfcMassFlowRateMeasure1(double d){
            IfcMassFlowRateMeasure1 msr = new IfcMassFlowRateMeasure1();
            msr.Value = d;
            return msr;
        }
    }
    
    public partial class IfcMassMeasure1 : DoubleValueType<IfcMassMeasure1>{
        public static explicit operator double(IfcMassMeasure1 msr){
            if(msr == null)
                return 0;
            return msr.Value;
        }
        
        public static implicit operator IfcMassMeasure1(double d){
            IfcMassMeasure1 msr = new IfcMassMeasure1();
            msr.Value = d;
            return msr;
        }
    }
    
    public partial class IfcMassPerLengthMeasure1 : DoubleValueType<IfcMassPerLengthMeasure1>{
        public static explicit operator double(IfcMassPerLengthMeasure1 msr){
            if(msr == null)
                return 0;
            return msr.Value;
        }
        
        public static implicit operator IfcMassPerLengthMeasure1(double d){
            IfcMassPerLengthMeasure1 msr = new IfcMassPerLengthMeasure1();
            msr.Value = d;
            return msr;
        }
    }
    
    public partial class IfcModulusOfElasticityMeasure1 : DoubleValueType<IfcModulusOfElasticityMeasure1>{
        public static explicit operator double(IfcModulusOfElasticityMeasure1 msr){
            if(msr == null)
                return 0;
            return msr.Value;
        }
        
        public static implicit operator IfcModulusOfElasticityMeasure1(double d){
            IfcModulusOfElasticityMeasure1 msr = new IfcModulusOfElasticityMeasure1();
            msr.Value = d;
            return msr;
        }
    }
    
    public partial class IfcModulusOfLinearSubgradeReactionMeasure1 : DoubleValueType<IfcModulusOfLinearSubgradeReactionMeasure1>{
        public static explicit operator double(IfcModulusOfLinearSubgradeReactionMeasure1 msr){
            if(msr == null)
                return 0;
            return msr.Value;
        }
        
        public static implicit operator IfcModulusOfLinearSubgradeReactionMeasure1(double d){
            IfcModulusOfLinearSubgradeReactionMeasure1 msr = new IfcModulusOfLinearSubgradeReactionMeasure1();
            msr.Value = d;
            return msr;
        }
    }
    
    public partial class IfcModulusOfRotationalSubgradeReactionMeasure1 : DoubleValueType<IfcModulusOfRotationalSubgradeReactionMeasure1>{
        public static explicit operator double(IfcModulusOfRotationalSubgradeReactionMeasure1 msr){
            if(msr == null)
                return 0;
            return msr.Value;
        }
        
        public static implicit operator IfcModulusOfRotationalSubgradeReactionMeasure1(double d){
            IfcModulusOfRotationalSubgradeReactionMeasure1 msr = new IfcModulusOfRotationalSubgradeReactionMeasure1();
            msr.Value = d;
            return msr;
        }
    }
    
    
    public partial class IfcModulusOfSubgradeReactionMeasure1 : DoubleValueType<IfcModulusOfSubgradeReactionMeasure1>{
        public static explicit operator double(IfcModulusOfSubgradeReactionMeasure1 msr){
            if(msr == null)
                return 0;
            return msr.Value;
        }
        
        public static implicit operator IfcModulusOfSubgradeReactionMeasure1(double d){
            IfcModulusOfSubgradeReactionMeasure1 msr = new IfcModulusOfSubgradeReactionMeasure1();
            msr.Value = d;
            return msr;
        }
    }
    
    public partial class IfcMoistureDiffusivityMeasure1 : DoubleValueType<IfcMoistureDiffusivityMeasure1>{
        public static explicit operator double(IfcMoistureDiffusivityMeasure1 msr){
            if(msr == null)
                return 0;
            return msr.Value;
        }
        
        public static implicit operator IfcMoistureDiffusivityMeasure1(double d){
            IfcMoistureDiffusivityMeasure1 msr = new IfcMoistureDiffusivityMeasure1();
            msr.Value = d;
            return msr;
        }
    }
    
    public partial class IfcMolecularWeightMeasure1 : DoubleValueType<IfcMolecularWeightMeasure1>{
        public static explicit operator double(IfcMolecularWeightMeasure1 msr){
            if(msr == null)
                return 0;
            return msr.Value;
        }
        
        public static implicit operator IfcMolecularWeightMeasure1(double d){
            IfcMolecularWeightMeasure1 msr = new IfcMolecularWeightMeasure1();
            msr.Value = d;
            return msr;
        }
    }
    
    public partial class IfcMomentOfInertiaMeasure1 : DoubleValueType<IfcMomentOfInertiaMeasure1>{
        public static explicit operator double(IfcMomentOfInertiaMeasure1 msr){
            if(msr == null)
                return 0;
            return msr.Value;
        }
        
        public static implicit operator IfcMomentOfInertiaMeasure1(double d){
            IfcMomentOfInertiaMeasure1 msr = new IfcMomentOfInertiaMeasure1();
            msr.Value = d;
            return msr;
        }
    }
    
    public partial class IfcMonetaryMeasure1 : DoubleValueType<IfcMonetaryMeasure1>{
        public static explicit operator double(IfcMonetaryMeasure1 msr){
            if(msr == null)
                return 0;
            return msr.Value;
        }
        
        public static implicit operator IfcMonetaryMeasure1(double d){
            IfcMonetaryMeasure1 msr = new IfcMonetaryMeasure1();
            msr.Value = d;
            return msr;
        }
    }
    
    public partial class IfcMonthInYearNumber1 : ValueType<long, IfcMonthInYearNumber1>{
        public static explicit operator long(IfcMonthInYearNumber1 msr){
            if(msr == null)
                return 0;
            return msr.Value;
        }
        
        public static implicit operator IfcMonthInYearNumber1(long d){
            IfcMonthInYearNumber1 msr = new IfcMonthInYearNumber1();
            msr.Value = d;
            return msr;
        }
    }
    
    /*
	//TODO New type in IFC Release 2x4.
	public partial class IfcNonNegativeLengthMeasure1{
		public static explicit operator double(IfcNonNegativeLengthMeasure1 msr){
			if(msr == null)
				return 0;
			return msr.Value;
		}
		
		public static implicit operator IfcNonNegativeLengthMeasure1(double d){
			IfcNonNegativeLengthMeasure1 msr = new IfcNonNegativeLengthMeasure1();
			msr.Value = d;
			return msr;
		}
	}
     */
    
    public partial class IfcNormalisedRatioMeasure1 : DoubleValueType<IfcNormalisedRatioMeasure1>{
        public static explicit operator double(IfcNormalisedRatioMeasure1 msr){
            if(msr == null)
                return 0;
            return msr.Value;
        }
        
        public static implicit operator IfcNormalisedRatioMeasure1(double d){
            IfcNormalisedRatioMeasure1 msr = new IfcNormalisedRatioMeasure1();
            msr.Value = d;
            return msr;
        }
    }
    
    public partial class IfcNumericMeasure1 : DoubleValueType<IfcNumericMeasure1>{
        public static explicit operator double(IfcNumericMeasure1 msr){
            if(msr == null)
                return 0;
            return msr.Value;
        }
        
        public static implicit operator IfcNumericMeasure1(double d){
            IfcNumericMeasure1 msr = new IfcNumericMeasure1();
            msr.Value = d;
            return msr;
        }
    }
    
    public partial class IfcParameterValue1 : DoubleValueType<IfcParameterValue1>{
        public static explicit operator double(IfcParameterValue1 msr){
            if(msr == null)
                return 0;
            return msr.Value;
        }
        
        public static implicit operator IfcParameterValue1(double d){
            IfcParameterValue1 msr = new IfcParameterValue1();
            msr.Value = d;
            return msr;
        }
    }

    public partial class IfcPHMeasure1 : DoubleValueType<IfcPHMeasure1>{
        public static explicit operator double(IfcPHMeasure1 msr){
            if(msr == null)
                return 0;
            return msr.Value;
        }
        
        public static implicit operator IfcPHMeasure1(double d){
            IfcPHMeasure1 msr = new IfcPHMeasure1();
            msr.Value = d;
            return msr;
        }
    }
    
    public partial class IfcPlanarForceMeasure1 : DoubleValueType<IfcPlanarForceMeasure1>{
        public static explicit operator double(IfcPlanarForceMeasure1 msr){
            if(msr == null)
                return 0;
            return msr.Value;
        }
        
        public static implicit operator IfcPlanarForceMeasure1(double d){
            IfcPlanarForceMeasure1 msr = new IfcPlanarForceMeasure1();
            msr.Value = d;
            return msr;
        }
    }
    
    /// <summary>
    /// Definition from ISO/CD 10303-41:1992: A plane angle measure is the value of an angle in a plane.
    /// </summary>
    public partial class IfcPlaneAngleMeasure1 : DoubleValueType<IfcPlaneAngleMeasure1>
    {
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
    
    public partial class IfcPositiveLengthMeasure1 : DoubleValueType<IfcPositiveLengthMeasure1>{
        public static explicit operator double(IfcPositiveLengthMeasure1 msr){
            if(msr == null)
                return 0;
            return msr.Value;
        }
        
        public static implicit operator IfcPositiveLengthMeasure1(double d){
            IfcPositiveLengthMeasure1 msr = new IfcPositiveLengthMeasure1();
            msr.Value = d;
            return msr;
        }
    }
    
    public partial class IfcPositivePlaneAngleMeasure1 : DoubleValueType<IfcPositivePlaneAngleMeasure1>{
        public static explicit operator double(IfcPositivePlaneAngleMeasure1 msr){
            if(msr == null)
                return 0;
            return msr.Value;
        }
        
        public static implicit operator IfcPositivePlaneAngleMeasure1(double d){
            IfcPositivePlaneAngleMeasure1 msr = new IfcPositivePlaneAngleMeasure1();
            msr.Value = d;
            return msr;
        }
    }
    
    public partial class IfcPositiveRatioMeasure1 : DoubleValueType<IfcPositiveRatioMeasure1>{
        public static explicit operator double(IfcPositiveRatioMeasure1 msr){
            if(msr == null)
                return 0;
            return msr.Value;
        }
        
        public static implicit operator IfcPositiveRatioMeasure1(double d){
            IfcPositiveRatioMeasure1 msr = new IfcPositiveRatioMeasure1();
            msr.Value = d;
            return msr;
        }
    }
    
    //FIXME------------------------------from below
    public partial class IfcPowerMeasure1 : DoubleValueType<IfcPowerMeasure1>{
        public static explicit operator double(IfcPowerMeasure1 msr){
            if(msr == null)
                return 0;
            return msr.Value;
        }
        
        public static implicit operator IfcPowerMeasure1(double d){
            IfcPowerMeasure1 msr = new IfcPowerMeasure1();
            msr.Value = d;
            return msr;
        }
    }
    public partial class IfcPressureMeasure1 : DoubleValueType<IfcPressureMeasure1>{
        public static explicit operator double(IfcPressureMeasure1 msr){
            if(msr == null)
                return 0;
            return msr.Value;
        }
        
        public static implicit operator IfcPressureMeasure1(double d){
            IfcPressureMeasure1 msr = new IfcPressureMeasure1();
            msr.Value = d;
            return msr;
        }
    }
    public partial class IfcRadioActivityMeasure1 : DoubleValueType<IfcRadioActivityMeasure1>{
        public static explicit operator double(IfcRadioActivityMeasure1 msr){
            if(msr == null)
                return 0;
            return msr.Value;
        }
        
        public static implicit operator IfcRadioActivityMeasure1(double d){
            IfcRadioActivityMeasure1 msr = new IfcRadioActivityMeasure1();
            msr.Value = d;
            return msr;
        }
    }
    public partial class IfcRatioMeasure1 : DoubleValueType<IfcRatioMeasure1>{
        public static explicit operator double(IfcRatioMeasure1 msr){
            if(msr == null)
                return 0;
            return msr.Value;
        }
        
        public static implicit operator IfcRatioMeasure1(double d){
            IfcRatioMeasure1 msr = new IfcRatioMeasure1();
            msr.Value = d;
            return msr;
        }
    }
    public partial class IfcReal1 : DoubleValueType<IfcReal1>{
        public static explicit operator double(IfcReal1 msr){
            if(msr == null)
                return 0;
            return msr.Value;
        }
        
        public static implicit operator IfcReal1(double d){
            IfcReal1 msr = new IfcReal1();
            msr.Value = d;
            return msr;
        }
    }
    public partial class IfcRotationalFrequencyMeasure1 : DoubleValueType<IfcRotationalFrequencyMeasure1>{
        public static explicit operator double(IfcRotationalFrequencyMeasure1 msr){
            if(msr == null)
                return 0;
            return msr.Value;
        }
        
        public static implicit operator IfcRotationalFrequencyMeasure1(double d){
            IfcRotationalFrequencyMeasure1 msr = new IfcRotationalFrequencyMeasure1();
            msr.Value = d;
            return msr;
        }
    }
    public partial class IfcRotationalMassMeasure1 : DoubleValueType<IfcRotationalMassMeasure1>{
        public static explicit operator double(IfcRotationalMassMeasure1 msr){
            if(msr == null)
                return 0;
            return msr.Value;
        }
        
        public static implicit operator IfcRotationalMassMeasure1(double d){
            IfcRotationalMassMeasure1 msr = new IfcRotationalMassMeasure1();
            msr.Value = d;
            return msr;
        }
    }
    public partial class IfcRotationalStiffnessMeasure1 : DoubleValueType<IfcRotationalStiffnessMeasure1>{
        public static explicit operator double(IfcRotationalStiffnessMeasure1 msr){
            if(msr == null)
                return 0;
            return msr.Value;
        }
        
        public static implicit operator IfcRotationalStiffnessMeasure1(double d){
            IfcRotationalStiffnessMeasure1 msr = new IfcRotationalStiffnessMeasure1();
            msr.Value = d;
            return msr;
        }
    }
    public partial class IfcSectionModulusMeasure1 : DoubleValueType<IfcSectionModulusMeasure1>{
        public static explicit operator double(IfcSectionModulusMeasure1 msr){
            if(msr == null)
                return 0;
            return msr.Value;
        }
        
        public static implicit operator IfcSectionModulusMeasure1(double d){
            IfcSectionModulusMeasure1 msr = new IfcSectionModulusMeasure1();
            msr.Value = d;
            return msr;
        }
    }
    public partial class IfcSectionalAreaIntegralMeasure1 : DoubleValueType<IfcSectionalAreaIntegralMeasure1>{
        public static explicit operator double(IfcSectionalAreaIntegralMeasure1 msr){
            if(msr == null)
                return 0;
            return msr.Value;
        }
        
        public static implicit operator IfcSectionalAreaIntegralMeasure1(double d){
            IfcSectionalAreaIntegralMeasure1 msr = new IfcSectionalAreaIntegralMeasure1();
            msr.Value = d;
            return msr;
        }
    }
    public partial class IfcShearModulusMeasure1 : DoubleValueType<IfcShearModulusMeasure1>{
        public static explicit operator double(IfcShearModulusMeasure1 msr){
            if(msr == null)
                return 0;
            return msr.Value;
        }
        
        public static implicit operator IfcShearModulusMeasure1(double d){
            IfcShearModulusMeasure1 msr = new IfcShearModulusMeasure1();
            msr.Value = d;
            return msr;
        }
    }
    public partial class IfcSolidAngleMeasure1 : DoubleValueType<IfcSolidAngleMeasure1>{
        public static explicit operator double(IfcSolidAngleMeasure1 msr){
            if(msr == null)
                return 0;
            return msr.Value;
        }
        
        public static implicit operator IfcSolidAngleMeasure1(double d){
            IfcSolidAngleMeasure1 msr = new IfcSolidAngleMeasure1();
            msr.Value = d;
            return msr;
        }
    }
    public partial class IfcSoundPowerMeasure1 : DoubleValueType<IfcSoundPowerMeasure1>{
        public static explicit operator double(IfcSoundPowerMeasure1 msr){
            if(msr == null)
                return 0;
            return msr.Value;
        }
        
        public static implicit operator IfcSoundPowerMeasure1(double d){
            IfcSoundPowerMeasure1 msr = new IfcSoundPowerMeasure1();
            msr.Value = d;
            return msr;
        }
    }
    public partial class IfcSoundPressureMeasure1 : DoubleValueType<IfcSoundPressureMeasure1>{
        public static explicit operator double(IfcSoundPressureMeasure1 msr){
            if(msr == null)
                return 0;
            return msr.Value;
        }
        
        public static implicit operator IfcSoundPressureMeasure1(double d){
            IfcSoundPressureMeasure1 msr = new IfcSoundPressureMeasure1();
            msr.Value = d;
            return msr;
        }
    }
    public partial class IfcSpecificHeatCapacityMeasure1 : DoubleValueType<IfcSpecificHeatCapacityMeasure1>{
        public static explicit operator double(IfcSpecificHeatCapacityMeasure1 msr){
            if(msr == null)
                return 0;
            return msr.Value;
        }
        
        public static implicit operator IfcSpecificHeatCapacityMeasure1(double d){
            IfcSpecificHeatCapacityMeasure1 msr = new IfcSpecificHeatCapacityMeasure1();
            msr.Value = d;
            return msr;
        }
    }
    public partial class IfcTemperatureGradientMeasure1 : DoubleValueType<IfcTemperatureGradientMeasure1>{
        public static explicit operator double(IfcTemperatureGradientMeasure1 msr){
            if(msr == null)
                return 0;
            return msr.Value;
        }
        
        public static implicit operator IfcTemperatureGradientMeasure1(double d){
            IfcTemperatureGradientMeasure1 msr = new IfcTemperatureGradientMeasure1();
            msr.Value = d;
            return msr;
        }
    }
    public partial class IfcThermalAdmittanceMeasure1 : DoubleValueType<IfcThermalAdmittanceMeasure1>{
        public static explicit operator double(IfcThermalAdmittanceMeasure1 msr){
            if(msr == null)
                return 0;
            return msr.Value;
        }
        
        public static implicit operator IfcThermalAdmittanceMeasure1(double d){
            IfcThermalAdmittanceMeasure1 msr = new IfcThermalAdmittanceMeasure1();
            msr.Value = d;
            return msr;
        }
    }
    public partial class IfcThermalConductivityMeasure1 : DoubleValueType<IfcThermalConductivityMeasure1>{
        public static explicit operator double(IfcThermalConductivityMeasure1 msr){
            if(msr == null)
                return 0;
            return msr.Value;
        }
        
        public static implicit operator IfcThermalConductivityMeasure1(double d){
            IfcThermalConductivityMeasure1 msr = new IfcThermalConductivityMeasure1();
            msr.Value = d;
            return msr;
        }
    }
    public partial class IfcThermalExpansionCoefficientMeasure1 : DoubleValueType<IfcThermalExpansionCoefficientMeasure1>{
        public static explicit operator double(IfcThermalExpansionCoefficientMeasure1 msr){
            if(msr == null)
                return 0;
            return msr.Value;
        }
        
        public static implicit operator IfcThermalExpansionCoefficientMeasure1(double d){
            IfcThermalExpansionCoefficientMeasure1 msr = new IfcThermalExpansionCoefficientMeasure1();
            msr.Value = d;
            return msr;
        }
    }
    public partial class IfcThermalResistanceMeasure1 : DoubleValueType<IfcThermalResistanceMeasure1>{
        public static explicit operator double(IfcThermalResistanceMeasure1 msr){
            if(msr == null)
                return 0;
            return msr.Value;
        }
        
        public static implicit operator IfcThermalResistanceMeasure1(double d){
            IfcThermalResistanceMeasure1 msr = new IfcThermalResistanceMeasure1();
            msr.Value = d;
            return msr;
        }
    }
    public partial class IfcThermalTransmittanceMeasure1 : DoubleValueType<IfcThermalTransmittanceMeasure1>{
        public static explicit operator double(IfcThermalTransmittanceMeasure1 msr){
            if(msr == null)
                return 0;
            return msr.Value;
        }
        
        public static implicit operator IfcThermalTransmittanceMeasure1(double d){
            IfcThermalTransmittanceMeasure1 msr = new IfcThermalTransmittanceMeasure1();
            msr.Value = d;
            return msr;
        }
    }
    public partial class IfcThermodynamicTemperatureMeasure1 : DoubleValueType<IfcThermodynamicTemperatureMeasure1>{
        public static explicit operator double(IfcThermodynamicTemperatureMeasure1 msr){
            if(msr == null)
                return 0;
            return msr.Value;
        }
        
        public static implicit operator IfcThermodynamicTemperatureMeasure1(double d){
            IfcThermodynamicTemperatureMeasure1 msr = new IfcThermodynamicTemperatureMeasure1();
            msr.Value = d;
            return msr;
        }
    }
    public partial class IfcTimeMeasure1 : DoubleValueType<IfcTimeMeasure1>{
        public static explicit operator double(IfcTimeMeasure1 msr){
            if(msr == null)
                return 0;
            return msr.Value;
        }
        
        public static implicit operator IfcTimeMeasure1(double d){
            IfcTimeMeasure1 msr = new IfcTimeMeasure1();
            msr.Value = d;
            return msr;
        }
    }
    public partial class IfcTorqueMeasure1 : DoubleValueType<IfcTorqueMeasure1>{
        public static explicit operator double(IfcTorqueMeasure1 msr){
            if(msr == null)
                return 0;
            return msr.Value;
        }
        
        public static implicit operator IfcTorqueMeasure1(double d){
            IfcTorqueMeasure1 msr = new IfcTorqueMeasure1();
            msr.Value = d;
            return msr;
        }
    }
    public partial class IfcVaporPermeabilityMeasure1 : DoubleValueType<IfcVaporPermeabilityMeasure1>{
        public static explicit operator double(IfcVaporPermeabilityMeasure1 msr){
            if(msr == null)
                return 0;
            return msr.Value;
        }
        
        public static implicit operator IfcVaporPermeabilityMeasure1(double d){
            IfcVaporPermeabilityMeasure1 msr = new IfcVaporPermeabilityMeasure1();
            msr.Value = d;
            return msr;
        }
    }
    public partial class IfcVolumeMeasure1 : DoubleValueType<IfcVolumeMeasure1>{
        public static explicit operator double(IfcVolumeMeasure1 msr){
            if(msr == null)
                return 0;
            return msr.Value;
        }
        
        public static implicit operator IfcVolumeMeasure1(double d){
            IfcVolumeMeasure1 msr = new IfcVolumeMeasure1();
            msr.Value = d;
            return msr;
        }
    }
    public partial class IfcVolumetricFlowRateMeasure1 : DoubleValueType<IfcVolumetricFlowRateMeasure1>{
        public static explicit operator double(IfcVolumetricFlowRateMeasure1 msr){
            if(msr == null)
                return 0;
            return msr.Value;
        }
        
        public static implicit operator IfcVolumetricFlowRateMeasure1(double d){
            IfcVolumetricFlowRateMeasure1 msr = new IfcVolumetricFlowRateMeasure1();
            msr.Value = d;
            return msr;
        }
    }
    public partial class IfcWarpingConstantMeasure1 : DoubleValueType<IfcWarpingConstantMeasure1>{
        public static explicit operator double(IfcWarpingConstantMeasure1 msr){
            if(msr == null)
                return 0;
            return msr.Value;
        }
        
        public static implicit operator IfcWarpingConstantMeasure1(double d){
            IfcWarpingConstantMeasure1 msr = new IfcWarpingConstantMeasure1();
            msr.Value = d;
            return msr;
        }
    }
    public partial class IfcWarpingMomentMeasure1 : DoubleValueType<IfcWarpingMomentMeasure1>{
        public static explicit operator double(IfcWarpingMomentMeasure1 msr){
            if(msr == null)
                return 0;
            return msr.Value;
        }
        
        public static implicit operator IfcWarpingMomentMeasure1(double d){
            IfcWarpingMomentMeasure1 msr = new IfcWarpingMomentMeasure1();
            msr.Value = d;
            return msr;
        }
    }
    public partial class IfcSpecularExponent1 : DoubleValueType<IfcSpecularExponent1>{
        public static explicit operator double(IfcSpecularExponent1 msr){
            if(msr == null)
                return 0;
            return msr.Value;
        }
        
        public static implicit operator IfcSpecularExponent1(double d){
            IfcSpecularExponent1 msr = new IfcSpecularExponent1();
            msr.Value = d;
            return msr;
        }
    }
    public partial class IfcSpecularRoughness1 : DoubleValueType<IfcSpecularRoughness1>{
        public static explicit operator double(IfcSpecularRoughness1 msr){
            if(msr == null)
                return 0;
            return msr.Value;
        }
        
        public static implicit operator IfcSpecularRoughness1(double d){
            IfcSpecularRoughness1 msr = new IfcSpecularRoughness1();
            msr.Value = d;
            return msr;
        }
    }
    public partial class IfcSecondInMinute1 : DoubleValueType<IfcSecondInMinute1>{
        public static explicit operator double(IfcSecondInMinute1 msr){
            if(msr == null)
                return 0;
            return msr.Value;
        }
        
        public static implicit operator IfcSecondInMinute1(double d){
            IfcSecondInMinute1 msr = new IfcSecondInMinute1();
            msr.Value = d;
            return msr;
        }
    }
    
    //TODO
    /*
    IfcPresentableText
    IfcSpecularExponent
    IfcSpecularRoughness
    IfcTemperatureRateOfChangeMeasure
    IfcText
    IfcTextAlignment
    IfcTextDecoration
    IfcTextFontName
    IfcTextTransformation
    IfcTime
    IfcTimeStamp
    IfcURIReference
    */
}

