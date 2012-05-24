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
	public partial class IfcRootOwnerHistory : AnonymousType<IfcOwnerHistory, IfcRootOwnerHistory>{}
	public partial class IfcOwnerHistoryOwningUser : AnonymousType<IfcPersonAndOrganization, IfcOwnerHistoryOwningUser>{}
	public partial class IfcPersonAndOrganizationThePerson : AnonymousType<IfcPerson, IfcPersonAndOrganizationThePerson>{}
	public partial class IfcPersonMiddleNames : ValueTypeArrayWrapper<string, IfcLabel1, IfcPersonMiddleNames>{}
	public partial class IfcPersonPrefixTitles : ValueTypeArrayWrapper<string, IfcLabel1, IfcPersonPrefixTitles> {}
	public partial class IfcPersonSuffixTitles : ValueTypeArrayWrapper<string, IfcLabel1, IfcPersonSuffixTitles> {}
	public partial class IfcPersonRoles : ArrayWrapper<IfcActorRole, IfcPersonRoles>{}
	public partial class IfcPersonAddresses : ArrayWrapper<IfcAddress, IfcPersonAddresses> {}
	public partial class IfcPostalAddressAddressLines : ValueTypeArrayWrapper<string, IfcLabel1, IfcPostalAddressAddressLines> {}
	public partial class IfcTelecomAddressTelephoneNumbers : ValueTypeArrayWrapper<string, IfcLabel1, IfcTelecomAddressTelephoneNumbers> {}
	public partial class IfcTelecomAddressFacsimileNumbers : ValueTypeArrayWrapper<string, IfcLabel1, IfcTelecomAddressFacsimileNumbers> {}
	public partial class IfcTelecomAddressElectronicMailAddresses : ValueTypeArrayWrapper<string, IfcLabel1, IfcTelecomAddressElectronicMailAddresses> {}
	public partial class IfcPersonAndOrganizationTheOrganization : AnonymousType<IfcOrganization, IfcPersonAndOrganizationTheOrganization> {}
	public partial class IfcOrganizationRoles : ArrayWrapper<IfcActorRole, IfcOrganizationRoles> {}
	public partial class IfcOrganizationAddresses : ArrayWrapper<IfcAddress, IfcOrganizationAddresses> {}
	public partial class IfcPersonAndOrganizationRoles : ArrayWrapper<IfcActorRole, IfcPersonAndOrganizationRoles> {}
	public partial class IfcOwnerHistoryOwningApplication : AnonymousType<IfcApplication, IfcOwnerHistoryOwningApplication> {}
	public partial class IfcApplicationApplicationDeveloper : AnonymousType<IfcOrganization, IfcApplicationApplicationDeveloper> {}
	public partial class IfcOwnerHistoryLastModifyingUser : AnonymousType<IfcPersonAndOrganization, IfcOwnerHistoryLastModifyingUser> {}
	public partial class IfcOwnerHistoryLastModifyingApplication : AnonymousType<IfcApplication, IfcOwnerHistoryLastModifyingApplication> {}
	public partial class IfcRelDefinesRelatedObjects : ArrayWrapper<IfcObject, IfcRelDefinesRelatedObjects> {}
	public partial class IfcActorTheActor : AnonymousType<Entity, IfcActorTheActor> {}
	public partial class IfcConstructionResourceBaseQuantity : AnonymousType<IfcMeasureWithUnit, IfcConstructionResourceBaseQuantity> {}
	public partial class IfcMeasureWithUnitValueComponent : AnonymousType<object, IfcMeasureWithUnitValueComponent> {}
	public partial class IfcMeasureWithUnitUnitComponent : AnonymousType<Entity, IfcMeasureWithUnitUnitComponent> {}
	public partial class IfcNamedUnitDimensions : AnonymousType<IfcDimensionalExponents, IfcNamedUnitDimensions> {}
	public partial class IfcConversionBasedUnitConversionFactor : AnonymousType<IfcMeasureWithUnit, IfcConversionBasedUnitConversionFactor>{}
	public partial class IfcDerivedUnitElements : ArrayWrapper<IfcDerivedUnitElement, IfcDerivedUnitElements> {}
	public partial class IfcDerivedUnitElementUnit : AnonymousType<IfcNamedUnit, IfcDerivedUnitElementUnit> {}
	public partial class IfcProductObjectPlacement : AnonymousType<IfcObjectPlacement, IfcProductObjectPlacement> {}
	public partial class IfcGridPlacementPlacementLocation : AnonymousType<IfcVirtualGridIntersection, IfcGridPlacementPlacementLocation> {}
	public partial class IfcVirtualGridIntersectionIntersectingAxes : ArrayWrapper<IfcGridAxis, IfcVirtualGridIntersectionIntersectingAxes> {}
	public partial class IfcGridAxisAxisCurve : AnonymousType<IfcCurve, IfcGridAxisAxisCurve> {}
	public partial class IfcLightSourceLightColour : AnonymousType<IfcColourRgb, IfcLightSourceLightColour> {}
	public partial class IfcCsgPrimitive3DPosition : AnonymousType<IfcAxis2Placement3D, IfcCsgPrimitive3DPosition> {}
	public partial class IfcAxis2Placement3DAxis : AnonymousType<IfcDirection, IfcAxis2Placement3DAxis> {}
	public partial class IfcDirectionDirectionRatios : ValueTypeArrayWrapper<double, doublewrapper, IfcDirectionDirectionRatios> {}
	public partial class IfcAxis2Placement3DRefDirection : AnonymousType<IfcDirection, IfcAxis2Placement3DRefDirection> {}
	public partial class IfcPlacementLocation : AnonymousType<IfcCartesianPoint, IfcPlacementLocation> {}
	public partial class IfcCartesianPointCoordinates : ValueTypeArrayWrapper<double, IfcLengthMeasure1, IfcCartesianPointCoordinates> {}
	public partial class IfcSweptAreaSolidSweptArea : AnonymousType<IfcProfileDef, IfcSweptAreaSolidSweptArea> {}
	public partial class IfcArbitraryClosedProfileDefOuterCurve : AnonymousType<IfcCurve, IfcArbitraryClosedProfileDefOuterCurve> {}
	public partial class IfcLinePnt : AnonymousType<IfcCartesianPoint, IfcLinePnt> {}
	public partial class IfcLineDir : AnonymousType<IfcVector, IfcLineDir> {}
	public partial class IfcVectorOrientation : AnonymousType<IfcDirection, IfcVectorOrientation> {}
	public partial class IfcOffsetCurve2DBasisCurve : AnonymousType<IfcCurve, IfcOffsetCurve2DBasisCurve> {}
	public partial class IfcOffsetCurve3DBasisCurve : AnonymousType<IfcCurve, IfcOffsetCurve3DBasisCurve> {}
	public partial class IfcOffsetCurve3DRefDirection : AnonymousType<IfcDirection, IfcOffsetCurve3DRefDirection> {}
	public partial class IfcParameterizedProfileDefPosition : AnonymousType<IfcAxis2Placement2D, IfcParameterizedProfileDefPosition> {}
	public partial class IfcAxis2Placement2DRefDirection : AnonymousType<IfcDirection, IfcAxis2Placement2DRefDirection> {}
	public partial class IfcArbitraryOpenProfileDefCurve : AnonymousType<IfcBoundedCurve, IfcArbitraryOpenProfileDefCurve> {}
	public partial class IfcBSplineCurveControlPointsList : ArrayWrapper<IfcCartesianPoint, IfcBSplineCurveControlPointsList> {}
	public partial class IfcCompositeCurveSegments : ArrayWrapper<IfcCompositeCurveSegment, IfcCompositeCurveSegments> {}
	public partial class IfcCompositeCurveSegmentParentCurve : AnonymousType<IfcCurve, IfcCompositeCurveSegmentParentCurve> {}
	public partial class IfcPolylinePoints : ArrayWrapper<IfcCartesianPoint, IfcPolylinePoints> {}
	public partial class IfcTrimmedCurveBasisCurve : AnonymousType<IfcCurve, IfcTrimmedCurveBasisCurve> {}
	public partial class IfcTrimmedCurveTrim1 : ArrayWrapper<object, IfcTrimmedCurveTrim1> {}
	public partial class IfcTrimmedCurveTrim2 : ArrayWrapper<object, IfcTrimmedCurveTrim2> {}
	public partial class IfcCompositeProfileDefProfiles : ArrayWrapper<IfcProfileDef, IfcCompositeProfileDefProfiles> {}
	public partial class IfcDerivedProfileDefParentProfile : AnonymousType<IfcProfileDef, IfcDerivedProfileDefParentProfile> {}
	public partial class IfcDerivedProfileDefOperator : AnonymousType<IfcCartesianTransformationOperator2D, IfcDerivedProfileDefOperator> {}
	public partial class IfcCartesianTransformationOperatorAxis1 : AnonymousType<IfcDirection, IfcCartesianTransformationOperatorAxis1> {}
	public partial class IfcCartesianTransformationOperatorAxis2 : AnonymousType<IfcDirection, IfcCartesianTransformationOperatorAxis2> {}
	public partial class IfcCartesianTransformationOperatorLocalOrigin : AnonymousType<IfcCartesianPoint, IfcCartesianTransformationOperatorLocalOrigin> {}
	public partial class IfcSweptAreaSolidPosition : AnonymousType<IfcAxis2Placement3D, IfcSweptAreaSolidPosition> {}
	public partial class IfcManifoldSolidBrepOuter : AnonymousType<IfcClosedShell, IfcManifoldSolidBrepOuter> {}
	public partial class IfcConnectedFaceSetCfsFaces : ArrayWrapper<IfcFace, IfcConnectedFaceSetCfsFaces> {}
	public partial class IfcFaceBounds : ArrayWrapper<IfcFaceBound, IfcFaceBounds> {}
	public partial class IfcFaceBoundBound : AnonymousType<IfcLoop, IfcFaceBoundBound> {}
	public partial class IfcEdgeLoopEdgeList : ArrayWrapper<IfcOrientedEdge, IfcEdgeLoopEdgeList> {}
	public partial class IfcOrientedEdgeEdgeElement : AnonymousType<IfcEdge, IfcOrientedEdgeEdgeElement>	{}
	public partial class IfcEdgeEdgeStart : AnonymousType<IfcVertex, IfcEdgeEdgeStart> {}
	public partial class IfcVertexPointVertexGeometry : AnonymousType<IfcPoint, IfcVertexPointVertexGeometry> {}
	public partial class IfcPointOnCurveBasisCurve : AnonymousType<IfcCurve, IfcPointOnCurveBasisCurve> {}
	public partial class IfcPointOnSurfaceBasisSurface : AnonymousType<IfcSurface, IfcPointOnSurfaceBasisSurface> {}
	public partial class IfcSweptSurfaceSweptCurve : AnonymousType<IfcProfileDef, IfcSweptSurfaceSweptCurve> {}
	public partial class IfcSweptSurfacePosition :AnonymousType<IfcAxis2Placement3D, IfcSweptSurfacePosition> {}
	public partial class IfcElementarySurfacePosition : AnonymousType<IfcAxis2Placement3D, IfcElementarySurfacePosition> {}
	public partial class IfcEdgeEdgeEnd : AnonymousType<IfcVertex, IfcEdgeEdgeEnd> {}
	public partial class IfcEdgeCurveEdgeGeometry : AnonymousType<IfcCurve, IfcEdgeCurveEdgeGeometry> {}
	public partial class IfcSubedgeParentEdge : AnonymousType<IfcEdge, IfcSubedgeParentEdge> {}
	public partial class IfcPolyLoopPolygon : ArrayWrapper<IfcCartesianPoint, IfcPolyLoopPolygon> {}
	public partial class IfcVertexLoopLoopVertex : AnonymousType<IfcVertex, IfcVertexLoopLoopVertex> {}
	public partial class IfcFaceSurfaceFaceSurface : AnonymousType<IfcSurface, IfcFaceSurfaceFaceSurface> {}
	public partial class IfcConicPosition : AnonymousType<IfcPlacement, IfcConicPosition> {}
	public partial class IfcVirtualGridIntersectionOffsetDistances : ValueTypeArrayWrapper<double, IfcLengthMeasure1, IfcVirtualGridIntersectionOffsetDistances> {}
	public partial class IfcGridPlacementPlacementRefDirection : AnonymousType<IfcVirtualGridIntersection, IfcGridPlacementPlacementRefDirection> {}
	public partial class IfcLocalPlacementPlacementRelTo : AnonymousType<IfcObjectPlacement, IfcLocalPlacementPlacementRelTo> {}
	public partial class IfcLocalPlacementRelativePlacement : AnonymousType<IfcPlacement, IfcLocalPlacementRelativePlacement> {}
	public partial class IfcProductRepresentation1 : AnonymousType<IfcProductRepresentation, IfcProductRepresentation1> {}
	public partial class IfcMaterialDefinitionRepresentationRepresentedMaterial : AnonymousType<IfcMaterial, IfcMaterialDefinitionRepresentationRepresentedMaterial> {}
	public partial class IfcProductRepresentationRepresentations : ArrayWrapper<IfcRepresentation, IfcProductRepresentationRepresentations> {}
	public partial class IfcRepresentationContextOfItems : AnonymousType<IfcRepresentationContext, IfcRepresentationContextOfItems> {}
	public partial class IfcGeometricRepresentationContextWorldCoordinateSystem : AnonymousType<IfcPlacement, IfcGeometricRepresentationContextWorldCoordinateSystem> {}
	public partial class IfcGeometricRepresentationContextTrueNorth : AnonymousType<IfcDirection, IfcGeometricRepresentationContextTrueNorth> {}
	public partial class IfcRepresentationItems : ArrayWrapper<IfcRepresentationItem, IfcRepresentationItems> {}
	public partial class IfcMappedItemMappingSource : AnonymousType<IfcRepresentationMap, IfcMappedItemMappingSource> {}
	public partial class IfcRepresentationMapMappingOrigin : AnonymousType<IfcPlacement, IfcRepresentationMapMappingOrigin>{}
	public partial class IfcRepresentationMapMappedRepresentation : AnonymousType<IfcRepresentation, IfcRepresentationMapMappedRepresentation>{}
	public partial class IfcMappedItemMappingTarget : AnonymousType<IfcCartesianTransformationOperator, IfcMappedItemMappingTarget>{}
	public partial class IfcCartesianTransformationOperator3DAxis3 : AnonymousType<IfcDirection, IfcCartesianTransformationOperator3DAxis3>{}
	public partial class IfcStyledItemItem : AnonymousType<IfcRepresentationItem, IfcStyledItemItem>{}
	public partial class IfcCurveStyleCurveFont : AnonymousType<Entity, IfcCurveStyleCurveFont>{}
	public partial class IfcCurveStyleFontAndScalingCurveFont : AnonymousType<Entity, IfcCurveStyleFontAndScalingCurveFont>{}
	public partial class IfcCurveStyleCurveWidth : AnonymousType<Object, IfcCurveStyleCurveWidth>{}
	public partial class IfcCurveStyleCurveColour : AnonymousType<Entity, IfcCurveStyleCurveColour>{}
	public partial class IfcFillAreaStyleHatchingHatchLineAppearance : AnonymousType<IfcCurveStyle, IfcFillAreaStyleHatchingHatchLineAppearance>{}
	public partial class IfcFillAreaStyleHatchingStartOfNextHatchLine : AnonymousType<Object, IfcFillAreaStyleHatchingStartOfNextHatchLine>{}
	public partial class IfcOneDirectionRepeatFactorRepeatFactor : AnonymousType<IfcVector, IfcOneDirectionRepeatFactorRepeatFactor>{}
	public partial class IfcTwoDirectionRepeatFactorSecondRepeatFactor : AnonymousType<IfcVector, IfcTwoDirectionRepeatFactorSecondRepeatFactor>{}
	public partial class IfcFillAreaStyleHatchingPointOfReferenceHatchLine : AnonymousType<IfcCartesianPoint, IfcFillAreaStyleHatchingPointOfReferenceHatchLine>{}
	public partial class IfcFillAreaStyleHatchingPatternStart : AnonymousType<IfcCartesianPoint, IfcFillAreaStyleHatchingPatternStart>{}
	public partial class IfcFillAreaStyleTilesTilingPattern : AnonymousType<IfcOneDirectionRepeatFactor, IfcFillAreaStyleTilesTilingPattern>{}
	public partial class IfcFillAreaStyleTileSymbolWithStyleSymbol : AnonymousType<IfcAnnotationSymbolOccurrence, IfcFillAreaStyleTileSymbolWithStyleSymbol>{}
	public partial class IfcTerminatorSymbolAnnotatedCurve : AnonymousType<IfcAnnotationCurveOccurrence, IfcTerminatorSymbolAnnotatedCurve>{}
	public partial class IfcSurfaceStyleLightingDiffuseTransmissionColour : AnonymousType<IfcColourRgb, IfcSurfaceStyleLightingDiffuseTransmissionColour>{}
	public partial class IfcSurfaceStyleLightingDiffuseReflectionColour : AnonymousType<IfcColourRgb, IfcSurfaceStyleLightingDiffuseReflectionColour>{}
	public partial class IfcSurfaceStyleLightingTransmissionColour : AnonymousType<IfcColourRgb, IfcSurfaceStyleLightingTransmissionColour>{}
	public partial class IfcSurfaceStyleLightingReflectanceColour : AnonymousType<IfcColourRgb, IfcSurfaceStyleLightingReflectanceColour>{}
	public partial class IfcSurfaceStyleRenderingDiffuseColour : AnonymousType<Object, IfcSurfaceStyleRenderingDiffuseColour>{}
	public partial class IfcSurfaceStyleRenderingTransmissionColour : AnonymousType<Object, IfcSurfaceStyleRenderingTransmissionColour>{}
	public partial class IfcSurfaceStyleRenderingDiffuseTransmissionColour : AnonymousType<Object, IfcSurfaceStyleRenderingDiffuseTransmissionColour>{}
	public partial class IfcSurfaceStyleRenderingReflectionColour : AnonymousType<Object, IfcSurfaceStyleRenderingReflectionColour>{}
	public partial class IfcSurfaceStyleRenderingSpecularColour : AnonymousType<Object, IfcSurfaceStyleRenderingSpecularColour>{}
	public partial class IfcSurfaceStyleRenderingSpecularHighlight : AnonymousType<Object, IfcSurfaceStyleRenderingSpecularHighlight>{}
	public partial class IfcSurfaceStyleShadingSurfaceColour : AnonymousType<IfcColourRgb, IfcSurfaceStyleShadingSurfaceColour>{}
	public partial class IfcSurfaceTextureTextureTransform : AnonymousType<IfcCartesianTransformationOperator2D, IfcSurfaceTextureTextureTransform>{}
	public partial class IfcSymbolStyleStyleOfSymbol : AnonymousType<Entity, IfcSymbolStyleStyleOfSymbol>{}
	public partial class IfcTextStyleTextCharacterAppearance : AnonymousType<IfcTextStyleForDefinedFont, IfcTextStyleTextCharacterAppearance>{}
	public partial class IfcTextStyleForDefinedFontColour : AnonymousType<Entity, IfcTextStyleForDefinedFontColour>{}
	public partial class IfcTextStyleForDefinedFontBackgroundColour : AnonymousType<Entity, IfcTextStyleForDefinedFontBackgroundColour>{}
	public partial class IfcTextStyleTextStyle : AnonymousType<Entity, IfcTextStyleTextStyle>{}
	public partial class IfcTextStyleTextModelTextIndent : AnonymousType<Object, IfcTextStyleTextModelTextIndent>{}
	public partial class IfcTextStyleTextModelLetterSpacing : AnonymousType<Object, IfcTextStyleTextModelLetterSpacing>{}
	public partial class IfcTextStyleTextModelWordSpacing : AnonymousType<Object, IfcTextStyleTextModelWordSpacing>{}
	public partial class IfcTextStyleTextModelLineHeight : AnonymousType<Object, IfcTextStyleTextModelLineHeight>{}
	public partial class IfcTextStyleWithBoxCharacteristicsCharacterSpacing : AnonymousType<Object, IfcTextStyleWithBoxCharacteristicsCharacterSpacing>{}
	public partial class IfcTextStyleTextFontStyle : AnonymousType<Entity, IfcTextStyleTextFontStyle>{}
	public partial class IfcTextStyleFontModelFontSize : AnonymousType<Object, IfcTextStyleFontModelFontSize>{}
	public partial class IfcStructuralConnectionAppliedCondition : AnonymousType<IfcBoundaryCondition, IfcStructuralConnectionAppliedCondition>{}
	public partial class IfcStructuralActivityAppliedLoad : AnonymousType<IfcStructuralLoad, IfcStructuralActivityAppliedLoad>{}
	public partial class IfcStructuralActionCausedBy : AnonymousType<IfcStructuralReaction, IfcStructuralActionCausedBy>{}
	public partial class IfcWorkControlCreationDate : AnonymousType<Entity, IfcWorkControlCreationDate>{}
	public partial class IfcDateAndTimeDateComponent : AnonymousType<IfcCalendarDate, IfcDateAndTimeDateComponent>{}
	public partial class IfcDateAndTimeTimeComponent : AnonymousType<IfcLocalTime, IfcDateAndTimeTimeComponent>{}
	public partial class IfcLocalTimeZone : AnonymousType<IfcCoordinatedUniversalTimeOffset, IfcLocalTimeZone>{}
	public partial class IfcWorkControlStartTime : AnonymousType<Entity, IfcWorkControlStartTime>{}
	public partial class IfcWorkControlFinishTime : AnonymousType<Entity, IfcWorkControlFinishTime>{}
	public partial class IfcProjectUnitsInContext : AnonymousType<IfcUnitAssignment, IfcProjectUnitsInContext>{}
	public partial class IfcRelDecomposesRelatingObject : AnonymousType<IfcObjectDefinition, IfcRelDecomposesRelatingObject>{}
	public partial class IfcDoorLiningPropertiesShapeAspectStyle : AnonymousType<IfcShapeAspect, IfcDoorLiningPropertiesShapeAspectStyle>{}
	public partial class IfcShapeAspectPartOfProductDefinitionShape : AnonymousType<IfcProductDefinitionShape, IfcShapeAspectPartOfProductDefinitionShape>{}
	public partial class IfcDoorPanelPropertiesShapeAspectStyle : AnonymousType<IfcShapeAspect, IfcDoorPanelPropertiesShapeAspectStyle>{}
	public partial class IfcPhysicalSimpleQuantityUnit : AnonymousType<IfcNamedUnit, IfcPhysicalSimpleQuantityUnit>{}
	public partial class IfcFluidFlowPropertiesFlowConditionTimeSeries : AnonymousType<IfcTimeSeries, IfcFluidFlowPropertiesFlowConditionTimeSeries>{}
	public partial class IfcIrregularTimeSeriesValueTimeStamp : AnonymousType<Entity, IfcIrregularTimeSeriesValueTimeStamp>{}
	public partial class IfcTimeSeriesStartTime : AnonymousType<Entity, IfcTimeSeriesStartTime>{}
	public partial class IfcTimeSeriesEndTime : AnonymousType<Entity, IfcTimeSeriesEndTime>{}
	public partial class IfcTimeSeriesUnit : AnonymousType<Entity, IfcTimeSeriesUnit>{}
	public partial class IfcFluidFlowPropertiesVelocityTimeSeries : AnonymousType<IfcTimeSeries, IfcFluidFlowPropertiesVelocityTimeSeries>{}
	public partial class IfcFluidFlowPropertiesFlowrateTimeSeries : AnonymousType<IfcTimeSeries, IfcFluidFlowPropertiesFlowrateTimeSeries>{}
	public partial class IfcFluidFlowPropertiesFluid : AnonymousType<IfcMaterial, IfcFluidFlowPropertiesFluid>{}
	public partial class IfcFluidFlowPropertiesPressureTimeSeries : AnonymousType<IfcTimeSeries, IfcFluidFlowPropertiesPressureTimeSeries>{}
	public partial class IfcFluidFlowPropertiesWetBulbTemperatureTimeSeries : AnonymousType<IfcTimeSeries, IfcFluidFlowPropertiesWetBulbTemperatureTimeSeries>{}
	public partial class IfcFluidFlowPropertiesTemperatureTimeSeries : AnonymousType<IfcTimeSeries, IfcFluidFlowPropertiesTemperatureTimeSeries>{}
	public partial class IfcFluidFlowPropertiesFlowrateSingleValue : AnonymousType<Object, IfcFluidFlowPropertiesFlowrateSingleValue>{}
	public partial class IfcPermeableCoveringPropertiesShapeAspectStyle : AnonymousType<IfcShapeAspect, IfcPermeableCoveringPropertiesShapeAspectStyle>{}
	public partial class IfcSectionReinforcementPropertiesSectionDefinition : AnonymousType<IfcSectionProperties, IfcSectionReinforcementPropertiesSectionDefinition>{}
	public partial class IfcSectionPropertiesStartProfile : AnonymousType<IfcProfileDef, IfcSectionPropertiesStartProfile>{}
	public partial class IfcSectionPropertiesEndProfile : AnonymousType<IfcProfileDef, IfcSectionPropertiesEndProfile>{}
	public partial class IfcServiceLifeFactorUpperValue : AnonymousType<Object, IfcServiceLifeFactorUpperValue>{}
	public partial class IfcServiceLifeFactorMostUsedValue : AnonymousType<Object, IfcServiceLifeFactorMostUsedValue>{}
	public partial class IfcServiceLifeFactorLowerValue : AnonymousType<Object, IfcServiceLifeFactorLowerValue>{}
	public partial class IfcSoundValueSoundLevelTimeSeries : AnonymousType<IfcTimeSeries, IfcSoundValueSoundLevelTimeSeries>{}
	public partial class IfcSoundValueSoundLevelSingleValue : AnonymousType<Object, IfcSoundValueSoundLevelSingleValue>{}
	public partial class IfcSpaceThermalLoadPropertiesThermalLoadTimeSeriesValues : AnonymousType<IfcTimeSeries, IfcSpaceThermalLoadPropertiesThermalLoadTimeSeriesValues>{}
	public partial class IfcWindowLiningPropertiesShapeAspectStyle : AnonymousType<IfcShapeAspect, IfcWindowLiningPropertiesShapeAspectStyle>{}
	public partial class IfcWindowPanelPropertiesShapeAspectStyle : AnonymousType<IfcShapeAspect, IfcWindowPanelPropertiesShapeAspectStyle>{}
	public partial class IfcProfilePropertiesProfileDefinition : AnonymousType<IfcProfileDef, IfcProfilePropertiesProfileDefinition>{}
	public partial class IfcMaterialPropertiesMaterial : AnonymousType<IfcMaterial, IfcMaterialPropertiesMaterial>{}
	public partial class IfcConstraintCreatingActor : AnonymousType<Entity, IfcConstraintCreatingActor>{}
	public partial class IfcConstraintCreationTime : AnonymousType<Entity, IfcConstraintCreationTime>{}
	public partial class IfcAppliedValueAppliedValue : AnonymousType<Object, IfcAppliedValueAppliedValue>{}
	public partial class IfcAppliedValueUnitBasis : AnonymousType<IfcMeasureWithUnit, IfcAppliedValueUnitBasis>{}
	public partial class IfcAppliedValueApplicableDate : AnonymousType<Entity, IfcAppliedValueApplicableDate>{}
	public partial class IfcAppliedValueFixedUntilDate : AnonymousType<Entity, IfcAppliedValueFixedUntilDate>{}
	public partial class IfcAnnotationFillAreaOuterBoundary : AnonymousType<IfcCurve, IfcAnnotationFillAreaOuterBoundary>{}
	public partial class IfcAnnotationFillAreaOccurrenceFillStyleTarget : AnonymousType<IfcPoint, IfcAnnotationFillAreaOccurrenceFillStyleTarget>{}
	public partial class IfcAnnotationSurfaceItem : AnonymousType<IfcGeometricRepresentationItem, IfcAnnotationSurfaceItem>{}
	public partial class IfcBooleanResultFirstOperand : AnonymousType<IfcGeometricRepresentationItem, IfcBooleanResultFirstOperand>{}
	public partial class IfcBoxedHalfSpaceEnclosure : AnonymousType<IfcBoundingBox, IfcBoxedHalfSpaceEnclosure>{}
	public partial class IfcBoundingBoxCorner : AnonymousType<IfcCartesianPoint, IfcBoundingBoxCorner>{}
	public partial class IfcHalfSpaceSolidBaseSurface : AnonymousType<IfcSurface, IfcHalfSpaceSolidBaseSurface>{}
	public partial class IfcCsgSolidTreeRootExpression : AnonymousType<IfcGeometricRepresentationItem, IfcCsgSolidTreeRootExpression>{}
	public partial class IfcPolygonalBoundedHalfSpacePosition : AnonymousType<IfcAxis2Placement3D, IfcPolygonalBoundedHalfSpacePosition>{}
	public partial class IfcPolygonalBoundedHalfSpacePolygonalBoundary : AnonymousType<IfcBoundedCurve, IfcPolygonalBoundedHalfSpacePolygonalBoundary>{}
	public partial class IfcSweptDiskSolidDirectrix : AnonymousType<IfcCurve, IfcSweptDiskSolidDirectrix>{}
	public partial class IfcBooleanResultSecondOperand : AnonymousType<IfcGeometricRepresentationItem, IfcBooleanResultSecondOperand>{}
	public partial class IfcDefinedSymbolDefinition : AnonymousType<Entity, IfcDefinedSymbolDefinition>{}
	public partial class IfcDefinedSymbolTarget : AnonymousType<IfcCartesianTransformationOperator2D, IfcDefinedSymbolTarget>{}
	public partial class IfcSectionedSpineSpineCurve : AnonymousType<IfcCompositeCurve, IfcSectionedSpineSpineCurve>{}
	public partial class IfcTextLiteralPlacement : AnonymousType<IfcPlacement, IfcTextLiteralPlacement>{}
	public partial class IfcAnnotationSurfaceTextureCoordinates : AnonymousType<IfcTextureCoordinate, IfcAnnotationSurfaceTextureCoordinates>{}
	public partial class IfcAppliedValueRelationshipComponentOfTotal : AnonymousType<IfcAppliedValue, IfcAppliedValueRelationshipComponentOfTotal>{}
	public partial class IfcApprovalApprovalDateTime : AnonymousType<Entity, IfcApprovalApprovalDateTime>{}
	public partial class IfcApprovalActorRelationshipActor : AnonymousType<Entity, IfcApprovalActorRelationshipActor>{}
	public partial class IfcApprovalActorRelationshipApproval : AnonymousType<IfcApproval, IfcApprovalActorRelationshipApproval>{}
	public partial class IfcApprovalActorRelationshipRole : AnonymousType<IfcActorRole, IfcApprovalActorRelationshipRole>{}
	public partial class IfcApprovalPropertyRelationshipApproval : AnonymousType<IfcApproval, IfcApprovalPropertyRelationshipApproval>{}
	public partial class IfcApprovalRelationshipRelatedApproval : AnonymousType<IfcApproval, IfcApprovalRelationshipRelatedApproval>{}
	public partial class IfcApprovalRelationshipRelatingApproval : AnonymousType<IfcApproval, IfcApprovalRelationshipRelatingApproval>{}
	public partial class IfcAssetOriginalValue : AnonymousType<IfcCostValue, IfcAssetOriginalValue>{}
	public partial class IfcAssetCurrentValue : AnonymousType<IfcCostValue, IfcAssetCurrentValue>{}
	public partial class IfcAssetTotalReplacementCost : AnonymousType<IfcCostValue, IfcAssetTotalReplacementCost>{}
	public partial class IfcAssetOwner : AnonymousType<Entity, IfcAssetOwner>{}
	public partial class IfcAssetUser : AnonymousType<Entity, IfcAssetUser>{}
	public partial class IfcAssetResponsiblePerson : AnonymousType<IfcPerson, IfcAssetResponsiblePerson>{}
	public partial class IfcAssetIncorporationDate : AnonymousType<IfcCalendarDate, IfcAssetIncorporationDate>{}
	public partial class IfcAssetDepreciatedValue : AnonymousType<IfcCostValue, IfcAssetDepreciatedValue>{}
	public partial class IfcAxis1PlacementAxis : AnonymousType<IfcDirection, IfcAxis1PlacementAxis>{}
	public partial class IfcBuildingBuildingAddress : AnonymousType<IfcPostalAddress, IfcBuildingBuildingAddress>{}
	public partial class IfcClassificationEditionDate : AnonymousType<IfcCalendarDate, IfcClassificationEditionDate>{}
	public partial class IfcClassificationItemNotation : AnonymousType<IfcClassificationNotationFacet, IfcClassificationItemNotation>{}
	public partial class IfcClassificationItemItemOf : AnonymousType<IfcClassification, IfcClassificationItemItemOf>{}
	public partial class IfcClassificationItemRelationshipRelatingItem : AnonymousType<IfcClassificationItem, IfcClassificationItemRelationshipRelatingItem>{}
	public partial class IfcClassificationReferenceReferencedSource : AnonymousType<IfcClassification, IfcClassificationReferenceReferencedSource>{}
	public partial class IfcConditionCriterionCriterion : AnonymousType<Object, IfcConditionCriterionCriterion>{}
	public partial class IfcConditionCriterionCriterionDateTime : AnonymousType<Entity, IfcConditionCriterionCriterionDateTime>{}
	public partial class IfcConnectionCurveGeometryCurveOnRelatingElement : AnonymousType<IfcRepresentationItem, IfcConnectionCurveGeometryCurveOnRelatingElement>{}
	public partial class IfcConnectionCurveGeometryCurveOnRelatedElement : AnonymousType<IfcRepresentationItem, IfcConnectionCurveGeometryCurveOnRelatedElement>{}
	public partial class IfcConnectionPointGeometryPointOnRelatingElement : AnonymousType<IfcRepresentationItem, IfcConnectionPointGeometryPointOnRelatingElement>{}
	public partial class IfcConnectionPointGeometryPointOnRelatedElement : AnonymousType<IfcRepresentationItem, IfcConnectionPointGeometryPointOnRelatedElement>{}
	public partial class IfcConnectionPortGeometryLocationAtRelatingElement : AnonymousType<IfcPlacement, IfcConnectionPortGeometryLocationAtRelatingElement>{}
	public partial class IfcConnectionPortGeometryLocationAtRelatedElement : AnonymousType<IfcPlacement, IfcConnectionPortGeometryLocationAtRelatedElement>{}
	public partial class IfcConnectionPortGeometryProfileOfPort : AnonymousType<IfcProfileDef, IfcConnectionPortGeometryProfileOfPort>{}
	public partial class IfcConnectionSurfaceGeometrySurfaceOnRelatingElement : AnonymousType<IfcRepresentationItem, IfcConnectionSurfaceGeometrySurfaceOnRelatingElement>{}
	public partial class IfcConnectionSurfaceGeometrySurfaceOnRelatedElement : AnonymousType<IfcRepresentationItem, IfcConnectionSurfaceGeometrySurfaceOnRelatedElement>{}
	public partial class IfcConstraintAggregationRelationshipRelatingConstraint : AnonymousType<IfcConstraint, IfcConstraintAggregationRelationshipRelatingConstraint>{}
	public partial class IfcMetricDataValue : AnonymousType<Object, IfcMetricDataValue>{}
	public partial class IfcObjectiveBenchmarkValues : AnonymousType<IfcMetric, IfcObjectiveBenchmarkValues>{}
	public partial class IfcObjectiveResultValues : AnonymousType<IfcMetric, IfcObjectiveResultValues>{}
	public partial class IfcConstraintClassificationRelationshipClassifiedConstraint : AnonymousType<IfcConstraint, IfcConstraintClassificationRelationshipClassifiedConstraint>{}
	public partial class IfcConstraintRelationshipRelatingConstraint : AnonymousType<IfcConstraint, IfcConstraintRelationshipRelatingConstraint>{}
	public partial class IfcCostScheduleSubmittedBy : AnonymousType<Entity, IfcCostScheduleSubmittedBy>{}
	public partial class IfcCostSchedulePreparedBy : AnonymousType<Entity, IfcCostSchedulePreparedBy>{}
	public partial class IfcCostScheduleSubmittedOn : AnonymousType<Entity, IfcCostScheduleSubmittedOn>{}
	public partial class IfcCostScheduleUpdateDate : AnonymousType<Entity, IfcCostScheduleUpdateDate>{}
	public partial class IfcCurrencyRelationshipRelatingMonetaryUnit : AnonymousType<IfcMonetaryUnit, IfcCurrencyRelationshipRelatingMonetaryUnit>{}
	public partial class IfcCurrencyRelationshipRelatedMonetaryUnit : AnonymousType<IfcMonetaryUnit, IfcCurrencyRelationshipRelatedMonetaryUnit>{}
	public partial class IfcCurrencyRelationshipRateDateTime : AnonymousType<IfcDateAndTime, IfcCurrencyRelationshipRateDateTime>{}
	public partial class IfcCurrencyRelationshipRateSource : AnonymousType<IfcLibraryInformation, IfcCurrencyRelationshipRateSource>{}
	public partial class IfcLibraryInformationPublisher : AnonymousType<IfcOrganization, IfcLibraryInformationPublisher>{}
	public partial class IfcLibraryInformationVersionDate : AnonymousType<IfcCalendarDate, IfcLibraryInformationVersionDate>{}
	public partial class IfcCurveBoundedPlaneBasisSurface : AnonymousType<IfcPlane, IfcCurveBoundedPlaneBasisSurface>{}
	public partial class IfcCurveBoundedPlaneOuterBoundary : AnonymousType<IfcCurve, IfcCurveBoundedPlaneOuterBoundary>{}
	public partial class IfcDraughtingCalloutRelationshipRelatingDraughtingCallout : AnonymousType<IfcDraughtingCallout, IfcDraughtingCalloutRelationshipRelatingDraughtingCallout>{}
	public partial class IfcDraughtingCalloutRelationshipRelatedDraughtingCallout : AnonymousType<IfcDraughtingCallout, IfcDraughtingCalloutRelationshipRelatedDraughtingCallout>{}
	public partial class IfcDocumentInformationDocumentOwner : AnonymousType<Entity, IfcDocumentInformationDocumentOwner>{}
	public partial class IfcDocumentInformationCreationTime : AnonymousType<IfcDateAndTime, IfcDocumentInformationCreationTime>{}
	public partial class IfcDocumentInformationLastRevisionTime : AnonymousType<IfcDateAndTime, IfcDocumentInformationLastRevisionTime>{}
	public partial class IfcDocumentInformationElectronicFormat : AnonymousType<IfcDocumentElectronicFormat, IfcDocumentInformationElectronicFormat>{}
	public partial class IfcDocumentInformationValidFrom : AnonymousType<IfcCalendarDate, IfcDocumentInformationValidFrom>{}
	public partial class IfcDocumentInformationValidUntil : AnonymousType<IfcCalendarDate, IfcDocumentInformationValidUntil>{}
	public partial class IfcDocumentInformationRelationshipRelatingDocument : AnonymousType<IfcDocumentInformation, IfcDocumentInformationRelationshipRelatingDocument>{}
	public partial class IfcExtrudedAreaSolidExtrudedDirection : AnonymousType<IfcDirection, IfcExtrudedAreaSolidExtrudedDirection>{}
	public partial class IfcGeometricRepresentationSubContextParentContext : AnonymousType<IfcGeometricRepresentationContext, IfcGeometricRepresentationSubContextParentContext>{}
	public partial class IfcInventoryJurisdiction : AnonymousType<Entity, IfcInventoryJurisdiction>{}
	public partial class IfcInventoryLastUpdateDate : AnonymousType<IfcCalendarDate, IfcInventoryLastUpdateDate>{}
	public partial class IfcInventoryCurrentValue : AnonymousType<IfcCostValue, IfcInventoryCurrentValue>{}
	public partial class IfcInventoryOriginalValue : AnonymousType<IfcCostValue, IfcInventoryOriginalValue>{}
	public partial class IfcLightSourceDirectionalOrientation : AnonymousType<IfcDirection, IfcLightSourceDirectionalOrientation>{}
	public partial class IfcLightSourceGoniometricPosition : AnonymousType<IfcAxis2Placement3D, IfcLightSourceGoniometricPosition>{}
	public partial class IfcLightSourceGoniometricColourAppearance : AnonymousType<IfcColourRgb, IfcLightSourceGoniometricColourAppearance>{}
	public partial class IfcLightSourceGoniometricLightDistributionDataSource : AnonymousType<Entity, IfcLightSourceGoniometricLightDistributionDataSource>{}
	public partial class IfcLightSourcePositionalPosition : AnonymousType<IfcCartesianPoint, IfcLightSourcePositionalPosition>{}
	public partial class IfcLightSourceSpotOrientation : AnonymousType<IfcDirection, IfcLightSourceSpotOrientation>{}
	public partial class IfcMaterialClassificationRelationshipClassifiedMaterial : AnonymousType<IfcMaterial, IfcMaterialClassificationRelationshipClassifiedMaterial>{}
	public partial class IfcMaterialLayerMaterial : AnonymousType<IfcMaterial, IfcMaterialLayerMaterial>{}
	public partial class IfcMaterialLayerSetUsageForLayerSet : AnonymousType<IfcMaterialLayerSet, IfcMaterialLayerSetUsageForLayerSet>{}
	public partial class IfcMoveMoveFrom : AnonymousType<IfcSpatialStructureElement, IfcMoveMoveFrom>{}
	public partial class IfcSiteSiteAddress : AnonymousType<IfcPostalAddress, IfcSiteSiteAddress>{}
	public partial class IfcMoveMoveTo : AnonymousType<IfcSpatialStructureElement, IfcMoveMoveTo>{}
	public partial class IfcOrganizationRelationshipRelatingOrganization : AnonymousType<IfcOrganization, IfcOrganizationRelationshipRelatingOrganization>{}
	public partial class IfcPlanarBoxPlacement : AnonymousType<IfcPlacement, IfcPlanarBoxPlacement>{}
	public partial class IfcRelAssignsToControlRelatingControl : AnonymousType<IfcControl, IfcRelAssignsToControlRelatingControl>{}
	public partial class IfcScheduleTimeControlActualStart : AnonymousType<Entity, IfcScheduleTimeControlActualStart>{}
	public partial class IfcScheduleTimeControlEarlyStart : AnonymousType<Entity, IfcScheduleTimeControlEarlyStart>{}
	public partial class IfcScheduleTimeControlLateStart : AnonymousType<Entity, IfcScheduleTimeControlLateStart>{}
	public partial class IfcScheduleTimeControlScheduleStart : AnonymousType<Entity, IfcScheduleTimeControlScheduleStart>{}
	public partial class IfcScheduleTimeControlActualFinish : AnonymousType<Entity, IfcScheduleTimeControlActualFinish>{}
	public partial class IfcScheduleTimeControlEarlyFinish : AnonymousType<Entity, IfcScheduleTimeControlEarlyFinish>{}
	public partial class IfcScheduleTimeControlLateFinish : AnonymousType<Entity, IfcScheduleTimeControlLateFinish>{}
	public partial class IfcScheduleTimeControlScheduleFinish : AnonymousType<Entity, IfcScheduleTimeControlScheduleFinish>{}
	public partial class IfcScheduleTimeControlStatusTime : AnonymousType<Entity, IfcScheduleTimeControlStatusTime>{}
	public partial class IfcSpaceProgramRequestedLocation : AnonymousType<IfcSpatialStructureElement, IfcSpaceProgramRequestedLocation>{}
	public partial class IfcTimeSeriesScheduleTimeSeries : AnonymousType<IfcTimeSeries, IfcTimeSeriesScheduleTimeSeries>{}
	public partial class IfcPropertyBoundedValueUpperBoundValue : AnonymousType<Object, IfcPropertyBoundedValueUpperBoundValue>{}
	public partial class IfcPropertyBoundedValueLowerBoundValue : AnonymousType<Object, IfcPropertyBoundedValueLowerBoundValue>{}
	public partial class IfcPropertyBoundedValueUnit : AnonymousType<Entity, IfcPropertyBoundedValueUnit>{}
	public partial class IfcPropertyConstraintRelationshipRelatingConstraint : AnonymousType<IfcConstraint, IfcPropertyConstraintRelationshipRelatingConstraint>{}
	public partial class IfcPropertyDependencyRelationshipDependingProperty : AnonymousType<IfcProperty, IfcPropertyDependencyRelationshipDependingProperty>{}
	public partial class IfcPropertyDependencyRelationshipDependantProperty : AnonymousType<IfcProperty, IfcPropertyDependencyRelationshipDependantProperty>{}
	public partial class IfcPropertyEnumeratedValueEnumerationReference : AnonymousType<IfcPropertyEnumeration, IfcPropertyEnumeratedValueEnumerationReference>{}
	public partial class IfcPropertyEnumerationUnit : AnonymousType<Entity, IfcPropertyEnumerationUnit>{}
	public partial class IfcPropertyListValueUnit : AnonymousType<Entity, IfcPropertyListValueUnit>{}
	public partial class IfcPropertyReferenceValuePropertyReference : AnonymousType<Entity, IfcPropertyReferenceValuePropertyReference>{}
	public partial class IfcPropertySingleValueNominalValue : AnonymousType<Object, IfcPropertySingleValueNominalValue>{}
	public partial class IfcPropertySingleValueUnit : AnonymousType<Entity, IfcPropertySingleValueUnit>{}
	public partial class IfcPropertyTableValueDefiningUnit : AnonymousType<Entity, IfcPropertyTableValueDefiningUnit>{}
	public partial class IfcPropertyTableValueDefinedUnit : AnonymousType<Entity, IfcPropertyTableValueDefinedUnit>{}
	public partial class IfcRectangularTrimmedSurfaceBasisSurface : AnonymousType<IfcSurface, IfcRectangularTrimmedSurfaceBasisSurface>{}
	public partial class IfcReferencesValueDocumentReferencedDocument : AnonymousType<Entity, IfcReferencesValueDocumentReferencedDocument>{}
	public partial class IfcRelAssignsTasksTimeForTask : AnonymousType<IfcScheduleTimeControl, IfcRelAssignsTasksTimeForTask>{}
	public partial class IfcRelAssignsToActorRelatingActor : AnonymousType<IfcActor, IfcRelAssignsToActorRelatingActor>{}
	public partial class IfcRelAssignsToActorActingRole : AnonymousType<IfcActorRole, IfcRelAssignsToActorActingRole>{}
	public partial class IfcRelAssignsToGroupRelatingGroup : AnonymousType<IfcGroup, IfcRelAssignsToGroupRelatingGroup>{}
	public partial class IfcStructuralResultGroupResultForLoadGroup : AnonymousType<IfcStructuralLoadGroup, IfcStructuralResultGroupResultForLoadGroup>{}
	public partial class IfcRelAssignsToProcessRelatingProcess : AnonymousType<IfcProcess, IfcRelAssignsToProcessRelatingProcess>{}
	public partial class IfcRelAssignsToProcessQuantityInProcess : AnonymousType<IfcMeasureWithUnit, IfcRelAssignsToProcessQuantityInProcess>{}
	public partial class IfcRelAssignsToProductRelatingProduct : AnonymousType<IfcProduct, IfcRelAssignsToProductRelatingProduct>{}
	public partial class IfcRelAssignsToResourceRelatingResource : AnonymousType<IfcResource, IfcRelAssignsToResourceRelatingResource>{}
	public partial class IfcRelAssociatesAppliedValueRelatingAppliedValue : AnonymousType<IfcAppliedValue, IfcRelAssociatesAppliedValueRelatingAppliedValue>{}
	public partial class IfcRelAssociatesApprovalRelatingApproval : AnonymousType<IfcApproval, IfcRelAssociatesApprovalRelatingApproval>{}
	public partial class IfcRelAssociatesClassificationRelatingClassification : AnonymousType<Entity, IfcRelAssociatesClassificationRelatingClassification>{}
	public partial class IfcRelAssociatesConstraintRelatingConstraint : AnonymousType<IfcConstraint, IfcRelAssociatesConstraintRelatingConstraint>{}
	public partial class IfcRelAssociatesDocumentRelatingDocument : AnonymousType<Entity, IfcRelAssociatesDocumentRelatingDocument>{}
	public partial class IfcRelAssociatesLibraryRelatingLibrary : AnonymousType<Entity, IfcRelAssociatesLibraryRelatingLibrary>{}
	public partial class IfcRelAssociatesMaterialRelatingMaterial : AnonymousType<Entity, IfcRelAssociatesMaterialRelatingMaterial>{}
	public partial class IfcRelAssociatesProfilePropertiesRelatingProfileProperties : AnonymousType<IfcProfileProperties, IfcRelAssociatesProfilePropertiesRelatingProfileProperties>{}
	public partial class IfcRelAssociatesProfilePropertiesProfileSectionLocation : AnonymousType<IfcShapeAspect, IfcRelAssociatesProfilePropertiesProfileSectionLocation>{}
	public partial class IfcRelAssociatesProfilePropertiesProfileOrientation : AnonymousType<Object, IfcRelAssociatesProfilePropertiesProfileOrientation>{}
	public partial class IfcRelConnectsElementsConnectionGeometry : AnonymousType<IfcConnectionGeometry, IfcRelConnectsElementsConnectionGeometry>{}
	public partial class IfcRelConnectsElementsRelatingElement : AnonymousType<IfcElement, IfcRelConnectsElementsRelatingElement>{}
	public partial class IfcRelConnectsElementsRelatedElement : AnonymousType<IfcElement, IfcRelConnectsElementsRelatedElement>{}
	public partial class IfcRelConnectsPortToElementRelatingPort : AnonymousType<IfcPort, IfcRelConnectsPortToElementRelatingPort>{}
	public partial class IfcRelConnectsPortToElementRelatedElement : AnonymousType<IfcElement, IfcRelConnectsPortToElementRelatedElement>{}
	public partial class IfcRelConnectsPortsRelatingPort : AnonymousType<IfcPort, IfcRelConnectsPortsRelatingPort>{}
	public partial class IfcRelConnectsPortsRelatedPort : AnonymousType<IfcPort, IfcRelConnectsPortsRelatedPort>{}
	public partial class IfcRelConnectsPortsRealizingElement : AnonymousType<IfcElement, IfcRelConnectsPortsRealizingElement>{}
	public partial class IfcRelConnectsStructuralActivityRelatingElement : AnonymousType<IfcProduct, IfcRelConnectsStructuralActivityRelatingElement>{}
	public partial class IfcRelConnectsStructuralActivityRelatedStructuralActivity : AnonymousType<IfcStructuralActivity, IfcRelConnectsStructuralActivityRelatedStructuralActivity>{}
	public partial class IfcRelConnectsStructuralElementRelatingElement : AnonymousType<IfcElement, IfcRelConnectsStructuralElementRelatingElement>{}
	public partial class IfcRelConnectsStructuralElementRelatedStructuralMember : AnonymousType<IfcStructuralMember, IfcRelConnectsStructuralElementRelatedStructuralMember>{}
	public partial class IfcRelConnectsStructuralMemberRelatingStructuralMember : AnonymousType<IfcStructuralMember, IfcRelConnectsStructuralMemberRelatingStructuralMember>{}
	public partial class IfcRelConnectsStructuralMemberRelatedStructuralConnection : AnonymousType<IfcStructuralConnection, IfcRelConnectsStructuralMemberRelatedStructuralConnection>{}
	public partial class IfcRelConnectsStructuralMemberAppliedCondition : AnonymousType<IfcBoundaryCondition, IfcRelConnectsStructuralMemberAppliedCondition>{}
	public partial class IfcRelConnectsStructuralMemberAdditionalConditions : AnonymousType<IfcStructuralConnectionCondition, IfcRelConnectsStructuralMemberAdditionalConditions>{}
	public partial class IfcRelConnectsStructuralMemberConditionCoordinateSystem : AnonymousType<IfcAxis2Placement3D, IfcRelConnectsStructuralMemberConditionCoordinateSystem>{}
	public partial class IfcRelConnectsWithEccentricityConnectionConstraint : AnonymousType<IfcConnectionGeometry, IfcRelConnectsWithEccentricityConnectionConstraint>{}
	public partial class IfcRelContainedInSpatialStructureRelatingStructure : AnonymousType<IfcSpatialStructureElement, IfcRelContainedInSpatialStructureRelatingStructure>{}
	public partial class IfcRelCoversBldgElementsRelatingBuildingElement : AnonymousType<IfcElement, IfcRelCoversBldgElementsRelatingBuildingElement>{}
	public partial class IfcRelCoversSpacesRelatedSpace : AnonymousType<IfcSpace, IfcRelCoversSpacesRelatedSpace>{}
	public partial class IfcRelDefinesByPropertiesRelatingPropertyDefinition : AnonymousType<IfcPropertySetDefinition, IfcRelDefinesByPropertiesRelatingPropertyDefinition>{}
	public partial class IfcRelDefinesByTypeRelatingType : AnonymousType<IfcTypeObject, IfcRelDefinesByTypeRelatingType>{}
	public partial class IfcRelFillsElementRelatingOpeningElement : AnonymousType<IfcOpeningElement, IfcRelFillsElementRelatingOpeningElement>{}
	public partial class IfcRelFillsElementRelatedBuildingElement : AnonymousType<IfcElement, IfcRelFillsElementRelatedBuildingElement>{}
	public partial class IfcRelFlowControlElementsRelatingFlowElement : AnonymousType<IfcDistributionFlowElement, IfcRelFlowControlElementsRelatingFlowElement>{}
	public partial class IfcRelInteractionRequirementsLocationOfInteraction : AnonymousType<IfcSpatialStructureElement, IfcRelInteractionRequirementsLocationOfInteraction>{}
	public partial class IfcRelInteractionRequirementsRelatedSpaceProgram : AnonymousType<IfcSpaceProgram, IfcRelInteractionRequirementsRelatedSpaceProgram>{}
	public partial class IfcRelInteractionRequirementsRelatingSpaceProgram : AnonymousType<IfcSpaceProgram, IfcRelInteractionRequirementsRelatingSpaceProgram>{}
	public partial class IfcRelProjectsElementRelatingElement : AnonymousType<IfcElement, IfcRelProjectsElementRelatingElement>{}
	public partial class IfcRelProjectsElementRelatedFeatureElement : AnonymousType<IfcFeatureElementAddition, IfcRelProjectsElementRelatedFeatureElement>{}
	public partial class IfcRelReferencedInSpatialStructureRelatingStructure : AnonymousType<IfcSpatialStructureElement, IfcRelReferencedInSpatialStructureRelatingStructure>{}
	public partial class IfcRelSequenceRelatingProcess : AnonymousType<IfcProcess, IfcRelSequenceRelatingProcess>{}
	public partial class IfcRelSequenceRelatedProcess : AnonymousType<IfcProcess, IfcRelSequenceRelatedProcess>{}
	public partial class IfcRelServicesBuildingsRelatingSystem : AnonymousType<IfcSystem, IfcRelServicesBuildingsRelatingSystem>{}
	public partial class IfcStructuralAnalysisModelOrientationOf2DPlane : AnonymousType<IfcAxis2Placement3D, IfcStructuralAnalysisModelOrientationOf2DPlane>{}
	public partial class IfcRelSpaceBoundaryRelatingSpace : AnonymousType<IfcSpace, IfcRelSpaceBoundaryRelatingSpace>{}
	public partial class IfcRelSpaceBoundaryRelatedBuildingElement : AnonymousType<IfcElement, IfcRelSpaceBoundaryRelatedBuildingElement>{}
	public partial class IfcRelSpaceBoundaryConnectionGeometry : AnonymousType<IfcConnectionGeometry, IfcRelSpaceBoundaryConnectionGeometry>{}
	public partial class IfcRelVoidsElementRelatingBuildingElement : AnonymousType<IfcElement, IfcRelVoidsElementRelatingBuildingElement>{}
	public partial class IfcRelVoidsElementRelatedOpeningElement : AnonymousType<IfcFeatureElementSubtraction, IfcRelVoidsElementRelatedOpeningElement>{}
	public partial class IfcRevolvedAreaSolidAxis : AnonymousType<IfcAxis1Placement, IfcRevolvedAreaSolidAxis>{}
	public partial class IfcStructuralLinearActionVaryingVaryingAppliedLoadLocation : AnonymousType<IfcShapeAspect, IfcStructuralLinearActionVaryingVaryingAppliedLoadLocation>{}
	public partial class IfcStructuralPlanarActionVaryingVaryingAppliedLoadLocation : AnonymousType<IfcShapeAspect, IfcStructuralPlanarActionVaryingVaryingAppliedLoadLocation>{}
	public partial class IfcStructuralSurfaceMemberVaryingVaryingThicknessLocation : AnonymousType<IfcShapeAspect, IfcStructuralSurfaceMemberVaryingVaryingThicknessLocation>{}
	public partial class IfcSubContractResourceSubContractor : AnonymousType<Entity, IfcSubContractResourceSubContractor>{}
	public partial class IfcSurfaceCurveSweptAreaSolidDirectrix : AnonymousType<IfcCurve, IfcSurfaceCurveSweptAreaSolidDirectrix>{}
	public partial class IfcSurfaceCurveSweptAreaSolidReferenceSurface : AnonymousType<IfcSurface, IfcSurfaceCurveSweptAreaSolidReferenceSurface>{}
	public partial class IfcSurfaceOfLinearExtrusionExtrudedDirection : AnonymousType<IfcDirection, IfcSurfaceOfLinearExtrusionExtrudedDirection>{}
	public partial class IfcSurfaceOfRevolutionAxisPosition : AnonymousType<IfcAxis1Placement, IfcSurfaceOfRevolutionAxisPosition>{}
	public partial class IfcTextLiteralWithExtentExtent : AnonymousType<IfcPlanarExtent, IfcTextLiteralWithExtentExtent>{}
	public partial class IfcTimeSeriesReferenceRelationshipReferencedTimeSeries : AnonymousType<IfcTimeSeries, IfcTimeSeriesReferenceRelationshipReferencedTimeSeries>{}
	
	
	//ValueTypes below
	public partial class IfcText1 : ValueType<String, IfcText1>{}
	public partial class IfcTimeStamp1 : ValueType<Int64, IfcTimeStamp1>{}
	public partial class IfcNullStyle1 : ValueType<IfcNullStyle, IfcNullStyle1>{}
	public partial class IfcTextFontName1 : ValueType<String, IfcTextFontName1>{}
	public partial class IfcActionSourceTypeEnum1 : ValueType<IfcActionSourceTypeEnum, IfcActionSourceTypeEnum1>{}
	public partial class IfcActionTypeEnum1 : ValueType<IfcActionTypeEnum, IfcActionTypeEnum1>{}
	public partial class IfcActuatorTypeEnum1 : ValueType<IfcActuatorTypeEnum, IfcActuatorTypeEnum1>{}
	public partial class IfcAddressTypeEnum1 : ValueType<IfcAddressTypeEnum, IfcAddressTypeEnum1>{}
	public partial class IfcAheadOrBehind1 : ValueType<IfcAheadOrBehind, IfcAheadOrBehind1>{}
	public partial class IfcAirTerminalBoxTypeEnum1 : ValueType<IfcAirTerminalBoxTypeEnum, IfcAirTerminalBoxTypeEnum1>{}
	public partial class IfcAirTerminalTypeEnum1 : ValueType<IfcAirTerminalTypeEnum, IfcAirTerminalTypeEnum1>{}
	public partial class IfcAirToAirHeatRecoveryTypeEnum1 : ValueType<IfcAirToAirHeatRecoveryTypeEnum, IfcAirToAirHeatRecoveryTypeEnum1>{}
	public partial class IfcAlarmTypeEnum1 : ValueType<IfcAlarmTypeEnum, IfcAlarmTypeEnum1>{}
	public partial class IfcAnalysisModelTypeEnum1 : ValueType<IfcAnalysisModelTypeEnum, IfcAnalysisModelTypeEnum1>{}
	public partial class IfcAnalysisTheoryTypeEnum1 : ValueType<IfcAnalysisTheoryTypeEnum, IfcAnalysisTheoryTypeEnum1>{}
	public partial class IfcArithmeticOperatorEnum1 : ValueType<IfcArithmeticOperatorEnum, IfcArithmeticOperatorEnum1>{}
	public partial class IfcAssemblyPlaceEnum1 : ValueType<IfcAssemblyPlaceEnum, IfcAssemblyPlaceEnum1>{}
	public partial class IfcBSplineCurveForm1 : ValueType<IfcBSplineCurveForm, IfcBSplineCurveForm1>{}
	public partial class IfcBeamTypeEnum1 : ValueType<IfcBeamTypeEnum, IfcBeamTypeEnum1>{}
	public partial class IfcBenchmarkEnum1 : ValueType<IfcBenchmarkEnum, IfcBenchmarkEnum1>{}
	public partial class IfcBoilerTypeEnum1 : ValueType<IfcBoilerTypeEnum, IfcBoilerTypeEnum1>{}
	public partial class IfcBooleanOperator1 : ValueType<IfcBooleanOperator, IfcBooleanOperator1>{}
	public partial class IfcBuildingElementProxyTypeEnum1 : ValueType<IfcBuildingElementProxyTypeEnum, IfcBuildingElementProxyTypeEnum1>{}
	public partial class IfcCableCarrierFittingTypeEnum1 : ValueType<IfcCableCarrierFittingTypeEnum, IfcCableCarrierFittingTypeEnum1>{}
	public partial class IfcCableCarrierSegmentTypeEnum1 : ValueType<IfcCableCarrierSegmentTypeEnum, IfcCableCarrierSegmentTypeEnum1>{}
	public partial class IfcCableSegmentTypeEnum1 : ValueType<IfcCableSegmentTypeEnum, IfcCableSegmentTypeEnum1>{}
	public partial class IfcChangeActionEnum1 : ValueType<IfcChangeActionEnum, IfcChangeActionEnum1>{}
	public partial class IfcChillerTypeEnum1 : ValueType<IfcChillerTypeEnum, IfcChillerTypeEnum1>{}
	public partial class IfcCoilTypeEnum1 : ValueType<IfcCoilTypeEnum, IfcCoilTypeEnum1>{}
	public partial class IfcColumnTypeEnum1 : ValueType<IfcColumnTypeEnum, IfcColumnTypeEnum1>{}
	public partial class IfcCompressorTypeEnum1 : ValueType<IfcCompressorTypeEnum, IfcCompressorTypeEnum1>{}
	public partial class IfcCondenserTypeEnum1 : ValueType<IfcCondenserTypeEnum, IfcCondenserTypeEnum1>{}
	public partial class IfcConnectionTypeEnum1 : ValueType<IfcConnectionTypeEnum, IfcConnectionTypeEnum1>{}
	public partial class IfcConstraintEnum1 : ValueType<IfcConstraintEnum, IfcConstraintEnum1>{}
	public partial class IfcControllerTypeEnum1 : ValueType<IfcControllerTypeEnum, IfcControllerTypeEnum1>{}
	public partial class IfcCooledBeamTypeEnum1 : ValueType<IfcCooledBeamTypeEnum, IfcCooledBeamTypeEnum1>{}
	public partial class IfcCoolingTowerTypeEnum1 : ValueType<IfcCoolingTowerTypeEnum, IfcCoolingTowerTypeEnum1>{}
	public partial class IfcCostScheduleTypeEnum1 : ValueType<IfcCostScheduleTypeEnum, IfcCostScheduleTypeEnum1>{}
	public partial class IfcCoveringTypeEnum1 : ValueType<IfcCoveringTypeEnum, IfcCoveringTypeEnum1>{}
	public partial class IfcCurrencyEnum1 : ValueType<IfcCurrencyEnum, IfcCurrencyEnum1>{}
	public partial class IfcCurtainWallTypeEnum1 : ValueType<IfcCurtainWallTypeEnum, IfcCurtainWallTypeEnum1>{}
	public partial class IfcDamperTypeEnum1 : ValueType<IfcDamperTypeEnum, IfcDamperTypeEnum1>{}
	public partial class IfcDataOriginEnum1 : ValueType<IfcDataOriginEnum, IfcDataOriginEnum1>{}
	public partial class IfcDerivedUnitEnum1 : ValueType<IfcDerivedUnitEnum, IfcDerivedUnitEnum1>{}
	public partial class IfcDimensionExtentUsage1 : ValueType<IfcDimensionExtentUsage, IfcDimensionExtentUsage1>{}
	public partial class IfcDirectionSenseEnum1 : ValueType<IfcDirectionSenseEnum, IfcDirectionSenseEnum1>{}
	public partial class IfcDistributionChamberElementTypeEnum1 : ValueType<IfcDistributionChamberElementTypeEnum, IfcDistributionChamberElementTypeEnum1>{}
	public partial class IfcDocumentConfidentialityEnum1 : ValueType<IfcDocumentConfidentialityEnum, IfcDocumentConfidentialityEnum1>{}
	public partial class IfcDocumentStatusEnum1 : ValueType<IfcDocumentStatusEnum, IfcDocumentStatusEnum1>{}
	public partial class IfcDoorPanelOperationEnum1 : ValueType<IfcDoorPanelOperationEnum, IfcDoorPanelOperationEnum1>{}
	public partial class IfcDoorPanelPositionEnum1 : ValueType<IfcDoorPanelPositionEnum, IfcDoorPanelPositionEnum1>{}
	public partial class IfcDoorStyleConstructionEnum1 : ValueType<IfcDoorStyleConstructionEnum, IfcDoorStyleConstructionEnum1>{}
	public partial class IfcDoorStyleOperationEnum1 : ValueType<IfcDoorStyleOperationEnum, IfcDoorStyleOperationEnum1>{}
	public partial class IfcDuctFittingTypeEnum1 : ValueType<IfcDuctFittingTypeEnum, IfcDuctFittingTypeEnum1>{}
	public partial class IfcDuctSegmentTypeEnum1 : ValueType<IfcDuctSegmentTypeEnum, IfcDuctSegmentTypeEnum1>{}
	public partial class IfcDuctSilencerTypeEnum1 : ValueType<IfcDuctSilencerTypeEnum, IfcDuctSilencerTypeEnum1>{}
	public partial class IfcElectricApplianceTypeEnum1 : ValueType<IfcElectricApplianceTypeEnum, IfcElectricApplianceTypeEnum1>{}
	public partial class IfcElectricCurrentEnum1 : ValueType<IfcElectricCurrentEnum, IfcElectricCurrentEnum1>{}
	public partial class IfcElectricDistributionPointFunctionEnum1 : ValueType<IfcElectricDistributionPointFunctionEnum, IfcElectricDistributionPointFunctionEnum1>{}
	public partial class IfcElectricFlowStorageDeviceTypeEnum1 : ValueType<IfcElectricFlowStorageDeviceTypeEnum, IfcElectricFlowStorageDeviceTypeEnum1>{}
	public partial class IfcElectricGeneratorTypeEnum1 : ValueType<IfcElectricGeneratorTypeEnum, IfcElectricGeneratorTypeEnum1>{}
	public partial class IfcElectricHeaterTypeEnum1 : ValueType<IfcElectricHeaterTypeEnum, IfcElectricHeaterTypeEnum1>{}
	public partial class IfcElectricMotorTypeEnum1 : ValueType<IfcElectricMotorTypeEnum, IfcElectricMotorTypeEnum1>{}
	public partial class IfcElectricTimeControlTypeEnum1 : ValueType<IfcElectricTimeControlTypeEnum, IfcElectricTimeControlTypeEnum1>{}
	public partial class IfcElementAssemblyTypeEnum1 : ValueType<IfcElementAssemblyTypeEnum, IfcElementAssemblyTypeEnum1>{}
	public partial class IfcElementCompositionEnum1 : ValueType<IfcElementCompositionEnum, IfcElementCompositionEnum1>{}
	public partial class IfcEnergySequenceEnum1 : ValueType<IfcEnergySequenceEnum, IfcEnergySequenceEnum1>{}
	public partial class IfcEnvironmentalImpactCategoryEnum1 : ValueType<IfcEnvironmentalImpactCategoryEnum, IfcEnvironmentalImpactCategoryEnum1>{}
	public partial class IfcEvaporativeCoolerTypeEnum1 : ValueType<IfcEvaporativeCoolerTypeEnum, IfcEvaporativeCoolerTypeEnum1>{}
	public partial class IfcEvaporatorTypeEnum1 : ValueType<IfcEvaporatorTypeEnum, IfcEvaporatorTypeEnum1>{}
	public partial class IfcFanTypeEnum1 : ValueType<IfcFanTypeEnum, IfcFanTypeEnum1>{}
	public partial class IfcFilterTypeEnum1 : ValueType<IfcFilterTypeEnum, IfcFilterTypeEnum1>{}
	public partial class IfcFireSuppressionTerminalTypeEnum1 : ValueType<IfcFireSuppressionTerminalTypeEnum, IfcFireSuppressionTerminalTypeEnum1>{}
	public partial class IfcFlowDirectionEnum1 : ValueType<IfcFlowDirectionEnum, IfcFlowDirectionEnum1>{}
	public partial class IfcFlowInstrumentTypeEnum1 : ValueType<IfcFlowInstrumentTypeEnum, IfcFlowInstrumentTypeEnum1>{}
	public partial class IfcFlowMeterTypeEnum1 : ValueType<IfcFlowMeterTypeEnum, IfcFlowMeterTypeEnum1>{}
	public partial class IfcFootingTypeEnum1 : ValueType<IfcFootingTypeEnum, IfcFootingTypeEnum1>{}
	public partial class IfcGasTerminalTypeEnum1 : ValueType<IfcGasTerminalTypeEnum, IfcGasTerminalTypeEnum1>{}
	public partial class IfcGeometricProjectionEnum1 : ValueType<IfcGeometricProjectionEnum, IfcGeometricProjectionEnum1>{}
	public partial class IfcGlobalOrLocalEnum1 : ValueType<IfcGlobalOrLocalEnum, IfcGlobalOrLocalEnum1>{}
	public partial class IfcHeatExchangerTypeEnum1 : ValueType<IfcHeatExchangerTypeEnum, IfcHeatExchangerTypeEnum1>{}
	public partial class IfcHumidifierTypeEnum1 : ValueType<IfcHumidifierTypeEnum, IfcHumidifierTypeEnum1>{}
	public partial class IfcInternalOrExternalEnum1 : ValueType<IfcInternalOrExternalEnum, IfcInternalOrExternalEnum1>{}
	public partial class IfcInventoryTypeEnum1 : ValueType<IfcInventoryTypeEnum, IfcInventoryTypeEnum1>{}
	public partial class IfcJunctionBoxTypeEnum1 : ValueType<IfcJunctionBoxTypeEnum, IfcJunctionBoxTypeEnum1>{}
	public partial class IfcLampTypeEnum1 : ValueType<IfcLampTypeEnum, IfcLampTypeEnum1>{}
	public partial class IfcLayerSetDirectionEnum1 : ValueType<IfcLayerSetDirectionEnum, IfcLayerSetDirectionEnum1>{}
	public partial class IfcLightDistributionCurveEnum1 : ValueType<IfcLightDistributionCurveEnum, IfcLightDistributionCurveEnum1>{}
	public partial class IfcLightEmissionSourceEnum1 : ValueType<IfcLightEmissionSourceEnum, IfcLightEmissionSourceEnum1>{}
	public partial class IfcLightFixtureTypeEnum1 : ValueType<IfcLightFixtureTypeEnum, IfcLightFixtureTypeEnum1>{}
	public partial class IfcLoadGroupTypeEnum1 : ValueType<IfcLoadGroupTypeEnum, IfcLoadGroupTypeEnum1>{}
	public partial class IfcLogicalOperatorEnum1 : ValueType<IfcLogicalOperatorEnum, IfcLogicalOperatorEnum1>{}
	public partial class IfcMemberTypeEnum1 : ValueType<IfcMemberTypeEnum, IfcMemberTypeEnum1>{}
	public partial class IfcMotorConnectionTypeEnum1 : ValueType<IfcMotorConnectionTypeEnum, IfcMotorConnectionTypeEnum1>{}
	public partial class IfcObjectTypeEnum1 : ValueType<IfcObjectTypeEnum, IfcObjectTypeEnum1>{}
	public partial class IfcObjectiveEnum1 : ValueType<IfcObjectiveEnum, IfcObjectiveEnum1>{}
	public partial class IfcOccupantTypeEnum1 : ValueType<IfcOccupantTypeEnum, IfcOccupantTypeEnum1>{}
	public partial class IfcOutletTypeEnum1 : ValueType<IfcOutletTypeEnum, IfcOutletTypeEnum1>{}
	public partial class IfcPermeableCoveringOperationEnum1 : ValueType<IfcPermeableCoveringOperationEnum, IfcPermeableCoveringOperationEnum1>{}
	public partial class IfcPhysicalOrVirtualEnum1 : ValueType<IfcPhysicalOrVirtualEnum, IfcPhysicalOrVirtualEnum1>{}
	public partial class IfcPileConstructionEnum1 : ValueType<IfcPileConstructionEnum, IfcPileConstructionEnum1>{}
	public partial class IfcPileTypeEnum1 : ValueType<IfcPileTypeEnum, IfcPileTypeEnum1>{}
	public partial class IfcPipeFittingTypeEnum1 : ValueType<IfcPipeFittingTypeEnum, IfcPipeFittingTypeEnum1>{}
	public partial class IfcPipeSegmentTypeEnum1 : ValueType<IfcPipeSegmentTypeEnum, IfcPipeSegmentTypeEnum1>{}
	public partial class IfcPlateTypeEnum1 : ValueType<IfcPlateTypeEnum, IfcPlateTypeEnum1>{}
	public partial class IfcProcedureTypeEnum1 : ValueType<IfcProcedureTypeEnum, IfcProcedureTypeEnum1>{}
	public partial class IfcProfileTypeEnum1 : ValueType<IfcProfileTypeEnum, IfcProfileTypeEnum1>{}
	public partial class IfcProjectOrderRecordTypeEnum1 : ValueType<IfcProjectOrderRecordTypeEnum, IfcProjectOrderRecordTypeEnum1>{}
	public partial class IfcProjectOrderTypeEnum1 : ValueType<IfcProjectOrderTypeEnum, IfcProjectOrderTypeEnum1>{}
	public partial class IfcProjectedOrTrueLengthEnum1 : ValueType<IfcProjectedOrTrueLengthEnum, IfcProjectedOrTrueLengthEnum1>{}
	public partial class IfcPropertySourceEnum1 : ValueType<IfcPropertySourceEnum, IfcPropertySourceEnum1>{}
	public partial class IfcProtectiveDeviceTypeEnum1 : ValueType<IfcProtectiveDeviceTypeEnum, IfcProtectiveDeviceTypeEnum1>{}
	public partial class IfcPumpTypeEnum1 : ValueType<IfcPumpTypeEnum, IfcPumpTypeEnum1>{}
	public partial class IfcRailingTypeEnum1 : ValueType<IfcRailingTypeEnum, IfcRailingTypeEnum1>{}
	public partial class IfcRampFlightTypeEnum1 : ValueType<IfcRampFlightTypeEnum, IfcRampFlightTypeEnum1>{}
	public partial class IfcRampTypeEnum1 : ValueType<IfcRampTypeEnum, IfcRampTypeEnum1>{}
	public partial class IfcReflectanceMethodEnum1 : ValueType<IfcReflectanceMethodEnum, IfcReflectanceMethodEnum1>{}
	public partial class IfcReinforcingBarRoleEnum1 : ValueType<IfcReinforcingBarRoleEnum, IfcReinforcingBarRoleEnum1>{}
	public partial class IfcReinforcingBarSurfaceEnum1 : ValueType<IfcReinforcingBarSurfaceEnum, IfcReinforcingBarSurfaceEnum1>{}
	public partial class IfcResourceConsumptionEnum1 : ValueType<IfcResourceConsumptionEnum, IfcResourceConsumptionEnum1>{}
	public partial class IfcRibPlateDirectionEnum1 : ValueType<IfcRibPlateDirectionEnum, IfcRibPlateDirectionEnum1>{}
	public partial class IfcRoleEnum1 : ValueType<IfcRoleEnum, IfcRoleEnum1>{}
	public partial class IfcRoofTypeEnum1 : ValueType<IfcRoofTypeEnum, IfcRoofTypeEnum1>{}
	public partial class IfcSIPrefix1 : ValueType<IfcSIPrefix, IfcSIPrefix1>{}
	public partial class IfcSIUnitName1 : ValueType<IfcSIUnitName, IfcSIUnitName1>{}
	public partial class IfcSanitaryTerminalTypeEnum1 : ValueType<IfcSanitaryTerminalTypeEnum, IfcSanitaryTerminalTypeEnum1>{}
	public partial class IfcSectionTypeEnum1 : ValueType<IfcSectionTypeEnum, IfcSectionTypeEnum1>{}
	public partial class IfcSensorTypeEnum1 : ValueType<IfcSensorTypeEnum, IfcSensorTypeEnum1>{}
	public partial class IfcSequenceEnum1 : ValueType<IfcSequenceEnum, IfcSequenceEnum1>{}
	public partial class IfcServiceLifeFactorTypeEnum1 : ValueType<IfcServiceLifeFactorTypeEnum, IfcServiceLifeFactorTypeEnum1>{}
	public partial class IfcServiceLifeTypeEnum1 : ValueType<IfcServiceLifeTypeEnum, IfcServiceLifeTypeEnum1>{}
	public partial class IfcSlabTypeEnum1 : ValueType<IfcSlabTypeEnum, IfcSlabTypeEnum1>{}
	public partial class IfcSoundScaleEnum1 : ValueType<IfcSoundScaleEnum, IfcSoundScaleEnum1>{}
	public partial class IfcSpaceHeaterTypeEnum1 : ValueType<IfcSpaceHeaterTypeEnum, IfcSpaceHeaterTypeEnum1>{}
	public partial class IfcSpaceTypeEnum1 : ValueType<IfcSpaceTypeEnum, IfcSpaceTypeEnum1>{}
	public partial class IfcStackTerminalTypeEnum1 : ValueType<IfcStackTerminalTypeEnum, IfcStackTerminalTypeEnum1>{}
	public partial class IfcStairFlightTypeEnum1 : ValueType<IfcStairFlightTypeEnum, IfcStairFlightTypeEnum1>{}
	public partial class IfcStairTypeEnum1 : ValueType<IfcStairTypeEnum, IfcStairTypeEnum1>{}
	public partial class IfcStateEnum1 : ValueType<IfcStateEnum, IfcStateEnum1>{}
	public partial class IfcStructuralCurveTypeEnum1 : ValueType<IfcStructuralCurveTypeEnum, IfcStructuralCurveTypeEnum1>{}
	public partial class IfcStructuralSurfaceTypeEnum1 : ValueType<IfcStructuralSurfaceTypeEnum, IfcStructuralSurfaceTypeEnum1>{}
	public partial class IfcSurfaceSide1 : ValueType<IfcSurfaceSide, IfcSurfaceSide1>{}
	public partial class IfcSurfaceTextureEnum1 : ValueType<IfcSurfaceTextureEnum, IfcSurfaceTextureEnum1>{}
	public partial class IfcSwitchingDeviceTypeEnum1 : ValueType<IfcSwitchingDeviceTypeEnum, IfcSwitchingDeviceTypeEnum1>{}
	public partial class IfcTankTypeEnum1 : ValueType<IfcTankTypeEnum, IfcTankTypeEnum1>{}
	public partial class IfcTendonTypeEnum1 : ValueType<IfcTendonTypeEnum, IfcTendonTypeEnum1>{}
	public partial class IfcTextPath1 : ValueType<IfcTextPath, IfcTextPath1>{}
	public partial class IfcThermalLoadSourceEnum1 : ValueType<IfcThermalLoadSourceEnum, IfcThermalLoadSourceEnum1>{}
	public partial class IfcThermalLoadTypeEnum1 : ValueType<IfcThermalLoadTypeEnum, IfcThermalLoadTypeEnum1>{}
	public partial class IfcTimeSeriesDataTypeEnum1 : ValueType<IfcTimeSeriesDataTypeEnum, IfcTimeSeriesDataTypeEnum1>{}
	public partial class IfcTimeSeriesScheduleTypeEnum1 : ValueType<IfcTimeSeriesScheduleTypeEnum, IfcTimeSeriesScheduleTypeEnum1>{}
	public partial class IfcTransformerTypeEnum1 : ValueType<IfcTransformerTypeEnum, IfcTransformerTypeEnum1>{}
	public partial class IfcTransitionCode1 : ValueType<IfcTransitionCode, IfcTransitionCode1>{}
	public partial class IfcTransportElementTypeEnum1 : ValueType<IfcTransportElementTypeEnum, IfcTransportElementTypeEnum1>{}
	public partial class IfcTrimmingPreference1 : ValueType<IfcTrimmingPreference, IfcTrimmingPreference1>{}
	public partial class IfcTubeBundleTypeEnum1 : ValueType<IfcTubeBundleTypeEnum, IfcTubeBundleTypeEnum1>{}
	public partial class IfcUnitEnum1 : ValueType<IfcUnitEnum, IfcUnitEnum1>{}
	public partial class IfcUnitaryEquipmentTypeEnum1 : ValueType<IfcUnitaryEquipmentTypeEnum, IfcUnitaryEquipmentTypeEnum1>{}
	public partial class IfcValveTypeEnum1 : ValueType<IfcValveTypeEnum, IfcValveTypeEnum1>{}
	public partial class IfcVibrationIsolatorTypeEnum1 : ValueType<IfcVibrationIsolatorTypeEnum, IfcVibrationIsolatorTypeEnum1>{}
	public partial class IfcWallTypeEnum1 : ValueType<IfcWallTypeEnum, IfcWallTypeEnum1>{}
	public partial class IfcWasteTerminalTypeEnum1 : ValueType<IfcWasteTerminalTypeEnum, IfcWasteTerminalTypeEnum1>{}
	public partial class IfcWindowPanelOperationEnum1 : ValueType<IfcWindowPanelOperationEnum, IfcWindowPanelOperationEnum1>{}
	public partial class IfcWindowPanelPositionEnum1 : ValueType<IfcWindowPanelPositionEnum, IfcWindowPanelPositionEnum1>{}
	public partial class IfcWindowStyleConstructionEnum1 : ValueType<IfcWindowStyleConstructionEnum, IfcWindowStyleConstructionEnum1>{}
	public partial class IfcWindowStyleOperationEnum1 : ValueType<IfcWindowStyleOperationEnum, IfcWindowStyleOperationEnum1>{}
	public partial class IfcWorkControlTypeEnum1 : ValueType<IfcWorkControlTypeEnum, IfcWorkControlTypeEnum1>{}
	public partial class IfcDaylightSavingHour1 : ValueType<Int64, IfcDaylightSavingHour1>{}
	public partial class IfcHourInDay1 : ValueType<Int64, IfcHourInDay1>{}
	public partial class IfcMinuteInHour1 : ValueType<Int64, IfcMinuteInHour1>{}
	public partial class IfcMonthInYearNumber1 : ValueType<Int64, IfcMonthInYearNumber1>{}
	public partial class IfcPresentableText1 : ValueType<String, IfcPresentableText1>{}
	public partial class IfcTextAlignment1 : ValueType<String, IfcTextAlignment1>{}
	public partial class IfcTextDecoration1 : ValueType<String, IfcTextDecoration1>{}
	public partial class IfcTextTransformation1 : ValueType<String, IfcTextTransformation1>{}
	public partial class IfcYearNumber1 : ValueType<Int64, IfcYearNumber1>{}
	public partial class IfcText1 : ValueType<String, IfcText1>{}
	public partial class IfcTimeStamp1 : ValueType<Int64, IfcTimeStamp1>{}
	public partial class IfcNullStyle1 : ValueType<IfcNullStyle, IfcNullStyle1>{}
	
	
	
	//ArrayWrappers below
	public partial class IfcMaterialLayerSetMaterialLayers : ArrayWrapper<IfcMaterialLayer, IfcMaterialLayerSetMaterialLayers>{}
	public partial class IfcTableRows : ArrayWrapper<IfcTableRow, IfcTableRows>{}
	public partial class IfcGeometricSetElements : ArrayWrapper<IfcGeometricRepresentationItem, IfcGeometricSetElements>{}
	
	public partial class IfcCompoundPlaneAngleMeasure : ValueTypeArrayWrapper<long, longwrapper, IfcCompoundPlaneAngleMeasure>{}
	public partial class IfcComplexNumber : ValueTypeArrayWrapper<double, doublewrapper, IfcComplexNumber>{}
	public partial class IfcStyledItemStyles : ArrayWrapper<IfcPresentationStyleAssignment, IfcStyledItemStyles>{}
	public partial class IfcPresentationStyleAssignmentStyles : ArrayWrapper<Object, IfcPresentationStyleAssignmentStyles>{}
	public partial class IfcCurveStyleFontPatternList : ArrayWrapper<IfcCurveStyleFontPattern, IfcCurveStyleFontPatternList>{}
	public partial class IfcFillAreaStyleFillStyles : ArrayWrapper<Entity, IfcFillAreaStyleFillStyles>{}
	public partial class IfcFillAreaStyleTilesTiles : ArrayWrapper<IfcFillAreaStyleTileSymbolWithStyle, IfcFillAreaStyleTilesTiles>{}
	public partial class IfcSurfaceStyleStyles : ArrayWrapper<Entity, IfcSurfaceStyleStyles>{}
	public partial class IfcSurfaceStyleWithTexturesTextures : ArrayWrapper<IfcSurfaceTexture, IfcSurfaceStyleWithTexturesTextures>{}
	public partial class IfcPixelTexturePixel : ArrayWrapper<hexBinarywrapper, IfcPixelTexturePixel>{}
	public partial class IfcTextStyleFontModelFontFamily : ArrayWrapper<IfcTextFontName1, IfcTextStyleFontModelFontFamily>{}
	public partial class IfcWorkControlCreators : ArrayWrapper<IfcPerson, IfcWorkControlCreators>{}
	public partial class IfcProjectRepresentationContexts : ArrayWrapper<IfcRepresentationContext, IfcProjectRepresentationContexts>{}
	public partial class IfcUnitAssignmentUnits : ArrayWrapper<Entity, IfcUnitAssignmentUnits>{}
	public partial class IfcTypeObjectHasPropertySets : ArrayWrapper<IfcPropertySetDefinition, IfcTypeObjectHasPropertySets>{}
	public partial class IfcShapeAspectShapeRepresentations : ArrayWrapper<IfcShapeModel, IfcShapeAspectShapeRepresentations>{}
	public partial class IfcElementQuantityQuantities : ArrayWrapper<IfcPhysicalQuantity, IfcElementQuantityQuantities>{}
	public partial class IfcPhysicalComplexQuantityHasQuantities : ArrayWrapper<IfcPhysicalComplexQuantity, IfcPhysicalComplexQuantityHasQuantities>{}
	public partial class IfcIrregularTimeSeriesValues : ArrayWrapper<IfcIrregularTimeSeriesValue, IfcIrregularTimeSeriesValues>{}
	public partial class IfcIrregularTimeSeriesValueListValues : ArrayWrapper<Object, IfcIrregularTimeSeriesValueListValues>{}
	public partial class IfcRegularTimeSeriesValues : ArrayWrapper<IfcTimeSeriesValue, IfcRegularTimeSeriesValues>{}
	public partial class IfcTimeSeriesValueListValues : ArrayWrapper<Object, IfcTimeSeriesValueListValues>{}
	public partial class IfcPropertySetHasProperties : ArrayWrapper<IfcProperty, IfcPropertySetHasProperties>{}
	public partial class IfcComplexPropertyHasProperties : ArrayWrapper<IfcComplexProperty, IfcComplexPropertyHasProperties>{}
	public partial class IfcReinforcementDefinitionPropertiesReinforcementSectionDefinitions : ArrayWrapper<IfcSectionReinforcementProperties, IfcReinforcementDefinitionPropertiesReinforcementSectionDefinitions>{}
	public partial class IfcSectionReinforcementPropertiesCrossSectionReinforcementDefinitions : ArrayWrapper<IfcReinforcementBarProperties, IfcSectionReinforcementPropertiesCrossSectionReinforcementDefinitions>{}
	public partial class IfcSoundPropertiesSoundValues : ArrayWrapper<IfcSoundValue, IfcSoundPropertiesSoundValues>{}
	public partial class IfcRelDecomposesRelatedObjects : ArrayWrapper<IfcObjectDefinition, IfcRelDecomposesRelatedObjects>{}
	public partial class IfcRelAssignsRelatedObjects : ArrayWrapper<IfcTypeObject, IfcRelAssignsRelatedObjects>{}
	public partial class IfcTypeProductRepresentationMaps : ArrayWrapper<IfcRepresentationMap, IfcTypeProductRepresentationMaps>{}
	public partial class IfcDraughtingCalloutContents : ArrayWrapper<IfcAnnotationOccurrence, IfcDraughtingCalloutContents>{}
	public partial class IfcAnnotationFillAreaInnerBoundaries : ArrayWrapper<IfcCurve, IfcAnnotationFillAreaInnerBoundaries>{}
	public partial class IfcFaceBasedSurfaceModelFbsmFaces : ArrayWrapper<IfcConnectedFaceSet, IfcFaceBasedSurfaceModelFbsmFaces>{}
	public partial class IfcSectionedSpineCrossSections : ArrayWrapper<IfcProfileDef, IfcSectionedSpineCrossSections>{}
	public partial class IfcSectionedSpineCrossSectionPositions : ArrayWrapper<IfcAxis2Placement3D, IfcSectionedSpineCrossSectionPositions>{}
	public partial class IfcShellBasedSurfaceModelSbsmBoundary : ArrayWrapper<IfcConnectedFaceSet, IfcShellBasedSurfaceModelSbsmBoundary>{}
	public partial class IfcTextureCoordinateGeneratorParameter : ArrayWrapper<Object, IfcTextureCoordinateGeneratorParameter>{}
	public partial class IfcTextureMapTextureMaps : ArrayWrapper<IfcVertexBasedTextureMap, IfcTextureMapTextureMaps>{}
	public partial class IfcVertexBasedTextureMapTextureVertices : ArrayWrapper<IfcTextureVertex, IfcVertexBasedTextureMapTextureVertices>{}
	public partial class IfcTextureVertexCoordinates : ArrayWrapper<IfcParameterValue1, IfcTextureVertexCoordinates>{}
	public partial class IfcVertexBasedTextureMapTexturePoints : ArrayWrapper<IfcCartesianPoint, IfcVertexBasedTextureMapTexturePoints>{}
	public partial class IfcAppliedValueRelationshipComponents : ArrayWrapper<IfcAppliedValue, IfcAppliedValueRelationshipComponents>{}
	public partial class IfcApprovalPropertyRelationshipApprovedProperties : ArrayWrapper<IfcComplexProperty, IfcApprovalPropertyRelationshipApprovedProperties>{}
	public partial class IfcArbitraryProfileDefWithVoidsInnerCurves : ArrayWrapper<IfcCurve, IfcArbitraryProfileDefWithVoidsInnerCurves>{}
	public partial class IfcClassificationItemRelationshipRelatedItems : ArrayWrapper<IfcClassificationItem, IfcClassificationItemRelationshipRelatedItems>{}
	public partial class IfcClassificationNotationNotationFacets : ArrayWrapper<IfcClassificationNotationFacet, IfcClassificationNotationNotationFacets>{}
	public partial class IfcTableRowRowCells : ArrayWrapper<Object, IfcTableRowRowCells>{}
	public partial class IfcConstraintAggregationRelationshipRelatedConstraints : ArrayWrapper<IfcConstraint, IfcConstraintAggregationRelationshipRelatedConstraints>{}
	public partial class IfcConstraintClassificationRelationshipRelatedClassifications : ArrayWrapper<Entity, IfcConstraintClassificationRelationshipRelatedClassifications>{}
	public partial class IfcConstraintRelationshipRelatedConstraints : ArrayWrapper<IfcConstraint, IfcConstraintRelationshipRelatedConstraints>{}
	public partial class IfcConstructionMaterialResourceSuppliers : ArrayWrapper<Entity, IfcConstructionMaterialResourceSuppliers>{}
	public partial class IfcCostScheduleTargetUsers : ArrayWrapper<Entity, IfcCostScheduleTargetUsers>{}
	public partial class IfcLibraryInformationLibraryReference : ArrayWrapper<IfcLibraryReference, IfcLibraryInformationLibraryReference>{}
	public partial class IfcCurveBoundedPlaneInnerBoundaries : ArrayWrapper<IfcCurve, IfcCurveBoundedPlaneInnerBoundaries>{}
	public partial class IfcDocumentInformationDocumentReferences : ArrayWrapper<IfcDocumentReference, IfcDocumentInformationDocumentReferences>{}
	public partial class IfcDocumentInformationEditors : ArrayWrapper<Entity, IfcDocumentInformationEditors>{}
	public partial class IfcDocumentInformationRelationshipRelatedDocuments : ArrayWrapper<IfcDocumentInformation, IfcDocumentInformationRelationshipRelatedDocuments>{}
	public partial class IfcExtendedMaterialPropertiesExtendedProperties : ArrayWrapper<IfcComplexProperty, IfcExtendedMaterialPropertiesExtendedProperties>{}
	public partial class IfcFacetedBrepWithVoidsVoids : ArrayWrapper<IfcClosedShell, IfcFacetedBrepWithVoidsVoids>{}
	public partial class IfcGridUAxes : ArrayWrapper<IfcGridAxis, IfcGridUAxes>{}
	public partial class IfcGridVAxes : ArrayWrapper<IfcGridAxis, IfcGridVAxes>{}
	public partial class IfcGridWAxes : ArrayWrapper<IfcGridAxis, IfcGridWAxes>{}
	public partial class IfcInventoryResponsiblePersons : ArrayWrapper<IfcPerson, IfcInventoryResponsiblePersons>{}
	public partial class IfcLightDistributionDataSecondaryPlaneAngle : ArrayWrapper<IfcPlaneAngleMeasure1, IfcLightDistributionDataSecondaryPlaneAngle>{}
	public partial class IfcLightDistributionDataLuminousIntensity : ArrayWrapper<IfcLuminousIntensityDistributionMeasure1, IfcLightDistributionDataLuminousIntensity>{}
	public partial class IfcLightIntensityDistributionDistributionData : ArrayWrapper<IfcLightDistributionData, IfcLightIntensityDistributionDistributionData>{}
	public partial class IfcMaterialClassificationRelationshipMaterialClassifications : ArrayWrapper<Entity, IfcMaterialClassificationRelationshipMaterialClassifications>{}
	public partial class IfcMaterialListMaterials : ArrayWrapper<IfcMaterial, IfcMaterialListMaterials>{}
	public partial class IfcMechanicalSteelMaterialPropertiesRelaxations : ArrayWrapper<IfcRelaxation, IfcMechanicalSteelMaterialPropertiesRelaxations>{}
	public partial class IfcMovePunchList : ArrayWrapper<IfcText1, IfcMovePunchList>{}
	public partial class IfcOrganizationRelationshipRelatedOrganizations : ArrayWrapper<IfcOrganization, IfcOrganizationRelationshipRelatedOrganizations>{}
	public partial class IfcPathEdgeList : ArrayWrapper<IfcOrientedEdge, IfcPathEdgeList>{}
	public partial class IfcPresentationLayerAssignmentAssignedItems : ArrayWrapper<Entity, IfcPresentationLayerAssignmentAssignedItems>{}
	public partial class IfcPresentationLayerWithStyleLayerStyles : ArrayWrapper<Object, IfcPresentationLayerWithStyleLayerStyles>{}
	public partial class IfcProjectOrderRecordRecords : ArrayWrapper<IfcRelAssignsToProjectOrder, IfcProjectOrderRecordRecords>{}
	public partial class IfcTimeSeriesScheduleApplicableDates : ArrayWrapper<Entity, IfcTimeSeriesScheduleApplicableDates>{}
	public partial class IfcPropertyConstraintRelationshipRelatedProperties : ArrayWrapper<IfcComplexProperty, IfcPropertyConstraintRelationshipRelatedProperties>{}
	public partial class IfcPropertyEnumeratedValueEnumerationValues : ArrayWrapper<Object, IfcPropertyEnumeratedValueEnumerationValues>{}
	public partial class IfcPropertyEnumerationEnumerationValues : ArrayWrapper<Object, IfcPropertyEnumerationEnumerationValues>{}
	public partial class IfcPropertyListValueListValues : ArrayWrapper<Object, IfcPropertyListValueListValues>{}
	public partial class IfcPropertyTableValueDefiningValues : ArrayWrapper<Object, IfcPropertyTableValueDefiningValues>{}
	public partial class IfcPropertyTableValueDefinedValues : ArrayWrapper<Object, IfcPropertyTableValueDefinedValues>{}
	public partial class IfcRationalBezierCurveWeightsData : ValueTypeArrayWrapper<double, doublewrapper, IfcRationalBezierCurveWeightsData>{}
	public partial class IfcReferencesValueDocumentReferencingValues : ArrayWrapper<IfcAppliedValue, IfcReferencesValueDocumentReferencingValues>{}
	public partial class IfcRelAssociatesRelatedObjects : ArrayWrapper<IfcRoot, IfcRelAssociatesRelatedObjects>{}
	public partial class IfcRelConnectsPathElementsRelatingPriorities : ArrayWrapper<longwrapper, IfcRelConnectsPathElementsRelatingPriorities>{}
	public partial class IfcRelConnectsPathElementsRelatedPriorities : ArrayWrapper<longwrapper, IfcRelConnectsPathElementsRelatedPriorities>{}
	public partial class IfcRelConnectsWithRealizingElementsRealizingElements : ArrayWrapper<IfcElement, IfcRelConnectsWithRealizingElementsRealizingElements>{}
	public partial class IfcRelContainedInSpatialStructureRelatedElements : ArrayWrapper<IfcProduct, IfcRelContainedInSpatialStructureRelatedElements>{}
	public partial class IfcRelCoversBldgElementsRelatedCoverings : ArrayWrapper<IfcCovering, IfcRelCoversBldgElementsRelatedCoverings>{}
	public partial class IfcRelCoversSpacesRelatedCoverings : ArrayWrapper<IfcCovering, IfcRelCoversSpacesRelatedCoverings>{}
	public partial class IfcRelFlowControlElementsRelatedControlElements : ArrayWrapper<IfcDistributionControlElement, IfcRelFlowControlElementsRelatedControlElements>{}
	public partial class IfcRelOverridesPropertiesOverridingProperties : ArrayWrapper<IfcComplexProperty, IfcRelOverridesPropertiesOverridingProperties>{}
	public partial class IfcRelReferencedInSpatialStructureRelatedElements : ArrayWrapper<IfcProduct, IfcRelReferencedInSpatialStructureRelatedElements>{}
	public partial class IfcStructuralAnalysisModelLoadedBy : ArrayWrapper<IfcStructuralLoadGroup, IfcStructuralAnalysisModelLoadedBy>{}
	public partial class IfcStructuralAnalysisModelHasResults : ArrayWrapper<IfcStructuralResultGroup, IfcStructuralAnalysisModelHasResults>{}
	public partial class IfcRelServicesBuildingsRelatedBuildings : ArrayWrapper<IfcSpatialStructureElement, IfcRelServicesBuildingsRelatedBuildings>{}
	public partial class IfcStructuralLinearActionVaryingSubsequentAppliedLoads : ArrayWrapper<IfcStructuralLoad, IfcStructuralLinearActionVaryingSubsequentAppliedLoads>{}
	public partial class IfcStructuralPlanarActionVaryingSubsequentAppliedLoads : ArrayWrapper<IfcStructuralLoad, IfcStructuralPlanarActionVaryingSubsequentAppliedLoads>{}
	public partial class IfcStructuralSurfaceMemberVaryingSubsequentThickness : ArrayWrapper<IfcPositiveLengthMeasure1, IfcStructuralSurfaceMemberVaryingSubsequentThickness>{}
	public partial class IfcTimeSeriesReferenceRelationshipTimeSeriesReferences : ArrayWrapper<Entity, IfcTimeSeriesReferenceRelationshipTimeSeriesReferences>{}

}