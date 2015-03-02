using System;

namespace noide.tests
{
	partial class SolutionTests
	{
		private class SolutionReader : ISolutionReader
		{
			public String Path;

			public IProject[] Projects = new[]
			{
				new Project("c:\\abc", "abc-name"),
				new Project("c:\\cde", "cde-name")
			};

			public PackageInfo[] Packages = new[]
			{
				new PackageInfo("NUnit", "2.6.3", new String[0])
			};

			public void Update(SolutionConfigurer configurer)
			{
				this.Path = configurer.Metadata.Path;

				configurer.SetOutput("output");			
				configurer.SetProjects("sources", this.Projects);
				configurer.SetPackages("packages", this.Packages);
			}
		}
	}
}