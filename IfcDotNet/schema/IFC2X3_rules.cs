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

#pragma warning disable 1591
namespace IfcDotNet.Schema
{
    
    public partial class IfcActorRole : IHasRules{
        [Rule("WR1", typeof(IfcActorRole), "If the attribute Role has the enumeration value USERDEFINED then a value for the attribute UserDefinedRole shall be asserted.")]
        public bool WR1(){
            //(Role <> IfcRoleEnum.USERDEFINED) OR ((Role = IfcRoleEnum.USERDEFINED) AND EXISTS(SELF.UserDefinedRole))
            return this.Role != IfcRoleEnum.userdefined ||
                (this.Role == IfcRoleEnum.userdefined && !String.IsNullOrEmpty(this.UserDefinedRole));
        }
    }
    
    /*//FIXME New entity in IFC2x4
    public partial class IfcActuator : IHasRules{
        //TODO
    }
    */
        public partial class IfcActuatorType : IHasRules{
        [Rule("CorrectPredefinedType", typeof(IfcActuatorType), "")]
        public bool CorrectPredefinedType(){
            //(PredefinedType <> IfcActuatorTypeEnum.USERDEFINED) OR ((PredefinedType = IfcActuatorTypeEnum.USERDEFINED) AND EXISTS(SELF\IfcElementType.ElementType))
            return this.PredefinedType != IfcActuatorTypeEnum.userdefined ||
                (this.PredefinedType == IfcActuatorTypeEnum.userdefined && !String.IsNullOrEmpty(this.ElementType));
        }
    }
    
