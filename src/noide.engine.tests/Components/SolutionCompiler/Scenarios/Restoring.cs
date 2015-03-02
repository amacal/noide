using NUnit.Framework;

namespace noide.tests
{
	partial class SolutionCompilerTests
	{
		[Test]
		public void WhenRestoringPackageItUsesPackageManagerToRestoreIt()
		{			
			PackageManagerFactory packageManagerFactory = new PackageManagerFactory();
			ProjectCompilerFactory compilerFactory = new ProjectCompilerFactory();
			ProjectTesterFactory testerFactory = new ProjectTesterFactory();
			ReporterFactory reporterFactory = new ReporterFactory();
			WaiterFactory waiterFactory = new WaiterFactory();

			Package package = new Package("NUnit");
			SolutionCompiler solutionCompiler = new SolutionCompiler(packageManagerFactory, compilerFactory, testerFactory, waiterFactory, reporterFactory);
			SolutionData solutionData = new SolutionData { Packages = new[] { package } };

			solutionCompiler.Compile(solutionData);

			Assert.That(packageManagerFactory.Instance.Restored, Has.Member(package));
		}
	}
}