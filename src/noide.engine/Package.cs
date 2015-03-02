using System;

namespace noide
{
    public class Package : IPackage
    {
        private readonly PackageMetadata metadata;
        
        public Package(PackageMetadata metadata)
        {
            this.metadata = metadata;
        }

        public PackageMetadata Metadata
        {
            get { return this.metadata; }
        }

        public String FindFile(IFileService fileService, String filename)
        {
        	return fileService.FindFile(this.metadata.Path, filename);
        }
    }
}