    public partial class IfcAddress : IHasRules{
        [Rule("WR1",typeof(IfcAddress), "Either attribute value Purpose is not given, or when attribute Purpose has enumeration value USERDEFINED then attribute UserDefinedPurpose shall also have a value.")]
        public bool WR1(){
            //WR1	 : (NOT(EXISTS(Purpose))) OR ((Purpose <> IfcAddressTypeEnum.USERDEFINED) OR ((Purpose = IfcAddressTypeEnum.USERDEFINED) AND EXISTS(SELF.UserDefinedPurpose)))
            return !this.Purpose.HasValue ||
                this.Purpose.Value != IfcAddressTypeEnum.userdefined ||
                (this.Purpose.Value == IfcAddressTypeEnum.userdefined && !String.IsNullOrEmpty(this.UserDefinedPurpose));
        }
    }
    /*
     * //FIXME new in IFC2X4
	public partial class IfcAdvancedBrep : IHasRules{
		//TODO HasAdvancedFaces	 : SIZEOF(QUERY(Afs <* SELF\IfcManifoldSolidBrep.Outer.CfsFaces | (NOT ( IFCTOPOLOGYRESOURCE.IFCADVANCEDFACE IN TYPEOF(Afs))) )) = 0
	}
	public partial class IfcAdvancedBrepWithVoids : IHasRules{
		//TODO WR1	 : SIZEOF (QUERY (Vsh <* Voids | SIZEOF (QUERY (Afs <* Vsh.CfsFaces | (NOT ( IFCTOPOLOGYRESOURCE.IFCADVANCEDFACE IN TYPEOF(Afs))) )) = 0 )) = 0
	}
	public partial class IfcAdvancedFace : IHasRules{
		//TODO ApplicableSurface	 : SIZEOF ( [ IFCGEOMETRYRESOURCE.IFCELEMENTARYSURFACE , IFCGEOMETRYRESOURCE.IFCSWEPTSURFACE , IFCGEOMETRYRESOURCE.IFCBSPLINESURFACE ] * TYPEOF(SELF\IfcFaceSurface.FaceSurface)) = 1
        //TODO RequiresEdgeCurve	 : SIZEOF(QUERY (ElpFbnds <* QUERY (Bnds <* SELF\IfcFace.Bounds | IFCTOPOLOGYRESOURCE.IFCEDGELOOP IN TYPEOF(Bnds.Bound)) | NOT (SIZEOF (QUERY (Oe <* ElpFbnds.Bound\IfcEdgeLoop.EdgeList | NOT( IFCTOPOLOGYRESOURCE.IFCEDGECURVE IN TYPEOF(Oe\IfcOrientedEdge.EdgeElement) ))) = 0 ))) = 0
        //TODO ApplicableEdgeCurves	 : SIZEOF(QUERY (ElpFbnds <* QUERY (Bnds <* SELF\IfcFace.Bounds | IFCTOPOLOGYRESOURCE.IFCEDGELOOP IN TYPEOF(Bnds.Bound)) | NOT (SIZEOF (QUERY (Oe <* ElpFbnds.Bound\IfcEdgeLoop.EdgeList | NOT (SIZEOF ([ IFCGEOMETRYRESOURCE.IFCLINE , IFCGEOMETRYRESOURCE.IFCCONIC , IFCGEOMETRYRESOURCE.IFCPOLYLINE , IFCGEOMETRYRESOURCE.IFCBSPLINECURVE ] * TYPEOF(Oe\IfcOrientedEdge.EdgeElement\IfcEdgeCurve.EdgeGeometry)) = 1 ) )) = 0 ))) = 0
	}
	public partial class IfcAirTerminal : IHasRules{
        [Rule("CorrectPredefinedType", typeof(IfcAirTerminal), "Either the PredefinedType attribute is unset (e.g. because an IfcAirTerminalType is associated), or the inherited attribute ObjectType shall be provided, if the PredefinedType is set to USERDEFINED.")]
        public bool CorrectPredefinedType(){
            //TODO CorrectPredefinedType	 : NOT(EXISTS(PredefinedType)) OR (PredefinedType <> IfcAirTerminalTypeEnum.USERDEFINED) OR ((PredefinedType = IfcAirTerminalTypeEnum.USERDEFINED) AND EXISTS (SELF\IfcObject.ObjectType))
        }
		//TODO CorrectTypeAssigned	 : NOT(EXISTS(IsTypedBy)) OR ( IFCHVACDOMAIN.IFCAIRTERMINALTYPE IN TYPEOF(IsTypedBy[1].RelatingType))
	}
	public partial class IfcAirTerminalBox : IHasRules{
		//TODO CorrectPredefinedType	 : NOT(EXISTS(PredefinedType)) OR (PredefinedType <> IfcAirTerminalBoxTypeEnum.USERDEFINED) OR ((PredefinedType = IfcAirTerminalBoxTypeEnum.USERDEFINED) AND EXISTS (SELF\IfcObject.ObjectType))
        //TODO CorrectTypeAssigned	 : NOT(EXISTS(IsTypedBy)) OR ( IFCHVACDOMAIN.IFCAIRTERMINALBOXTYPE IN TYPEOF(IsTypedBy[1].RelatingType))
	}*/
    public partial class IfcAirTerminalBoxType : IHasRules{
        [Rule("CorrectPredefinedType", typeof(IfcAirTerminalBoxType), "")]
        public bool CorrectPredefinedType(){
            return this.PredefinedType != IfcAirTerminalBoxTypeEnum.userdefined ||
                (this.PredefinedType == IfcAirTerminalBoxTypeEnum.userdefined && !String.IsNullOrEmpty(this.ElementType));
        }
    }
    public partial class IfcAirTerminalType : IHasRules{
        [Rule("CorrectPredefinedType", typeof(IfcAirTerminalType), "")]
        public bool CorrectPredefinedType(){
            return this.PredefinedType != IfcAirTerminalTypeEnum.userdefined ||
                (this.PredefinedType == IfcAirTerminalTypeEnum.userdefined && !string.IsNullOrEmpty(this.ElementType));
        }
    }
    /* //FIXME new in IFC2X4
	public partial class IfcAirToAirHeatRecovery : IHasRules{
		//TODO CorrectPredefinedType	 : NOT(EXISTS(PredefinedType)) OR (PredefinedType <> IfcAirToAirHeatRecoveryTypeEnum.USERDEFINED) OR ((PredefinedType = IfcAirToAirHeatRecoveryTypeEnum.USERDEFINED) AND EXISTS (SELF\IfcObject.ObjectType))
        //TODO CorrectTypeAssigned	 : NOT(EXISTS(IsTypedBy)) OR ( IFCHVACDOMAIN.IFCAIRTOAIRHEATRECOVERYTYPE IN TYPEOF(IsTypedBy[1].RelatingType))
	}
     */
    public partial class IfcAirToAirHeatRecoveryType : IHasRules{
        [Rule("CorrectPredefinedType", typeof(IfcAirToAirHeatRecoveryType), "")]
        public bool CorrectPredefinedType(){
            return this.PredefinedType != IfcAirToAirHeatRecoveryTypeEnum.userdefined ||
                (this.PredefinedType == IfcAirToAirHeatRecoveryTypeEnum.userdefined && !string.IsNullOrEmpty(this.ElementType));
        }
    }
    /* //FIXME new in IFC2X4
	public partial class IfcAlarm : IHasRules{
		//TODO CorrectPredefinedType	 : NOT(EXISTS(PredefinedType)) OR (PredefinedType <> IfcAlarmTypeEnum.USERDEFINED) OR ((PredefinedType = IfcAlarmTypeEnum.USERDEFINED) AND EXISTS (SELF\IfcObject.ObjectType))
        //TODO CorrectTypeAssigned	 : NOT(EXISTS(IsTypedBy)) OR ( IFCBUILDINGCONTROLSDOMAIN.IFCALARMTYPE IN TYPEOF(IsTypedBy[1].RelatingType))
	}
     */
    public partial class IfcAlarmType : IHasRules{
        [Rule("CorrectPredefinedType", typeof(IfcAlarmType), "")]
        public bool CorrectPredefinedType(){
            return this.PredefinedType != IfcAlarmTypeEnum.userdefined ||
                (this.PredefinedType == IfcAlarmTypeEnum.userdefined && !string.IsNullOrEmpty(this.ElementType) );
        }
    }
    public partial class IfcAppliedValue : IHasRules{
        [Rule("WR1", typeof(IfcAppliedValue), "")]
        public bool WR1(){
            return this.AppliedValue != null; //FIXME || this.ValueOfComponents != null; //inverse properties are not implemented
            //WR1	 : EXISTS (AppliedValue) OR EXISTS (ValueOfComponents)
        }
    }
    public partial class IfcAppliedValueRelationship : IHasRules{
        [Rule("ValidBinaryOperator", typeof(IfcAppliedValueRelationship), "Either there are two Components in the relationship (in which case any arithmetic operator may be used) or the operator is not subtraction nor division. If the operator is subtraction or division, there must be exactly two components.")]
        public bool ValidBinaryOperator(){
            return this.Components.Items.Length == 2 ||
                (this.ArithmeticOperator != IfcArithmeticOperatorEnum.subtract && this.ArithmeticOperator != IfcArithmeticOperatorEnum.divide);
        }
    }
    public partial class IfcApproval : IHasRules{
        [Rule("HasIdentifierOrName", typeof(IfcApproval), "Either Identifier or Name (or both) by which the approval is known shall be given.")]
        public bool HasIdentifierOrName(){
            return !string.IsNullOrEmpty(this.Identifier) || !string.IsNullOrEmpty(this.Name);
        }
    }
    public partial class IfcArbitraryClosedProfileDef : IHasRules{
        //TODO 	WR1	 : OuterCurve.Dim = 2
        [Rule("WR1", typeof(IfcArbitraryClosedProfileDef), "The curve used for the outer curve definition shall have the dimensionality of 2.")]
        public bool WR1(){
            return this.OuterCurve.Item.Dim == 2;
        }
        [Rule("WR2", typeof(IfcArbitraryClosedProfileDef), "The outer curve shall not be of type IfcLine as IfcLine is not a closed curve.")]
        public bool WR2(){
            return !(this.OuterCurve.Item is IfcLine);
        }
        [Rule("WR3", typeof(IfcArbitraryClosedProfileDef), "The outer curve shall not be of type IfcOffsetCurve2D as it should not be defined as an offset of another curve.")]
        public bool WR3(){
            return !(this.OuterCurve.Item is IfcOffsetCurve2D);
        }
    }
    public partial class IfcArbitraryOpenProfileDef : IHasRules{
        [Rule("WR11", typeof(IfcArbitraryOpenProfileDef), "The profile type is a .CURVE., an open profile can only be used to define a swept surface.")]
        public bool WR11(){
            return this is IfcCenterLineProfileDef ||
                ProfileType == IfcProfileTypeEnum.curve;
        }
        [Rule("WR12", typeof(IfcArbitraryOpenProfileDef), "The dimensionality of the curve shall be 2.")]
        public bool WR12(){
            return this.Curve.Item.Dim == 2;
        }
    }
    public partial class IfcArbitraryProfileDefWithVoids : IHasRules{
        [Rule("WR1", typeof(IfcArbitraryProfileDefWithVoids), "The type of the profile shall be AREA, as it can only be involved in the definition of a swept area.")]
        public bool WR11(){
            return this.ProfileType == IfcProfileTypeEnum.area;
        }
        [Rule("WR2", typeof(IfcArbitraryProfileDefWithVoids), "All inner curves shall have the dimensionality of 2.")]
        public bool WR12(){
            foreach(IfcCurve curve in this.InnerCurves.Items){
                if(curve.Dim != 2)
                    return false;
            }
            return true;
        }
        [Rule("WR3", typeof(IfcArbitraryProfileDefWithVoids), "None of the inner curves shall by of type IfcLine, as an IfcLine can not be a closed curve.")]
        public bool WR13(){
            foreach(IfcCurve curve in this.InnerCurves.Items){
                if(curve is IfcLine)
                    return false;
            }
            return true;
        }
    }
    /* //FIXME new in IFC2X4
    public partial class IfcAudioVisualAppliance : IHasRules{
        //TODO CorrectPredefinedType	 : NOT(EXISTS(PredefinedType)) OR (PredefinedType <> IfcAudioVisualApplianceTypeEnum.USERDEFINED) OR ((PredefinedType = IfcAudioVisualApplianceTypeEnum.USERDEFINED) AND EXISTS (SELF\IfcObject.ObjectType))
        //TODO CorrectTypeAssigned	 : NOT(EXISTS(IsTypedBy)) OR ( IFCELECTRICALDOMAIN.IFCAUDIOVISUALAPPLIANCETYPE IN TYPEOF(IsTypedBy[1].RelatingType))
    }
    public partial class IfcAudioVisualApplianceType : IHasRules{
        //TODO CorrectPredefinedType	 : (PredefinedType <> IfcAudioVisualApplianceTypeEnum.USERDEFINED) OR ((PredefinedType = IfcAudioVisualApplianceTypeEnum.USERDEFINED) AND EXISTS(SELF\IfcElementType.ElementType))
    }*/
    public partial class IfcAxis1Placement : IHasRules{
        [Rule("WR1", typeof(IfcAxis1Placement), "The Axis when given should only reference a three-dimensional IfcDirection.")]
        public bool WR1(){
            return this.Axis == null || this.Axis.Item == null || this.Axis.Item.Dim == 3;
        }
        
