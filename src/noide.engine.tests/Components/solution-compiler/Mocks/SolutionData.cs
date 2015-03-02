using System;
using System.Collections.Generic;

namespace noide.tests
{
	partial class SolutionCompilerTests
	{
		private class SolutionData : ISolutionData
		{
			public ICollection<IProject> Projects = new IProject[0];
			public ICollection<IPackage> Packages = new IPackage[0];

			public String Output 
			{
				get { return "c:\\output"; }
			}

			public ISolutionSchedule Order()
			{
				return new SolutionSchedule
				{ 
					Projects = this.Projects,
					Packages = this.Packages
				};
			}

			public IPackage GetPackage(String name, String version)
			{
				return null;
			}

			public IReadOnlyCollection<String> GetReferences(String name, String version)
			{
				return new String[0];
			}
		}
	}
}