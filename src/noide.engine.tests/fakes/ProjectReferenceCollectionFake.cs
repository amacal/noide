using System.Collections.Generic;

namespace noide.tests
{
	public class ProjectReferenceCollectionFake : IProjectReferenceCollection
	{
		public virtual int Count
		{
			get { return 0; }
		}

		public virtual bool Contains(ProjectReference reference)
		{
			return false;
		}

		public virtual void Clear()
		{
		}

		public virtual void Add(ProjectReference reference)
		{
		}

		public virtual IEnumerable<ProjectReference> AsEnumerable()
		{
			return new ProjectReference[0];
		}
	}
}