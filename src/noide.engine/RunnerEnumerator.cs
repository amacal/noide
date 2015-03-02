namespace noide
{
	public class RunnerEnumerator : IRunnerEnumerator
	{
		private readonly IProcessFactory processFactory;
		private readonly IFileService fileService;

		public RunnerEnumerator(IProcessFactory processFactory, IFileService fileService)
		{
			this.processFactory = processFactory;
			this.fileService = fileService;
		}

		public ITester FindRunner(IPackageEnumerator packageEnumerator, IProject project)
		{
			foreach (PackageReference reference in project.PackageReferences.AsEnumerable())
			{
				if (reference.Name == "NUnit" && packageEnumerator.Contains("NUnit.Runners", reference.Version))
				{
					return new Runner(this.processFactory, this.fileService, packageEnumerator.GetPackage("NUnit.Runners", reference.Version));
				}
			}

			return null;
		}
	}
}