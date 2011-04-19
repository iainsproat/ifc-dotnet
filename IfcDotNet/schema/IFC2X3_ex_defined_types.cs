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
	public partial class IfcDimensionCount1
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
	}
	
	//TODO
	/*
	IfcDoseEquivalentMeasure
    IfcDuration
    IfcDynamicViscosityMeasure
    IfcElectricCapacitanceMeasure
    IfcElectricChargeMeasure
    IfcElectricConductanceMeasure
    IfcElectricCurrentMeasure
    IfcElectricResistanceMeasure
    IfcElectricVoltageMeasure
    IfcEnergyMeasure
    IfcFontStyle
    IfcFontVariant
    IfcFontWeight
    IfcForceMeasure
    IfcFrequencyMeasure
    IfcGloballyUniqueId
    IfcHeatFluxDensityMeasure
    IfcHeatingValueMeasure
    IfcIdentifier
    IfcIlluminanceMeasure
    IfcInductanceMeasure
    IfcInteger
    IfcIntegerCountRateMeasure
    IfcIonConcentrationMeasure
    IfcIsothermalMoistureCapacityMeasure
    IfcKinematicViscosityMeasure
    IfcLabel
    IfcLanguageId
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
	
	//TODO
	/*
	IfcLinearForceMeasure
    IfcLinearMomentMeasure
    IfcLinearStiffnessMeasure
    IfcLinearVelocityMeasure
    IfcLogical
    IfcLuminousFluxMeasure
    IfcLuminousIntensityDistributionMeasure
    IfcLuminousIntensityMeasure
    IfcMagneticFluxDensityMeasure
    IfcMagneticFluxMeasure
    IfcMassDensityMeasure
    IfcMassFlowRateMeasure
    IfcMassMeasure
    IfcMassPerLengthMeasure
    IfcModulusOfElasticityMeasure
    IfcModulusOfLinearSubgradeReactionMeasure
    IfcModulusOfRotationalSubgradeReactionMeasure
    IfcModulusOfSubgradeReactionMeasure
    IfcMoistureDiffusivityMeasure
    IfcMolecularWeightMeasure
    IfcMomentOfInertiaMeasure
    IfcMonetaryMeasure
    IfcMonthInYearNumber
    IfcNonNegativeLengthMeasure
    IfcNormalisedRatioMeasure
    IfcNumericMeasure
    IfcParameterValue
    IfcPHMeasure
    IfcPlanarForceMeasure
	*/
	
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
	
	//TODO
	/*
	IfcPositiveLengthMeasure
    IfcPositivePlaneAngleMeasure
    IfcPositiveRatioMeasure
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

