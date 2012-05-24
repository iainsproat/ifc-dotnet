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
using System.Collections.Generic;
using System.Xml.Serialization;

#pragma warning disable 1591
namespace IfcDotNet.Schema
{
	public partial class IfcAxis1Placement{
		[XmlIgnore()]
		public IfcDirection Z{
			get{
				return Functions.NVL<IfcDirection>(Functions.IfcNormalise(this.Axis.Item), new IfcDirection(0, 0, 1));
			}
		}
		
	}
	
	public partial class IfcAxis2Placement2D{
		[XmlIgnore()]
		public IList<IfcDirection> P{
			get{ return Functions.IfcBuild2Axes(this.RefDirection.Item); }
		}
	}
	
	public partial class IfcAxis2Placement3D{
		[XmlIgnore()]
		public IList<IfcDirection> P{
			get{ return Functions.IfcBuild2Axes(this.RefDirection.Item); }
		}
	}
	
	public partial class IfcBooleanResult : IfcBooleanOperand{
		[XmlIgnore()]
		public override IfcDimensionCount1 Dim{
			get{
				IfcBooleanOperand op = this.FirstOperand.Item as IfcBooleanOperand;
				if(op == null)
					return null;
				else
					return op.Dim;
			}
		}
	}
	
	public partial class IfcBoundingBox{
		[XmlIgnore()]
		public override IfcDimensionCount1 Dim{
			get{ return new IfcDimensionCount1(3); }
		}
	}
	
	public partial class IfcBSplineCurve{
		[XmlIgnore()]
		public int UpperIndexOnControlPoints{
			get{ return this.ControlPointsList.Items.Length -1; }
		}
		
		[XmlIgnore()]
		public IfcCartesianPoint[] ControlPoints{
			get{ return this.ControlPointsList.Items; }
		}
	}
	
	//FIXME new release for IFC2X4
	/*public partial class IfcBSplineCurveWithKnots{
		[XmlIgnore()]
		public int UpperIndexOnKnots{
			get{ return this.Knots.Length; }
		}
		//UpperIndexOnKnots	: INTEGER := SIZEOF(Knots);
	}
	 */
	
	public partial class IfcBSplineSurface{
		//TODO new release for IFC2X4
	}
	
	public partial class IfcBSplineSurfaceWithKnots{
		//TODO new release for IFC2X4
	}
	
	public partial class IfcCartesianPoint{
		[XmlIgnore()]
		public override IfcDimensionCount1 Dim{
			get{ return this.Coordinates.Items.Length; }
		}
	}
	
	public partial class IfcCartesianTransformationOperator{
		[XmlIgnore()]
		public double Scl{
			get{
				if(this.Scale.HasValue)
					return this.Scale.Value;
				return 1.0;
			}
		}
		
		[XmlIgnore()]
		public override IfcDimensionCount1 Dim{
			get{ return this.LocalOrigin.Item.Dim; }
		}
	}
	
	public partial class IfcCartesianTransformationOperator2D{
		[XmlIgnore()]
		public IList<IfcDirection> U{
			get{
				return Functions.IfcBaseAxis(2, this.Axis1.Item, this.Axis2.Item);
			}
		}
	}
	
	public partial class IfcCartesianTransformationOperator2DnonUniform{
		[XmlIgnore()]
		public double Scl2{
			get{ if(this.Scale2.HasValue)
					return this.Scale2.Value;
				return this.Scl;
			}
		}
	}
	
	public partial class IfcCartesianTransformationOperator3D{
		[XmlIgnore()]
		public IList<IfcDirection> U{
			get{
				return Functions.IfcBaseAxis(3,this.Axis1.Item, this.Axis2.Item, this.Axis3.Item);
			}
		}
	}
	
	public partial class IfcCartesianTransformationOperator3DnonUniform{
		[XmlIgnore()]
		public double Scl2{
			get{ if(this.Scale2.HasValue)
					return this.Scale2.Value;
				return this.Scl;
			}
		}
		[XmlIgnore()]
		public double Scl3{
			get{ if(this.Scale3.HasValue)
					return this.Scale3.Value;
				return this.Scl;
			}
		}
	}
	
