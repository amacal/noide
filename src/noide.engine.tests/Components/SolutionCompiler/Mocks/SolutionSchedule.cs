using System;
using System.Collections.Generic;
using System.Linq;

namespace noide.tests
{
	partial class SolutionCompilerTests
	{
		private class SolutionSchedule : ISolutionSchedule
		{
			public ICollection<IProject> Projects = new IProject[0];
			public ICollection<IPackage> Packages = new IPackage[0];
			public ICollection<IProject> Completed = new HashSet<IProject>();

			public bool IsCompleted()
			{
				return this.Projects.Count == 0 && this.Packages.Count == 0;
			}

			public void Succeed(IPackage package)
			{
			}

			public void Fail(IPackage package)
			{
			}

			public void Succeed(IProject project)
			{
				this.Completed.Add(project);
			}

			public void Fail(IProject project)
			{
				this.Completed.Add(project);
			}

			public IReadOnlyCollection<IPackage> GetPackages()
			{
				IPackage[] packages = this.Packages.ToArray();
				this.Packages = new IPackage[0];
				return packages;
			}

			public IReadOnlyCollection<IProject> GetProjects()
			{
				IProject[] projects = this.Projects.ToArray();
				this.Projects = new IProject[0];
				return projects;
			}
		}
	}
}