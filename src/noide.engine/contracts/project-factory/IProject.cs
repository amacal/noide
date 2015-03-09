using System;

namespace noide
{
	public interface IProject
	{
		IProjectMetadata Metadata { get; }

		IReferenceCollection References { get; }

		IProjectReferenceCollection ProjectReferences { get; }

		IPackageReferenceCollection PackageReferences { get; }
	}
}