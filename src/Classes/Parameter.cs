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

		// Flag related
		public bool IsFlag { get; }
		public char? ShortName { get; }
		public string LongName { get; }
		public bool DefaultValue { get; }

		public bool IsParams { get; }
		public bool IsContext { get; }
		public bool IsArrayOrList { get; }

		public Type ConvertType { get; }

		public Parameter(ParameterInfo parameterInfo, bool isFlag, char? shortName,
			string longName, bool isParams, bool isContext, bool defaultValue)
		{
			ParameterInfo = parameterInfo;
			IsFlag = isFlag;
			ShortName = shortName;
			LongName = longName;
			IsParams = isParams;
			IsContext = isContext;

			IsArrayOrList = ParameterInfo.ParameterType.IsArray
					|| (ParameterInfo.ParameterType.IsGenericType
						&& ParameterInfo.ParameterType.GetGenericTypeDefinition() == typeof(List<>));
			if (IsArrayOrList)
				ConvertType = ParameterInfo.ParameterType.GetElementType() ?? ParameterInfo.ParameterType.GetGenericArguments()[0];
			else
				ConvertType = ParameterInfo.ParameterType;

			DefaultValue = defaultValue;
		}
	}
}
