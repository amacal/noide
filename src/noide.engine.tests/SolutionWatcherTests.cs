using System;
using System.Collections.Generic;
using NUnit.Framework;
using Onlysharp;

namespace noide.tests
{
	[TestFixture]
	public class SolutionWatcherTests
	{
		[Test]
		public void WhenWatchingItCanBeStopped()
		{
			SolutionFactory solutionFactory = new SolutionFactory();
			WatcherFactory watcherFactory = new WatcherFactory();
			SolutionCompiler solutionCompiler = new SolutionCompiler();
			ReporterFactory reporterFactory = new ReporterFactory();
			ProjectReader projectReader = new ProjectReader();

			Stop stop = new Stop();
			SolutionWatcher solutionWatcher = new SolutionWatcher(solutionFactory, watcherFactory, solutionCompiler, reporterFactory, projectReader);

			solutionWatcher.Watch(stop, "c:\\projects\\abc");
		}

		[Test]
		public void WhenWatchingItCanBeStoppedAfterHundredIterations()
		{
			SolutionFactory solutionFactory = new SolutionFactory();
			WatcherFactory watcherFactory = new WatcherFactory();
			SolutionCompiler solutionCompiler = new SolutionCompiler();
			ReporterFactory reporterFactory = new ReporterFactory();
			ProjectReader projectReader = new ProjectReader();

			Stop stop = new Stop { Requested = 100 };
			SolutionWatcher solutionWatcher = new SolutionWatcher(solutionFactory, watcherFactory, solutionCompiler, reporterFactory, projectReader);

			solutionWatcher.Watch(stop, "c:\\projects\\abc");

			Assert.That(reporterFactory.Beats, Is.EqualTo(100));
		}

		[Test]
		public void WhenWatchingItCompilesSolutionFromScratch()
		{
			SolutionFactory solutionFactory = new SolutionFactory();
			WatcherFactory watcherFactory = new WatcherFactory();
			SolutionCompiler solutionCompiler = new SolutionCompiler();
			ReporterFactory reporterFactory = new ReporterFactory();
			ProjectReader projectReader = new ProjectReader();

			Stop stop = new Stop();
			SolutionWatcher solutionWatcher = new SolutionWatcher(solutionFactory, watcherFactory, solutionCompiler, reporterFactory, projectReader);

			solutionWatcher.Watch(stop, "c:\\projects\\abc");

			Assert.That(solutionFactory.CompiledTimes, Is.EqualTo(1));
		}

		private class SolutionFactory : ISolutionFactory
		{
			public int CompiledTimes;

			public ISolution Create(String path)
			{
				return new Solution { Parent = this };
			}
		}

		private class Solution : ISolution
		{
			public SolutionFactory Parent;

			public SolutionMetadata Metadata
			{
				get { return new SolutionMetadata("abc"); }
			}

			public IPackageCollection Packages
			{
				get { return null; }
			}

			public IReadOnlyCollection<IProject> Projects
			{
				get { return new IProject[0]; }
			}

			public void Compile(ISolutionCompiler compiler)
			{
				this.Parent.CompiledTimes++;
			}

			public void Compile(ISolutionCompiler compiler, IProject trigger)
			{
				this.Parent.CompiledTimes++;
			}
		}

		private class WatcherFactory : IWatcherFactory
		{
			public IWatcher<T> Create<T>(IHeartBeatFilter<T> filter)
			{
				return new Watcher<T>();
			}
		}

		private class Watcher<T> : IWatcher<T>
		{
			public void Add(IWatchable<T> target)
			{
			}

			public IHeartBeat<T> Next()
			{
				return new HeartBeat<T>();
			}
		}

		private class HeartBeat<T> : IHeartBeat<T>
		{
			public bool IsSuccessful { get; set; }

			public T Payload { get; set; }
		}

		private class SolutionCompiler : ISolutionCompiler
		{
			public void Compile(ISolutionData solutionData)
			{
			}
		}

		private class ReporterFactory : IReporterFactory
		{
			public int Beats;

			public IReporter Create()
			{
				return new Reporter { Parent = this };
			}
		}

		private class Reporter : IReporter
		{
			public ReporterFactory Parent;

			public void Beat()
			{
				this.Parent.Beats++;
			}

			public void Trigger(IProject project)
			{
			}

			public void BeginRestoring(int round, IPackage package)
			{
			}

			public void CompleteRestoring(IPackage package, bool isSuccessful)
			{
			}

			public void BeginCompiling(int round, IProject project)
			{
			}

			public void CompleteCompiling(IProject project, bool isSuccessful)
			{
			}

			public void BeginTesting(int round, IProject project)
			{
			}

			public void CompleteTesting(IProject project, ITestingResult result)
			{
			}
		}

		private class ProjectReader : IProjectReader
		{
			public void Update(ProjectConfigurer configurer)
			{
			}
		}

		private class Stop : IStop
		{
			public int Requested;
			public int Called;

			public bool IsRequested()
			{
				return this.Requested == this.Called++;
			}
		}
	}
}