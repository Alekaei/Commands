using Commands.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Commands.Classes
{
	public class Parameters
	{
		public List<Parameter> Arguments { get; set; }
		public string Syntax { get; }
		public bool ContainsParams { get; }
		public Parameters(MethodInfo methodInfo)
		{
			List<Parameter> args = new List<Parameter>();

			bool hasContextParameter = false;
			foreach (ParameterInfo param in methodInfo.GetParameters())
			{
				bool isContext = false;
				if (param.ParameterType.IsAssignableFrom(typeof(ICommandContext)))
				{
					hasContextParameter = true;
					isContext = true;
				}

				FlagAttribute flagAttribute = param.GetCustomAttribute<FlagAttribute>(false);

				if (flagAttribute != null && param.ParameterType != typeof(bool))
					throw new FlagNotBoolException(methodInfo, param);

				TypeConverter converter = TypeDescriptor.GetConverter(param.ParameterType);
				if (!converter.CanConvertFrom(typeof(string)))
					throw new UnconvertableTypeParameterException(methodInfo, param);

				args.Add(new Parameter(
					parameterInfo: param,
					isFlag: flagAttribute != null,
					shortName: flagAttribute?.ShortName,
					longName: flagAttribute?.LongName,
					isParams: param.IsDefined(typeof(ParamArrayAttribute), false),
					isContext: isContext,
					defaultFlagValue: flagAttribute?.DefaultValue ?? false));
			}

			// Throw exception if a ICommandContext parameter isnt found
			if (!hasContextParameter)
				throw new MissingContextParameterException(methodInfo);

			Arguments = args;
			Syntax = GenerateSyntax();
		}

		public bool TryParseStringArgs(ICommandContext context, string[] sArgs, out object[] args)
		{
			args = new object[sArgs.Length];

			List<(Parameter param, int index)> flagParams = new List<(Parameter, int)>();
			List<string> flags = new List<string>();

			int paramI = 0;
			for (int i = 0; i < sArgs.Length; i++)
			{
				Parameter param = Arguments[paramI];

				if (param.IsContext)
				{
					args[i] = context;
					continue;
				}

				if (sArgs[i].StartsWith("--"))
				{
					flags.Add(sArgs[i].TrimStart('-'));
					continue;
				}
				if (sArgs[i].StartsWith("-"))
				{
					flags.AddRange(sArgs[i].TrimStart('-').ToCharArray().Select(c => c.ToString()));
					continue;
				}

				if (!param.IsParams)
					paramI++;

				if (param.IsFlag)
				{
					flagParams.Add((param, i));
					continue;
				}

				TypeConverter converter = TypeDescriptor.GetConverter(param.ParameterInfo.ParameterType);
				try
				{
					object result = converter.ConvertFrom(sArgs[i]);
					if (result.GetType() != param.ParameterInfo.ParameterType)
						throw new Exception();

					args[i] = result;
				}
				catch
				{
					if (param.IsOptional)
						args[i] = param.DefaultValue;
				}
			}

			foreach ((Parameter param, int index) flag in flagParams)
			{
				if (flags.FirstOrDefault(f => f == flag.param.ShortName.ToString() || f == flag.param.LongName) == null)
				{
					// if flag param not present, set to default
					args[flag.index] = flag.param.DefaultValue;
					continue;
				}
				// If flag param is present,   set to opposite of default
				args[flag.index] = !flag.param.DefaultValue;
			}

			return true;
		}

		private string GenerateSyntax()
		{
			string flags = "";
			string args = "";
			foreach (Parameter arg in Arguments)
			{
				if (arg.IsFlag)
					flags += $" [{(arg.ShortName == null ? $"--{arg.LongName}" : $"-{arg.ShortName}")}]";
				if (arg.IsParams)
					args += $"({arg.Name}...)";
				else
					args += arg.IsOptional ? $"[{arg.Name}]" : $"<{arg.Name}>";
			}
			return $"{flags.Trim()} {args.Trim()}".Trim();
		}
	}
}
