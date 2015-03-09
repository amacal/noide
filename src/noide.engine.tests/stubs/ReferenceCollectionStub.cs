using System;
using System.Collections.Generic;
using System.Linq;

namespace noide.tests
{
	public class ReferenceCollectionStub : IReferenceCollection
	{
		private readonly List<Reference> references;

		public ReferenceCollectionStub(params String[] references)
		{
			this.references = new List<Reference>();

			foreach (String reference in references)
			{
				this.references.Add(new Reference(reference));
			}
		}

		public int Count
		{
			get { return this.references.Count; }
		}

		public bool Contains(Reference reference)
		{
			return this.references.Contains(reference);
		}

		public void Clear()
		{
			this.references.Clear();
		}

		public void Add(Reference reference)
		{
			this.references.Add(reference);
		}

		public IEnumerable<Reference> AsEnumerable()
		{
			return this.references;
		}
	}
}