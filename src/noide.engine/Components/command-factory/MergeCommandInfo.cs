namespace noide
{
	partial class CommandFactory
	{
		private class MergeCommandInfo : ICommandInfo
		{
			private readonly ISolutionMerger solutionMerger;

			public MergeCommandInfo(ISolutionMerger solutionMerger)
			{
				this.solutionMerger = solutionMerger;
			}

			public bool CanHandle(IArgument[] arguments)
			{
				return arguments.Length > 1
					&& arguments[0].Value == "merge"
					&& arguments[1].Option != null
					&& arguments[1].Option.Long == "path"
					&& arguments[1].Value != null;
			}

			public ICommand Create(IArgument[] arguments)
			{
				return new MergeCommand(this.solutionMerger, arguments[1].Value);
			}
		}
	}
}