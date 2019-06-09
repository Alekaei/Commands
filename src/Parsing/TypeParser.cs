using System;

namespace Commands.Parsing
{
	public abstract class TypeParser<T> : ITypeParser
	{
		public Type ConvertTo => typeof(T);

		public abstract object Parse(string from);
	}
}
