using System;
using System.Collections.Generic;

namespace noide.tests
{
	partial class SolutionCompilerTests
	{
		private class ProjectTester : IProjectTester
		{
			public ICollection<IProject> Testable = new IProject[0];
			public ICollection<IProject> Projects = new List<IProject>();

			public bool IsTestable(IProject project)
			{
				return this.Testable.Contains(project);
			}

			public ITester GetRunner(IProject project)
			{
				return new Runner { Owner = this };
			}

			public IResource<ITestingResult> Test(ITester runner, IProject project)
			{
				this.Projects.Add(project);
				
				return new Resource();
			}
		}
	}
}