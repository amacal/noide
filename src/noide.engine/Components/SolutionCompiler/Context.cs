namespace noide
{
	partial class SolutionCompiler
	{
        private class Context : IContext
        {
            public int Round { get; set; }
            public IReporter Reporter { get; set; }
            public IProcessingSchedule Schedule { get; set; }
            public IWaiter<ITask> Waiter { get; set; }
            public IProjectCompiler Compiler { get; set; }
            public IProjectTester Tester { get; set; }
            public IPackageManager Packager { get; set; }
        }
	}
}