using System;
using System.Collections.Generic;
using System.Linq;

namespace noide.tests
{
	public class ProjectReferenceCollectionStub : IProjectReferenceCollection
	{
		private readonly List<ProjectReference> references;

		public ProjectReferenceCollectionStub(params String[] references)
		{
			this.references = new List<ProjectReference>();

			foreach (String reference in references)
			{
				this.references.Add(new ProjectReference(reference));
			}
		}

		public ProjectReferenceCollectionStub(ProjectReference[] references)
		{
			this.references = references.ToList();
		}

		public virtual int Count
		{
			get { return this.references.Count; }
		}

		public virtual bool Contains(ProjectReference reference)
		{
			return this.references.Contains(reference);
		}

		public virtual void Clear()
		{
			this.references.Clear();
		}

		public virtual void Add(ProjectReference reference)
		{
			this.references.Add(reference);
		}

		public virtual IEnumerable<ProjectReference> AsEnumerable()
		{
			return this.references;
		}
	}
}