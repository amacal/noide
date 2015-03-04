using System;

namespace noide
{
	partial class CommandFactory
	{
		private class WatchCommand : ICommand
		{
			private readonly ISolutionWatcher solutionWatcher;
			private readonly String path;

			public WatchCommand(ISolutionWatcher solutionWatcher, String path)
			{
				this.solutionWatcher = solutionWatcher;
				this.path = path;
			}

			public void Execute()
			{
            	this.solutionWatcher.Watch(new Never(), this.path);
			}

			private class Never : IStop
	        {
	        	public bool IsRequested()
	        	{
	        		return false;
	        	}
	        }
		}
	}
}