	public partial class IfcCompositeCurve{
		[XmlIgnore()]
		public int NSegments{
			get{ return this.Segments.Items.Length; }
		}
		[XmlIgnore()]
		public IfcLogical ClosedCurve{
			get{
				if(this.Segments == null || this.Segments.Items == null || this.Segments.Items.Length == 0)
					return IfcLogical.unknown;
				if( this.Segments[NSegments - 1].Transition != IfcTransitionCode.discontinuous )
					return IfcLogical.True;
				return IfcLogical.False;
			}
		}
	}
	
	/*
	 * //FIXME new in IFC2X4
	public partial class IfcCompositeCurveOnSurface{
	    [XmlIgnore()]
	    public IList<IfcSurface> BasisSurface{
	        get{ return Functions.IfcGetBasisSurface(this); }
	    }
	}
	 */
	
	public partial class IfcCompositeCurveSegment{
		[XmlIgnore()]
		public override IfcDimensionCount1 Dim{
			get{ return this.ParentCurve.Item.Dim; }
		}
	}
	
	public partial class IfcCsgPrimitive3D : IfcBooleanOperand{
		[XmlIgnore()]
		public override IfcDimensionCount1 Dim{
			get{ return new IfcDimensionCount1(3); }
		}
	}
	
	public partial class IfcCurve{
		[XmlIgnore()]
		public override IfcDimensionCount1 Dim{
			get{ return Functions.IfcCurveDim(this); }
		}
	}
	
	public partial class IfcDerivedUnit{
		[XmlIgnore()]
		public IfcDimensionalExponents Dimensions{
			get{ return Functions.IfcDeriveDimensionalExponents(this.Elements.Items); }
		}
	}
	
	/* HACK to be re-instated
    public partial class IfcDirection{
        [XmlIgnore()]
        public IfcDimensionCount1 Dim{
            get{ return this.DirectionRatios.Items.Length; }
        }
    }*/
	
	public partial class IfcEdgeLoop{
		[XmlIgnore()]
		public int Ne{
			get{ return this.EdgeList.Items.Length; }
		}
	}
	
	public partial class IfcFaceBasedSurfaceModel{
		[XmlIgnore()]
		public override IfcDimensionCount1 Dim{
			get{ return new IfcDimensionCount1(3); }
		}
	}
	
	public partial class IfcGeometricRepresentationSubContext{
		[StepProperty(4, true)]
		public override IfcGeometricRepresentationContextWorldCoordinateSystem WorldCoordinateSystem{
			get{ return this.ParentContext.Item.WorldCoordinateSystem; }
		}
		
		[StepProperty(2, true)]
		public override long CoordinateSpaceDimension{
			get{ return this.ParentContext.Item.CoordinateSpaceDimension; }
		}
		
		[StepProperty(5, true)]
		public override IfcGeometricRepresentationContextTrueNorth TrueNorth{
			get{
				if(this.ParentContext.Item.TrueNorth != null)
					return this.ParentContext.Item.TrueNorth;
				IfcGeometricRepresentationContextTrueNorth t = new IfcGeometricRepresentationContextTrueNorth();
				IfcAxis2Placement placement = this.WorldCoordinateSystem.Item as IfcAxis2Placement;
				if(placement == null || placement.P == null || placement.P.Count < 1)
					return null;
				t.Item = placement.P[1];
				return t;
			}
		}
		
		[StepProperty(3, true)]
		public override Nullable<double> Precision{
			get{ if(ParentContext.Item.Precision.HasValue)
					return ParentContext.Item.Precision;
				return new Nullable<double>(1.0E-5);
			}
		}
	}
	
	public partial class IfcGeometricSet{
		[XmlIgnore()]
		public override IfcDimensionCount1 Dim{
			get{ return this.Elements[0].Dim;}
		}
	}
	
	public partial class IfcHalfSpaceSolid : IfcBooleanOperand{
		[XmlIgnore()]
		public override IfcDimensionCount1 Dim{
			get{ return new IfcDimensionCount1(3); }
		}
	}
	