        [Rule("WR2", typeof(IfcAxis1Placement), "The Cartesian point defining the Location shall have the dimensionality of 3.")]
        public bool WR2(){
            return this.Location.Item.Dim == 3;
        }
    }
    public partial class IfcAxis2Placement2D : IHasRules{
        [Rule("WR1", typeof(IfcAxis2Placement2D), "")]
        public bool WR1(){
            return this.RefDirection == null || this.RefDirection.Item == null || this.RefDirection.Item.Dim == 2;
        }
        [Rule("WR2", typeof(IfcAxis2Placement2D), "")]
        public bool WR2(){
            return this.Location.Item.Dim == 2;
        }
    }
    public partial class IfcAxis2Placement3D : IHasRules{
        [Rule("WR1", typeof(IfcAxis2Placement3D), "The dimensionality of the placement location shall be 3.")]
        public bool WR1(){
            return this.Location.Item.Dim == 3;
        }
        [Rule("WR2", typeof(IfcAxis2Placement3D), "The Axis when given should only reference a three-dimensional IfcDirection.")]
        public bool WR2(){
            return this.Axis == null || this.Axis.Item == null || this.Axis.Item.Dim == 3;
        }
        [Rule("WR3", typeof(IfcAxis2Placement3D), "The RefDirection when given should only reference a three-dimensional IfcDirection.")]
        public bool WR3(){
            return this.RefDirection == null || this.RefDirection.Item == null || this.RefDirection.Item.Dim == 3;
        }
        [Rule("WR4", typeof(IfcAxis2Placement3D), "The Axis and RefDirection shall not be parallel or anti-parallel.")]
        public bool WR4(){
            return this.Axis == null || this.Axis.Item == null ||
                this.RefDirection == null || this.RefDirection.Item == null ||
                Functions.IfcCrossProduct(this.Axis.Item, this.RefDirection.Item).Magnitude > 0;
        }
        [Rule("WR5", typeof(IfcAxis2Placement3D), "Either both (Axis and RefDirection) are not given and therefore defaulted, or both shall be given. This is a further constraint in IFC Release 1.5.")]
        public bool WR5(){
            if(this.Axis == null || this.Axis.Item == null){
                if(this.RefDirection == null || this.RefDirection.Item == null){
                    return false;
                }else{
                    return true;
                }                
            }else{
                if(this.RefDirection == null || this.RefDirection.Item == null){
                    return true;
                }else{
                    return false;
                }  
            }
        }
    }
    public partial class IfcBeam : IHasRules{
        /* //FIXME refers to a property added in IFC2X4
        [Rule("CorrectPredefinedType", typeof(IfcBeam), "Either the PredefinedType attribute is unset (e.g. because an IfcBeamType is associated), or the inherited attribute ObjectType shall be provided, if the PredefinedType is set to USERDEFINED.")]
        public bool CorrectPredefinedType(){
            return this.PredefinedType == null ||
                this.PredefinedType != IfcBeamTypeEnum.userdefined ||
                (this.PredefinedType == IfcBeamTypeEnum.userdefined && !string.IsNullOrEmpty(this.ObjectType) );
        }
        
       [Rule("CorrectTypeAssigned", typeof(IfcBeam), "Either there is no beam type object associated, i.e. the IsTypedBy inverse relationship is not provided, or the associated type object has to be of type IfcBeamType.")]
       public bool CorrectTypeAssigned(){
           return this.IsTypedBy == null || this.IsTypedBy[0].RelatingType is IfcBeamType;
       }
       */
    }
    public partial class IfcBeamStandardCase : IHasRules{
        /* //FIXME impossible to implement in the constraints of the schema - may have to make a global rule
        [Rule("HasMaterialProfileSetUsage", typeof(IfcBeamStandardCase), "A valid instance of IfcBeamStandardCase relies on the provision of an IfcMaterialProfileSetUsage.")]
        public bool HasMaterialProfileSetUsage(){
            
        }
        //TODO SIZEOF (QUERY(temp <* USEDIN(SELF, IFCKERNEL.IFCRELASSOCIATES.RELATEDOBJECTS ) | ( IFCPRODUCTEXTENSION.IFCRELASSOCIATESMATERIAL IN TYPEOF(temp)) AND ( IFCMATERIALRESOURCE.IFCMATERIALPROFILESETUSAGE IN TYPEOF(temp.RelatingMaterial)) )) = 1
        */
    }
    public partial class IfcBeamType : IHasRules{
        //TODO
    }
    public partial class IfcBlobTexture : IHasRules{
        //TODO
    }
    public partial class IfcBoiler : IHasRules{
        //TODO
    }
    public partial class IfcBoilerType : IHasRules{
        //TODO
    }
    public partial class IfcBooleanClippingResult : IHasRules{
        //TODO
    }
    public partial class IfcBooleanResult : IHasRules{
        //TODO
        //TODO additional rules regarding properties FirstOperand and SecondOperand, as only select types of element are allowed as per the IfcBooleanOperand SELECT
    }
    public partial class IfcBoundaryCurve : IHasRules{
        //TODO
    }
    public partial class IfcBoxAlignment : IHasRules{
        //TODO
    }
    public partial class IfcBoxedHalfSpace : IHasRules{
        //TODO
    }
    public partial class IfcBSplineCurve : IHasRules{
        //TODO
    }
    public partial class IfcBSplineCurveWithKnots : IHasRules{
        //TODO
    }
    public partial class IfcBSplineSurfaceWithKnots : IHasRules{
        //TODO
    }
    public partial class IfcBuildingElement : IHasRules{
        //TODO
    }
    public partial class IfcBuildingElementPart : IHasRules{
        //TODO
    }
    public partial class IfcBuildingElementPartType : IHasRules{
        //TODO
    }
    public partial class IfcBuildingElementProxy : IHasRules{
        //TODO
    }
    public partial class IfcBurner : IHasRules{
        //TODO
    }
    public partial class IfcBurnerType : IHasRules{
        //TODO
    }
    public partial class IfcCableCarrierFitting : IHasRules{
        //TODO
    }
    public partial class IfcCableCarrierFittingType : IHasRules{
        //TODO
    }
    public partial class IfcCableCarrierSegment : IHasRules{
        //TODO
    }
    public partial class IfcCableCarrierSegmentType : IHasRules{
        //TODO
    }
    public partial class IfcCableFitting : IHasRules{
        //TODO
    }
    public partial class IfcCableFittingType : IHasRules{
        //TODO
    }
    public partial class IfcCableSegment : IHasRules{
        //TODO
    }
    public partial class IfcCableSegmentType : IHasRules{
        //TODO
    }
    public partial class IfcCardinalPointReference : IHasRules{
        //TODO
    }
    public partial class IfcCartesianPoint : IHasRules{
        //TODO
    }
    public partial class IfcCartesianTransformationOperator : IHasRules{
        //TODO
    }
    public partial class IfcCartesianTransformationOperator2D : IHasRules{
        //TODO
    }
    public partial class IfcCartesianTransformationOperator2DnonUniform : IHasRules{
        //TODO
    }
    public partial class IfcCartesianTransformationOperator3D : IHasRules{
        //TODO
    }
    public partial class IfcCartesianTransformationOperator3DnonUniform : IHasRules{
        //TODO
    }
    public partial class IfcChiller : IHasRules{
        //TODO
    }
    public partial class IfcChillerType : IHasRules{
        //TODO
    }
    public partial class IfcChimney : IHasRules{
        //TODO
    }
    public partial class IfcChimneyType : IHasRules{
        //TODO
    }
    public partial class IfcCircleHollowProfileDef : IHasRules{
        //TODO
    }
    public partial class IfcCoil : IHasRules{
        //TODO
    }
    public partial class IfcCoilType : IHasRules{
        //TODO
    }
    public partial class IfcColumn : IHasRules{
        //TODO
    }
    public partial class IfcColumnStandardCase : IHasRules{
        //TODO
    }
    public partial class IfcColumnType : IHasRules{
        //TODO
    }
    public partial class IfcCommunicationsAppliance : IHasRules{
        //TODO
    }
    public partial class IfcCommunicationsApplianceType : IHasRules{
        //TODO
    }
    public partial class IfcComplexProperty : IHasRules{
        //TODO
    }
    public partial class IfcComplexPropertyTemplate : IHasRules{
        //TODO
    }
    public partial class IfcCompositeCurve : IHasRules{
        //TODO
    }
    public partial class IfcCompositeCurveOnSurface : IHasRules{
        //TODO
    }
    public partial class IfcCompositeCurveSegment : IHasRules{
        //TODO
    }
    public partial class IfcCompositeProfileDef : IHasRules{
        //TODO
    }
    public partial class IfcCompoundPlaneAngleMeasure : IHasRules{
        //TODO
    }
    public partial class IfcCompressor : IHasRules{
        //TODO
    }
    public partial class IfcCompressorType : IHasRules{
        //TODO
    }
    public partial class IfcCondenser : IHasRules{
        //TODO
    }
    public partial class IfcCondenserType : IHasRules{
        //TODO
    }
    public partial class IfcConstraint : IHasRules{
        //TODO
    }
    public partial class IfcConstraintRelationship : IHasRules{
        //TODO
    }
    public partial class IfcConstructionEquipmentResource : IHasRules{
        //TODO
    }
    public partial class IfcConstructionEquipmentResourceType : IHasRules{
        //TODO
    }
    public partial class IfcConstructionMaterialResource : IHasRules{
        //TODO
    }
    public partial class IfcConstructionMaterialResourceType : IHasRules{
        //TODO
    }
    public partial class IfcConstructionProductResource : IHasRules{
        //TODO
    }
    public partial class IfcConstructionProductResourceType : IHasRules{
        //TODO
    }
    public partial class IfcController : IHasRules{
        //TODO
    }
    public partial class IfcControllerType : IHasRules{
        //TODO
    }
    public partial class IfcCooledBeam : IHasRules{
        //TODO
    }
    public partial class IfcCooledBeamType : IHasRules{
        //TODO
    }
    public partial class IfcCoolingTower : IHasRules{
        //TODO
    }
    public partial class IfcCoolingTowerType : IHasRules{
        //TODO
    }
    public partial class IfcCovering : IHasRules{
        //TODO
    }
    public partial class IfcCoveringType : IHasRules{
        //TODO
    }
    public partial class IfcCrewResource : IHasRules{
        //TODO
    }
    public partial class IfcCrewResourceType : IHasRules{
        //TODO
    }
    public partial class IfcCShapeProfileDef : IHasRules{
        //TODO
    }
    public partial class IfcCurtainWall : IHasRules{
        //TODO
    }
    public partial class  IfcCurtainWallType : IHasRules{
        //TODO
    }
    public partial class IfcCurveStyle : IHasRules{
        //TODO
    }
    public partial class IfcCurveStyleFontPattern : IHasRules{
        //TODO
    }
    public partial class IfcDamper : IHasRules{
        //TODO
    }
    public partial class IfcDamperType : IHasRules{
        //TODO
    }
    public partial class IfcDayInMonthNumber : IHasRules{
        //TODO
    }
    public partial class IfcDayInWeekNumber : IHasRules{
        //TODO
    }
    public partial class IfcDerivedProfileDef : IHasRules{
        //TODO
    }
    public partial class IfcDerivedUnit : IHasRules{
        //TODO
    }
    public partial class IfcDimensionCount1 : IHasRules{
        //TODO
    }
    public partial class IfcDiscreteAccessory : IHasRules{
        //TODO
    }
    public partial class IfcDiscreteAccessoryType : IHasRules{
        //TODO
    }
    public partial class IfcDistributionChamberElement : IHasRules{
        //TODO
    }
    public partial class IfcDistributionChamberElementType : IHasRules{
        //TODO
    }
    public partial class IfcDocumentElectronicFormat : IHasRules{
        //TODO
    }
    public partial class IfcDocumentReference : IHasRules{
        //TODO
    }
    public partial class IfcDoor : IHasRules{
        //TODO
    }
    public partial class IfcDoorLiningProperties : IHasRules{
        //TODO
    }
    public partial class IfcDoorPanelProperties : IHasRules{
        //TODO
    }
    public partial class IfcDraughtingPreDefinedColour : IHasRules{
        //TODO
    }
    public partial class IfcDraughtingPreDefinedCurveFont : IHasRules{
        //TODO
    }
    public partial class IfcDraughtingPreDefinedTextFont : IHasRules{
        //TODO
    }
    public partial class IfcDuctFitting : IHasRules{
        //TODO
    }
    public partial class IfcDuctFittingType : IHasRules{
        //TODO
    }
    public partial class IfcDuctSegment : IHasRules{
        //TODO
    }
    public partial class IfcDuctSegmentType : IHasRules{
        //TODO
    }
    public partial class IfcDuctSilencer : IHasRules{
        //TODO
    }
    public partial class IfcDuctSilencerType : IHasRules{
        //TODO
    }
    public partial class IfcEdgeLoop : IHasRules{
        //TODO
    }
    public partial class IfcElectricAppliance : IHasRules{
        //TODO
    }
    public partial class IfcElectricApplianceType : IHasRules{
        //TODO
    }
    public partial class IfcElectricDistributionBoard : IHasRules{
        //TODO
    }
    public partial class IfcElectricDistributionBoardType : IHasRules{
        //TODO
    }
    public partial class IfcElectricFlowStorageDevice : IHasRules{
        //TODO
    }
    public partial class IfcElectricFlowStorageDeviceType : IHasRules{
        //TODO
    }
    public partial class  IfcElectricGenerator : IHasRules{
        //TODO
    }
    public partial class IfcElectricGeneratorType : IHasRules{
        //TODO
    }
    public partial class IfcElectricMotor : IHasRules{
        //TODO
    }
    public partial class IfcElectricMotorType : IHasRules{
        //TODO
    }
    public partial class IfcElectricTimeControl : IHasRules{
        //TODO
    }
    public partial class IfcElectricTimeControlType : IHasRules{
        //TODO
    }
    public partial class IfcElementAssembly : IHasRules{
        //TODO
    }
    public partial class IfcElementAssemblyType : IHasRules{
        //TODO
    }
    public partial class IfcElementQuantity : IHasRules{
        //TODO
    }
    public partial class IfcEngine : IHasRules{
        //TODO
    }
    public partial class IfcEngineType : IHasRules{
        //TODO
    }
    public partial class IfcEvaporativeCooler : IHasRules{
        //TODO
    }
    public partial class IfcEvaporativeCoolerType : IHasRules{
        //TODO
    }
    public partial class IfcEvaporator : IHasRules{
        //TODO
    }
    public partial class IfcEvaporatorType : IHasRules{
        //TODO
    }
    public partial class IfcEvent : IHasRules{
        //TODO
    }
    public partial class IfcEventType : IHasRules{
        //TODO
    }
    public partial class IfcExternalReference : IHasRules{
        //TODO
    }
    public partial class IfcExtrudedAreaSolid : IHasRules{
        //TODO
    }
    public partial class IfcExtrudedAreaSolidTapered : IHasRules{
        //TODO
    }
    public partial class IfcFace : IHasRules{
        //TODO
    }
    public partial class IfcFan : IHasRules{
        //TODO
    }
    public partial class IfcFanType : IHasRules{
        //TODO
    }
    public partial class IfcFastener : IHasRules{
        //TODO
    }
    public partial class IfcFastenerType : IHasRules{
        //TODO
    }
    public partial class IfcFeatureElementSubtraction : IHasRules{
        //TODO
    }
    public partial class IfcFillAreaStyle : IHasRules{
        //TODO
    }
    public partial class IfcFillAreaStyleHatching : IHasRules{
        //TODO
    }
    public partial class IfcFilter : IHasRules{
        //TODO
    }
    public partial class IfcFilterType : IHasRules{
        //TODO
    }
    public partial class IfcFireSuppressionTerminal : IHasRules{
        //TODO
    }
    public partial class IfcFireSuppressionTerminalType : IHasRules{
        //TODO
    }
    public partial class IfcFlowInstrument : IHasRules{
        //TODO
    }
    public partial class IfcFlowInstrumentType : IHasRules{
        //TODO
    }
    public partial class IfcFlowMeter : IHasRules{
        //TODO
    }
    public partial class IfcFlowMeterType : IHasRules{
        //TODO
    }
    public partial class IfcFontStyle : IHasRules{
        //TODO
    }
    public partial class IfcFontVariant : IHasRules{
        //TODO
    }
    public partial class IfcFontWeight : IHasRules{
        //TODO
    }
    public partial class IfcFooting : IHasRules{
        //TODO
    }
    public partial class IfcFootingType : IHasRules{
        //TODO
    }
    public partial class IfcFurniture : IHasRules{
        //TODO
    }
    public partial class IfcFurnitureType : IHasRules{
        //TODO
    }
    public partial class IfcGeographicElement : IHasRules{
        //TODO
    }
    public partial class IfcGeographicElementType : IHasRules{
        //TODO
    }
    public partial class IfcGeometricCurveSet : IHasRules{
        //TODO
    }
    public partial class IfcGeometricRepresentationContext : IHasRules{
        //TODO
    }
    public partial class IfcGeometricRepresentationSubContext : IHasRules{
        //TODO
    }
    public partial class IfcGeometricSet : IHasRules{
        //TODO
    }
    public partial class IfcGrid : IHasRules{
        //TODO
    }
    public partial class IfcGridAxis : IHasRules{
        //TODO
    }
    public partial class IfcHeatExchanger : IHasRules{
        //TODO
    }
    public partial class IfcHeatExchangerType : IHasRules{
        //TODO
    }
    public partial class IfcHeatingValueMeasure : IHasRules{
        //TODO
    }
    public partial class IfcHumidifier : IHasRules{
        //TODO
    }
    public partial class IfcHumidifierType : IHasRules{
        //TODO
    }
    public partial class IfcInterceptor : IHasRules{
        //TODO
    }
    public partial class IfcInterceptorType : IHasRules{
        //TODO
    }
    public partial class IfcIShapeProfileDef : IHasRules{
        //TODO
    }
    public partial class IfcJunctionBox : IHasRules{
        //TODO
    }
    public partial class IfcJunctionBoxType : IHasRules{
        //TODO
    }
    public partial class IfcLaborResource : IHasRules{
        //TODO
    }
    public partial class IfcLaborResourceType : IHasRules{
        //TODO
    }
    public partial class IfcLamp : IHasRules{
        //TODO
    }
    public partial class IfcLampType : IHasRules{
        //TODO
    }
    public partial class IfcLightFixture : IHasRules{
        //TODO
    }
    public partial class IfcLightFixtureType : IHasRules{
        //TODO
    }
    public partial class IfcLine : IHasRules{
        //TODO
    }
    public partial class IfcLocalPlacement : IHasRules{
        //TODO
    }
    public partial class IfcLShapeProfileDef : IHasRules{
        //TODO
    }
    public partial class IfcMaterialDefinitionRepresentation : IHasRules{
        //TODO
    }
    public partial class IfcMechanicalFastener : IHasRules{
        //TODO
    }
    public partial class IfcMechanicalFastenerType : IHasRules{
        //TODO
    }
    public partial class IfcMedicalDevice : IHasRules{
        //TODO
    }
    public partial class IfcMedicalDeviceType : IHasRules{
        //TODO
    }
    public partial class IfcMember : IHasRules{
        //TODO
    }
    public partial class IfcMemberStandardCase : IHasRules{
        //TODO
    }
    public partial class IfcMemberType : IHasRules{
        //TODO
    }
    public partial class IfcMonthInYearNumber : IHasRules{
        //TODO
    }
    public partial class IfcMotorConnection : IHasRules{
        //TODO
    }
    public partial class IfcMotorConnectionType : IHasRules{
        //TODO
    }
    public partial class IfcNamedUnit : IHasRules{
        //TODO
    }
    public partial class IfcNonNegativeLengthMeasure : IHasRules{
        //TODO
    }
    public partial class IfcNormalisedRatioMeasure : IHasRules{
        //TODO
    }
    public partial class IfcObjective : IHasRules{
        //TODO
    }
    public partial class IfcOccupant : IHasRules{
        //TODO
    }
    public partial class IfcOffsetCurve2D : IHasRules{
        //TODO
    }
    public partial class IfcOffsetCurve3D : IHasRules{
        //TODO
    }
    public partial class IfcOrientedEdge : IHasRules{
        //TODO
    }
    public partial class IfcOutlet : IHasRules{
        //TODO
    }
    public partial class IfcOutletType : IHasRules{
        //TODO
    }
    
    
    public partial class IfcOwnerHistory : IHasRules{
        [Rule("CorrectChangeAction", typeof(IfcOwnerHistory),
              "If ChangeAction is asserted and LastModifiedDate is not defined, ChangeAction must be set to NOTDEFINED")]
        public bool CorrectChangeAction(){
            return this.LastModifiedDate != null ||
                (this.LastModifiedDate == null && this.ChangeAction == IfcChangeActionEnum.notdefined); //HACK notdefined is not added until 2X4
        }
    }
    
