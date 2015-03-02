using System;
using NUnit.Framework;
using Onlysharp;

namespace noide.tests
{
	[TestFixture]
	public class ProjectTesterFactoryTests
	{
		[Test]
		public void WhenGettingTesterItReturnsNotNull()
		{
			RunnerEnumerator runnerEnumerator = new RunnerEnumerator();
			PackageEnumerator packageEnumerator = new PackageEnumerator();
			ProjectTesterFactory factory = new ProjectTesterFactory(runnerEnumerator);

			IProjectTester result = factory.Create(packageEnumerator, "c:\\output");

			Assert.That(result, Is.Not.Null);
		}

		private class PackageEnumerator : IPackageEnumerator
		{
			public String[] FindReferences(String package, String version)
			{
				return new String[0];
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
			public ITester FindRunner(IPackageEnumerator packageEnumerator, IProject project)
			{
				return new Tester();
			}
		}

		private class Tester : ITester
		{
	        public IPackageReferenceCollection PackageReferences
	        {
	            get { return null; }
	        }

			public IResource<ITestingResult> Test(ITestable target)
			{
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