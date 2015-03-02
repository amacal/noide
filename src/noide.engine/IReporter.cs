namespace noide
{
	public interface IReporter
	{
		void Beat();

		void Trigger(IProject project);

		void BeginRestoring(int round, IPackage package);

		void CompleteRestoring(IPackage package, bool isSuccessful);

		void BeginCompiling(int round, IProject project);

		void CompleteCompiling(IProject project, bool isSuccessful);

		void BeginTesting(int round, IProject project);

		void CompleteTesting(IProject project, ITestingResult result);
	}
}