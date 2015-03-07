using System.Collections.Generic;

namespace noide
{
    partial class ProjectFactory
    {
        private class ProjectReferenceCollection : IProjectReferenceCollection
        {
            private readonly ICollection<ProjectReference> references;

            public ProjectReferenceCollection(ICollection<ProjectReference> references)
            {
                this.references = references;
            }

            public int Count
            {
            	get { return this.references.Count; }
            }

            public bool Contains(ProjectReference reference)
            {
                return this.references.Contains(reference);
            }

            public void Clear()
            {
            	this.references.Clear();
            }

            public void Add(ProjectReference reference)
            {
            	this.references.Add(reference);
            }

            public IEnumerable<ProjectReference> AsEnumerable()
            {
                return this.references;
            }
        }
    }
}