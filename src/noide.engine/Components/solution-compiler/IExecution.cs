namespace noide
{
	partial class SolutionCompiler
	{
		private interface IExecution : IExecutable
		{
			IPackageReferenceCollection PackageReferences { get; }
		}
	}
}