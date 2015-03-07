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
			get { return new ReferenceCollectionFake(); }
		}

		public virtual IProjectReferenceCollection ProjectReferences
		{
			get { return new ProjectReferenceCollectionFake(); }
		}

		public virtual IPackageReferenceCollection PackageReferences
		{
			get { return new PackageReferenceCollectionFake(); }
		}

		public void Update(IProjectReader reader)
		{
		}
	}
}