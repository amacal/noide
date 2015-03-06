namespace noide
{
	public class Bootstrapper
	{
		private IFileService CreateFileService()
		{
			return new FileService();
		}

		private IJsonReader CreateJsonReader()
		{
			return new JsonReader();
		}

		private IProjectEnumerator CreateProjectEnumerator()
		{
			return new ProjectEnumerator();
		}

		private INative CreateNative()
		{
			return new Native();
		}

		private ISourceEnumerator CreateSourceEnumerator()
		{
			return new SourceEnumerator();
		}

		private IReporterFactory CreateReporterFactory()
		{
			return new ConsoleReporterFactory();
		}

		public IArgumentParser CreateArgumentParser()
		{
			return new ArgumentParser();
		}

		private IProjectReader CreateProjectReader()
		{
			return 
				new ProjectReader(
					this.CreateJsonReader());
		}

		private IProjectFactory CreateProjectFactory()
		{
			return
				new ProjectFactory(
					this.CreateProjectReader());
		}

		private ISolutionReader CreateSolutionReader()
		{
			return
				new SolutionReader(
					this.CreateJsonReader(),
					this.CreateProjectEnumerator(),
					this.CreateProjectFactory());
		}

		private ISolutionFactory CreateSolutionFactory()
		{
			return
				new SolutionFactory(
					this.CreateSolutionReader());
		}

		private IWaiterFactory CreateWaiterFactory()
		{
			return
				new WaiterFactory(
					this.CreateNative());
		}

		private IWatcherFactory CreateWatcherFactory()
		{
			return
				new WatcherFactory(
					this.CreateNative(),
					this.CreateWaiterFactory());
		}

		private IProcessFactory CreateProcessFactory()
		{
			return
				new ProcessFactory(
					this.CreateNative());
		}

		private ICompiler CreateCompiler()
		{
			return
				new Compiler(
					this.CreateProcessFactory());
		}

		private IProjectCompilerFactory CreateProjectCompilerFactory()
		{
			return
				new ProjectCompilerFactory(
					this.CreateSourceEnumerator(),
					this.CreateCompiler());
		}

		private IRunnerEnumerator CreateRunnerEnumerator()
		{
			return
				new RunnerEnumerator(
					this.CreateProcessFactory(),
					this.CreateFileService());
		}

		private IProjectTesterFactory CreateProjectTesterFactory()
		{
			return
				new ProjectTesterFactory(
					this.CreateRunnerEnumerator());
		}

		private IPackageManagerFactory CreatePackageManagerFactory()
		{
			return
				new NugetManagerFactory(
					this.CreateProcessFactory(),
					this.CreateFileService());
		}

		private ISolutionCompiler CreateSolutionCompiler()
		{
			return
				new SolutionCompiler(
					this.CreatePackageManagerFactory(),
					this.CreateProjectCompilerFactory(),
					this.CreateProjectTesterFactory(),
					this.CreateWaiterFactory(),
					this.CreateReporterFactory());
		}

		private ISolutionWatcher CreateSolutionWatcher()
		{
			return
				new SolutionWatcher(
					this.CreateSolutionFactory(),
					this.CreateWatcherFactory(),
					this.CreateSolutionCompiler(),
					this.CreateReporterFactory(),
					this.CreateProjectReader());
		}

		private ISolutionMerger CreateSolutionMerger()
		{
			return
				new SolutionMerger(
					this.CreateSolutionFactory(),
					this.CreateSourceEnumerator());
		}

		public ICommandFactory CreateCommandFactory()
		{
			return
				new CommandFactory(
					this.CreateSolutionWatcher(),
					this.CreateSolutionMerger());
		}
	}
}