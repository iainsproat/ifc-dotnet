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

namespace IfcDotNet.Schema
{
	public partial class IfcRootOwnerHistory : AnonymousType<IfcOwnerHistory, IfcRootOwnerHistory>{
	}
	public partial class IfcOwnerHistoryOwningUser : AnonymousType<IfcPersonAndOrganization, IfcOwnerHistoryOwningUser>{
	}
	public partial class IfcPersonAndOrganizationThePerson : AnonymousType<IfcPerson, IfcPersonAndOrganizationThePerson>{
	}
	public partial class IfcPersonMiddleNames : ValueTypeArrayWrapper<string, IfcLabel1, IfcPersonMiddleNames>{
	}
	public partial class IfcPersonPrefixTitles : ValueTypeArrayWrapper<string, IfcLabel1, IfcPersonPrefixTitles> {
	}
	public partial class IfcPersonSuffixTitles : ValueTypeArrayWrapper<string, IfcLabel1, IfcPersonSuffixTitles> {
	}
	public partial class IfcPersonRoles : ArrayWrapper<IfcActorRole, IfcPersonRoles>{
	}
	public partial class IfcPersonAddresses : ArrayWrapper<IfcAddress, IfcPersonAddresses> {
	}
	public partial class IfcPostalAddressAddressLines : ValueTypeArrayWrapper<string, IfcLabel1, IfcPostalAddressAddressLines> {
	}
	public partial class IfcTelecomAddressTelephoneNumbers : ValueTypeArrayWrapper<string, IfcLabel1, IfcTelecomAddressTelephoneNumbers> {
	}
	public partial class IfcTelecomAddressFacsimileNumbers : ValueTypeArrayWrapper<string, IfcLabel1, IfcTelecomAddressFacsimileNumbers> {
	}
	public partial class IfcTelecomAddressElectronicMailAddresses : ValueTypeArrayWrapper<string, IfcLabel1, IfcTelecomAddressElectronicMailAddresses> {
	}
	public partial class IfcPersonAndOrganizationTheOrganization : AnonymousType<IfcOrganization, IfcPersonAndOrganizationTheOrganization> {
	}
	public partial class IfcOrganizationRoles : ArrayWrapper<IfcActorRole, IfcOrganizationRoles> {
	}
	public partial class IfcOrganizationAddresses : ArrayWrapper<IfcAddress, IfcOrganizationAddresses> {
	}
	public partial class IfcPersonAndOrganizationRoles : ArrayWrapper<IfcActorRole, IfcPersonAndOrganizationRoles> {
	}
	public partial class IfcOwnerHistoryOwningApplication : AnonymousType<IfcApplication, IfcOwnerHistoryOwningApplication> {
	}
	public partial class IfcApplicationApplicationDeveloper : AnonymousType<IfcOrganization, IfcApplicationApplicationDeveloper> {
	}
	public partial class IfcOwnerHistoryLastModifyingUser : AnonymousType<IfcPersonAndOrganization, IfcOwnerHistoryLastModifyingUser> {
	}
	public partial class IfcOwnerHistoryLastModifyingApplication : AnonymousType<IfcApplication, IfcOwnerHistoryLastModifyingApplication> {
	}
	public partial class IfcRelDefinesRelatedObjects : ArrayWrapper<IfcObject, IfcRelDefinesRelatedObjects> {
	}
	public partial class IfcActorTheActor : AnonymousType<Entity, IfcActorTheActor> {
	}
	public partial class IfcConstructionResourceBaseQuantity : AnonymousType<IfcMeasureWithUnit, IfcConstructionResourceBaseQuantity> {
	}
	public partial class IfcMeasureWithUnitValueComponent : AnonymousType<object, IfcMeasureWithUnitValueComponent> {
	}
	public partial class IfcMeasureWithUnitUnitComponent : AnonymousType<Entity, IfcMeasureWithUnitUnitComponent> {
	}
	public partial class IfcNamedUnitDimensions : AnonymousType<IfcDimensionalExponents, IfcNamedUnitDimensions> {
	}
	public partial class IfcConversionBasedUnitConversionFactor : AnonymousType<IfcMeasureWithUnit, IfcConversionBasedUnitConversionFactor>{
	}
	public partial class IfcDerivedUnitElements : ArrayWrapper<IfcDerivedUnitElement, IfcDerivedUnitElements> {
	}
	public partial class IfcDerivedUnitElementUnit : AnonymousType<IfcNamedUnit, IfcDerivedUnitElementUnit> {
	}
	public partial class IfcProductObjectPlacement : AnonymousType<IfcObjectPlacement, IfcProductObjectPlacement> {
	}
	public partial class IfcGridPlacementPlacementLocation : AnonymousType<IfcVirtualGridIntersection, IfcGridPlacementPlacementLocation> {
	}
	public partial class IfcVirtualGridIntersectionIntersectingAxes : ArrayWrapper<IfcGridAxis, IfcVirtualGridIntersectionIntersectingAxes> {
	}
	public partial class IfcGridAxisAxisCurve : AnonymousType<IfcCurve, IfcGridAxisAxisCurve> {
	}
	public partial class IfcLightSourceLightColour : AnonymousType<IfcColourRgb, IfcLightSourceLightColour> {
	}
	public partial class IfcCsgPrimitive3DPosition : AnonymousType<IfcAxis2Placement3D, IfcCsgPrimitive3DPosition> {
	}
	public partial class IfcAxis2Placement3DAxis : AnonymousType<IfcDirection, IfcAxis2Placement3DAxis> {
	}
	public partial class IfcDirectionDirectionRatios : ValueTypeArrayWrapper<double, doublewrapper, IfcDirectionDirectionRatios> {
	}
	public partial class IfcAxis2Placement3DRefDirection : AnonymousType<IfcDirection, IfcAxis2Placement3DRefDirection> {
	}
	public partial class IfcPlacementLocation : AnonymousType<IfcCartesianPoint, IfcPlacementLocation> {
	}
	public partial class IfcCartesianPointCoordinates : ValueTypeArrayWrapper<double, IfcLengthMeasure1, IfcCartesianPointCoordinates> {
	}
	public partial class IfcSweptAreaSolidSweptArea : AnonymousType<IfcProfileDef, IfcSweptAreaSolidSweptArea> {
	}
	public partial class IfcArbitraryClosedProfileDefOuterCurve : AnonymousType<IfcCurve, IfcArbitraryClosedProfileDefOuterCurve> {
	}
	public partial class IfcLinePnt : AnonymousType<IfcCartesianPoint, IfcLinePnt> {
	}
	public partial class IfcLineDir : AnonymousType<IfcVector, IfcLineDir> {
	}
	public partial class IfcVectorOrientation : AnonymousType<IfcDirection, IfcVectorOrientation> {
	}
	public partial class IfcOffsetCurve2DBasisCurve : AnonymousType<IfcCurve, IfcOffsetCurve2DBasisCurve> {
	}
	public partial class IfcOffsetCurve3DBasisCurve : AnonymousType<IfcCurve, IfcOffsetCurve3DBasisCurve> {
	}
	public partial class IfcOffsetCurve3DRefDirection : AnonymousType<IfcDirection, IfcOffsetCurve3DRefDirection> {
	}
	public partial class IfcParameterizedProfileDefPosition : AnonymousType<IfcAxis2Placement2D, IfcParameterizedProfileDefPosition> {
	}
	public partial class IfcAxis2Placement2DRefDirection : AnonymousType<IfcDirection, IfcAxis2Placement2DRefDirection> {
	}
	public partial class IfcArbitraryOpenProfileDefCurve : AnonymousType<IfcBoundedCurve, IfcArbitraryOpenProfileDefCurve> {
	}
	public partial class IfcBSplineCurveControlPointsList : ArrayWrapper<IfcCartesianPoint, IfcBSplineCurveControlPointsList> {
	}
	public partial class IfcCompositeCurveSegments : ArrayWrapper<IfcCompositeCurveSegment, IfcCompositeCurveSegments> {
	}
	public partial class IfcCompositeCurveSegmentParentCurve : AnonymousType<IfcCurve, IfcCompositeCurveSegmentParentCurve> {
	}
	public partial class IfcPolylinePoints : ArrayWrapper<IfcCartesianPoint, IfcPolylinePoints> {
	}
	public partial class IfcTrimmedCurveBasisCurve : AnonymousType<IfcCurve, IfcTrimmedCurveBasisCurve> {
	}
	public partial class IfcTrimmedCurveTrim1 : ArrayWrapper<object, IfcTrimmedCurveTrim1> {
	}
	public partial class IfcTrimmedCurveTrim2 : ArrayWrapper<object, IfcTrimmedCurveTrim2> {
	}
	public partial class IfcCompositeProfileDefProfiles : ArrayWrapper<IfcProfileDef, IfcCompositeProfileDefProfiles> {
	}
	public partial class IfcDerivedProfileDefParentProfile : AnonymousType<IfcProfileDef, IfcDerivedProfileDefParentProfile> {
	}
	public partial class IfcDerivedProfileDefOperator : AnonymousType<IfcCartesianTransformationOperator2D, IfcDerivedProfileDefOperator> {
	}
	public partial class IfcCartesianTransformationOperatorAxis1 : AnonymousType<IfcDirection, IfcCartesianTransformationOperatorAxis1> {
	}
	public partial class IfcCartesianTransformationOperatorAxis2 : AnonymousType<IfcDirection, IfcCartesianTransformationOperatorAxis2> {
	}
	public partial class IfcCartesianTransformationOperatorLocalOrigin : AnonymousType<IfcCartesianPoint, IfcCartesianTransformationOperatorLocalOrigin> {
	}
	public partial class IfcSweptAreaSolidPosition : AnonymousType<IfcAxis2Placement3D, IfcSweptAreaSolidPosition> {
	}
	public partial class IfcManifoldSolidBrepOuter : AnonymousType<IfcClosedShell, IfcManifoldSolidBrepOuter> {
	}
	public partial class IfcConnectedFaceSetCfsFaces : ArrayWrapper<IfcFace, IfcConnectedFaceSetCfsFaces> {
	}
	public partial class IfcFaceBounds : ArrayWrapper<IfcFaceBound, IfcFaceBounds> {
	}
	public partial class IfcFaceBoundBound : AnonymousType<IfcLoop, IfcFaceBoundBound> {
	}
	public partial class IfcEdgeLoopEdgeList : ArrayWrapper<IfcOrientedEdge, IfcEdgeLoopEdgeList> {
	}
	public partial class IfcOrientedEdgeEdgeElement : AnonymousType<IfcEdge, IfcOrientedEdgeEdgeElement>	{
	}
	public partial class IfcEdgeEdgeStart : AnonymousType<IfcVertex, IfcEdgeEdgeStart> {
	}
	public partial class IfcVertexPointVertexGeometry : AnonymousType<IfcPoint, IfcVertexPointVertexGeometry> {
	}
	public partial class IfcPointOnCurveBasisCurve : AnonymousType<IfcCurve, IfcPointOnCurveBasisCurve> {
	}
	public partial class IfcPointOnSurfaceBasisSurface : AnonymousType<IfcSurface, IfcPointOnSurfaceBasisSurface> {
	}
	public partial class IfcSweptSurfaceSweptCurve : AnonymousType<IfcProfileDef, IfcSweptSurfaceSweptCurve> {
	}
	public partial class IfcSweptSurfacePosition :AnonymousType<IfcAxis2Placement3D, IfcSweptSurfacePosition> {
	}
	public partial class IfcElementarySurfacePosition : AnonymousType<IfcAxis2Placement3D, IfcElementarySurfacePosition> {
	}
	public partial class IfcEdgeEdgeEnd : AnonymousType<IfcVertex, IfcEdgeEdgeEnd> {
	}
	public partial class IfcEdgeCurveEdgeGeometry : AnonymousType<IfcCurve, IfcEdgeCurveEdgeGeometry> {
	}
	public partial class IfcSubedgeParentEdge : AnonymousType<IfcEdge, IfcSubedgeParentEdge> {
	}
	public partial class IfcPolyLoopPolygon : ArrayWrapper<IfcCartesianPoint, IfcPolyLoopPolygon> {
	}
	public partial class IfcVertexLoopLoopVertex : AnonymousType<IfcVertex, IfcVertexLoopLoopVertex> {
	}
	public partial class IfcFaceSurfaceFaceSurface : AnonymousType<IfcSurface, IfcFaceSurfaceFaceSurface> {
	}
	public partial class IfcConicPosition : AnonymousType<IfcPlacement, IfcConicPosition> {
	}
	public partial class IfcVirtualGridIntersectionOffsetDistances : ValueTypeArrayWrapper<double, IfcLengthMeasure1, IfcVirtualGridIntersectionOffsetDistances> {
	}
	public partial class IfcGridPlacementPlacementRefDirection : AnonymousType<IfcVirtualGridIntersection, IfcGridPlacementPlacementRefDirection> {
	}
	public partial class IfcLocalPlacementPlacementRelTo : AnonymousType<IfcObjectPlacement, IfcLocalPlacementPlacementRelTo> {
	}
	/*public partial class IfcLocalPlacementRelativePlacement
IfcProductRepresentation1
IfcMaterialDefinitionRepresentationRepresentedMaterial
IfcProductRepresentationRepresentations
IfcRepresentationContextOfItems
IfcGeometricRepresentationContextWorldCoordinateSystem
IfcGeometricRepresentationContextTrueNorth
IfcRepresentationItems
IfcMappedItemMappingSource
IfcRepresentationMapMappingOrigin
IfcRepresentationMapMappedRepresentation
IfcMappedItemMappingTarget
IfcCartesianTransformationOperator3DAxis3
IfcStyledItemItem
IfcStyledItemStyles
IfcPresentationStyleAssignmentStyles
IfcCurveStyleCurveFont
IfcCurveStyleFontPatternList
IfcCurveStyleFontAndScalingCurveFont
IfcCurveStyleCurveWidth
IfcCurveStyleCurveColour
IfcFillAreaStyleFillStyles
IfcFillAreaStyleHatchingHatchLineAppearance
IfcFillAreaStyleHatchingStartOfNextHatchLine
IfcOneDirectionRepeatFactorRepeatFactor
IfcTwoDirectionRepeatFactorSecondRepeatFactor
IfcFillAreaStyleHatchingPointOfReferenceHatchLine
IfcFillAreaStyleHatchingPatternStart
IfcFillAreaStyleTilesTilingPattern
IfcFillAreaStyleTilesTiles
IfcFillAreaStyleTileSymbolWithStyleSymbol
IfcTerminatorSymbolAnnotatedCurve
IfcNullStyle1
IfcSurfaceStyleStyles
IfcSurfaceStyleLightingDiffuseTransmissionColour
IfcSurfaceStyleLightingDiffuseReflectionColour
IfcSurfaceStyleLightingTransmissionColour
IfcSurfaceStyleLightingReflectanceColour
IfcSurfaceStyleRenderingDiffuseColour
IfcSurfaceStyleRenderingTransmissionColour
IfcSurfaceStyleRenderingDiffuseTransmissionColour
IfcSurfaceStyleRenderingReflectionColour
IfcSurfaceStyleRenderingSpecularColour
IfcSurfaceStyleRenderingSpecularHighlight
IfcSpecularExponent1
IfcSpecularRoughness1
IfcSurfaceStyleShadingSurfaceColour
IfcSurfaceStyleWithTexturesTextures
IfcSurfaceTextureTextureTransform
IfcPixelTexturePixel
hexBinarywrapper
IfcSymbolStyleStyleOfSymbol
IfcTextStyleTextCharacterAppearance
IfcTextStyleForDefinedFontColour
IfcTextStyleForDefinedFontBackgroundColour
IfcTextStyleTextStyle
IfcTextStyleTextModelTextIndent
IfcTextStyleTextModelLetterSpacing
IfcTextStyleTextModelWordSpacing
IfcTextStyleTextModelLineHeight
IfcTextStyleWithBoxCharacteristicsCharacterSpacing
IfcTextStyleTextFontStyle
IfcTextStyleFontModelFontFamily
IfcTextFontName1
IfcTextStyleFontModelFontSize
IfcStructuralConnectionAppliedCondition
IfcStructuralActivityAppliedLoad
IfcStructuralActionCausedBy
IfcWorkControlCreationDate
IfcDateAndTimeDateComponent
IfcDateAndTimeTimeComponent
IfcLocalTimeZone
IfcWorkControlCreators
IfcWorkControlStartTime
IfcWorkControlFinishTime
IfcProjectRepresentationContexts
IfcProjectUnitsInContext
IfcUnitAssignmentUnits
IfcRelDecomposesRelatingObject
IfcTypeObjectHasPropertySets
IfcDoorLiningPropertiesShapeAspectStyle
IfcShapeAspectShapeRepresentations
IfcShapeAspectPartOfProductDefinitionShape
IfcDoorPanelPropertiesShapeAspectStyle
IfcElementQuantityQuantities
IfcPhysicalComplexQuantityHasQuantities
IfcPhysicalSimpleQuantityUnit
IfcFluidFlowPropertiesFlowConditionTimeSeries
IfcIrregularTimeSeriesValues
IfcIrregularTimeSeriesValueTimeStamp
IfcIrregularTimeSeriesValueListValues
IfcTimeSeriesStartTime
IfcTimeSeriesEndTime
IfcTimeSeriesUnit
IfcRegularTimeSeriesValues
IfcTimeSeriesValueListValues
IfcFluidFlowPropertiesVelocityTimeSeries
IfcFluidFlowPropertiesFlowrateTimeSeries
IfcFluidFlowPropertiesFluid
IfcFluidFlowPropertiesPressureTimeSeries
IfcFluidFlowPropertiesWetBulbTemperatureTimeSeries
IfcFluidFlowPropertiesTemperatureTimeSeries
IfcFluidFlowPropertiesFlowrateSingleValue
IfcPermeableCoveringPropertiesShapeAspectStyle
IfcPropertySetHasProperties
IfcComplexPropertyHasProperties
IfcReinforcementDefinitionPropertiesReinforcementSectionDefinitions
IfcSectionReinforcementPropertiesSectionDefinition
IfcSectionPropertiesStartProfile
IfcSectionPropertiesEndProfile
IfcSectionReinforcementPropertiesCrossSectionReinforcementDefinitions
IfcServiceLifeFactorUpperValue
IfcServiceLifeFactorMostUsedValue
IfcServiceLifeFactorLowerValue
IfcSoundPropertiesSoundValues
IfcSoundValueSoundLevelTimeSeries
IfcSoundValueSoundLevelSingleValue
IfcSpaceThermalLoadPropertiesThermalLoadTimeSeriesValues
IfcWindowLiningPropertiesShapeAspectStyle
IfcWindowPanelPropertiesShapeAspectStyle
IfcRelDecomposesRelatedObjects
IfcRelAssignsRelatedObjects
IfcProfilePropertiesProfileDefinition
IfcMaterialPropertiesMaterial
IfcConstraintCreatingActor
IfcConstraintCreationTime
IfcAppliedValueAppliedValue
IfcAppliedValueUnitBasis
IfcAppliedValueApplicableDate
IfcAppliedValueFixedUntilDate
IfcTypeProductRepresentationMaps
IfcDraughtingCalloutContents
IfcAnnotationFillAreaOuterBoundary
IfcAnnotationFillAreaInnerBoundaries
IfcAnnotationFillAreaOccurrenceFillStyleTarget
IfcAnnotationSurfaceItem
IfcBooleanResultFirstOperand
IfcBoxedHalfSpaceEnclosure
IfcBoundingBoxCorner
IfcHalfSpaceSolidBaseSurface
IfcCsgSolidTreeRootExpression
IfcPolygonalBoundedHalfSpacePosition
IfcPolygonalBoundedHalfSpacePolygonalBoundary
IfcSweptDiskSolidDirectrix
IfcBooleanResultSecondOperand
IfcDefinedSymbolDefinition
IfcDefinedSymbolTarget
IfcFaceBasedSurfaceModelFbsmFaces
IfcGeometricSetElements
IfcSectionedSpineSpineCurve
IfcSectionedSpineCrossSections
IfcSectionedSpineCrossSectionPositions
IfcShellBasedSurfaceModelSbsmBoundary
IfcTextLiteralPlacement
IfcAnnotationSurfaceTextureCoordinates
IfcTextureCoordinateGeneratorParameter
IfcTextureMapTextureMaps
IfcVertexBasedTextureMapTextureVertices
IfcTextureVertexCoordinates
IfcVertexBasedTextureMapTexturePoints
IfcAppliedValueRelationshipComponentOfTotal
IfcAppliedValueRelationshipComponents
IfcApprovalApprovalDateTime
IfcApprovalActorRelationshipActor
IfcApprovalActorRelationshipApproval
IfcApprovalActorRelationshipRole
IfcApprovalPropertyRelationshipApprovedProperties
IfcApprovalPropertyRelationshipApproval
IfcApprovalRelationshipRelatedApproval
IfcApprovalRelationshipRelatingApproval
IfcArbitraryProfileDefWithVoidsInnerCurves
IfcAssetOriginalValue
IfcAssetCurrentValue
IfcAssetTotalReplacementCost
IfcAssetOwner
IfcAssetUser
IfcAssetResponsiblePerson
IfcAssetIncorporationDate
IfcAssetDepreciatedValue
IfcAxis1PlacementAxis
IfcBuildingBuildingAddress
IfcClassificationEditionDate
IfcClassificationItemNotation
IfcClassificationItemItemOf
IfcClassificationItemRelationshipRelatingItem
IfcClassificationItemRelationshipRelatedItems
IfcClassificationNotationNotationFacets
IfcClassificationReferenceReferencedSource
IfcConditionCriterionCriterion
IfcConditionCriterionCriterionDateTime
IfcConnectionCurveGeometryCurveOnRelatingElement
IfcConnectionCurveGeometryCurveOnRelatedElement
IfcConnectionPointGeometryPointOnRelatingElement
IfcConnectionPointGeometryPointOnRelatedElement
IfcConnectionPortGeometryLocationAtRelatingElement
IfcConnectionPortGeometryLocationAtRelatedElement
IfcConnectionPortGeometryProfileOfPort
IfcConnectionSurfaceGeometrySurfaceOnRelatingElement
IfcConnectionSurfaceGeometrySurfaceOnRelatedElement
IfcConstraintAggregationRelationshipRelatingConstraint
IfcMetricDataValue
IfcTableRows
IfcTableRowRowCells
IfcObjectiveBenchmarkValues
IfcObjectiveResultValues
IfcConstraintAggregationRelationshipRelatedConstraints
IfcConstraintClassificationRelationshipClassifiedConstraint
IfcConstraintClassificationRelationshipRelatedClassifications
IfcConstraintRelationshipRelatingConstraint
IfcConstraintRelationshipRelatedConstraints
IfcConstructionMaterialResourceSuppliers
IfcCostScheduleSubmittedBy
IfcCostSchedulePreparedBy
IfcCostScheduleSubmittedOn
IfcCostScheduleTargetUsers
IfcCostScheduleUpdateDate
IfcCurrencyRelationshipRelatingMonetaryUnit
IfcCurrencyRelationshipRelatedMonetaryUnit
IfcCurrencyRelationshipRateDateTime
IfcCurrencyRelationshipRateSource
IfcLibraryInformationPublisher
IfcLibraryInformationVersionDate
IfcLibraryInformationLibraryReference
IfcCurveBoundedPlaneBasisSurface
IfcCurveBoundedPlaneOuterBoundary
IfcCurveBoundedPlaneInnerBoundaries
IfcDraughtingCalloutRelationshipRelatingDraughtingCallout
IfcDraughtingCalloutRelationshipRelatedDraughtingCallout
IfcDocumentInformationDocumentReferences
IfcDocumentInformationDocumentOwner
IfcDocumentInformationEditors
IfcDocumentInformationCreationTime
IfcDocumentInformationLastRevisionTime
IfcDocumentInformationElectronicFormat
IfcDocumentInformationValidFrom
IfcDocumentInformationValidUntil
IfcDocumentInformationRelationshipRelatingDocument
IfcDocumentInformationRelationshipRelatedDocuments
IfcExtendedMaterialPropertiesExtendedProperties
IfcExtrudedAreaSolidExtrudedDirection
IfcFacetedBrepWithVoidsVoids
IfcGeometricRepresentationSubContextParentContext
IfcGridUAxes
IfcGridVAxes
IfcGridWAxes
IfcInventoryJurisdiction
IfcInventoryResponsiblePersons
IfcInventoryLastUpdateDate
IfcInventoryCurrentValue
IfcInventoryOriginalValue
IfcLightDistributionDataSecondaryPlaneAngle
IfcLightDistributionDataLuminousIntensity
IfcLightIntensityDistributionDistributionData
IfcLightSourceDirectionalOrientation
IfcLightSourceGoniometricPosition
IfcLightSourceGoniometricColourAppearance
IfcLightSourceGoniometricLightDistributionDataSource
IfcLightSourcePositionalPosition
IfcLightSourceSpotOrientation
IfcMaterialClassificationRelationshipMaterialClassifications
IfcMaterialClassificationRelationshipClassifiedMaterial
IfcMaterialLayerMaterial
IfcMaterialLayerSetMaterialLayers
IfcMaterialLayerSetUsageForLayerSet
IfcMaterialListMaterials
IfcMechanicalSteelMaterialPropertiesRelaxations
IfcMoveMoveFrom
IfcSiteSiteAddress
IfcMoveMoveTo
IfcMovePunchList
IfcOrganizationRelationshipRelatingOrganization
IfcOrganizationRelationshipRelatedOrganizations
IfcPathEdgeList
IfcPlanarBoxPlacement
IfcPresentationLayerAssignmentAssignedItems
IfcPresentationLayerWithStyleLayerStyles
IfcProjectOrderRecordRecords
IfcRelAssignsToControlRelatingControl
IfcScheduleTimeControlActualStart
IfcScheduleTimeControlEarlyStart
IfcScheduleTimeControlLateStart
IfcScheduleTimeControlScheduleStart
IfcScheduleTimeControlActualFinish
IfcScheduleTimeControlEarlyFinish
IfcScheduleTimeControlLateFinish
IfcScheduleTimeControlScheduleFinish
IfcScheduleTimeControlStatusTime
IfcSpaceProgramRequestedLocation
IfcTimeSeriesScheduleApplicableDates
IfcTimeSeriesScheduleTimeSeries
IfcPropertyBoundedValueUpperBoundValue
IfcPropertyBoundedValueLowerBoundValue
IfcPropertyBoundedValueUnit
IfcPropertyConstraintRelationshipRelatingConstraint
IfcPropertyConstraintRelationshipRelatedProperties
IfcPropertyDependencyRelationshipDependingProperty
IfcPropertyDependencyRelationshipDependantProperty
IfcPropertyEnumeratedValueEnumerationValues
IfcPropertyEnumeratedValueEnumerationReference
IfcPropertyEnumerationEnumerationValues
IfcPropertyEnumerationUnit
IfcPropertyListValueListValues
IfcPropertyListValueUnit
IfcPropertyReferenceValuePropertyReference
IfcPropertySingleValueNominalValue
IfcPropertySingleValueUnit
IfcPropertyTableValueDefiningValues
IfcPropertyTableValueDefinedValues
IfcPropertyTableValueDefiningUnit
IfcPropertyTableValueDefinedUnit
IfcRationalBezierCurveWeightsData
IfcRectangularTrimmedSurfaceBasisSurface
IfcReferencesValueDocumentReferencedDocument
IfcReferencesValueDocumentReferencingValues
IfcRelAssignsTasksTimeForTask
IfcRelAssignsToActorRelatingActor
IfcRelAssignsToActorActingRole
IfcRelAssignsToGroupRelatingGroup
IfcStructuralResultGroupResultForLoadGroup
IfcRelAssignsToProcessRelatingProcess
IfcRelAssignsToProcessQuantityInProcess
IfcRelAssignsToProductRelatingProduct
IfcRelAssignsToResourceRelatingResource
IfcRelAssociatesRelatedObjects
IfcRelAssociatesAppliedValueRelatingAppliedValue
IfcRelAssociatesApprovalRelatingApproval
IfcRelAssociatesClassificationRelatingClassification
IfcRelAssociatesConstraintRelatingConstraint
IfcRelAssociatesDocumentRelatingDocument
IfcRelAssociatesLibraryRelatingLibrary
IfcRelAssociatesMaterialRelatingMaterial
IfcRelAssociatesProfilePropertiesRelatingProfileProperties
IfcRelAssociatesProfilePropertiesProfileSectionLocation
IfcRelAssociatesProfilePropertiesProfileOrientation
IfcRelConnectsElementsConnectionGeometry
IfcRelConnectsElementsRelatingElement
IfcRelConnectsElementsRelatedElement
IfcRelConnectsPathElementsRelatingPriorities
IfcRelConnectsPathElementsRelatedPriorities
IfcRelConnectsPortToElementRelatingPort
IfcRelConnectsPortToElementRelatedElement
IfcRelConnectsPortsRelatingPort
IfcRelConnectsPortsRelatedPort
IfcRelConnectsPortsRealizingElement
IfcRelConnectsStructuralActivityRelatingElement
IfcRelConnectsStructuralActivityRelatedStructuralActivity
IfcRelConnectsStructuralElementRelatingElement
IfcRelConnectsStructuralElementRelatedStructuralMember
IfcRelConnectsStructuralMemberRelatingStructuralMember
IfcRelConnectsStructuralMemberRelatedStructuralConnection
IfcRelConnectsStructuralMemberAppliedCondition
IfcRelConnectsStructuralMemberAdditionalConditions
IfcRelConnectsStructuralMemberConditionCoordinateSystem
IfcRelConnectsWithEccentricityConnectionConstraint
IfcRelConnectsWithRealizingElementsRealizingElements
IfcRelContainedInSpatialStructureRelatedElements
IfcRelContainedInSpatialStructureRelatingStructure
IfcRelCoversBldgElementsRelatingBuildingElement
IfcRelCoversBldgElementsRelatedCoverings
IfcRelCoversSpacesRelatedSpace
IfcRelCoversSpacesRelatedCoverings
IfcRelDefinesByPropertiesRelatingPropertyDefinition
IfcRelDefinesByTypeRelatingType
IfcRelFillsElementRelatingOpeningElement
IfcRelFillsElementRelatedBuildingElement
IfcRelFlowControlElementsRelatedControlElements
IfcRelFlowControlElementsRelatingFlowElement
IfcRelInteractionRequirementsLocationOfInteraction
IfcRelInteractionRequirementsRelatedSpaceProgram
IfcRelInteractionRequirementsRelatingSpaceProgram
IfcRelOverridesPropertiesOverridingProperties
IfcRelProjectsElementRelatingElement
IfcRelProjectsElementRelatedFeatureElement
IfcRelReferencedInSpatialStructureRelatedElements
IfcRelReferencedInSpatialStructureRelatingStructure
IfcRelSequenceRelatingProcess
IfcRelSequenceRelatedProcess
IfcRelServicesBuildingsRelatingSystem
IfcStructuralAnalysisModelOrientationOf2DPlane
IfcStructuralAnalysisModelLoadedBy
IfcStructuralAnalysisModelHasResults
IfcRelServicesBuildingsRelatedBuildings
IfcRelSpaceBoundaryRelatingSpace
IfcRelSpaceBoundaryRelatedBuildingElement
IfcRelSpaceBoundaryConnectionGeometry
IfcRelVoidsElementRelatingBuildingElement
IfcRelVoidsElementRelatedOpeningElement
IfcRevolvedAreaSolidAxis
IfcStructuralLinearActionVaryingVaryingAppliedLoadLocation
IfcStructuralLinearActionVaryingSubsequentAppliedLoads
IfcStructuralPlanarActionVaryingVaryingAppliedLoadLocation
IfcStructuralPlanarActionVaryingSubsequentAppliedLoads
IfcStructuralSurfaceMemberVaryingSubsequentThickness
IfcStructuralSurfaceMemberVaryingVaryingThicknessLocation
IfcSubContractResourceSubContractor
IfcSurfaceCurveSweptAreaSolidDirectrix
IfcSurfaceCurveSweptAreaSolidReferenceSurface
IfcSurfaceOfLinearExtrusionExtrudedDirection
IfcSurfaceOfRevolutionAxisPosition
IfcTextLiteralWithExtentExtent
IfcTimeSeriesReferenceRelationshipReferencedTimeSeries
IfcTimeSeriesReferenceRelationshipTimeSeriesReferences
iso_10303
iso_10303_28_header
	 */
}