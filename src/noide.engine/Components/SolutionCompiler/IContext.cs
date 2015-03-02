namespace noide
{
	partial class SolutionCompiler
	{
		private interface IContext
        {
            int Round { get; }

            IReporter Reporter { get; }
            IProcessingSchedule Schedule { get; }
            IWaiter<ITask> Waiter { get; }

            IPackageManager Packager { get; }
            IProjectCompiler Compiler { get; }
            IProjectTester Tester { get; }
        }
	}
}