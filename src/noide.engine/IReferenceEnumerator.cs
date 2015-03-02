using System;

namespace noide
{
	public interface IReferenceEnumerator
	{
		String[] FindReferences(String package, String version);
	}
}