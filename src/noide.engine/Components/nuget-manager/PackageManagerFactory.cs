namespace noide
{
	public partial class NugetManagerFactory : IPackageManagerFactory
	{
		private readonly IProcessFactory processFactory;
		private readonly IFileService fileService;

		public NugetManagerFactory(IProcessFactory processFactory, IFileService fileService)
		{
			this.processFactory = processFactory;
			this.fileService = fileService;
		}

		public IPackageManager Create()
		{
			return new NugetManager(this.processFactory, this.fileService);
		}
	}
}