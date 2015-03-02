using System;
using System.Collections.Generic;
using System.Linq;

namespace noide.tests
{
	partial class SolutionTests
	{
		private class SolutionCompiler : ISolutionCompiler
		{
			public String Output;
			public IProject[] Projects;
			public Action<ISolutionData> CompileCallback;

			public SolutionCompiler()
			{
				CompileCallback = this.DefaultCompileCallback;
			}

			public void Compile(ISolutionData solutionData)
			{
				this.CompileCallback.Invoke(solutionData);
			}

			private void DefaultCompileCallback(ISolutionData solutionData)
			{
				this.Output = solutionData.Output;
				this.Projects = solutionData.Order().GetProjects().ToArray();
			}
		}
	}
}