using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Commands.Classes
{
	public class Method
	{
		public MethodInfo MethodInfo { get; }
		public ParametersList Parameters { get; }
		public bool IsAsync { get; }

		public Method(MethodInfo methodInfo)
		{
			MethodInfo = methodInfo;
			Parameters = new ParametersList(methodInfo);

			IsAsync = methodInfo.IsDefined(typeof(AsyncStateMachineAttribute), false);
		}

		public bool TryExecute(object[] args)
		{
			try
			{
				MethodInfo.Invoke(null, args);
				return true;
			}
			catch
			{
				return false;
			}
		}

		public async Task<bool> TryExecuteAsync(object[] args)
		{
			try
			{
				await (Task)MethodInfo.Invoke(null, args);
				return true;
			}
			catch
			{
				return false;
			}
		}
	}
}
