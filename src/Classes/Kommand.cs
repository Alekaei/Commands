using Kommands.Handlers;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Kommands.Classes
{
	public class Kommand
	{
		public string Name { get; }
		public string[] Aliases { get; }
		public string Summary { get; }
		public IEnumerable<Kommand> SubCommands { get; }
		public MethodInfo MethodInfo { get; }
		public Paramaters Paramaters { get; }

		public Kommand(string name, string[] aliases, string summary,
			IEnumerable<Kommand> subCommands, MethodInfo methodInfo, Paramaters paramaters)
		{
			Name = name;
			Aliases = aliases;
			Summary = summary;
			SubCommands = subCommands;
			MethodInfo = methodInfo;
			Paramaters = paramaters;
		}

		public void Execute(ICommandHandler handler, IExecuter executer, string[] args)
		{
			KommandContext context = new KommandContext(handler, executer);
			throw new NotImplementedException();
		}
	}
}
