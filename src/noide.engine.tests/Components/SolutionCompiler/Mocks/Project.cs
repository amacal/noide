using System;

namespace noide.tests
{
	partial class SolutionCompilerTests
	{
		private class Project : ProjectStub, IProject
		{
			public const String DefaultName = "abc";
			public const String DefaultPath = "c:\\projects\\abc";

			public ProjectMetadata Metadata
			{
				get { return new ProjectMetadata(DefaultPath, DefaultName); }
			}
		}
	}
}