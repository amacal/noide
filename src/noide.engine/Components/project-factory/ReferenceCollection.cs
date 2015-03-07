using System.Collections.Generic;

namespace noide
{
    partial class ProjectFactory
    {
        private class ReferenceCollection : IReferenceCollection
        {   
            private readonly ICollection<Reference> references;

            public ReferenceCollection(ICollection<Reference> references)
            {
                this.references = references;
            }

            public int Count
            {
            	get { return this.references.Count; }
            }

            public bool Contains(Reference reference)
            {
                return this.references.Contains(reference);
            }

            public void Clear()
            {
            	this.references.Clear();
            }

            public void Add(Reference reference)
            {
            	this.references.Add(reference);
            }

            public IEnumerable<Reference> AsEnumerable()
            {
                return this.references;
            }
        }
    }
}