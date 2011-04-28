
using System;

#pragma warning disable 1591
namespace IfcDotNet.Schema
{
	//TODO
	/* IfcActorRole
	 * IfcActuator
	 * IfcActuatorType
	 * IfcAddress
	 * IfcAdvancedBrep
	 * IfcAdvancedBrepWithVoids
	 * IfcAdvancedFace
	 * IfcAirTerminal
	 * IfcAirTerminalBox
	 * IfcAirTerminalBoxType
	 * IfcAirTerminalType
	 * IfcAirToAirHeatRecovery
	 * IfcAirToAirHeatRecoveryType
	 * IfcAlarm
	 * IfcAlarmType
	 * IfcAppliedValue
	 * IfcAppliedValueRelationship
	 * IfcApproval
	 * IfcArbitraryClosedProfileDef
	 * IfcArbitraryOpenProfileDef
	 * IfcArbitraryProfileDefWithVoids
	 * IfcAudioVisualAppliance
	 * IfcAudioVisualApplianceType
	 * IfcAxis1Placement
	 * IfcAxis2Placement2D
	 * IfcAxis2Placement3D
	 * IfcBeam
	 * IfcBeamStandardCase
	 * IfcBeamType
	 * IfcBlobTexture
	 * IfcBoiler
	 * IfcBoilerType
	 * IfcBooleanClippingResult
	 * IfcBooleanResult
	 * IfcBoundaryCurve
	 * IfcBoxAlignment
	 * IfcBoxedHalfSpace
	 * IfcBSplineCurve
	 * IfcBSplineCurveWithKnots
	 * IfcBSplineSurfaceWithKnots
	 * IfcBuildingElement
	 * IfcBuildingElementPart
	 * IfcBuildingElementPartType
	 * IfcBuildingElementProxy
	 * IfcBurner
	 * IfcBurnerType
	 * IfcCableCarrierFitting
	 * IfcCableCarrierFittingType
	 * IfcCableCarrierSegment
	 * IfcCableCarrierSegmentType
	 * IfcCableFitting
	 * IfcCableFittingType
	 * IfcCableSegment
	 * IfcCableSegmentType
	 * IfcCardinalPointReference
	 * IfcCartesianPoint
	 * IfcCartesianTransformationOperator
	 * IfcCartesianTransformationOperator2D
	 * IfcCartesianTransformationOperator2DnonUniform
	 * IfcCartesianTransformationOperator3D
	 * IfcCartesianTransformationOperator3DnonUniform
	 * IfcChiller
	 * IfcChillerType
	 * IfcChimney
	 * IfcChimneyType
	 * IfcCircleHollowProfileDef
	 * IfcCoil
	 * IfcCoilType
	 * IfcColumn
	 * IfcColumnStandardCase
	 * IfcColumnType
	 * IfcCommunicationsAppliance
	 * IfcCommunicationsApplianceType
	 * IfcComplexProperty
	 * IfcComplexPropertyTemplate
	 * IfcCompositeCurve
	 * IfcCompositeCurveOnSurface
	 * IfcCompositeCurveSegment
	 * IfcCompositeProfileDef
	 * IfcCompoundPlaneAngleMeasure
	 * IfcCompressor
	 * IfcCompressorType
	 * IfcCondenser
	 * IfcCondenserType
	 * IfcConstraint
	 * IfcConstraintRelationship
	 * IfcConstructionEquipmentResource
	 * IfcConstructionEquipmentResourceType
	 * IfcConstructionMaterialResource
	 * IfcConstructionMaterialResourceType
	 * IfcConstructionProductResource
	 * IfcConstructionProductResourceType
	 * IfcController
	 * IfcControllerType
	 * IfcCooledBeam
	 * IfcCooledBeamType
	 * IfcCoolingTower
	 * IfcCoolingTowerType
	 * IfcCovering
	 * IfcCoveringType
	 */
	
	public partial class IfcOwnerHistory : IHasRules{
		[Rule("CorrectChangeAction", typeof(IfcOwnerHistory), "If ChangeAction is asserted and LastModifiedDate is not defined, ChangeAction must be set to NOTDEFINED")]
		public bool CorrectChangeAction(){
			return this.LastModifiedDate != null ||
				(this.LastModifiedDate == null && this.ChangeAction == IfcChangeActionEnum.notdefined); //HACK notdefined is not added until 2X4
		}
	}
	
	public partial class IfcPositiveRatioMeasure1 : IHasRules{
		[Rule("WR1", typeof(IfcPositiveRatioMeasure1), "A positive measure shall be greater than zero.")]
		public bool WR1(){
			return this.Value > 0;
		}
	}
	
	
}