	public partial class IfcMaterialLayerSet{
		[XmlIgnore()]
		public IfcLengthMeasure1 TotalThickness{
			get{ return Functions.IfcMlsTotalThickness(this); }
		}
	}
	
	public partial class IfcMirroredProfileDef{
		[XmlIgnore()]
		public IfcCartesianTransformationOperator2D Operator{
			get{
				IfcCartesianTransformationOperator2D op = new IfcCartesianTransformationOperator2D();
				op.Axis1  = (IfcCartesianTransformationOperatorAxis1)(new IfcDirection(-1, 0));
				op.Axis2 = (IfcCartesianTransformationOperatorAxis2)(new IfcDirection(0, 1));
				op.LocalOrigin = (IfcCartesianTransformationOperatorLocalOrigin)(new IfcCartesianPoint(0, 0));
				op.Scale = 1;
				return op;
			}
		}
	}
	
	public partial class IfcOrientedEdge{
		[StepProperty(0, true)]
		public override IfcEdgeEdgeStart EdgeStart{
			get{
				IfcEdgeEdgeStart s = new IfcEdgeEdgeStart();
				s.Item = Functions.IfcBooleanChoose(this.Orientation, this.EdgeElement.Item.EdgeStart.Item, this.EdgeElement.Item.EdgeEnd.Item);
				return s;
			}
		}
		
		[StepProperty(1, true)]
		public override IfcEdgeEdgeEnd EdgeEnd{
			get{
				IfcEdgeEdgeEnd e = new IfcEdgeEdgeEnd();
				e.Item = Functions.IfcBooleanChoose(this.Orientation, this.EdgeElement.Item.EdgeEnd.Item, this.EdgeElement.Item.EdgeStart.Item);
				return e;
			}
		}
	}

	public partial class IfcPlacement{
		[XmlIgnore()]
		public override IfcDimensionCount1 Dim{
			get{ return this.Location.Item.Dim; }
		}
	}

	public partial class IfcPointOnCurve{
		[XmlIgnore()]
		public override IfcDimensionCount1 Dim{
			get{ return this.BasisCurve.Item.Dim; }
		}
	}

	public partial class IfcPointOnSurface{
		[XmlIgnore()]
		public override IfcDimensionCount1 Dim{
			get{ return this.BasisSurface.Item.Dim; }
		}
	}

	/*
	 * //TODO new in IFC2X4
    public partial class IfcRationalBSplineCurveWithKnots{
        [XmlIgnore()]
        public double[] Weights{
            get{
                return this.WeightsData;
            }
        }
        //IfcListToArray(WeightsData,0,SELF\IfcBSplineCurve.UpperIndexOnControlPoints);
    }

    
    public partial class IfcRationalBSplineSurfaceWithKnots{
        [XmlIgnore()]
        public double[][] Weights{
            get{
                double[][] result = new double[this.UUpper][this.VUpper];
                result = IfcMakeArrayOfArray(this.WeightsData, 0, UUpper, 0, VUpper);
            }
        }
    }
	 */

	public partial class IfcReinforcingBar{
		[XmlIgnore()]
		public IfcReinforcingElementTypeEnum PredefinedType{
			get{ return IfcReinforcingElementTypeEnum.Bar;
			}
		}
	}
	
	//FIXME move to a more appropriate file
	public enum IfcReinforcingElementTypeEnum{
		Bar,
		Mesh,
		Tendon,
		TendonAnchor,
		TendonSheath,
		PunchingShearReinforcement,
		UserDefined,
		NotDefined
	}

	public partial class IfcReinforcingBarType{
		[XmlIgnore()]
		public IfcReinforcingElementTypeEnum PredefinedType{
			get{ return IfcReinforcingElementTypeEnum.Bar; }
		}
	}

	public partial class IfcReinforcingMesh{
		[XmlIgnore()]
		public IfcReinforcingElementTypeEnum PredefinedType{
			get{ return IfcReinforcingElementTypeEnum.Mesh; }
		}
	}

	public partial class IfcReinforcingMeshType{
		[XmlIgnore()]
		public IfcReinforcingElementTypeEnum PredefinedType{
			get{ return IfcReinforcingElementTypeEnum.Mesh; }
		}
	}