    public partial class IfcPath : IHasRules{
        //TODO
    }
    public partial class IfcPCurve : IHasRules{
        //TODO
    }
    public partial class IfcPerson : IHasRules{
        //TODO
    }
    public partial class IfcPHMeasure : IHasRules{
        //TODO
    }
    public partial class IfcPhysicalComplexQuantity : IHasRules{
        //TODO
    }
    public partial class IfcPile : IHasRules{
        //TODO
    }
    public partial class IfcPileType : IHasRules{
        //TODO
    }
    public partial class IfcPipeFitting : IHasRules{
        //TODO
    }
    public partial class IfcPipeFittingType : IHasRules{
        //TODO
    }
    public partial class IfcPipeSegmentType : IHasRules{
        //TODO
    }
    public partial class IfcPixelTexture : IHasRules{
        //TODO
    }
    public partial class IfcPlate : IHasRules{
        //TODO
    }
    public partial class IfcPlateStandardCase : IHasRules{
        //TODO
    }
    public partial class IfcPlateType : IHasRules{
        //TODO
    }
    public partial class IfcPolygonalBoundedHalfSpace : IHasRules{
        //TODO
    }
    public partial class IfcPolyline : IHasRules{
        //TODO
    }
    public partial class IfcPolyLoop : IHasRules{
        //TODO
    }
    public partial class IfcPositiveLengthMeasure : IHasRules{
        //TODO
    }
    public partial class IfcPositivePlaneAngleMeasure : IHasRules{
        //TODO
    }
    public partial class IfcPositiveRatioMeasure1 : IHasRules{
        [Rule("WR1", typeof(IfcPositiveRatioMeasure1), "A positive measure shall be greater than zero.")]
        public bool WR1(){
            return this.Value > 0;
        }
    }
    
