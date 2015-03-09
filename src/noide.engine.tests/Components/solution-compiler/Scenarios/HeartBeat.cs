using NUnit.Framework;

namespace noide.tests
{
	partial class SolutionCompilerTests
	{		
		[Test]
		public void WhenBeatingWaiterReturnsFalseItTriesSecondTime()
		{
			PackageManagerFactory packageManagerFactory = new PackageManagerFactory();
			ProjectCompilerFactory compilerFactory = new ProjectCompilerFactory();
			ProjectTesterFactory testerFactory = new ProjectTesterFactory();
			ReporterFactory reporterFactory = new ReporterFactory();
			WaiterFactory waiterFactory = new WaiterFactory { TimeOuts = 5 };

			ProjectFake project = new ProjectFake();
			SolutionCompiler solutionCompiler = new SolutionCompiler(packageManagerFactory, compilerFactory, testerFactory, waiterFactory, reporterFactory);
			SolutionData solutionData = new SolutionData { Projects = new[] { project } };

			solutionCompiler.Compile(solutionData);

			Assert.That(waiterFactory.Attempts, Is.EqualTo(6));
			Assert.That(compilerFactory.Instance.Projects, Contains.Item(project));
		}
	}
}