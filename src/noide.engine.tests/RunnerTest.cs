using System;
using System.Collections.Generic;
using NUnit.Framework;
using Onlysharp;

namespace noide.tests
{
	[TestFixture]
	public class RunnerTest
	{
		[Test]
		public void WhenTestingItExecutesProcessInPassesWorkingDirectory()
		{
			ProcessFactory factory = new ProcessFactory();
			Testable testable = new Testable();
			Package package = new Package();
			FileService fileService = new FileService();
			Runner runner = new Runner(factory, fileService, package);

			runner.Test(testable);

			Assert.That(factory.WorkingDirectory, Is.EqualTo(Factory.DefaultOutput));
		}

		[Test]
		public void WhenTestingItCallsRunnerCommand()
		{
			ProcessFactory factory = new ProcessFactory();
			Testable testable = new Testable();
			Package package = new Package();
			FileService fileService = new FileService();
			Runner runner = new Runner(factory, fileService, package);

			runner.Test(testable);

			Assert.That(factory.CommandLine[0], Is.EqualTo(Factory.RunnerCommand));
		}

		[Test]
		public void WhenTestingItIncludesTestedTarget()
		{
			ProcessFactory factory = new ProcessFactory();
			Testable testable = new Testable();
			Package package = new Package();
			FileService fileService = new FileService();
			Runner runner = new Runner(factory, fileService, package);

			runner.Test(testable);

			Assert.That(factory.CommandLine, Contains.Item(Factory.DefaultOutput + "\\abc.dll"));
		}

		[Test]
		public void WhenTestingItAddsRunnerPathToPrivateBinPath()
		{
			ProcessFactory factory = new ProcessFactory();
			Testable testable = new Testable();
			Package package = new Package();
			FileService fileService = new FileService();
			Runner runner = new Runner(factory, fileService, package);

			runner.Test(testable);			

			Assert.That(factory.CommandLine, Contains.Item("/privatebinpath:" + Factory.RunnerPath));
		}

		[Test]
		public void WhenTestedItReturnsResults()
		{
			ProcessFactory factory = new ProcessFactory();
			Testable testable = new Testable();
			Package package = new Package();
			FileService fileService = new FileService();
			Runner runner = new Runner(factory, fileService, package);

			IResource<ITestingResult> resource = runner.Test(testable);
			ITestingCase[] cases = resource.Payload.GetCases();

			Assert.That(cases, Has.Length.EqualTo(2));
		}

		private static class Factory
		{
			public const String DefaultOutput = "c:\\output";
			public const String RunnerPath = "c:\\packages\\NUnit.Runners-2.6.3\\tools";
			public const String RunnerCommand = "c:\\packages\\NUnit.Runners-2.6.3\\tools\\nunit-console.exe";
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
				return @"ProcessModel: Separate    DomainUsage: None
Execution Runtime: net-3.5

Tests run: 127, Errors: 1, Failures: 1, Inconclusive: 0, Time: 0,620729047413272 seconds
  Not run: 0, Invalid: 0, Ignored: 0, Skipped: 0

Errors and Failures:
1) Test Error : noide.tests.CommandLineParserTests.WhenParsingNothingItReturnsEmptyArray
   System.ArgumentException : Value does not fall within the expected range.
   at noide.tests.CommandLineParserTests.WhenParsingNothingItReturnsEmptyArray()


2) Test Failure : noide.tests.CommandLineParserTests.WhenParsingNullItReturnsEmptyArray
     Expected: not <empty>
  But was:  <empty>

at noide.tests.CommandLineParserTests.WhenParsingNullItReturnsEmptyArray()


";
			}

			public void Release()
			{
			}
		}

		private class Package : IPackage
		{
			public PackageMetadata Metadata
			{
				get { return new PackageMetadata("c:\\packages\\NUnit.Runners-2.6.3", "NUnit.Runner", "2.6.3"); }
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
				return new [] 
				{ 
					"c:\\output\\MyProject.dll",
					"c:\\packages\\NUnit-2.6.3\\net40\\tools\\nunit.framework.dll"
				};
			}
		}
	}
}