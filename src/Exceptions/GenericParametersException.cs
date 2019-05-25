using System;
using System.Reflection;

namespace Commands.Exceptions
{
	public class GenericParametersException : Exception
	{
		public MethodInfo MethodInfo { get; }
		public GenericParametersException(MethodInfo methodInfo)
			: base($"Found one or more generic parameters in the method `{methodInfo.Name}`")
		{ MethodInfo = methodInfo; }
	}
}
