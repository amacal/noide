using System.Collections.Generic;

namespace noide.tests
{
	public class PackageReferenceCollectionFake : IPackageReferenceCollection
	{
		public int Count
		{
			get { return 0; }
		}

		public bool Contains(PackageReference reference)
		{
			return false;
		}

		public void Clear()
		{
		}

		public void Add(PackageReference reference)
		{
		}

		public IEnumerable<PackageReference> AsEnumerable()
		{
			return new PackageReference[0];
		}
	}
}