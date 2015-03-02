using System;

namespace noide
{
	public class ProjectCompilerFactory : IProjectCompilerFactory
	{
		private readonly ISourceEnumerator sourceEnumerator;
		private readonly ICompiler compiler;

		public ProjectCompilerFactory(ISourceEnumerator sourceEnumerator, ICompiler compiler)
		{
			this.sourceEnumerator = sourceEnumerator;
			this.compiler = compiler;
		}

		public IProjectCompiler Create(IReferenceEnumerator referenceEnumerator, String output)
		{
			return new ProjectCompiler(this.sourceEnumerator, referenceEnumerator, this.compiler, output);
		}
	}
}