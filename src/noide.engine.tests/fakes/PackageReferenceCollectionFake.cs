using System.Collections.Generic;

namespace noide.tests
{
	public class PackageReferenceCollectionFake : IPackageReferenceCollection
	{
		public virtual int Count
		{
			get { return 0; }
		}

		public virtual bool Contains(PackageReference reference)
		{
			return false;
		}

		public virtual void Clear()
		{
		}

		public virtual void Add(PackageReference reference)
		{
		}

		public virtual IEnumerable<PackageReference> AsEnumerable()
		{
			return new PackageReference[0];
		}
	}
}