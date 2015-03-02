namespace noide
{
	public class PackageManagerFactory : IPackageManagerFactory
	{
		private readonly IProcessFactory processFactory;
		private readonly IFileService fileService;

		public PackageManagerFactory(IProcessFactory processFactory, IFileService fileService)
		{
			this.processFactory = processFactory;
			this.fileService = fileService;
		}

		public IPackageManager Create()
		{
			return new PackageManager(this.processFactory, this.fileService);
		}
	}
}