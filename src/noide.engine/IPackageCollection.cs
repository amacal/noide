using System;

namespace noide
{
	public interface IPackageCollection
	{
		IPackage GetPackage(String name, String version);
	}
}