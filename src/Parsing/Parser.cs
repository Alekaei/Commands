using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Commands.Parsing
{
	public static class Parser
	{
		private static IEnumerable<ITypeParser> _parsers = null;
		private static IEnumerable<ITypeParser> Parsers {
			get {
				if (_parsers == null)
				{
					List<ITypeParser> parsers = new List<ITypeParser>();
					foreach (Type t in AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetTypes()))
					{
						if (typeof(ITypeParser).IsAssignableFrom(t)
							&& t.IsAbstract == false
							&& t != typeof(FallbackParser))
						{
							parsers.Add(Activator.CreateInstance(t) as ITypeParser);
						}
					}
					_parsers = parsers;
				}
				return _parsers;
			}
		}

		public static ITypeParser GetParserForType(Type type)
		{
			if (type == null) throw new ArgumentNullException("type");

			ITypeParser parser = Parsers.FirstOrDefault(p => p.ConvertTo == type);

			if (parser == null)
			{
				// Incase a parser cant be found, fallback to using TypeConverter
				TypeConverter converter = TypeDescriptor.GetConverter(type);
				if (converter == null || !converter.CanConvertFrom(typeof(string))) return null;
				return new FallbackParser(converter);
			}

			return parser;
		}
	}
}
