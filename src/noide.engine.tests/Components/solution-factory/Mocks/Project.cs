using System;
using System.Collections.Generic;
using System.Linq;

namespace noide.tests
{
	partial class SolutionTests
	{
		private class Project : ProjectStub, IProject
		{
			private readonly String path;
			private readonly String name;
			private readonly ProjectReference[] projects;
			private readonly PackageReference[] packages;

			public Project(String path, String name)
			{
				this.path = path;
				this.name = name;
				this.projects = new ProjectReference[0];
				this.packages = new PackageReference[0];
			}

			public Project(String path, String name, ProjectReference[] projects)
			{
				this.path = path;
				this.name = name;
				this.projects = projects;
				this.packages = new PackageReference[0];
			}

			public Project(String path, String name, PackageReference[] packages)
			{
				this.path = path;
				this.name = name;
				this.packages = packages;
				this.projects = new ProjectReference[0];
			}

			public ProjectMetadata Metadata
			{
				get { return new ProjectMetadata(this.path, this.name); }
			}

			public override IProjectReferenceCollection ProjectReferences
			{
				get { return new ProjectReferenceCollection(this.projects); }
			}

			public override IPackageReferenceCollection PackageReferences
			{
				get { return new PackageReferenceCollection(this.packages); }
			}

			private class ProjectReferenceCollection : IProjectReferenceCollection
			{
				private readonly ProjectReference[] references;

				public ProjectReferenceCollection(ProjectReference[] references)
				{
					this.references = references;
				}

				public IEnumerable<ProjectReference> AsEnumerable()
				{
					return this.references;
				}

				public bool Contains(ProjectReference reference)
				{
					return this.references.Contains(reference);
				}
			}

			private class PackageReferenceCollection : IPackageReferenceCollection
			{
				private readonly PackageReference[] references;

				public PackageReferenceCollection(PackageReference[] references)
				{
					this.references = references;
				}

				public IEnumerable<PackageReference> AsEnumerable()
				{
					return this.references;
				}

				public bool Contains(PackageReference reference)
				{
					return this.references.Contains(reference);
				}
			}
		}
	}
}