using System;
using System.Reflection;

namespace Commands.Exceptions
{
	public class MissingContextParameterException : Exception
	{
		public MethodInfo MethodInfo { get; }
		public MissingContextParameterException(MethodInfo methodInfo)
			: base($"Missing ICommandContext parameter in method `{methodInfo.Name}`")
		{ MethodInfo = methodInfo; }
	}
}
