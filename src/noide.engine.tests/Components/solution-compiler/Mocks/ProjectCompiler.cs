using System;
using System.Collections.Generic;

namespace noide.tests
{
	partial class SolutionCompilerTests
	{
		private class ProjectCompiler : IProjectCompiler
		{
			public ICollection<IProject> Failures = new IProject[0];
			public ICollection<IProject> Projects = new List<IProject>();

			public IResource Compile(IProject project)
			{
				this.Projects.Add(project);

				return new Resource { Successful = this.Failures.Contains(project) == false };
			}
		}
	}
}