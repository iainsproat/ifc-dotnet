﻿
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
	 * IfcCrewResource
	 * IfcCrewResourceType
	 * IfcCShapeProfileDef
	 * IfcCurtainWall
	 * IfcCurtainWallType
	 * IfcCurveStyle
	 * IfcCurveStyleFontPattern
	 * IfcDamper
	 * IfcDamperType
	 * IfcDayInMonthNumber
	 * IfcDayInWeekNumber
	 * IfcDerivedProfileDef
	 * IfcDerivedUnit
	 * IfcDimensionCount
	 * IfcDiscreteAccessory
	 * IfcDiscreteAccessoryType
	 * IfcDistributionChamberElement
	 * IfcDistributionChamberElementType
	 * IfcDocumentElectronicFormat
	 * IfcDocumentReference
	 * IfcDoor
	 * IfcDoorLiningProperties
	 * IfcDoorPanelProperties
	 * IfcDraughtingPreDefinedColour
	 * IfcDraughtingPreDefinedCurveFont
	 * IfcDraughtingPreDefinedTextFont
	 * IfcDuctFitting
	 * IfcDuctFittingType
	 * IfcDuctSegment
	 * IfcDuctSegmentType
	 * IfcDuctSilencer
	 * IfcDuctSilencerType
	 * IfcEdgeLoop
	 * IfcElectricAppliance
	 * IfcElectricApplianceType
	 * IfcElectricDistributionBoard
	 * IfcElectricDistributionBoardType
	 * IfcElectricFlowStorageDevice
	 * IfcElectricFlowStorageDeviceType
	 * IfcElectricGenerator
	 * IfcElectricGeneratorType
	 * IfcElectricMotor
	 * IfcElectricMotorType
	 * IfcElectricTimeControl
	 * IfcElectricTimeControlType
	 * IfcElementAssembly
	 * IfcElementAssemblyType
	 * IfcElementQuantity
	 * IfcEngine
	 * IfcEngineType
	 * IfcEvaporativeCooler
	 * IfcEvaporativeCoolerType
	 * IfcEvaporator
	 * IfcEvaporatorType
	 * IfcEvent
	 * IfcEventType
	 * IfcExternalReference
	 * IfcExtrudedAreaSolid
	 * IfcExtrudedAreaSolidTapered
	 * IfcFace
	 * IfcFan
	 * IfcFanType
	 * IfcFastener
	 * IfcFastenerType
	 * IfcFeatureElementSubtraction
	 * IfcFillAreaStyle
	 * IfcFillAreaStyleHatching
	 * IfcFilter
	 * IfcFilterType
	 * IfcFireSuppressionTerminal
	 * IfcFireSuppressionTerminalType
	 * IfcFlowInstrument
	 * IfcFlowInstrumentType
	 * IfcFlowMeter
	 * IfcFlowMeterType
	 * IfcFontStyle
	 * IfcFontVariant
	 * IfcFontWeight
	 * IfcFooting
	 * IfcFootingType
	 * IfcFurniture
	 * IfcFurnitureType
	 * IfcGeographicElement
	 * IfcGeographicElementType
	 * IfcGeometricCurveSet
	 * IfcGeometricRepresentationContext
	 * IfcGeometricRepresentationSubContext
	 * IfcGeometricSet
	 * IfcGrid
	 * IfcGridAxis
	 * IfcHeatExchanger
	 * IfcHeatExchangerType
	 * IfcHeatingValueMeasure
	 * IfcHumidifier
	 * IfcHumidifierType
	 * IfcInterceptor
	 * IfcInterceptorType
	 * IfcIShapeProfileDef
	 * IfcJunctionBox
	 * IfcJunctionBoxType
	 * IfcLaborResource
	 * IfcLaborResourceType
	 * IfcLamp
	 * IfcLampType
	 * IfcLightFixture
	 * IfcLightFixtureType
	 * IfcLine
	 * IfcLocalPlacement
	 * IfcLShapeProfileDef
	 * IfcMaterialDefinitionRepresentation
	 * IfcMechanicalFastener
	 * IfcMechanicalFastenerType
	 * IfcMedicalDevice
	 * IfcMedicalDeviceType
	 * IfcMember
	 * IfcMemberStandardCase
	 * IfcMemberType
	 * IfcMonthInYearNumber
	 * IfcMotorConnection
	 * IfcMotorConnectionType
	 * IfcNamedUnit
	 * IfcNonNegativeLengthMeasure
	 * IfcNormalisedRatioMeasure
	 * IfcObjective
	 * IfcOccupant
	 * IfcOffsetCurve2D
	 * IfcOffsetCurve3D
	 * IfcOrientedEdge
	 * IfcOutlet
	 * IfcOutletType
	 */
	
	public partial class IfcOwnerHistory : IHasRules{
		[Rule("CorrectChangeAction", typeof(IfcOwnerHistory), "If ChangeAction is asserted and LastModifiedDate is not defined, ChangeAction must be set to NOTDEFINED")]
		public bool CorrectChangeAction(){
			return this.LastModifiedDate != null ||
				(this.LastModifiedDate == null && this.ChangeAction == IfcChangeActionEnum.notdefined); //HACK notdefined is not added until 2X4
		}
	}
	
	//TODO
	/* IfcPath
	 * IfcPCurve
	 * IfcPerson
	 * IfcPHMeasure
	 * IfcPhysicalComplexQuantity
	 * IfcPile
	 * IfcPileType
	 * IfcPipeFitting
	 * IfcPipeFittingType
	 * IfcPipeSegmentType
	 * IfcPixelTexture
	 * IfcPlate
	 * IfcPlateStandardCase
	 * IfcPlateType
	 * IfcPolygonalBoundedHalfSpace
	 * IfcPolyline
	 * IfcPolyLoop
	 * IfcPositiveLengthMeasure
	 * IfcPositivePlaneAngleMeasure
	 */
	
	public partial class IfcPositiveRatioMeasure1 : IHasRules{
		[Rule("WR1", typeof(IfcPositiveRatioMeasure1), "A positive measure shall be greater than zero.")]
		public bool WR1(){
			return this.Value > 0;
		}
	}
	
	//TODO
	/*
	 * IfcPostalAddress
	 * IfcPresentationLayerAssignment
	 * IfcPresentationLayerWithStyle
	 * IfcProcedure
	 * IfcProcedureType
	 * IfcProduct
	 * IfcProductDefinitionShape
	 * IfcProject
	 * IfcProjectedCRS
	 * IfcPropertyBoundedValue
	 * IfcPropertyDependencyRelationship
	 * IfcPropertyEnumeratedValue
	 * IfcPropertyEnumeration
	 * IfcPropertyListValue
	 * IfcPropertySet
	 * IfcPropertySetTemplate
	 * IfcPropertyTableValue
	 * IfcProtectiveDevice
	 * IfcProtectiveDeviceTrippingUnit
	 * IfcProtectiveDeviceTrippingUnitType
	 * IfcProtectiveDeviceType
	 * IfcProxy
	 * IfcPump
	 * IfcPumpType
	 * IfcQuantityArea
	 * IfcQuantityCount
	 * IfcQuantityLength
	 * IfcQuantityTime
	 * IfcQuantityVolume 
	 * IfcQuantityWeight
	 * IfcRailing
	 * IfcRailingType
	 * IfcRamp
	 * IfcRampFlight
	 * IfcRampFlightType
	 * IfcRampType
	 * IfcRationalBSplineCurveWithKnots
	 * IfcRationalBSplineSurfaceWithKnots
	 * IfcRectangleHollowProfileDef
	 * IfcRectangleProfileDef
	 * IfcRectangularTrimmedSurface
	 * IfcReinforcingBar
	 * IfcReinforcingElement
	 * IfcReinforcingElementType
	 * IfcReinforcingMesh
	 * IfcRelAggregates
	 * IfcRelAssigns
	 * IfcRelAssignsToActor
	 * IfcRelAssignsToControl
	 * IfcRelAssignsToGroup
	 * IfcRelAssignsToProcess
	 * IfcRelAssignsToProduct
	 * IfcRelAssignsToResource
	 * IfcRelAssociatesMaterial
	 * IfcRelConnectsElements
	 * IfcRelConnectsPorts
	 * IfcRelConnectsPortToElement
	 * IfcRelContainedInSpatialStructure
	 * IfcRelDeclares
	 * IfcRelInterferesElements
	 * IfcRelNests
	 * IfcRelReferencedInSpatialStructure
	 * IfcRelSequence
	 * IfcRelSpaceBoundary
	 * IfcReparametrisedCompositeCurveSegment
	 * IfcRepresentationContextSameWCS
	 * IfcRepresentationMap
	 * IfcRevolvedAreaSolid
	 * IfcRevolvedAreaSolidTapered
	 * IfcRoof
	 * IfcRoofType
	 * IfcRoundedRectangleProfileDef
	 * IfcSanitaryTerminal
	 * IfcSanitaryTerminalType
	 * IfcSectionedSpine
	 * IfcSensor
	 * IfcSensorType
	 * IfcShadingDevice
	 * IfcShapeModel
	 * IfcSingleProjectInstance
	 * IfcSlab
	 * IfcSlabElementedCase
	 * IfcSlabStandardCase
	 * IfcSlabType
	 * IfcSolarDevice
	 * IfcSolarDeviceType
	 * IfcSpace
	 * IfcSpaceHeater
	 * IfcSpaceHeaterType
	 * IfcSpaceType
	 * IfcSpatialStructureElement
	 * IfcSpatialZone
	 * IfcSpatialZoneType
	 * IfcSpecularRoughness
	 * IfcStackTerminal
	 * IfcStackTerminalType
	 * IfcStair
	 * IfcStairFlight
	 * IfcStairFlightType
	 * IfcStairType
	 * IfcStructuralAnalysisModel
	 * IfcStructuralCurveAction
	 * IfcStructuralCurveMember
	 * IfcStructuralCurveReaction
	 * IfcStructuralLoadConfiguration
	 * IfcStructuralLoadGroup
	 * IfcStructuralResultGroup
	 * IfcStructuralSurfaceAction
	 * IfcStructuralSurfaceMember
	 * IfcStructuralSurfaceReaction
	 * IfcStyledItem
	 * IfcStyledRepresentation
	 * IfcSubContractResource
	 * IfcSubContractResourceType
	 * 
	 */
}
