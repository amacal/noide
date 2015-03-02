using System;

namespace noide
{
	public interface IPackage
	{
		PackageMetadata Metadata { get; }

		String FindFile(IFileService fileService, String filename);
	}
}