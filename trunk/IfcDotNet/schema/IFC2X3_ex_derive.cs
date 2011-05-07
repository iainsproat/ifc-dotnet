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
        public IfcDimensionCount1 Dim{
            get{ return this.FirstOperand.Item.Dim; }
        }
    }
    
    public partial class IfcBoundingBox{
        [XmlIgnore()]
        public IfcDimensionCount1 Dim{
            get{ return new IfcDimensionCount1(3); }
        }
    }
    
    public partial class IfcBSplineCurve{
        [XmlIgnore()]
        public int UpperIndexOnControlPoints{
            get{ return this.ControlPointsList.IfcCartesianPoint.Length -1; }
        }
        
        [XmlIgnore()]
        public IfcCartesianPoint[] ControlPoints{
            get{ return this.ControlPointsList.IfcCartesianPoint; }
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
            get{ return this.Coordinates.IfcLengthMeasure.Length; }
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
        public IfcDimensionCount1 Dim{
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
            get{ return this.Segments.IfcCompositeCurveSegment.Length; }
        }
        [XmlIgnore()]
        public IfcLogical ClosedCurve{
            get{
                if(this.Segments == null || this.Segments.IfcCompositeCurveSegment == null || this.Segments.IfcCompositeCurveSegment.Length == 0)
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
        public IfcDimensionCount1 Dim{
            get{ return this.ParentCurve.Item.Dim; }
        }
    }
    
    public partial class IfcCsgPrimitive3D : IfcBooleanOperand{
        [XmlIgnore()]
        public IfcDimensionCount1 Dim{
            get{ return new IfcDimensionCount1(3); }
        }
    }
    
    public partial class IfcCurve{
        [XmlIgnore()]
        public IfcDimensionCount1 Dim{
            get{ return Functions.IfcCurveDim(this); }
        }
    }
    
    public partial class IfcDerivedUnit{
        [XmlIgnore()]
        public IfcDimensionalExponents Dimensions{
            get{ return Functions.IfcDeriveDimensionalExponents(this.Elements.IfcDerivedUnitElement); }
        }
    }
    
    public partial class IfcDirection{
        [XmlIgnore()]
        public IfcDimensionCount1 Dim{
            get{ return this.DirectionRatios.doublewrapper.Length; }
        }
    }
    
    public partial class IfcEdgeLoop{
        [XmlIgnore()]
        public int Ne{
            get{ return this.EdgeList.IfcOrientedEdge.Length; }
        }
    }
    
    public partial class IfcFaceBasedSurfaceModel{
        [XmlIgnore()]
        public IfcDimensionCount1 Dim{
            get{ return new IfcDimensionCount1(3); }
        }
    }
    
    public partial class IfcGeometricRepresentationSubContext{
        [StepProperty(4, true)]
        [XmlIgnore()]
        public override IfcGeometricRepresentationContextWorldCoordinateSystem WorldCoordinateSystem{
            get{ return this.ParentContext.Item.WorldCoordinateSystem; }
        }
        
        [StepProperty(2, true)]
        [XmlIgnore()]
        public override long CoordinateSpaceDimension{
            get{ return this.ParentContext.Item.CoordinateSpaceDimension; }
        }
        
        [StepProperty(5, true)]
        [XmlIgnore()]
        public override IfcGeometricRepresentationContextTrueNorth TrueNorth{
            get{
                if(this.ParentContext.Item.TrueNorth != null)
                    return this.ParentContext.Item.TrueNorth;
                IfcGeometricRepresentationContextTrueNorth t = new IfcGeometricRepresentationContextTrueNorth();
                t.Item = this.WorldCoordinateSystem.Item.P[1];
                return t;
            }
        }
        
        [StepProperty(3, true)]
        [XmlIgnore()]
        public override Nullable<double> Precision{
            get{ if(ParentContext.Item.Precision.HasValue)
                    return ParentContext.Item.Precision;
                return new Nullable<double>(1.0E-5);
            }
        }
    }
    
    public partial class IfcGeometricSet{
        [XmlIgnore()]
        public IfcDimensionCount1 Dim{
            get{ return this.Elements[0].Dim;}
        }
    }
    
    public partial class IfcHalfSpaceSolid : IfcBooleanOperand{
        [XmlIgnore()]
        public IfcDimensionCount1 Dim{
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
                op.Axis1  = new IfcDirection(-1, 0);
                op.Axis2 = new IfcDirection(0, 1);
                op.LocalOrigin = new IfcCartesianPoint(0, 0);
                op.Scale = 1;
                return op;
            }
        }
    }
    
    public partial class IfcOrientedEdge{
        [XmlIgnore()]
        [StepProperty(0, true)]
        public override IfcEdgeEdgeStart EdgeStart{
            get{
                IfcEdgeEdgeStart s = new IfcEdgeEdgeStart();
                s.Item = Functions.IfcBooleanChoose(this.Orientation, this.EdgeElement.Item.EdgeStart.Item, this.EdgeElement.Item.EdgeEnd.Item);
                return s;
            }
        }
        
        [XmlIgnore()]
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
        public IfcDimensionCount1 Dim{
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

    public partial class IfcRationalBSplineCurveWithKnots{
        //TODO
    }

    public partial class IfcRationalBSplineSurfaceWithKnots{
        //TODO
    }

    public partial class IfcReinforcingBar{
        //TODO
    }

    public partial class IfcReinforcingBarType{
        //TODO
    }

    public partial class IfcReinforcingMesh{
        //TODO
    }

    public partial class IfcReinforcingMeshType{
        //TODO
    }

    public partial class IfcRevolvedAreaSolid{
        //TODO
    }

    public partial class IfcSectionedSpine{
        //TODO
    }

    public partial class IfcShellBasedSurfaceModel{
        //TODO
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
        public IfcDimensionCount1 Dim{
            get{ return new IfcDimensionCount1(3); }
        }
    }

    public partial class IfcStructuralLinearAction{
        //TODO
    }

    public partial class IfcStructuralLoadCase{
        //TODO
    }

    public partial class IfcStructuralPlanarAction{
        //TODO
    }

    public partial class IfcSurface{
        public IfcDimensionCount1 Dim{ get{ return new IfcDimensionCount1(3);} }
    }

    public partial class IfcSurfaceOfLinearExtrusion{
        //TODO
    }

    public partial class IfcSurfaceOfRevolution{
        //TODO
    }

    public partial class IfcTable{
        //TODO
    }

    public partial class IfcVector{
        public IfcDimensionCount1 Dim{
            get{ return this.Orientation.Item.Dim;}
        }
    }

}