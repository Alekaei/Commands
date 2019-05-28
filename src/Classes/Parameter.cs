using System;
using System.Collections.Generic;
using System.Reflection;

namespace Commands.Classes
{
	public class Parameter
	{
		public ParameterInfo ParameterInfo { get; }
		public string Name {
			get {
				return ParameterInfo.Name;
			}
		}

		public bool IsFlag { get; }
		public char? ShortName { get; }
		public string LongName { get; }
		public bool DefaultValue { get; }

		public bool IsParams { get; }
		public bool IsContext { get; }
		public bool IsOptional {
			get {
				return ParameterInfo.HasDefaultValue;
			}
		}
		public bool IsArrayOrList {
			get {
				return ParameterInfo.ParameterType.IsArray
					|| (ParameterInfo.ParameterType.IsGenericType && ParameterInfo.ParameterType.GetGenericTypeDefinition() == typeof(List<>));
			}
		}

		public Type ArrayType {
			get {
				if (IsArrayOrList)
					return ParameterInfo.ParameterType.GetElementType() ?? ParameterInfo.ParameterType.GetGenericArguments()[0];
				return null;
			}
		}

		public Parameter(ParameterInfo parameterInfo, bool isFlag, char? shortName,
			string longName, bool isParams, bool isContext, bool defaultFlagValue)
		{
			ParameterInfo = parameterInfo;
			IsFlag = isFlag;
			ShortName = shortName;
			LongName = longName;
			IsParams = isParams;
			IsContext = isContext;

			DefaultValue = parameterInfo.HasDefaultValue ? (bool)parameterInfo.DefaultValue : defaultFlagValue;
		}
	}
}
