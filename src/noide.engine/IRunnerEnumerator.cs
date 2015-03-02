using System;

namespace noide
{
	public interface IRunnerEnumerator
	{
		ITester FindRunner(IPackageEnumerator packageEnumerator, IProject project);
	}
}