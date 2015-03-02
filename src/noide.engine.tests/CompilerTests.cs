using System;
using System.Collections.Generic;
using NUnit.Framework;
using Onlysharp;

namespace noide.tests
{
	[TestFixture]
	public class CompilerTests
	{
		[Test]
		public void WhenCompilingItExecutesProcessInPassesWorkingDirectory()
		{
			ProcessFactory factory = new ProcessFactory();
			Compilable compilable = new Compilable();
			Compiler compiler = new Compiler(factory);

			compiler.Compile(compilable);

			Assert.That(factory.WorkingDirectory, Is.EqualTo(Factory.DefaultOutput));
		}

		[Test]
		public void WhenCompilingItCallsCscCommand()
		{
			ProcessFactory factory = new ProcessFactory();
			Compilable compilable = new Compilable();
			Compiler compiler = new Compiler(factory);

			compiler.Compile(compilable);

			Assert.That(factory.CommandLine[0], Is.EqualTo(Factory.CscCommand));
		}

		[Test]
		public void WhenCompilingItIncludesCsFiles()
		{
			ProcessFactory factory = new ProcessFactory();
			Compilable compilable = new Compilable();
			Compiler compiler = new Compiler(factory);

			compiler.Compile(compilable);

			Assert.That(factory.CommandLine, Contains.Item("c:\\projects\\abc\\a.cs"));
			Assert.That(factory.CommandLine, Contains.Item("c:\\projects\\abc\\b.cs"));
		}

		[Test]
		public void WhenCompilingItIncludesReferences()
		{
			ProcessFactory factory = new ProcessFactory();
			Compilable compilable = new Compilable();
			Compiler compiler = new Compiler(factory);

			compiler.Compile(compilable);

			Assert.That(factory.CommandLine, Contains.Item("/reference:System.dll"));
			Assert.That(factory.CommandLine, Contains.Item("/reference:c:\\output\\MyProject.dll"));
			Assert.That(factory.CommandLine, Contains.Item("/reference:c:\\packages\\NUnit-2.6.3\\net40\\tools\\nunit.framework.dll"));
		}

		[Test]
		public void WhenCompilingItAddsOutOption()
		{
			ProcessFactory factory = new ProcessFactory();
			Compilable compilable = new Compilable();
			Compiler compiler = new Compiler(factory);

			compiler.Compile(compilable);

			Assert.That(factory.CommandLine, Contains.Item("/out:abc.dll"));
		}

		[Test]
		public void WhenCompiledItReturnsCreatedResource()
		{
			ProcessFactory factory = new ProcessFactory();
			Compilable compilable = new Compilable();
			Compiler compiler = new Compiler(factory);

			IResource resource = compiler.Compile(compilable);			

			Assert.That(resource, Is.Not.Null);
		}

		private static class Factory
		{
			public const String DefaultOutput = "c:\\output";
			public const String CscCommand = "C:\\Windows\\Microsoft.NET\\Framework\\v4.0.30319\\csc.exe";
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

		private class Compilable : ICompilable
		{
			public String Name
			{
				get { return "abc"; }
			}

			public String Output
			{
				get { return Factory.DefaultOutput + "\\abc.dll"; }
			}

			public String Type
			{
				get { return "library"; }
			}

			public IReadOnlyCollection<String> GetSources()
			{
				return new[] { "c:\\projects\\abc\\a.cs", "c:\\projects\\abc\\b.cs" };
			}

			public IReadOnlyCollection<String> GetReferences()
			{
				return new [] 
				{ 
					"System.dll",
					"c:\\output\\MyProject.dll",
					"c:\\packages\\NUnit-2.6.3\\net40\\tools\\nunit.framework.dll"
				};
			}

			public IReadOnlyCollection<String> GetResources()
			{
				return new String[0];
			}
		}
	}
}