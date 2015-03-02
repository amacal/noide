using System;
using System.Collections.Generic;

namespace noide.tests
{
	partial class SolutionCompilerTests
	{
		private class ProjectCompilerFactory : IProjectCompilerFactory
		{
			public ProjectCompiler Instance;
			public ICollection<IProject> Failures = new IProject[0];

			public IProjectCompiler Create(IReferenceEnumerator referenceEnumerator, String output)
			{
				return this.Instance = new ProjectCompiler { Failures = this.Failures };
			}
		}
	}
}