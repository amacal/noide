using System.Collections.Generic;

namespace noide
{
	public interface IPackageReferenceCollection
	{
		int Count { get; }

		bool Contains(PackageReference reference);

		void Clear();

		void Add(PackageReference reference);

		IEnumerable<PackageReference> AsEnumerable();
	}
}