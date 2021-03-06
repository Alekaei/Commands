﻿using Commands.Classes;
using Commands.Exceptions;
using Commands.Parsing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Commands.Handlers
{
	public class CommandHandlerBase : ICommandHandler
	{
		public CommandHandlerOptions Options { get; }
		public CommandsList Commands { get; }

		public CommandHandlerBase(CommandHandlerOptions options)
		{
			Options = options;
			Commands = new CommandsList();

			RegisterTypeParsers();
		}

		private void RegisterTypeParsers()
		{
			IEnumerable<Type> parsers = AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetTypes())
				.Where(t => t.IsDefined(typeof(TypeParser<>), false));

			foreach (Type parser in parsers)
			{
				Attribute[] attribute = new Attribute[1];
				TypeConverterAttribute converterAttribute = new TypeConverterAttribute(parser);
				attribute[0] = converterAttribute;
				TypeDescriptor.AddAttributes(parser.GetGenericArguments()[0], attribute);
			}
		}

		public void Debug(string message)
		{
			if (!Options.Debug) return;
			WriteLine("[Debug] " + message);
		}

		public virtual bool HandleCommand(IExecuter executer, string commandText)
		{
			Debug($"Handling command `{commandText}`");
			//Regex re = new Regex("\"(?<arg>.*?)\\"|'(?<arg>.*?)'|(?<arg>[^\\s]+)");
			// yes i know this is a cunt of a regex but unless someone know how to make it smaller, it stays
			Regex re = new Regex("\"(?<val>.*?)\"|'(?<val>.*?)'|(\"(?<val>.*?)\"|'(?<val>.*?)'|(?<val>[\\w-=!@#$%^&*()_+\\[\\]{}\\|<>/?.:;`~]+))(,\\s*(\"(?<val>.*?)\"|'(?<val>.*?)'|(?<val>[\\w-=!@#$%^&*()_+\\[\\]{}\\|<>/?.:;`~]+)))*|(?<val>[^\\s]+)");
			MatchCollection matches = re.Matches(commandText.Trim());
			string[] split = new string[matches.Count];
			for (int i = 0; i < matches.Count; i++)
			{
				if (matches[i].Groups[4].Captures.Count > 1)
				{
					string[] captures = new string[matches[i].Groups[4].Captures.Count];
					int j = 0;
					foreach (object capture in matches[i].Groups[4].Captures)
					{
						captures[j] = capture.ToString().Trim();
						j++;
					}
					split[i] = String.Join(":#:", captures);
				}
				else
					split[i] = matches[i].Groups[4].Captures[0].ToString().Trim();
			}

			string commandName = split[0];
			string[] args = split.Skip(1).ToArray();

			Command command = Commands.FindCommand(commandName);
			if (command == null) return false;

			try
			{
				return command.Execute(new CommandContext(this, executer), args);
			}
			catch (InvalidSyntaxException ex)
			{
				WriteLine(Color.Red, ex.Message);
			}
			return false;
		}

		public virtual async Task<bool> HandleCommandAsync(IExecuter executer, string commandText)
		{
			Debug($"Handling command `{commandText}`");
			//Regex re = new Regex("\"(?<arg>.*?)\\"|'(?<arg>.*?)'|(?<arg>[^\\s]+)");
			// yes i know this is a cunt of a regex but unless someone know how to make it smaller, it stays
			Regex re = new Regex("\"(?<val>.*?)\"|'(?<val>.*?)'|(\"(?<val>.*?)\"|'(?<val>.*?)'|(?<val>[\\w-=!@#$%^&*()_+\\[\\]{}\\|<>/?.:;`~]+))(,\\s*(\"(?<val>.*?)\"|'(?<val>.*?)'|(?<val>[\\w-=!@#$%^&*()_+\\[\\]{}\\|<>/?.:;`~]+)))*|(?<val>[^\\s]+)");
			MatchCollection matches = re.Matches(commandText.Trim());
			string[] split = new string[matches.Count];
			for (int i = 0; i < matches.Count; i++)
			{
				if (matches[i].Groups[4].Captures.Count > 1)
				{
					string[] captures = new string[matches[i].Groups[4].Captures.Count];
					int j = 0;
					foreach (object capture in matches[i].Groups[4].Captures)
					{
						captures[j] = capture.ToString().Trim();
						j++;
					}
					split[i] = String.Join(":#:", captures);
				}
				else
					split[i] = matches[i].Groups[4].Captures[0].ToString().Trim();
			}

			string commandName = split[0];
			string[] args = split.Skip(1).ToArray();

			Command command = Commands.FindCommand(commandName);
			if (command == null) return false;

			try
			{
				return await command.ExecuteAsync(new CommandContext(this, executer), args);
			}
			catch (InvalidSyntaxException ex)
			{
				await WriteLineAsync(Color.Red, ex.Message);
			}
			return false;
		}

		#region Write to output
		public virtual void Write(string text, params object[] args)
		{
			throw new NotImplementedException();
		}

		public virtual void Write(Color color, string text, params object[] args)
		{
			throw new NotImplementedException();
		}

		public virtual async Task WriteAsync(string text, params object[] args)
		{
			throw new NotImplementedException();
		}

		public virtual async Task WriteAsync(Color color, string Text, params object[] args)
		{
			throw new NotImplementedException();
		}

		public virtual void WriteLine(string text, params object[] args)
		{
			throw new NotImplementedException();
		}

		public virtual void WriteLine(Color color, string text, params object[] args)
		{
			throw new NotImplementedException();
		}

		public virtual async Task WriteLineAsync(string text, params object[] args)
		{
			throw new NotImplementedException();
		}

		public virtual async Task WriteLineAsync(Color color, string text, params object[] args)
		{
			throw new NotImplementedException();
		}
		#endregion
	}
}
