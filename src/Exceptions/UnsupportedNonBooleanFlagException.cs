using System;
using System.Reflection;

namespace Commands.Exceptions
{
	public class UnsupportedNonBooleanFlagException : Exception
	{
		public MethodInfo MethodInfo { get; }
		public ParameterInfo ParameterInfo { get; }

		public UnsupportedNonBooleanFlagException(MethodInfo methodInfo, ParameterInfo parameterInfo)
			: base($"Flag parameter isnt of type bool in method `{methodInfo.Name}`, parameter `{parameterInfo.Name}`")
		{
			MethodInfo = methodInfo;
			ParameterInfo = parameterInfo;
		}
	}
}
