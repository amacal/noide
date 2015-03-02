using System;
using System.Collections.Generic;

namespace noide
{
    partial class SolutionFactory
    {
        private class PackageRepository : IPackageCollection
        {
            private readonly String path;
            private readonly List<PackageInfo> items;
            
            public PackageRepository(String path)
            {
                this.path = path;
                this.items = new List<PackageInfo>();
            }
            
            public String Path
            {
                get { return this.path; }
            }
            
            public void AddPackages(IReadOnlyCollection<PackageInfo> packages)
            {
                this.items.AddRange(packages);
            }

            public IPackage GetPackage(String name, String version)
            {
                foreach (PackageInfo package in this.items)
                {
                    if (package.Name == name && package.Version == version)
                    {
                        return new Package(new PackageMetadata(System.IO.Path.Combine(this.path, name + "." + version), name, version));
                    }
                }
                
                return null;
            }
            
            public IReadOnlyCollection<String> GetReferences(String name, String version)
            {
                List<String> references = new List<String>();

                foreach (PackageInfo package in this.items)
                {
                    if (package.Name == name && package.Version == version)
                    {
                        foreach (String reference in package.GetReferences())
                        {
                            references.Add(System.IO.Path.Combine(this.path, name + "." + version, reference));
                        }
                    }
                }

                return references.AsReadOnly();
            }
        }
    }
}
