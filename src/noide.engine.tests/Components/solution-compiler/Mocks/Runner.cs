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
				get { return new References(); }
			}

			public IResource<ITestingResult> Test(ITestable target)
			{
				return new Resource();
			}

			private class References : IPackageReferenceCollection
			{
				public IEnumerable<PackageReference> AsEnumerable()
				{
					yield return new PackageReference("NUnit.Runners", "2.6.3");
				}

				public bool Contains(PackageReference reference)
				{
					return reference.Name == "NUnit.Runners"
					    && reference.Version == "2.6.3";
				}
			}
		}
	}
}