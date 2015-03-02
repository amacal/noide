using NUnit.Framework;

namespace noide.tests
{
	partial class SolutionCompilerTests
	{		
		[Test]
		public void WhenProjectIsSuccessfullyCompiledItIsTested()
		{
			Project project = new Project();

			PackageManagerFactory packageManagerFactory = new PackageManagerFactory();
			ProjectCompilerFactory compilerFactory = new ProjectCompilerFactory();
			ProjectTesterFactory testerFactory = new ProjectTesterFactory { Testable = new[] { project } };
			ReporterFactory reporterFactory = new ReporterFactory();
			WaiterFactory waiterFactory = new WaiterFactory();

			Package package = new Package("NUnit.Runners");
			SolutionCompiler solutionCompiler = new SolutionCompiler(packageManagerFactory, compilerFactory, testerFactory, waiterFactory, reporterFactory);
			SolutionData solutionData = new SolutionData { Projects = new[] { project }, Packages = new[] { package } };

			solutionCompiler.Compile(solutionData);

			Assert.That(testerFactory.Instance.Projects, Contains.Item(project));			
		}

		[Test]
		public void WhenProjectIsNotSuccessfullyCompiledItIsNotTested()
		{
			Project project = new Project();

			PackageManagerFactory packageManagerFactory = new PackageManagerFactory();
			ProjectCompilerFactory compilerFactory = new ProjectCompilerFactory { Failures = new[] { project } };
			ProjectTesterFactory testerFactory = new ProjectTesterFactory { Testable = new[] { project } };
			ReporterFactory reporterFactory = new ReporterFactory();
			WaiterFactory waiterFactory = new WaiterFactory();

			SolutionCompiler solutionCompiler = new SolutionCompiler(packageManagerFactory, compilerFactory, testerFactory, waiterFactory, reporterFactory);
			SolutionData solutionData = new SolutionData { Projects = new[] { project } };

			solutionCompiler.Compile(solutionData);

			Assert.That(testerFactory.Instance.Projects, Has.No.Member(project));
		}

		[Test]
		public void WhenProjectIsNotTestableItIsNotTested()
		{			
			PackageManagerFactory packageManagerFactory = new PackageManagerFactory();
			ProjectCompilerFactory compilerFactory = new ProjectCompilerFactory();
			ProjectTesterFactory testerFactory = new ProjectTesterFactory();
			ReporterFactory reporterFactory = new ReporterFactory();
			WaiterFactory waiterFactory = new WaiterFactory();

			Project project = new Project();
			SolutionCompiler solutionCompiler = new SolutionCompiler(packageManagerFactory, compilerFactory, testerFactory, waiterFactory, reporterFactory);
			SolutionData solutionData = new SolutionData { Projects = new[] { project } };

			solutionCompiler.Compile(solutionData);

			Assert.That(testerFactory.Instance.Projects, Has.No.Member(project));
		}
	}
}