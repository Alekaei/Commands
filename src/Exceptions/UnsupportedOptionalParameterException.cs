using System;
using System.Reflection;

namespace Commands.Exceptions
{
	public class UnsupportedOptionalParameterException : Exception
	{
		public MethodInfo MethodInfo { get; }
		public ParameterInfo ParameterInfo { get; }

		public UnsupportedOptionalParameterException(MethodInfo methodInfo, ParameterInfo parameterInfo)
			: base($"Optional parameter `{parameterInfo.Name}` in `{methodInfo.Name}`")
		{ MethodInfo = methodInfo; ParameterInfo = parameterInfo; }
	}
}
