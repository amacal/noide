using System;

namespace noide
{
	public class ProjectTesterFactory : IProjectTesterFactory
	{
		private readonly IRunnerEnumerator runnerEnumerator;

		public ProjectTesterFactory(IRunnerEnumerator runnerEnumerator)
		{
			this.runnerEnumerator = runnerEnumerator;
		}

		public IProjectTester Create(IPackageEnumerator packageEnumerator, String output)
		{
			return new ProjectTester(packageEnumerator, this.runnerEnumerator, output);
		}
	}
}