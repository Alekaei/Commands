using System;
using System.Reflection;

namespace Commands.Exceptions
{
	public class MethodNotStaticException : Exception
	{
		public MethodInfo MethodInfo { get; }

		public MethodNotStaticException(MethodInfo methodInfo)
			: base($"Command attributes must be placed on static methods, `{methodInfo.Name}`")
		{ MethodInfo = methodInfo; }
	}
}
