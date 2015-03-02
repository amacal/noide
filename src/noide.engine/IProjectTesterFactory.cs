using System;

namespace noide
{
	public interface IProjectTesterFactory
	{
		IProjectTester Create(IPackageEnumerator packageEnumerator, String output);
	}
}