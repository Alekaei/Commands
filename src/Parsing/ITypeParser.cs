using System;

namespace Commands.Parsing
{
	public interface ITypeParser
	{
		Type ConvertTo { get; }

		/// <summary>
		/// Tries to parse from string to type
		/// </summary>
		/// <param name="from">String to parse from</param>
		/// <returns>type as object if succesfull, otherwise null</returns>
		object Parse(string from);
	}
}
