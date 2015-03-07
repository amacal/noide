using System.Collections.Generic;

namespace noide.tests
{
	partial class SolutionCompilerTests
	{
		private class Runner : ITester
		{
			public ProjectTester Owner;

			public IPackageReferenceCollection PackageReferences
			{
				get { return new PackageReferenceCollectionStub("NUnit.Runners", "2.6.3"); }
			}

			public IResource<ITestingResult> Test(ITestable target)
			{
				return new Resource();
			}
		}
	}
}