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
	
	
	
	public partial class IfcAbsorbedDoseMeasure1{
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
	
	public partial class IfcAccelerationMeasure1{
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
	
	public partial class IfcAmountOfSubstanceMeasure1{
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
	
	
	public partial class IfcAngularVelocityMeasure1{
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
	
	public partial class IfcAreaMeasure1{
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
	
	public partial class IfcBoolean1{
		public static explicit operator bool(IfcBoolean1 msr){
			if(msr == null)
				return false;
			return msr.Value;
		}
		
		public static implicit operator IfcBoolean1(bool b){
			IfcBoolean1 msr = new IfcBoolean1();
			msr.Value = b;
			return msr;
		}
	}
	
	public partial class IfcBoxAlignment1{
		public static explicit operator string(IfcBoxAlignment1 msr){
			if(msr == null)
				return string.Empty;
			return msr.Value;
		}
		
		public static implicit operator IfcBoxAlignment1(string b){
			IfcBoxAlignment1 msr = new IfcBoxAlignment1();
			msr.Value = b;
			return msr;
		}
	}
	
	/*
	 //TODO New Type in IFC2x4.
	public partial class IfcCardinalPointReference{
		public static explicit operator string(IfcCardinalPointReference msr){
			if(msr == null)
				return string.Empty;
			return msr;
		}
		
		public static implicit operator IfcCardinalPointReference1(string b){
			IfcCardinalPointReference1 msr = new IfcCardinalPointReference1();
			msr.Value = b;
			return msr;
		}
	}
	*/
	
	public partial class IfcComplexNumber1{
		public static explicit operator doublewrapper[](IfcComplexNumber1 msr){
			if(msr == null)
				return null;
			return msr.doublewrapper;
		}
		
		public static implicit operator IfcComplexNumber1(doublewrapper[] d){
			IfcComplexNumber1 msr = new IfcComplexNumber1();
			msr.doublewrapper = d;
			return msr;
		}
		
		public static explicit operator double[](IfcComplexNumber1 cnr){
			if(cnr == null)
				return null;
			if(cnr.doublewrapper == null)
				return null;
			double[] dw = new double[cnr.doublewrapper.Length];
			for(int i = 0; i < cnr.doublewrapper.Length; i++){
				dw[i] = (double)cnr.doublewrapper[i];
			}
			return dw;
		}
		
		public static implicit operator IfcComplexNumber1(double[] d){
			IfcComplexNumber1 cnr = new IfcComplexNumber1();
			if(d == null)
				return cnr;
			cnr.doublewrapper = new doublewrapper[d.Length];
			for(int i = 0; i < d.Length; i++){
				cnr.doublewrapper[i] = d[i];
			}
			return cnr;
		}
	}
	
	public partial class IfcCompoundPlaneAngleMeasure1{
		public static explicit operator longwrapper[](IfcCompoundPlaneAngleMeasure1 msr){
			if(msr == null)
				return null;
			return msr.longwrapper;
		}
		
		public static implicit operator IfcCompoundPlaneAngleMeasure1(longwrapper[] b){
			IfcCompoundPlaneAngleMeasure1 msr = new IfcCompoundPlaneAngleMeasure1();
			msr.longwrapper = b;
			return msr;
		}
		
		public static explicit operator long[](IfcCompoundPlaneAngleMeasure1 msr){
			if(msr == null)
				return null;
			if(msr.longwrapper == null)
				return null;
			long[] d = new long[msr.longwrapper.Length];
			for(int i = 0; i < msr.longwrapper.Length; i++){
				d[i] = (long)msr.longwrapper[i];
			}
			return d;
		}
		
		public static implicit operator IfcCompoundPlaneAngleMeasure1(long[] d){
			IfcCompoundPlaneAngleMeasure1 msr = new IfcCompoundPlaneAngleMeasure1();
			if(d == null)
				return msr;
			msr.longwrapper = new longwrapper[d.Length];
			for(int i = 0; i < d.Length; i++){
				msr.longwrapper[i] = d[i];
			}
			return msr;
		}
	}
	
	public partial class IfcContextDependentMeasure1{
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
	
	public partial class IfcCountMeasure1{
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
	
	public partial class IfcCurvatureMeasure1{
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
	
	public partial class IfcDayInMonthNumber1{
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
	
	public partial class IfcDescriptiveMeasure1{
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
	public partial class IfcDimensionCount1 : IEquatable<IfcDimensionCount1>
	{
		/// <summary>
		/// A helper method for casting from an IfcDimensionCount1 value to an integer
		/// </summary>
		/// <param name="cnt"></param>
		/// <returns></returns>
		public static explicit operator long(IfcDimensionCount1 cnt){
			if(cnt == null)
				return 0;
			return cnt.Value;
		}
		
		/// <summary>
		/// A helper method for casting from an integer value to an IfcDimensionCount1
		/// </summary>
		/// <param name="i"></param>
		/// <returns></returns>
		public static implicit operator IfcDimensionCount1(int i){
			IfcDimensionCount1 cnt = new IfcDimensionCount1();
			cnt.Value = (long)i;
			return cnt;
		}
		
		/// <summary>
		/// A helper method for casting from a long value to an IfcDimensionCount1
		/// </summary>
		/// <param name="i"></param>
		/// <returns></returns>
		public static implicit operator IfcDimensionCount1(long i){
			IfcDimensionCount1 cnt = new IfcDimensionCount1();
			cnt.Value = i;
			return cnt;
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
		
		#region Equals and GetHashCode implementation
		public override bool Equals(object obj)
		{
			IfcDimensionCount1 other = obj as IfcDimensionCount1;
			if (other == null)
				return false;
			return this.Equals(other);
		}
		
		public bool Equals(IfcDimensionCount1 other){
			if(other == null)
				return false;
			return this.Value.Equals( other.Value );
		}
		
		public override int GetHashCode()
		{
			int hashCode = 0;
			hashCode += this.Value.GetHashCode();
			return hashCode;
		}
		
		public static bool operator ==(IfcDimensionCount1 lhs, IfcDimensionCount1 rhs)
		{
			if (ReferenceEquals(lhs, rhs))
				return true;
			if (ReferenceEquals(lhs, null) || ReferenceEquals(rhs, null))
				return false;
			return lhs.Equals(rhs);
		}
		
		public static bool operator !=(IfcDimensionCount1 lhs, IfcDimensionCount1 rhs)
		{
			return !(lhs == rhs);
		}
		#endregion

	}
	
	public partial class IfcDoseEquivalentMeasure1{
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
	public partial class IfcDuration1{
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
	
	public partial class IfcDynamicViscosityMeasure1{
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
	
	public partial class IfcElectricCapacitanceMeasure1{
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
	
	public partial class IfcElectricChargeMeasure1{
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
	
	public partial class IfcElectricConductanceMeasure1{
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
	
	public partial class IfcElectricCurrentMeasure1{
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
	
	public partial class IfcElectricResistanceMeasure1{
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
	
	public partial class IfcElectricVoltageMeasure1{
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
	
	public partial class IfcEnergyMeasure1{
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
	
	public partial class IfcFontStyle1{
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
	
	public partial class IfcFontVariant1{
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
	
	public partial class IfcFontWeight1{
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
	
	public partial class IfcForceMeasure1{
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
	
	public partial class IfcFrequencyMeasure1{
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
	
	public partial class IfcGloballyUniqueId1{
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
	
	public partial class IfcHeatFluxDensityMeasure1{
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
	
	public partial class IfcHeatingValueMeasure1{
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
	
	public partial class IfcIdentifier1{
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
	
	public partial class IfcIlluminanceMeasure1{
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
	
	public partial class IfcInductanceMeasure1{
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
	
	public partial class IfcInteger1{
		public static explicit operator long(IfcInteger1 msr){
			if(msr == null)
				return 0;
			return msr.Value;
		}
		
		public static implicit operator IfcInteger1(long d){
			IfcInteger1 msr = new IfcInteger1();
			msr.Value = d;
			return msr;
		}
	}
	
	public partial class IfcIntegerCountRateMeasure1{
		public static explicit operator long(IfcIntegerCountRateMeasure1 msr){
			if(msr == null)
				return 0;
			return msr.Value;
		}
		
		public static implicit operator IfcIntegerCountRateMeasure1(long d){
			IfcIntegerCountRateMeasure1 msr = new IfcIntegerCountRateMeasure1();
			msr.Value = d;
			return msr;
		}
	}
	
	public partial class IfcIonConcentrationMeasure1{
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
	
	public partial class IfcIsothermalMoistureCapacityMeasure1{
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
	
	public partial class IfcKinematicViscosityMeasure1{
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
	
	public partial class IfcLabel1{
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
	public partial class IfcLanguageId1{
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
	public partial class IfcLengthMeasure1
	{
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
	
	public partial class IfcLinearForceMeasure1{
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
	
	public partial class IfcLinearMomentMeasure1{
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
	
	public partial class IfcLinearStiffnessMeasure1{
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
	
	public partial class IfcLinearVelocityMeasure1{
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
	
	public partial class IfcLogical1{
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
	
	public partial class IfcLuminousFluxMeasure1{
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
	
	public partial class IfcLuminousIntensityDistributionMeasure1{
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
	
	public partial class IfcLuminousIntensityMeasure1{
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
	
	public partial class IfcMagneticFluxDensityMeasure1{
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
	
	public partial class IfcMagneticFluxMeasure1{
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
	
	public partial class IfcMassDensityMeasure1{
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
	
	public partial class IfcMassFlowRateMeasure1{
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
	
	public partial class IfcMassMeasure1{
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
	
	public partial class IfcMassPerLengthMeasure1{
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
	
	public partial class IfcModulusOfElasticityMeasure1{
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
	
	public partial class IfcModulusOfLinearSubgradeReactionMeasure1{
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
	
	public partial class IfcModulusOfRotationalSubgradeReactionMeasure1{
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
	
	public partial class IfcModulusOfSubgradeReactionMeasure1{
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
	
	public partial class IfcMoistureDiffusivityMeasure1{
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
	
	public partial class IfcMolecularWeightMeasure1{
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
	
	public partial class IfcMomentOfInertiaMeasure1{
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
	
	public partial class IfcMonetaryMeasure1{
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
	
	public partial class IfcMonthInYearNumber1{
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
	
	public partial class IfcNormalisedRatioMeasure1{
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
	
	public partial class IfcNumericMeasure1{
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
	
	public partial class IfcParameterValue1{
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

	public partial class IfcPHMeasure1{
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
	
	public partial class IfcPlanarForceMeasure1{
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
	public partial class IfcPlaneAngleMeasure1
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
	
	public partial class IfcPositiveLengthMeasure1{
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
	
	public partial class IfcPositivePlaneAngleMeasure1{
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
	
	public partial class IfcPositiveRatioMeasure1{
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
	
	//TODO
	/*
    IfcPowerMeasure
    IfcPresentableText
    IfcPressureMeasure
    IfcRadioActivityMeasure
    IfcRatioMeasure
    IfcReal
    IfcRotationalFrequencyMeasure
    IfcRotationalMassMeasure
    IfcRotationalStiffnessMeasure
    IfcSectionalAreaIntegralMeasure
    IfcSectionModulusMeasure
    IfcShearModulusMeasure
    IfcSolidAngleMeasure
    IfcSoundPowerMeasure
    IfcSoundPressureMeasure
    IfcSpecificHeatCapacityMeasure
    IfcSpecularExponent
    IfcSpecularRoughness
    IfcTemperatureGradientMeasure
    IfcTemperatureRateOfChangeMeasure
    IfcText
    IfcTextAlignment
    IfcTextDecoration
    IfcTextFontName
    IfcTextTransformation
    IfcThermalAdmittanceMeasure
    IfcThermalConductivityMeasure
    IfcThermalExpansionCoefficientMeasure
    IfcThermalResistanceMeasure
    IfcThermalTransmittanceMeasure
    IfcThermodynamicTemperatureMeasure
    IfcTime
    IfcTimeMeasure
    IfcTimeStamp
    IfcTorqueMeasure
    IfcURIReference
    IfcVaporPermeabilityMeasure
    IfcVolumeMeasure
    IfcVolumetricFlowRateMeasure
    IfcWarpingConstantMeasure
    IfcWarpingMomentMeasure
	*/
}

