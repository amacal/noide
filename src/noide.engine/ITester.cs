using System;

namespace noide
{
	public interface ITester
	{
		IPackageReferenceCollection PackageReferences { get; }

		IResource<ITestingResult> Test(ITestable target);
	}

	public interface ITestingResult
	{
		bool IsSuccessful();

		ITestingCase[] GetCases();
	}

	public interface ITestingCase
	{
		String Name { get; }

		String[] GetRemarks();
	}
}