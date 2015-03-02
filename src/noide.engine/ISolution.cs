using System.Collections.Generic;

namespace noide
{
	public interface ISolution
	{
		SolutionMetadata Metadata { get; }

		IPackageCollection Packages { get; }

		IReadOnlyCollection<IProject> Projects { get; }

		void Compile(ISolutionCompiler compiler);

		void Compile(ISolutionCompiler compiler, IProject trigger);
	}
}