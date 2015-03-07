using System.Collections.Generic;

namespace noide
{
	public interface IProjectReferenceCollection
	{
		int Count { get; }

		bool Contains(ProjectReference reference);

		void Clear();

		void Add(ProjectReference reference);

		IEnumerable<ProjectReference> AsEnumerable();
	}
}