using System;

namespace noide.tests
{
	partial class CommandFactoryTests
	{
		private class SolutionWatcher : ISolutionWatcher
		{
			public String Path;

			public void Watch(IStop stop, String path)
			{
				this.Path = path;
			}
		}
	}
}