using System.Collections.Generic;

namespace noide
{
	public interface IReferenceCollection
	{
		int Count { get; }

		bool Contains(Reference reference);

		void Clear();

		void Add(Reference reference);

		IEnumerable<Reference> AsEnumerable();
	}
}