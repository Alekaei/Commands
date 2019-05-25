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
		public bool DefaultValue {
			get {
				return (bool)(ParameterInfo.DefaultValue ?? false);
			}
		}

		public bool IsParams { get; }
		public bool IsContext { get; }
		public bool IsOptional {
			get {
				return ParameterInfo.HasDefaultValue;
			}
		}

		public Parameter(ParameterInfo parameterInfo, bool isFlag, char? shortName,
			string longName, bool isParams, bool isContext)
		{
			ParameterInfo = parameterInfo;
			IsFlag = isFlag;
			ShortName = shortName;
			LongName = longName;
			IsParams = isParams;
			IsContext = isContext;
		}
	}
}
