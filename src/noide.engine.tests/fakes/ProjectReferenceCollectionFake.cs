using System.Collections.Generic;

namespace noide.tests
{
	public class ProjectReferenceCollectionFake : IProjectReferenceCollection
	{
		public int Count
		{
			get { return 0; }
		}

		public bool Contains(ProjectReference reference)
		{
			return false;
		}

		public void Clear()
		{
		}

		public void Add(ProjectReference reference)
		{
		}

		public IEnumerable<ProjectReference> AsEnumerable()
		{
			return new ProjectReference[0];
		}
	}
}