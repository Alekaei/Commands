using Commands.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Commands.Classes
{
	public class Paramaters
	{
		public List<Parameter> Arguments { get; set; }
		public string Syntax { get; }
		public bool ContainsParams { get; }
		public Paramaters(MethodInfo methodInfo)
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
					isContext: isContext));
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

			int paramI = 0;
			for (int i = 0; i < sArgs.Length; i++)
			{
				Parameter param = Arguments[paramI];

				if (param.IsContext)
					args[i] = context;
				else if (param.IsOptional)
					args[i] = param.DefaultValue;
				else if (param.IsFlag)
				{
					// Implement flag parsing 
				}
				else if (!param.IsParams)
					paramI++;

				TypeConverter converter = TypeDescriptor.GetConverter(param.ParameterInfo.ParameterType);
				object result = converter.ConvertFrom(sArgs[i]);
				if (result.GetType() != param.ParameterInfo.ParameterType)
					return false;
				args[i] = result;
			}
			return true;
		}

		private string GenerateSyntax()
		{
			return string.Join(" ", Arguments.Select(a =>
			{
				if (a.IsFlag) return (a.ShortName == null) ? $"--{a.LongName}" : $"-{a.ShortName}";
				if (a.IsOptional) return $"[{a.Name}]";
				return $"<{a.Name}>";
			}));
		}
	}
}
