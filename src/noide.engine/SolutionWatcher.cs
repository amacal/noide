using System;
using System.Collections.Generic;
using System.Linq;

namespace noide
{
	public class SolutionWatcher : ISolutionWatcher
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

        private class Watchable : IWatchable<IProject>
        {
            private readonly IProject project;

            public Watchable(IProject project)
            {
                this.project = project;
            }

            public String Path
            {
                get { return this.project.Metadata.Path; }
            }

            public IProject Payload
            {
                get { return this.project; }
            }
        }

        private class HeartBeatFilter : IHeartBeatFilter<IProject>
        {
            private int total;
            private readonly HashSet<IProject> successful;

            public HeartBeatFilter()
            {
                this.total = 0;
                this.successful = new HashSet<IProject>();
            }

            public IHeartBeat<IProject> Filter(IHeartBeat<IProject> beat)
            {
                IHeartBeat<IProject> result = new HeartBeat();

                if (beat.IsSuccessful)
                {
                    this.successful.Add(beat.Payload);
                    this.total = 0;
                }

                if (this.total++ > 5)
                {
                    this.total = 0;

                    if (this.successful.Count > 0)
                    {
                        IProject project = this.successful.First();

                        result = new HeartBeat(project);
                        this.successful.Remove(project);
                    }
                }

                return result;
            }
        }

        private class HeartBeat : IHeartBeat<IProject>
        {
            private readonly IProject project;

            public HeartBeat()
            {
            }

            public HeartBeat(IProject project)
            {
                this.project = project;
            }

            public bool IsSuccessful
            {
                get { return this.project != null; }
            }

            public IProject Payload
            {
                get { return this.project; }
            }
        }
   	}
}