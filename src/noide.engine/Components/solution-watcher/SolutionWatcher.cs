using System;

namespace noide
{
	public partial class SolutionWatcher : ISolutionWatcher
	{
		private readonly ISolutionFactory solutionFactory;
		private readonly IWatcherFactory watcherFactory;
		private readonly ISolutionCompiler solutionCompiler;
        private readonly IReporterFactory reporterFactory;
        private readonly IProjectReader projectReader;

		public SolutionWatcher(
			ISolutionFactory solutionFactory, 
			IWatcherFactory watcherFactory,
			ISolutionCompiler solutionCompiler,
            IReporterFactory reporterFactory,
            IProjectReader projectReader)
		{
			this.solutionFactory = solutionFactory;
			this.watcherFactory = watcherFactory;
			this.solutionCompiler = solutionCompiler;
            this.reporterFactory = reporterFactory;
            this.projectReader = projectReader;
		}

		public void Watch(IStop stop, String path)
		{
			ISolution solution = this.solutionFactory.Create(path);
            IHeartBeatFilter<IProject> filter = new HeartBeatFilter();
			IWatcher<IProject> watcher = this.watcherFactory.Create<IProject>(filter);
            IReporter reporter = this.reporterFactory.Create();

            foreach (IProject project in solution.Projects)
            {
                watcher.Add(new Watchable(project));
            }
            
            solution.Compile(this.solutionCompiler);
            
            while (true)
            {
                IHeartBeat<IProject> beat = watcher.Next();

                if (beat.IsSuccessful == true)
                {
                    reporter.Trigger(beat.Payload);
                    beat.Payload.Update(this.projectReader);
                    solution.Compile(this.solutionCompiler, beat.Payload);
                }

                if (stop.IsRequested() == true)
                {
                    break;
                }

                reporter.Beat();
            }
		}
   	}
}