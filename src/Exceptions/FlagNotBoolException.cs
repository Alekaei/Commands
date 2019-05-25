using System;
using System.Reflection;

namespace Kommands.Exceptions
{
	public class FlagNotBoolException : Exception
	{
		public MethodInfo MethodInfo { get; }
		public ParameterInfo ParameterInfo { get; }

		public FlagNotBoolException(MethodInfo methodInfo, ParameterInfo parameterInfo)
			: base($"Flag parametere isnt of type bool in method `{methodInfo.Name}`, parameter `{parameterInfo.Name}`")
		{
			MethodInfo = methodInfo;
			ParameterInfo = parameterInfo;
		}
	}
}
