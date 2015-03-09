using System;
using System.Collections.Generic;
using System.Linq;

namespace noide.tests
{
	public class ProjectStub : IProject
	{
		private readonly IProjectMetadata metadata;
		private readonly IReferenceCollection references;
		private readonly IProjectReferenceCollection projects;
		private readonly IPackageReferenceCollection packages;

		public ProjectStub()
		{
			this.metadata = new ProjectMetadataFake();
			this.references = new ReferenceCollectionStub();
			this.projects = new ProjectReferenceCollectionStub();
			this.packages = new PackageReferenceCollectionStub();
		}

		public ProjectStub(String path, String name)
		{
			this.metadata = new ProjectMetadataStub(path, name);
			this.references = new ReferenceCollectionStub();
			this.projects = new ProjectReferenceCollectionStub();
			this.packages = new PackageReferenceCollectionStub();
		}

		public IProjectMetadata Metadata
		{
			get { return this.metadata; }
		}

		public IReferenceCollection References
		{
			get { return this.references; }
		}

		public IProjectReferenceCollection ProjectReferences
		{
			get { return this.projects; }
		}

		public IPackageReferenceCollection PackageReferences
		{
			get { return this.packages; }
		}
	}
}