using System;
using System.Collections.Generic;

namespace noide
{
	public interface ICompilable
	{
		String Name { get; }

		String Output { get; }

		String Type { get; }

		IReadOnlyCollection<String> GetSources();

		IReadOnlyCollection<String> GetReferences();

		IReadOnlyCollection<String> GetResources();
	}
}