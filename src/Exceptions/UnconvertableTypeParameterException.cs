using System;
using System.Reflection;

namespace Commands.Exceptions
{
	public class UnconvertableTypeParameterException : Exception
	{
		public MethodInfo MethodInfo { get; }
		public ParameterInfo ParameterInfo { get; }

		public UnconvertableTypeParameterException(MethodInfo methodInfo, ParameterInfo parameterInfo)
			: base($"Cannot convert type `{parameterInfo.ParameterType}` from string in method `{methodInfo.Name}`, try adding a TypeConverter")
		{
			MethodInfo = methodInfo;
			ParameterInfo = parameterInfo;
		}
	}
}