	public partial class IfcRevolvedAreaSolid{
		[XmlIgnore()]
		public IfcLine AxisLine{
			get{
				return new IfcLine(this.Axis.Item.Location.Item, new IfcVector(this.Axis.Item.Z, 1));
			}
		}
	}

	public partial class IfcSectionedSpine{
		[XmlIgnore()]
		public override IfcDimensionCount1 Dim{
			get{ return new IfcDimensionCount1(3); }
		}
	}

	public partial class IfcShellBasedSurfaceModel{
		[XmlIgnore()]
		public override IfcDimensionCount1 Dim{
			get{ return new IfcDimensionCount1(3); }
		}
	}

	public partial class IfcSIUnit{
		
		/// <summary>
		/// The dimensional exponents of SI units are derived by function IfcDimensionsForSiUnit.
		/// </summary>
		[StepProperty(0,true)]
		public override IfcNamedUnitDimensions Dimensions{
			get{ return null; }
			set{ //do nothing
			}
		}
	}

	public partial class IfcSolidModel : IfcBooleanOperand{
		[XmlIgnore()]
		public override IfcDimensionCount1 Dim{
			get{ return new IfcDimensionCount1(3); }
		}
	}

	public partial class IfcStructuralLinearAction{
		[XmlIgnore()]
		public IfcStructuralCurveActivityTypeEnum PredefinedType{
			get{ return IfcStructuralCurveActivityTypeEnum.CONST; }
		}
	}
	
	//FIXME move to a more appropriate file
	public enum IfcStructuralCurveActivityTypeEnum{
		CONST,
		LINEAR,
		POLYGONAL,
		EQUIDISTANT,
		SINUS,
		PARABOLA,
		DISCRETE,
		USERDEFINED,
		NOTDEFINED
	}

	public partial class IfcStructuralLoadCase{
		[XmlIgnore()]
		public IfcLoadGroupTypeEnum PredefinedType{
			get{ return IfcLoadGroupTypeEnum.load_case; }
		}
	}

	public partial class IfcStructuralPlanarAction{
		[XmlIgnore()]
		public IfcStructuralSurfaceActivityTypeEnum PredefinedType{
			get{ return IfcStructuralSurfaceActivityTypeEnum.CONST; }
		}
	}
	
	//FIXME move to a more appropriate file
	public enum IfcStructuralSurfaceActivityTypeEnum {
		CONST,
		BILINEAR,
		DISCRETE,
		ISOCONTOUR,
		USERDEFINED,
		NOTDEFINED
	}

	public partial class IfcSurface{
		[XmlIgnore()]
		public override IfcDimensionCount1 Dim{ get{ return new IfcDimensionCount1(3);} }
	}

	public partial class IfcSurfaceOfLinearExtrusion{
		[XmlIgnore()]
		public IfcVector ExtrusionAxis{
			get{ return new IfcVector(this.ExtrudedDirection.Item, this.Depth);}
		}
	}

	public partial class IfcSurfaceOfRevolution{
		[XmlIgnore()]
		public IfcLine AxisLine{
			get{
				return new IfcLine(this.AxisPosition.Item.Location.Item, new IfcVector(this.AxisPosition.Item.Z, 1));
			}
		}
	}

	public partial class IfcTable{
		[XmlIgnore()]
		public int NumberOfCellsInRow{
			get{ return this.Rows[0].RowCells.Items.Length; }
		}
		
		[XmlIgnore()]
		public int NumberOfHeadings{
			get{
				int result = 0;
				foreach(IfcTableRow row in this.Rows){
					if(row.IsHeading)
						result++;
				}
				return result;
			}
		}
		
		[XmlIgnore()]
		public int NumberOfDataRows{
			get{
				int result = 0;
				foreach(IfcTableRow row in this.Rows){
					if(!row.IsHeading)
						result++;
				}
				return result;
			}
		}
	}

	public partial class IfcVector{
		[XmlIgnore()]
        public override IfcDimensionCount1 Dim{
			get{ return this.Orientation.Item.Dim;}
		}
	}
	
