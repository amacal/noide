using System;

namespace noide
{
	public interface IProject
	{
		ProjectMetadata Metadata { get; }

		String Type { get; }

		IReferenceCollection References { get; }

		IProjectReferenceCollection ProjectReferences { get; }

		IPackageReferenceCollection PackageReferences { get; }

		void Update(IProjectReader reader);
	}
}