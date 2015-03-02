using System;

namespace noide.tests
{
	partial class SolutionCompilerTests
	{
		private class Package : IPackage
		{
			private readonly String name;

			public Package(String name)
			{
				this.name = name;
			}

			public PackageMetadata Metadata
			{
				get { return new PackageMetadata("c:\\packages", this.name, "2.6.3"); }
			}

			public String FindFile(IFileService fileService, String filename)
			{
				return null;
			}
		}
	}
}