    public partial class IfcPostalAddress : IHasRules{
        //TODO
    }
    public partial class IfcPresentationLayerAssignment : IHasRules{
        //TODO
    }
    public partial class IfcPresentationLayerWithStyle : IHasRules{
        //TODO
    }
    public partial class IfcProcedure : IHasRules{
        //TODO
    }
    public partial class IfcProcedureType : IHasRules{
        //TODO
    }
    public partial class IfcProduct : IHasRules{
        //TODO
    }
    public partial class IfcProductDefinitionShape : IHasRules{
        //TODO
    }
    public partial class IfcProject : IHasRules{
        //TODO
    }
    public partial class IfcProjectedCRS : IHasRules{
        //TODO
    }
    public partial class IfcPropertyBoundedValue : IHasRules{
        //TODO
    }
    public partial class IfcPropertyDependencyRelationship : IHasRules{
        //TODO
    }
    public partial class IfcPropertyEnumeratedValue : IHasRules{
        //TODO
    }
    public partial class IfcPropertyEnumeration : IHasRules{
        //TODO
    }
    public partial class IfcPropertyListValue: IHasRules{
        //TODO
    }
    public partial class IfcPropertySet : IHasRules{
        //TODO
    }
    public partial class IfcPropertySetTemplate : IHasRules{
        //TODO
    }
    public partial class IfcPropertyTableValue : IHasRules{
        //TODO
    }
    public partial class IfcProtectiveDevice : IHasRules{
        //TODO
    }
    public partial class IfcProtectiveDeviceTrippingUnit : IHasRules{
        //TODO
    }
    public partial class IfcProtectiveDeviceTrippingUnitType : IHasRules{
        //TODO
    }
    public partial class IfcProtectiveDeviceType : IHasRules{
        //TODO
    }
    public partial class IfcProxy : IHasRules{
        //TODO
    }
    public partial class IfcPump : IHasRules{
        //TODO
    }
    public partial class IfcPumpType : IHasRules{
        //TODO
    }
    public partial class IfcQuantityArea : IHasRules{
        //TODO
    }
    public partial class IfcQuantityCount : IHasRules{
        //TODO
    }
    public partial class IfcQuantityLength : IHasRules{
        //TODO
    }
    public partial class IfcQuantityTime : IHasRules{
        //TODO
    }
    public partial class IfcQuantityVolume  : IHasRules{
        //TODO
    }
    public partial class IfcQuantityWeight : IHasRules{
        //TODO
    }
    public partial class IfcRailing : IHasRules{
        //TODO
    }
    public partial class IfcRailingType : IHasRules{
        //TODO
    }
    public partial class IfcRamp : IHasRules{
        //TODO
    }
    public partial class IfcRampFlight : IHasRules{
        //TODO
    }
    public partial class IfcRampFlightType : IHasRules{
        //TODO
    }
    public partial class IfcRampType : IHasRules{
        //TODO
    }
    public partial class IfcRationalBSplineCurveWithKnots : IHasRules{
        //TODO
    }
    public partial class IfcRationalBSplineSurfaceWithKnots : IHasRules{
        //TODO
    }
    public partial class IfcRectangleHollowProfileDef : IHasRules{
        //TODO
    }
    public partial class IfcRectangleProfileDef : IHasRules{
        //TODO
    }
    public partial class IfcRectangularTrimmedSurface : IHasRules{
        //TODO
    }
    public partial class IfcReinforcingBar : IHasRules{
        //TODO
    }
    public partial class IfcReinforcingElement : IHasRules{
        //TODO
    }
    public partial class IfcReinforcingElementType : IHasRules{
        //TODO
    }
    public partial class IfcReinforcingMesh : IHasRules{
        //TODO
    }
    public partial class IfcRelAggregates : IHasRules{
        //TODO
    }
    public partial class IfcRelAssigns : IHasRules{
        //TODO
    }
    public partial class IfcRelAssignsToActor : IHasRules{
        //TODO
    }
    public partial class IfcRelAssignsToControl : IHasRules{
        //TODO
    }
    public partial class IfcRelAssignsToGroup : IHasRules{
        //TODO
    }
    public partial class IfcRelAssignsToProcess : IHasRules{
        //TODO
    }
    public partial class IfcRelAssignsToProduct : IHasRules{
        //TODO
    }
    public partial class IfcRelAssignsToResource : IHasRules{
        //TODO
    }
    public partial class IfcRelAssociatesMaterial : IHasRules{
        //TODO
    }
    public partial class IfcRelConnectsElements : IHasRules{
        //TODO
    }
    public partial class IfcRelConnectsPorts : IHasRules{
        //TODO
    }
    public partial class IfcRelConnectsPortToElement : IHasRules{
        //TODO
    }
    public partial class IfcRelContainedInSpatialStructure : IHasRules{
        //TODO
    }
    public partial class IfcRelDeclares : IHasRules{
        //TODO
    }
    public partial class IfcRelInterferesElements : IHasRules{
        //TODO
    }
    public partial class IfcRelNests : IHasRules{
        //TODO
    }
    public partial class IfcRelReferencedInSpatialStructure : IHasRules{
        //TODO
    }
    public partial class IfcRelSequence : IHasRules{
        //TODO
    }
    public partial class IfcRelSpaceBoundary : IHasRules{
        //TODO
    }
    public partial class IfcReparametrisedCompositeCurveSegment : IHasRules{
        //TODO
    }
    public partial class IfcRepresentationContextSameWCS : IHasRules{
        //TODO
    }
    public partial class IfcRepresentationMap : IHasRules{
        //TODO
    }
    public partial class IfcRevolvedAreaSolid : IHasRules{
        //TODO
    }
    public partial class IfcRevolvedAreaSolidTapered : IHasRules{
        //TODO
    }
    public partial class IfcRoof : IHasRules{
        //TODO
    }
    public partial class IfcRoofType : IHasRules{
        //TODO
    }
    public partial class IfcRoundedRectangleProfileDef : IHasRules{
        //TODO
    }
    public partial class IfcSanitaryTerminal : IHasRules{
        //TODO
    }
    public partial class IfcSanitaryTerminalType : IHasRules{
        //TODO
    }
    public partial class IfcSectionedSpine : IHasRules{
        //TODO
    }
    public partial class IfcSensor : IHasRules{
        //TODO
    }
    public partial class IfcSensorType : IHasRules{
        //TODO
    }
    public partial class IfcShadingDevice : IHasRules{
        //TODO
    }
    public partial class IfcShapeModel : IHasRules{
        //TODO
    }
    public partial class IfcSingleProjectInstance : IHasRules{
        //TODO
    }
    public partial class IfcSlab : IHasRules{
        //TODO
    }
    public partial class IfcSlabElementedCase : IHasRules{
        //TODO
    }
    public partial class IfcSlabStandardCase : IHasRules{
        //TODO
    }
    public partial class IfcSlabType : IHasRules{
        //TODO
    }
    public partial class IfcSolarDevice : IHasRules{
        //TODO
    }
    public partial class IfcSolarDeviceType : IHasRules{
        //TODO
    }
    public partial class IfcSpace : IHasRules{
        //TODO
    }
    public partial class IfcSpaceHeater : IHasRules{
        //TODO
    }
    public partial class IfcSpaceHeaterType : IHasRules{
        //TODO
    }
    public partial class IfcSpaceType : IHasRules{
        //TODO
    }
    public partial class IfcSpatialStructureElement : IHasRules{
        //TODO
    }
    public partial class IfcSpatialZone : IHasRules{
        //TODO
    }
    public partial class IfcSpatialZoneType : IHasRules{
        //TODO
    }
    public partial class IfcSpecularRoughness : IHasRules{
        //TODO
    }
    public partial class IfcStackTerminal : IHasRules{
        //TODO
    }
    public partial class IfcStackTerminalType : IHasRules{
        //TODO
    }
    public partial class IfcStair : IHasRules{
        //TODO
    }
    public partial class IfcStairFlight : IHasRules{
        //TODO
    }
    public partial class IfcStairFlightType : IHasRules{
        //TODO
    }
    public partial class IfcStairType : IHasRules{
        //TODO
    }
    public partial class IfcStructuralAnalysisModel : IHasRules{
        //TODO
    }
    public partial class IfcStructuralCurveAction : IHasRules{
        //TODO
    }
    public partial class IfcStructuralCurveMember : IHasRules{
        //TODO
    }
    public partial class IfcStructuralCurveReaction : IHasRules{
        //TODO
    }
    public partial class IfcStructuralLoadConfiguration : IHasRules{
        //TODO
    }
    public partial class IfcStructuralLoadGroup : IHasRules{
        //TODO
    }
    public partial class IfcStructuralResultGroup : IHasRules{
        //TODO
    }
    public partial class IfcStructuralSurfaceAction : IHasRules{
        //TODO
    }
    public partial class  IfcStructuralSurfaceMember : IHasRules{
        //TODO
    }
    public partial class IfcStructuralSurfaceReaction : IHasRules{
        //TODO
    }
    public partial class IfcStyledItem : IHasRules{
        //TODO
    }
    public partial class IfcStyledRepresentation : IHasRules{
        //TODO
    }
    public partial class IfcSubContractResource : IHasRules{
        //TODO
    }
    public partial class IfcSubContractResourceType : IHasRules{
        //TODO
    }
    public partial class IfcSurfaceFeature : IHasRules{
        //TODO
    }
    public partial class IfcSurfaceOfLinearExtrusion : IHasRules{
        //TODO
    }
    public partial class IfcSurfaceReinforcementArea : IHasRules{
        //TODO
    }
    public partial class IfcSurfaceStyle : IHasRules{
        //TODO
    }
    public partial class IfcSweptAreaSolid : IHasRules{
        //TODO
    }
    public partial class IfcSweptDiskSolid : IHasRules{
        //TODO
    }
    public partial class IfcSweptDiskSolidPolygonal : IHasRules{
        //TODO
    }
    public partial class IfcSweptSurface : IHasRules{
        //TODO
    }
    public partial class IfcSwitchingDevice : IHasRules{
        //TODO
    }
    public partial class IfcSwitchingDeviceType : IHasRules{
        //TODO
    }
    public partial class IfcSystemFurnitureElement : IHasRules{
        //TODO
    }
    public partial class IfcSystemFurnitureElementType : IHasRules{
        //TODO
    }
    public partial class IfcTable : IHasRules{
        //TODO
    }
    public partial class IfcTank : IHasRules{
        //TODO
    }
    public partial class IfcTankType : IHasRules{
        //TODO
    }
    public partial class IfcTask : IHasRules{
        //TODO
    }
    public partial class IfcTaskType : IHasRules{
        //TODO
    }
    public partial class IfcTelecomAddress : IHasRules{
        //TODO
    }
    public partial class IfcTextAlignment : IHasRules{
        //TODO
    }
    public partial class IfcTextDecoration : IHasRules{
        //TODO
    }
    public partial class IfcTextLiteralWithExtent : IHasRules{
        //TODO
    }
    public partial class IfcTextStyleFontModel : IHasRules{
        //TODO
    }
    public partial class IfcTextTransformation : IHasRules{
        //TODO
    }
    public partial class IfcTopologyRepresentation : IHasRules{
        //TODO
    }
    public partial class IfcTransformer : IHasRules{
        //TODO
    }
    public partial class IfcTransformerType : IHasRules{
        //TODO
    }
    public partial class IfcTransportElement : IHasRules{
        //TODO
    }
    public partial class IfcTransportElementType : IHasRules{
        //TODO
    }
    public partial class IfcTrimmedCurve : IHasRules{
        //TODO
    }
    public partial class IfcTShapeProfileDef : IHasRules{
        //TODO
    }
    public partial class IfcTubeBundle : IHasRules{
        //TODO
    }
    public partial class IfcTubeBundleType : IHasRules{
        //TODO
    }
    public partial class IfcTypeObject : IHasRules{
        //TODO
    }
    public partial class IfcTypeProduct : IHasRules{
        //TODO
    }
    public partial class IfcUnitaryControlElement : IHasRules{
        //TODO
    }
    public partial class IfcUnitaryControlElementType : IHasRules{
        //TODO
    }
    public partial class IfcUnitaryEquipment : IHasRules{
        //TODO
    }
    public partial class IfcUnitaryEquipmentType : IHasRules{
        //TODO
    }
    public partial class IfcUnitAssignment : IHasRules{
        //TODO
    }
    public partial class IfcUShapeProfileDef : IHasRules{
        //TODO
    }
    public partial class IfcValve : IHasRules{
        //TODO
    }
    public partial class IfcValveType : IHasRules{
        //TODO
    }
    public partial class IfcVector : IHasRules{
        //TODO
    }
    public partial class IfcVibrationIsolator : IHasRules{
        //TODO
    }
    public partial class IfcVibrationIsolatorType : IHasRules{
        //TODO
    }
    public partial class IfcVoidingFeature : IHasRules{
        //TODO
    }
    public partial class IfcWall : IHasRules{
        //TODO
    }
    public partial class IfcWallElementedCase : IHasRules{
        //TODO
    }
    public partial class IfcWallStandardCase : IHasRules{
        //TODO
    }
    public partial class IfcWallType : IHasRules{
        //TODO
    }
    public partial class IfcWasteTerminal : IHasRules{
        //TODO
    }
    public partial class IfcWasteTerminalType : IHasRules{
        //TODO
    }
    public partial class IfcWindow : IHasRules{
        //TODO
    }
    public partial class IfcWindowLiningProperties : IHasRules{
        //TODO
    }
    public partial class IfcWorkCalendar : IHasRules{
        //TODO
    }
    public partial class IfcWorkPlan : IHasRules{
        //TODO
    }
    public partial class IfcWorkSchedule : IHasRules{
        //TODO
    }
    public partial class IfcZone : IHasRules{
        //TODO
    }
    public partial class IfcZShapeProfileDef : IHasRules{
        //TODO
    }
}
