using System;

namespace noide.tests
{
	partial class ProjectFactoryTests
	{
		private class ProjectReader : IProjectReader
		{
			public String Path;

			public void Update(ProjectConfigurer configurer)
			{
				this.Path = configurer.Metadata.Path;

				configurer.AddReference(new Reference("System"));
				configurer.AddProject(new ProjectReference("MyProject"));
				configurer.AddPackage(new PackageReference("NUnit", "2.6.3"));
			}
		}
	}
}