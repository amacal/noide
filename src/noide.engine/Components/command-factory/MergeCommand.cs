using System;

namespace noide
{
	partial class CommandFactory
	{
		private class MergeCommand : ICommand
		{
			private readonly ISolutionMerger solutionMerger;
			private readonly String path;

			public MergeCommand(ISolutionMerger solutionMerger, String path)
			{
				this.solutionMerger = solutionMerger;
				this.path = path;
			}

			public void Execute()
			{
				this.solutionMerger.Merge(this.path);
			}
		}
	}
}