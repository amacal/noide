using System;
using System.Collections.Generic;
using System.Linq;

namespace noide.tests
{
	public class PackageReferenceCollectionStub : IPackageReferenceCollection
	{
		private readonly List<PackageReference> references;

		public PackageReferenceCollectionStub(String name, String version)
		{
			this.references = new List<PackageReference>();
			this.references.Add(new PackageReference(name, version));
		}

		public PackageReferenceCollectionStub(params PackageReference[] references)
		{
			this.references = references.ToList();
		}

		public int Count
		{
			get { return this.references.Count; }
		}

		public bool Contains(PackageReference reference)
		{
			return this.references.Contains(reference);
		}

		public void Clear()
		{
			this.references.Clear();
		}

		public void Add(PackageReference reference)
		{
			this.references.Add(reference);
		}

		public IEnumerable<PackageReference> AsEnumerable()
		{
			return this.references;
		}
	}
}