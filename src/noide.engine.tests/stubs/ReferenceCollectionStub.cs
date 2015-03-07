using System;
using System.Collections.Generic;
using System.Linq;

namespace noide.tests
{
	public class ReferenceCollectionStub : IReferenceCollection
	{
		private readonly List<String> references;

		public ReferenceCollectionStub(params String[] references)
		{
			this.references = references.ToList();
		}

		public virtual int Count
		{
			get { return this.references.Count; }
		}

		public virtual bool Contains(Reference reference)
		{
			return this.references.Contains(reference.Name);
		}

		public virtual void Clear()
		{
			this.references.Clear();
		}

		public virtual void Add(Reference reference)
		{
			this.references.Add(reference.Name);
		}

		public virtual IEnumerable<Reference> AsEnumerable()
		{
			foreach (String reference in this.references)
			{
				yield return new Reference(reference);
			}
		}
	}
}