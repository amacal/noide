using System;
using System.Collections.Generic;
using System.Linq;

namespace noide.tests
{
	public class ProjectStub
	{
		public virtual String Type
		{
			get { return "library"; }
		}

		public virtual IReferenceCollection References
		{
			get { return new ReferenceCollection(); }
		}

		public virtual IProjectReferenceCollection ProjectReferences
		{
			get { return new ProjectReferenceCollection(); }
		}

		public virtual IPackageReferenceCollection PackageReferences
		{
			get { return new PackageReferenceCollection(); }
		}

		public void Update(IProjectReader reader)
		{
		}

		private class ReferenceCollection : IReferenceCollection
		{
			public IEnumerable<Reference> AsEnumerable()
			{
				return Enumerable.Empty<Reference>();
			}

			public bool Contains(Reference reference)
			{
				return false;
			}
		}

		private class ProjectReferenceCollection : IProjectReferenceCollection
		{
			public IEnumerable<ProjectReference> AsEnumerable()
			{
				return Enumerable.Empty<ProjectReference>();
			}

			public bool Contains(ProjectReference reference)
			{
				return false;
			}
		}

		private class PackageReferenceCollection : IPackageReferenceCollection
		{
			public IEnumerable<PackageReference> AsEnumerable()
			{
				return Enumerable.Empty<PackageReference>();
			}

			public bool Contains(PackageReference reference)
			{
				return false;
			}
		}
	}
}