	//FIXME the below class inherits Dim property, but does not have a Dim property in the IFC schema so should not implement it.
	public partial class IfcFillAreaStyleHatching{
		[XmlIgnore()]
        public override IfcDimensionCount1 Dim{
			get{ throw new NotImplementedException(); }
		}
	}
	
	//FIXME the below class inherits Dim property, but does not have a Dim property in the IFC schema so should not implement it.
	public partial class IfcOneDirectionRepeatFactor{
		[XmlIgnore()]
        public override IfcDimensionCount1 Dim{
			get{ throw new NotImplementedException(); }
		}
	}
	
	//FIXME the below class inherits Dim property, but does not have a Dim property in the IFC schema so should not implement it.
	public partial class IfcFillAreaStyleTiles{
		[XmlIgnore()]
        public override IfcDimensionCount1 Dim{
			get{ throw new NotImplementedException(); }
		}
	}
	
	//FIXME the below class inherits Dim property, but does not have a Dim property in the IFC schema so should not implement it.
	public partial class IfcFillAreaStyleTileSymbolWithStyle{
		[XmlIgnore()]
        public override IfcDimensionCount1 Dim{
			get{ throw new NotImplementedException(); }
		}
	}
	
	//FIXME the below class inherits Dim property, but does not have a Dim property in the IFC schema so should not implement it.
	public partial class IfcDraughtingCallout{
		[XmlIgnore()]
        public override IfcDimensionCount1 Dim{
			get{ throw new NotImplementedException(); }
		}
	}
	
	//FIXME the below class inherits Dim property, but does not have a Dim property in the IFC schema so should not implement it.
	public partial class IfcAnnotationFillArea{
		[XmlIgnore()]
        public override IfcDimensionCount1 Dim{
			get{ throw new NotImplementedException(); }
		}
	}
	
	//FIXME the below class inherits Dim property, but does not have a Dim property in the IFC schema so should not implement it.
	public partial class IfcAnnotationSurface{
		[XmlIgnore()]
        public override IfcDimensionCount1 Dim{
			get{ throw new NotImplementedException(); }
		}
	}
	
	//FIXME the below class inherits Dim property, but does not have a Dim property in the IFC schema so should not implement it.
	public partial class IfcDefinedSymbol{
		[XmlIgnore()]
        public override IfcDimensionCount1 Dim{
			get{ throw new NotImplementedException(); }
		}
	}
	
	//FIXME the below class inherits Dim property, but does not have a Dim property in the IFC schema so should not implement it.
	public partial class IfcPlanarExtent{
		[XmlIgnore()]
        public override IfcDimensionCount1 Dim{
			get{ throw new NotImplementedException(); }
		}
	}
	
	//FIXME the below class inherits Dim property, but does not have a Dim property in the IFC schema so should not implement it.
	public partial class IfcTextLiteral{
		[XmlIgnore()]
        public override IfcDimensionCount1 Dim{
			get{ throw new NotImplementedException(); }
		}
	}
	
	//FIXME the below class inherits Dim property, but does not have a Dim property in the IFC schema so should not implement it.
	public partial class IfcLightSourceAmbient{
		[XmlIgnore()]
        public override IfcDimensionCount1 Dim{
			get{ throw new NotImplementedException(); }
		}
	}
	
	//FIXME the below class inherits Dim property, but does not have a Dim property in the IFC schema so should not implement it.
	public partial class IfcLightSourceDirectional{
		[XmlIgnore()]
        public override IfcDimensionCount1 Dim{
			get{ throw new NotImplementedException(); }
		}
	}
	
	//FIXME the below class inherits Dim property, but does not have a Dim property in the IFC schema so should not implement it.
	public partial class IfcLightSourceGoniometric{
		[XmlIgnore()]
        public override IfcDimensionCount1 Dim{
			get{ throw new NotImplementedException(); }
		}
	}
	
	//FIXME the below class inherits Dim property, but does not have a Dim property in the IFC schema so should not implement it.
	public partial class IfcLightSourcePositional{
		[XmlIgnore()]
        public override IfcDimensionCount1 Dim{
			get{ throw new NotImplementedException(); }
		}
	}
}