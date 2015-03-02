using System;
using System.Collections.Generic;

namespace noide
{
	public interface ITestable
	{
		String Name { get; }

		String Path { get; }

		IReadOnlyCollection<String> GetReferences();
	}
}