using System;
using System.Reflection;

namespace Kommands.Exceptions
{
	public class MissingContextParameterException : Exception
	{
		public MethodInfo MethodInfo { get; }
		public MissingContextParameterException(MethodInfo methodInfo)
			: base($"Missing IKommandContext parameter in method `{methodInfo.Name}`")
		{ MethodInfo = methodInfo; }
	}
}
