using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using NUnit.Framework;
using Onlysharp;

namespace noide.tests
{
	[TestFixture]
	public class ProjectTesterTests
	{
		[Test]
		public void WhenCheckingTestabilityItReturnsTrue()
		{
			RunnerEnumerator runnerEnumerator = new RunnerEnumerator();
			PackageEnumerator packageEnumerator = new PackageEnumerator();

			Project project = new Project();
			ProjectTester projectTester = new ProjectTester(packageEnumerator, runnerEnumerator, Factory.DefaultOutput);

			bool testable = projectTester.IsTestable(project);

			Assert.That(testable, Is.True);
		}

		[Test]
		public void WhenTestingItPassesRightName()
		{
			RunnerEnumerator runnerEnumerator = new RunnerEnumerator();
			Tester runner = new Tester { Owner = runnerEnumerator };
			PackageEnumerator packageEnumerator = new PackageEnumerator();

			Project project = new Project();
			ProjectTester projectTester = new ProjectTester(packageEnumerator, runnerEnumerator, Factory.DefaultOutput);

			projectTester.Test(runner, project);

			Assert.That(runnerEnumerator.Name, Is.EqualTo("abc"));
		}

		[Test]
		public void WhenTestingItPassesAllProjectReferences()
		{			
			RunnerEnumerator runnerEnumerator = new RunnerEnumerator();
			Tester runner = new Tester { Owner = runnerEnumerator };
			PackageEnumerator packageEnumerator = new PackageEnumerator();

			Project project = new Project();
			ProjectTester projectTester = new ProjectTester(packageEnumerator, runnerEnumerator, Factory.DefaultOutput);

			projectTester.Test(runner, project);

			Assert.That(runnerEnumerator.References, Contains.Item(Factory.DefaultOutput + "\\MyProject.dll"));
		}

		[Test]
		public void WhenTestingItPassesAllPackageReferences()
		{			
			RunnerEnumerator runnerEnumerator = new RunnerEnumerator();
			Tester runner = new Tester { Owner = runnerEnumerator };
			PackageEnumerator packageEnumerator = new PackageEnumerator();

			Project project = new Project();
			ProjectTester projectTester = new ProjectTester(packageEnumerator, runnerEnumerator, Factory.DefaultOutput);

			projectTester.Test(runner, project);

			Assert.That(runnerEnumerator.References, Contains.Item(Factory.DefaultPackages + "\\NUnit-2.6.3\\net40\\nunit.framework.dll"));
		}

		[Test]
		public void WhenTestedItReturnsCreatedResource()
		{
			RunnerEnumerator runnerEnumerator = new RunnerEnumerator();
			Tester runner = new Tester { Owner = runnerEnumerator };
			PackageEnumerator packageEnumerator = new PackageEnumerator();

			Project project = new Project();
			ProjectTester projectTester = new ProjectTester(packageEnumerator, runnerEnumerator, Factory.DefaultOutput);

			IResource resource = projectTester.Test(runner, project);

			Assert.That(resource, Is.Not.Null);
		}

		private static class Factory
		{
			public const String DefaultOutput = "c:\\output";
			public const String DefaultPackages = "c:\\packages";
		}

		private class Project : IProject
		{
			public IProjectMetadata Metadata
			{
				get { return new ProjectMetadataStub("c:\\projects\\abc", "abc"); }
			}

			public IReferenceCollection References
			{
				get { return new ReferenceCollectionStub("System"); }
			}

			public IProjectReferenceCollection ProjectReferences
			{
				get { return new ProjectReferenceCollectionStub("MyProject"); }
			}

			public IPackageReferenceCollection PackageReferences
			{
				get { return new PackageReferenceCollectionStub("NUnit", "2.6.3"); }
			}
		}

		private class PackageEnumerator : IPackageEnumerator
		{
			public String[] FindReferences(String package, String version)
			{
				return new [] { Factory.DefaultPackages + "\\NUnit-2.6.3\\net40\\nunit.framework.dll" };
			}

			public bool Contains(String package, String version)
			{
				return false;
			}

			public IPackage GetPackage(String package, String version)
			{
				return null;
			}			
		}

		private class RunnerEnumerator : IRunnerEnumerator
		{
			public String Name;
			public String[] References;

			public ITester FindRunner(IPackageEnumerator packageEnumerator, IProject project)
			{
				return new Tester { Owner = this };
			}
		}

		private class Tester : ITester
		{
			public RunnerEnumerator Owner;

	        public IPackageReferenceCollection PackageReferences
	        {
	            get { return null; }
	        }

			public IResource<ITestingResult> Test(ITestable target)
			{
				this.Owner.Name = target.Name;
				this.Owner.References = target.GetReferences().ToArray();

				return new Resource();
			}
		}

		private class Resource : IResource<ITestingResult>
		{
			public IntPtr[] Handles
			{
				get { return new[] { new IntPtr(20) }; }
			}

            public bool Complete(IntPtr handle)
			{
				return false;
			}

			public bool IsSuccessful()
			{
				return false;
			}

			public void Release()
			{
			}

			public ITestingResult Payload
			{
				get { return new TestingResult(); }
			}
		}

		private class TestingResult : ITestingResult
		{
			public bool IsSuccessful()
			{
				return false;
			}

			public ITestingCase[] GetCases()
			{
				return new ITestingCase[0];
			}			
		}
	}
}