using System.Collections.Generic;

namespace noide.tests
{
	public class ReferenceCollectionFake : IReferenceCollection
	{
		public virtual int Count
		{
			get { return 0; }
		}

		public virtual bool Contains(Reference reference)
		{
			return false;
		}

		public virtual void Clear()
		{
		}

		public virtual void Add(Reference reference)
		{
		}

		public virtual IEnumerable<Reference> AsEnumerable()
		{
			return new Reference[0];
		}
	}
}