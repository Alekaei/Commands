using Commands.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Commands.Classes
{
	public class ParametersList
	{
		public List<Parameter> Parameters { get; set; }
		public string Syntax { get; }
		public bool ContainsParams { get; }
		public ParametersList(MethodInfo methodInfo)
		{
			List<Parameter> args = new List<Parameter>();

			bool hasContextParameter = false;
			foreach (ParameterInfo param in methodInfo.GetParameters())
			{
				if (param.IsOptional)
					throw new UnsupportedOptionalParameterException(methodInfo, param);

				bool isContext = false;
				if (param.ParameterType.IsAssignableFrom(typeof(ICommandContext)))
				{
					hasContextParameter = true;
					isContext = true;
				}

				FlagAttribute flagAttribute = param.GetCustomAttribute<FlagAttribute>(false);

				if (flagAttribute != null && param.ParameterType != typeof(bool))
					throw new UnsupportedNonBooleanFlagException(methodInfo, param);

				Type checkType = param.ParameterType;
				if (param.ParameterType.IsArray || (param.ParameterType.IsGenericType && param.ParameterType.GetGenericTypeDefinition() == typeof(List<>)))
					checkType = param.ParameterType.GetElementType() ?? param.ParameterType.GetGenericArguments()[0];

				if (checkType != typeof(ICommandContext))
				{
					TypeConverter converter = TypeDescriptor.GetConverter(checkType);
					if (!converter.CanConvertFrom(typeof(string)))
						throw new UnconvertableTypeParameterException(methodInfo, param);
				}

				args.Add(new Parameter(
					parameterInfo: param,
					isFlag: flagAttribute != null,
					shortName: flagAttribute?.ShortName,
					longName: flagAttribute?.LongName,
					isParams: param.IsDefined(typeof(ParamArrayAttribute), false),
					isContext: isContext,
					defaultValue: flagAttribute?.DefaultValue ?? false));
			}

			// Throw exception if a ICommandContext parameter isnt found
			if (!hasContextParameter)
				throw new MissingContextParameterException(methodInfo);

			Parameters = args;
			//Syntax = GenerateSyntax();
		}

		public bool TryParseStringArgs(ICommandContext context, string[] stringArgs, out object[] args)
		{
			// +1 to account for ICommandContext
			args = new object[Parameters.Count];

			List<(Parameter param, int index)> flagParameters = new List<(Parameter param, int index)>();
			List<string> flags = new List<string>();

			if (args.Length < Parameters.Where(p => !p.IsFlag).Count())
				return false;

			int stringArgIndex = 0;
			foreach (string sArg in stringArgs)
			{
				if (sArg.StartsWith("--"))
				{
					flags.Add(sArg.TrimStart('-'));
					stringArgIndex++;
				}
				else if (sArg.StartsWith("-"))
				{
					flags.AddRange(sArg.TrimStart('-').ToCharArray().Select(f => f.ToString()));
					stringArgIndex++;
				}
			}

			for (int i = 0; i < Parameters.Count; i++)
			{
				Parameter parameter = Parameters[i];

				if (parameter.IsFlag)
				{
					flagParameters.Add((parameter, i));
					continue;
				}

				if (parameter.IsContext)
				{
					args[i] = context;
					continue;
				}

				if (stringArgIndex < stringArgs.Length)
				{
					string sArg = stringArgs[stringArgIndex];

					if (parameter.IsArrayOrList && !parameter.IsParams)
					{
						string[] arraySArgs = sArg.Split(new string[] { ":#:" }, StringSplitOptions.None);
						object[] array = new object[arraySArgs.Length];

						for (int j = 0; j < arraySArgs.Length; j++)
						{
							if (!TryConvert(parameter.ConvertType, arraySArgs[j], out array[j]))
								return false;
						}

						args[i] = array;
						stringArgIndex++;
						continue;
					}

					if (!TryConvert(parameter.ConvertType, sArg, out args[i]))
						return false;
					stringArgIndex++;
				}
			}

			if (stringArgIndex != stringArgs.Length)
				return false;

			foreach ((Parameter param, int index) flag in flagParameters)
			{
				if (flags.FirstOrDefault(f => f == flag.param.ShortName.ToString() || f == flag.param.LongName) != null)
				{
					args[flag.index] = !flag.param.DefaultValue;
				}
				else args[flag.index] = flag.param.DefaultValue;
			}

			return true;
		}

		private bool TryConvert(Type from, string value, out object parsed)
		{
			parsed = null;
			try
			{
				TypeConverter converter = TypeDescriptor.GetConverter(from);
				parsed = converter.ConvertFromString(value);
				if (parsed.GetType() != from || parsed == null)
					return false;
				return true;
			}
			catch
			{
				return false;
			}
		}

		private string GenerateSyntax()
		{
			//string flags = "";
			//string args = "";
			//foreach (Parameter arg in Parameters)
			//{
			//	if (arg.IsFlag)
			//		flags += $" [{(arg.ShortName == null ? $"--{arg.LongName}" : $"-{arg.ShortName}")}]";
			//	if (arg.IsParams)
			//		args += $"({arg.Name}...)";
			//	else
			//		args += arg.IsOptional ? $"[{arg.Name}]" : $"<{arg.Name}>";
			//}
			//return $"{flags.Trim()} {args.Trim()}".Trim();

			throw new NotImplementedException();
		}
	}
}
