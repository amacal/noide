namespace noide
{
	partial class CommandFactory
	{
		private class WatchCommandInfo : ICommandInfo
		{
			private readonly ISolutionWatcher solutionWatcher;

			public WatchCommandInfo(ISolutionWatcher solutionWatcher)
			{
				this.solutionWatcher = solutionWatcher;
			}

			public bool CanHandle(IArgument[] arguments)
			{
				return arguments.Length > 1
					&& arguments[0].Value == "watch"
					&& arguments[1].Option != null
					&& arguments[1].Option.Long == "path"
					&& arguments[1].Value != null;
			}

			public ICommand Create(IArgument[] arguments)
			{
				return new WatchCommand(this.solutionWatcher, arguments[1].Value);
			}
		}
	}
}