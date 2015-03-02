using System;
using System.Linq;

namespace noide
{
	partial class SolutionCompiler
	{
        private class PackageEnumerator : IPackageEnumerator
        {
            private readonly ISolutionData solutionData;

            public PackageEnumerator(ISolutionData solutionData)
            {
                this.solutionData = solutionData;
            }

            public String[] FindReferences(String package, String version)
            {
                return this.solutionData.GetReferences(package, version).ToArray();
            }

            public bool Contains(String package, String version)
            {
                return this.solutionData.GetPackage(package, version) != null;
            }

            public IPackage GetPackage(String package, String version)
            {
                return this.solutionData.GetPackage(package, version);
            }
        }
	}
}