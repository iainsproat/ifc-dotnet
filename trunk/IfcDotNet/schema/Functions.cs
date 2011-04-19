
using System;

namespace IfcDotNet.Schema
{
	/// <summary>
	/// Functions is a class to hold all functions defined in the IFC specification
	/// </summary>
	public class Functions
	{
		/// <summary>
		/// Definition from ISO/CD 10303-41:1992: The function returns the dimensional exponents of the given SI-unit.
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public static IfcDimensionalExponents IfcDimensionsForSiUnit(IfcSIUnitName name){
			
			switch(name){
				case IfcSIUnitName.metre:
					return new IfcDimensionalExponents(1, 0, 0, 0, 0, 0, 0);
				case IfcSIUnitName.square_metre:
					return new IfcDimensionalExponents(2, 0, 0, 0, 0, 0, 0);
				case IfcSIUnitName.cubic_metre:
					return new IfcDimensionalExponents(3, 0, 0, 0, 0, 0, 0);
				case IfcSIUnitName.gram:
					return new IfcDimensionalExponents(0, 1, 0, 0, 0, 0, 0);
				case IfcSIUnitName.second:
					return new IfcDimensionalExponents(0, 0, 1, 0, 0, 0, 0);
				case IfcSIUnitName.ampere:
					return new IfcDimensionalExponents(0, 0, 0, 1, 0, 0, 0);
				case IfcSIUnitName.kelvin:
					return new IfcDimensionalExponents(0, 0, 0, 0, 1, 0, 0);
				case IfcSIUnitName.mole:
					return new IfcDimensionalExponents(0, 0, 0, 0, 0, 1, 0);
				case IfcSIUnitName.candela:
					return new IfcDimensionalExponents(0, 0, 0, 0, 0, 0, 1);
				case IfcSIUnitName.radian:
					return new IfcDimensionalExponents(0, 0, 0, 0, 0, 0, 0);
				case IfcSIUnitName.steradian:
					return new IfcDimensionalExponents(0, 0, 0, 0, 0, 0, 0);
				case IfcSIUnitName.hertz:
					return new IfcDimensionalExponents(0, 0, -1, 0, 0, 0, 0);
				case IfcSIUnitName.newton:
					return new IfcDimensionalExponents(1, 1, -2, 0, 0, 0, 0);
				case IfcSIUnitName.pascal:
					return new IfcDimensionalExponents(-1, 1, -2, 0, 0, 0, 0);
				case IfcSIUnitName.joule:
					return new IfcDimensionalExponents(2, 1, -2, 0, 0, 0, 0);
				case IfcSIUnitName.watt:
					return new IfcDimensionalExponents(2, 1, -3, 0, 0, 0, 0);
				case IfcSIUnitName.coulomb:
					return new IfcDimensionalExponents(0, 0, 1, 1, 0, 0, 0);
				case IfcSIUnitName.volt:
					return new IfcDimensionalExponents(2, 1, -3, -1, 0, 0, 0);
				case IfcSIUnitName.farad:
					return new IfcDimensionalExponents(-2, -1, 4, 1, 0, 0, 0);
				case IfcSIUnitName.ohm:
					return new IfcDimensionalExponents(2, 1, -3, -2, 0, 0, 0);
				case IfcSIUnitName.siemens:
					return new IfcDimensionalExponents(-2, -1, 3, 2, 0, 0, 0);
				case IfcSIUnitName.weber:
					return new IfcDimensionalExponents(2, 1, -2, -1, 0, 0, 0);
				case IfcSIUnitName.tesla:
					return new IfcDimensionalExponents(0, 1, -2, -1, 0, 0, 0);
				case IfcSIUnitName.henry:
					return new IfcDimensionalExponents(2, 1, -2, -2, 0, 0, 0);
				case IfcSIUnitName.degree_celsius:
					return new IfcDimensionalExponents(0, 0, 0, 0, 1, 0, 0);
				case IfcSIUnitName.lumen:
					return new IfcDimensionalExponents(0, 0, 0, 0, 0, 0, 1);
				case IfcSIUnitName.lux:
					return new IfcDimensionalExponents(-2, 0, 0, 0, 0, 0, 1);
				case IfcSIUnitName.becquerel:
					return new IfcDimensionalExponents(0, 0, -1, 0, 0, 0, 0);
				case IfcSIUnitName.gray:
					return new IfcDimensionalExponents(2, 0, -2, 0, 0, 0, 0);
				case IfcSIUnitName.sievert:
					return new IfcDimensionalExponents(2, 0, -2, 0, 0, 0, 0);
				default:
					return new IfcDimensionalExponents(0, 0, 0, 0, 0, 0, 0);
			}
			
		}
	}
}
