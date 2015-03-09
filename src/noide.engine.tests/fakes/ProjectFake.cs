using System;
using System.Collections.Generic;
using System.Linq;

namespace noide.tests
{
	public class ProjectFake : IProject
	{
		public IProjectMetadata Metadata
		{
			get { return new ProjectMetadataFake(); }
		}

		public IReferenceCollection References
		{
			get { return new ReferenceCollectionFake(); }
		}

		public IProjectReferenceCollection ProjectReferences
		{
			get { return new ProjectReferenceCollectionFake(); }
		}

		public IPackageReferenceCollection PackageReferences
		{
			get { return new PackageReferenceCollectionFake(); }
		}
	}
}