using System;

namespace noide
{
	public interface IPackageEnumerator : IReferenceEnumerator
	{	
		bool Contains(String package, String version);

		IPackage GetPackage(String package, String version);
	}
}