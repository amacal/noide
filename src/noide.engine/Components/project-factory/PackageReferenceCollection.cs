using System.Collections.Generic;

namespace noide
{
    partial class ProjectFactory
    {
        private class PackageReferenceCollection : IPackageReferenceCollection
        {
            private readonly ICollection<PackageReference> references;

            public PackageReferenceCollection(ICollection<PackageReference> references)
            {
                this.references = references;
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
}