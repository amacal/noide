using System;

namespace noide.tests
{
	partial class ProjectFactoryTests
	{
		private class ProjectReader : IProjectReader
		{
			public String Path;

			public void Update(IProject project)
			{
				this.Path = project.Metadata.Path;

				project.References.Add(new Reference("System"));
				project.ProjectReferences.Add(new ProjectReference("MyProject"));
				project.PackageReferences.Add(new PackageReference("NUnit", "2.6.3"));
			}
		}
	}
}