using System;
using System.Collections.Generic;

namespace noide
{
	public interface ISolutionData
	{
		String Output { get; }

		ISolutionSchedule Order();

		IPackage GetPackage(String name, String version);

		IReadOnlyCollection<String> GetReferences(String name, String version);
	}
}