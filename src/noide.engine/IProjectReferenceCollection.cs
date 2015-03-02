using System.Collections.Generic;

namespace noide
{
	public interface IProjectReferenceCollection
	{
		IEnumerable<ProjectReference> AsEnumerable();

		bool Contains(ProjectReference reference);
	}
}