
using System;

#pragma warning disable 1591
namespace IfcDotNet.Schema
{
	public partial class IfcPositiveRatioMeasure1 : IHasRules{
		[Rule("WR1", typeof(IfcPositiveRatioMeasure1), "A positive measure shall be greater than zero.")]
		public bool WR1(){
			return this.Value > 0;
		}
	}
}
