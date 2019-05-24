using Kommands.Handlers;

namespace Kommands
{
	public interface IKommandContext
	{
		ICommandHandler Handler { get; }
		IExecuter Executer { get; }
	}
}
