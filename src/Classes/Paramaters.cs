using Kommands.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Kommands.Classes
{
	public class Paramaters
	{
		public IEnumerable<Parameter> Arguments { get; set; }
		public string Syntax { get; }
		public bool ContainsParams { get; }
		public Paramaters(MethodInfo methodInfo)
		{
			List<Parameter> args = new List<Parameter>();

			bool hasContextParameter = false;
			foreach (ParameterInfo param in methodInfo.GetParameters())
			{
				bool isContext = false;
				if (param.ParameterType.IsAssignableFrom(typeof(IKommandContext)))
				{
					hasContextParameter = true;
					isContext = true;
				}

				FlagAttribute flagAttribute = param.GetCustomAttribute<FlagAttribute>(false);

				if (flagAttribute != null && param.ParameterType != typeof(bool))
					throw new FlagNotBoolException(methodInfo, param);

				args.Add(new Parameter(
					parameterInfo: param,
					isFlag: flagAttribute != null,
					shortName: flagAttribute?.ShortName,
					longName: flagAttribute?.LongName,
					isParams: param.IsDefined(typeof(ParamArrayAttribute), false),
					isContext: isContext));
			}

			// Throw exception if a IKommandContext parameter isnt found
			if (!hasContextParameter)
				throw new MissingContextParameterException(methodInfo);

			Arguments = args;
			Syntax = GenerateSyntax();
		}

		public bool TryParseStringArgs(IKommandContext context, string[] sArgs, out object[] args)
		{
			throw new NotImplementedException();
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
