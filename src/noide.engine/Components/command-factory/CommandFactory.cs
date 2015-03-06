namespace noide
{
	public partial class CommandFactory : ICommandFactory
	{
		private readonly ICommandInfo[] registrants;
		private readonly ICommand fallback;

		public CommandFactory(
			ISolutionWatcher solutionWatcher,
			ISolutionMerger solutionMerger)
		{
			this.fallback = new FallbackCommand();
			this.registrants = new ICommandInfo[]
			{
				new WatchCommandInfo(solutionWatcher),
				new MergeCommandInfo(solutionMerger)
			};
		}

		public ICommand Create(IArgument[] arguments)
		{
			foreach (ICommandInfo info in this.registrants)
			{
				if (info.CanHandle(arguments) == true)
				{
					return info.Create(arguments);
				}
			}

			return this.fallback;
		}
	}
}