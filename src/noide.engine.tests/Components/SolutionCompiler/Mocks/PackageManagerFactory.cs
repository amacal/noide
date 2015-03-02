namespace noide.tests
{
	partial class SolutionCompilerTests
	{
		private class PackageManagerFactory : IPackageManagerFactory
		{
			public PackageManager Instance;

			public IPackageManager Create()
			{
				return this.Instance = new PackageManager();
			}
		}
	}
}