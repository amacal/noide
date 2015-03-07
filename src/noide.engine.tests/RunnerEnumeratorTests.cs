using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using NUnit.Framework;
using Onlysharp;

namespace noide.tests
{
	[TestFixture]
	public class RunnerEnumeratorTests
	{
		[Test]
		public void WhenFindingRunnerItAsksForRightPackageAndFile()
		{
			ProcessFactory processFactory = new ProcessFactory();
			PackageEnumerator packageEnumerator = new PackageEnumerator();
			FileService fileService = new FileService();
			RunnerEnumerator runnerEnumerator = new RunnerEnumerator(processFactory, fileService);
			Project project = new Project();

			runnerEnumerator.FindRunner(packageEnumerator, project);

			Assert.That(packageEnumerator.Package, Is.EqualTo("NUnit.Runners"));
			Assert.That(packageEnumerator.Version, Is.EqualTo("2.6.3"));
		}

		[Test]
		public void WhenFindingAvailableRunnerItReturnsIt()
		{
			ProcessFactory processFactory = new ProcessFactory();
			PackageEnumerator packageEnumerator = new PackageEnumerator();
			FileService fileService = new FileService();
			RunnerEnumerator runnerEnumerator = new RunnerEnumerator(processFactory, fileService);
			Project project = new Project();

			ITester runner = runnerEnumerator.FindRunner(packageEnumerator, project);

			Assert.That(runner, Is.Not.Null);
		}

		[Test]
		public void WhenFindingUnavailableRunnerItReturnsNull()
		{
			ProcessFactory processFactory = new ProcessFactory();
			PackageEnumerator packageEnumerator = new PackageEnumerator();
			FileService fileService = new FileService();
			RunnerEnumerator runnerEnumerator = new RunnerEnumerator(processFactory, fileService);
			PackageReference reference = new PackageReference("NUnit", "2.6.4");
			Project project = new Project(reference);

			ITester runner = runnerEnumerator.FindRunner(packageEnumerator, project);

			Assert.That(runner, Is.Null);
		}

		[Test]
		public void WhenRunningFoundRunnerCallsFoundFile()
		{
			ProcessFactory processFactory = new ProcessFactory();
			PackageEnumerator packageEnumerator = new PackageEnumerator();
			FileService fileService = new FileService();
			RunnerEnumerator runnerEnumerator = new RunnerEnumerator(processFactory, fileService);
			Project project = new Project();

			ITester runner = runnerEnumerator.FindRunner(packageEnumerator, project);
			Testable testable = new Testable();

			runner.Test(testable);

			Assert.That(processFactory.CommandLine[0], Is.EqualTo(Factory.RunnerCommand));
		}

		private static class Factory
		{
			public const String DefaultOutput = "c:\\output";
			public const String RunnerCommand = "c:\\packages\\NUnit.Runners-2.6.3\\tools\\nunit-console.exe";
		}

		private class Project : ProjectStub, IProject
		{
			public const String DefaultName = "abc";
			public const String DefaultPath = "c:\\projects\\abc";

			private readonly PackageReference package;

			public Project()
			{
				this.package = new PackageReference("NUnit", "2.6.3");
			}

			public Project(PackageReference package)
			{
				this.package = package;
			}

			public ProjectMetadata Metadata
			{
				get { return new ProjectMetadata(DefaultPath, DefaultName); }
			}

			public override IPackageReferenceCollection PackageReferences
			{
				get { return new PackageReferenceCollectionStub(this.package); }
			}
		}

		private class PackageEnumerator : IPackageEnumerator
		{
			public String Package;
			public String Version;

			public String[] FindReferences(String package, String version)
			{
				return new String[0];
			}

			public bool Contains(String package, String version)
			{
				return package == "NUnit.Runners" && version == "2.6.3";
			}

			public IPackage GetPackage(String package, String version)
			{
				this.Package = package;
				this.Version = version;

				return new Package();
			}			
		}

		private class Package : IPackage
		{
			public PackageMetadata Metadata
			{
				get { return new PackageMetadata("c:\\", "NUnit.Runners", "2.6.3"); }
			}

			public String FindFile(IFileService fileService, String filename)
			{
				return Factory.RunnerCommand;
			}
		}

		private class FileService : IFileService
		{
			public bool Exists(String path)
			{
				return false;
			}

			public String FindFile(String path, String filename)
			{
				return null;
			}
		}

		private class ProcessFactory : IProcessFactory
		{
			public String WorkingDirectory { get; set; }
			public String[] CommandLine { get; set; }

			public IProcess Execute(String workingDirectory, String commandLine)
			{
				this.WorkingDirectory = workingDirectory;
				this.CommandLine = new CommandLineParser().Parse(commandLine);

				return new Process { Handle = new IntPtr(20) };
			}
		}

		private class Process : IProcess
		{
			public IntPtr Handle { get; set; }
			public IntPtr Thread { get; set; }
			public IntPtr Output { get; set; }
			public IntPtr StandardError { get; set; }

			public int GetExitCode()
			{
				return 0;
			}

			public void ReadOutput()
			{			
			}

			public String GetOutput()
			{
				return String.Empty;
			}

			public void Release()
			{
			}
		}

		private class ProjectReader : IProjectReader
		{
			private readonly PackageReference reference;

			public ProjectReader(PackageReference reference)
			{
				this.reference = reference;
			}

			public void Update(ProjectConfigurer configurer)
			{
				configurer.AddPackage(this.reference);
			}
		}

		private class Testable : ITestable
		{
			public String Name
			{
				get { return "abc"; }
			}

			public String Path
			{
				get { return Factory.DefaultOutput + "\\abc.dll"; }
			}

			public IReadOnlyCollection<String> GetReferences()
			{
				return new String[0];
			}
		}

	}
}