using System;
using System.Reflection;

namespace Kommands.Exceptions
{
	public class GenericParametersException : Exception
	{
		public MethodInfo MethodInfo { get; }
		public GenericParametersException(MethodInfo methodInfo)
			: base($"Found one or more generic parameters in the method `{methodInfo.Name}`")
		{ MethodInfo = methodInfo; }
	}
}
