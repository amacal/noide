using System.Collections.Generic;

namespace noide
{
	public interface IPackageReferenceCollection
	{
		IEnumerable<PackageReference> AsEnumerable();

		bool Contains(PackageReference reference);
	}
}