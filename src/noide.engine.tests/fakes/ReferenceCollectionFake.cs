using System.Collections.Generic;

namespace noide.tests
{
	public class ReferenceCollectionFake : IReferenceCollection
	{
		public int Count
		{
			get { return 0; }
		}

		public bool Contains(Reference reference)
		{
			return false;
		}

		public void Clear()
		{
		}

		public void Add(Reference reference)
		{
		}

		public IEnumerable<Reference> AsEnumerable()
		{
			return new Reference[0];
		}
	}
}