using NUnit.Framework;

namespace noide.tests
{
	partial class SolutionCompilerTests
	{		
		[Test]
		public void WhenCompilingEmptySolutionItCompilesNothing()
		{
			PackageManagerFactory packageManagerFactory = new PackageManagerFactory();
			ProjectCompilerFactory compilerFactory = new ProjectCompilerFactory();
			ProjectTesterFactory testerFactory = new ProjectTesterFactory();
			ReporterFactory reporterFactory = new ReporterFactory();
			WaiterFactory waiterFactory = new WaiterFactory();

			SolutionCompiler solutionCompiler = new SolutionCompiler(packageManagerFactory, compilerFactory, testerFactory, waiterFactory, reporterFactory);
			SolutionData solutionData = new SolutionData();

			solutionCompiler.Compile(solutionData);

			Assert.That(compilerFactory.Instance.Projects, Is.Empty);
		}

		[Test]
		public void WhenCompilingOneProjectSolutionItCompilesIt()
		{
			PackageManagerFactory packageManagerFactory = new PackageManagerFactory();
			ProjectCompilerFactory compilerFactory = new ProjectCompilerFactory();
			ProjectTesterFactory testerFactory = new ProjectTesterFactory();
			ReporterFactory reporterFactory = new ReporterFactory();
			WaiterFactory waiterFactory = new WaiterFactory();

			ProjectFake project = new ProjectFake();
			SolutionCompiler solutionCompiler = new SolutionCompiler(packageManagerFactory, compilerFactory, testerFactory, waiterFactory, reporterFactory);
			SolutionData solutionData = new SolutionData { Projects = new[] { project } };

			solutionCompiler.Compile(solutionData);

			Assert.That(compilerFactory.Instance.Projects, Contains.Item(project));
		}
	}
}