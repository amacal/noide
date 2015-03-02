using System;
using System.Collections.Generic;

namespace noide.tests
{
	partial class SolutionCompilerTests
	{
		private class ProjectTesterFactory : IProjectTesterFactory
		{
			public ProjectTester Instance;
			public ICollection<IProject> Testable = new IProject[0];

			public IProjectTester Create(IPackageEnumerator packageEnumerator, String output)
			{
				return this.Instance = new ProjectTester { Testable = this.Testable };
			}
		}
	}
}