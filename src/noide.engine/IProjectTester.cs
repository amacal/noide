namespace noide
{
	public interface IProjectTester
	{
		bool IsTestable(IProject project);

		ITester GetRunner(IProject project);

		IResource<ITestingResult> Test(ITester runner, IProject project);
	}
}