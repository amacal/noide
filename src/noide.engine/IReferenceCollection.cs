using System.Collections.Generic;

namespace noide
{
	public interface IReferenceCollection
	{
		IEnumerable<Reference> AsEnumerable();

		bool Contains(Reference reference);
	}
}