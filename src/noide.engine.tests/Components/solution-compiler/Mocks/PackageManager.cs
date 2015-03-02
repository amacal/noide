using System.Collections.Generic;

namespace noide.tests
{
	partial class SolutionCompilerTests
	{
		private class PackageManager : IPackageManager
		{
			public ICollection<IPackage> Restored = new List<IPackage>();

			public IResource Restore(IPackage package)
			{
				this.Restored.Add(package);

				return new Resource();
			}
		}
	}
}