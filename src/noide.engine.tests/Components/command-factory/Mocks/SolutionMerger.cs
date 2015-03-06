using System;

namespace noide.tests
{
	partial class CommandFactoryTests
	{
		private class SolutionMerger : ISolutionMerger
		{
			public String Path;

			public void Merge(String path)
			{
				this.Path = path;
			}
		}
	}
}