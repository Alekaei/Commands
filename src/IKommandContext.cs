using Kommands.Handlers;

namespace Kommands
{
	public interface IKcommandContext
	{
		ICommandHandler Handler { get; }
		IExecuter Executer { get; }
	}
}
