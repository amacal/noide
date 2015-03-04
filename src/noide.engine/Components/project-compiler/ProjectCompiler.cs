using System;

namespace noide
{
	public partial class ProjectCompiler : IProjectCompiler
	{
		private readonly IReferenceEnumerator referenceEnumerator;
		private readonly ISourceEnumerator sourceEnumerator;
		private readonly ICompiler compiler;
		private readonly String output;

		public ProjectCompiler(ISourceEnumerator sourceEnumerator, IReferenceEnumerator referenceEnumerator, ICompiler compiler, String output)
		{
			this.referenceEnumerator = referenceEnumerator;
			this.sourceEnumerator = sourceEnumerator;
			this.compiler = compiler;
			this.output = output;
		}

		public IResource Compile(IProject project)
		{
			Target target = new Target(this.output, project, this.referenceEnumerator, this.sourceEnumerator);
			IResource resource = this.compiler.Compile(target);

			return resource;
		}
	}
}