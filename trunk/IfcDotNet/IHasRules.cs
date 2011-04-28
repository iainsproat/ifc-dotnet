
using System;
using System.Globalization;
using System.Reflection;
using System.Collections.Generic;

using log4net;

namespace IfcDotNet
{	
	
	
	/// <summary>
	/// A class which has methods (parameterless, returns a boolean value) which are decorated with the RuleAttribute.
	/// These rules should evaluate the state of properties whithin the class.
	/// </summary>
	public interface IHasRules{
		//empty on purpose
	}
}
