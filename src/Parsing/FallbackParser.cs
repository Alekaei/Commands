using System;
using System.ComponentModel;

namespace Commands.Parsing
{
	public class FallbackParser : ITypeParser
	{
		public TypeConverter Converter { get; }
		public Type ConvertTo => null;

		public FallbackParser(TypeConverter converter)
		{
			Converter = converter;
		}

		public object Parse(string from)
		{
			try
			{
				return Converter.ConvertFrom(from);
			}
			catch
			{
				return null;
			}
		}
	}
}
