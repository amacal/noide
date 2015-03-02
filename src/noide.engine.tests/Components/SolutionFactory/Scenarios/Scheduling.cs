using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace noide.tests
{
	partial class SolutionTests
	{
		[Test]
		public void WhenCompilingIndependentProjectsItPassesAllProjectsToCompiler()
		{
			SolutionCompiler compiler = new SolutionCompiler();
			SolutionReader reader = new SolutionReader();
			SolutionFactory factory = new SolutionFactory(reader);
			ISolution solution = factory.Create(Factory.DefaultPath);

			solution.Compile(compiler);

			Assert.That(compiler.Projects, Has.Length.EqualTo(2));
		}

		[Test]
		public void WhenCompilingDependentProjectsItPassesThemOneByOne()
		{
			IProject[] rounds = new IProject[2];

			SolutionCompiler compiler = new SolutionCompiler
			{
				CompileCallback = data =>
				{
					ISolutionSchedule schedule = data.Order();
					int round = 0;

					while (schedule.IsCompleted() == false && round < 2)
					{
						foreach (IProject project in schedule.GetProjects())
						{
							schedule.Succeed(project);
							rounds[round] = project;
						}

						round++;
					}
				}
			};

			SolutionReader reader = new SolutionReader
			{
				Projects = new[]
				{
					new Project("c:\\abc", "abc-name"),
					new Project("c:\\cde", "cde-name", new[]{ new ProjectReference("abc-name") })
				}
			};

			SolutionFactory factory = new SolutionFactory(reader);
			ISolution solution = factory.Create(Factory.DefaultPath);

			solution.Compile(compiler);

			Assert.That(rounds[0].Metadata.Name, Is.EqualTo("abc-name"));
			Assert.That(rounds[1].Metadata.Name, Is.EqualTo("cde-name"));
		}

		[Test]
		public void WhenCompilingProjectSatisfiedByPackageItSchedulesItInTheSecondRound()
		{
			IProject[] rounds = new IProject[2];

			SolutionCompiler compiler = new SolutionCompiler
			{
				CompileCallback = data =>
				{
					ISolutionSchedule schedule = data.Order();
					int round = 0;

					while (schedule.IsCompleted() == false && round < 2)
					{
						foreach (IPackage package in schedule.GetPackages())
						{
							schedule.Succeed(package);
						}

						foreach (IProject project in schedule.GetProjects())
						{
							schedule.Succeed(project);
							rounds[round] = project;
						}

						round++;
					}
				}
			};

			SolutionReader reader = new SolutionReader
			{
				Projects = new[]
				{
					new Project("c:\\abc", "abc-name", new[] { new PackageReference("NUnit", "2.6.3") })
				}
			};

			SolutionFactory factory = new SolutionFactory(reader);
			ISolution solution = factory.Create(Factory.DefaultPath);

			solution.Compile(compiler);

			Assert.That(rounds[0], Is.Null);
			Assert.That(rounds[1].Metadata.Name, Is.EqualTo("abc-name"));
		}

		[Test]
		public void WhenCompilingProjectNotSatisfiedByPackageItSchedulesNoProject()
		{
			IProject scheduled = null;

			SolutionCompiler compiler = new SolutionCompiler
			{
				CompileCallback = data =>
				{
					ISolutionSchedule schedule = data.Order();
					int round = 0;

					while (schedule.IsCompleted() == false && round < 2)
					{
						foreach (IPackage package in schedule.GetPackages())
						{
							schedule.Fail(package);
						}

						foreach (IProject project in schedule.GetProjects())
						{
							schedule.Succeed(project);
							scheduled = project;
						}

						round++;
					}
				}
			};

			SolutionReader reader = new SolutionReader
			{
				Projects = new[]
				{
					new Project("c:\\abc", "abc-name", new[] { new PackageReference("NUnit", "2.6.3") })
				}
			};

			SolutionFactory factory = new SolutionFactory(reader);
			ISolution solution = factory.Create(Factory.DefaultPath);

			solution.Compile(compiler);

			Assert.That(scheduled, Is.Null);
		}
	}
}
