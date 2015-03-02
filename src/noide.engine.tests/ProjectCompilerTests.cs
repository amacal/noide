using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace noide.tests
{
	[TestFixture]
	public class ProjectCompilerTests
	{
		[Test]
		public void WhenCompilingItLooksForSourcesInProjectDirectory()
		{
			Compiler compiler = new Compiler();
			SourceEnumerator sourceEnumerator = new SourceEnumerator();
			ReferenceEnumerator referenceEnumerator = new ReferenceEnumerator();

			Project project = new Project();
			ProjectCompiler projectCompiler = new ProjectCompiler(sourceEnumerator, referenceEnumerator, compiler, Factory.DefaultOutput);

			projectCompiler.Compile(project);

			Assert.That(sourceEnumerator.Path, Is.EqualTo(Project.DefaultPath));
		}

		[Test]
		public void WhenCompilingItPassesRightName()
		{
			Compiler compiler = new Compiler();
			SourceEnumerator sourceEnumerator = new SourceEnumerator();
			ReferenceEnumerator referenceEnumerator = new ReferenceEnumerator();

			Project project = new Project();
			ProjectCompiler projectCompiler = new ProjectCompiler(sourceEnumerator, referenceEnumerator, compiler, Factory.DefaultOutput);

			projectCompiler.Compile(project);

			Assert.That(compiler.Name, Is.EqualTo(Project.DefaultName));
		}

		[Test]
		public void WhenCompilingItPassesAllSources()
		{
			Compiler compiler = new Compiler();
			SourceEnumerator sourceEnumerator = new SourceEnumerator();
			ReferenceEnumerator referenceEnumerator = new ReferenceEnumerator();

			Project project = new Project();
			ProjectCompiler projectCompiler = new ProjectCompiler(sourceEnumerator, referenceEnumerator, compiler, Factory.DefaultOutput);

			projectCompiler.Compile(project);

			Assert.That(compiler.Sources, Has.Length.EqualTo(2));
		}

		[Test]
		public void WhenCompilingItPassesAllGacReferences()
		{
			Compiler compiler = new Compiler();
			SourceEnumerator sourceEnumerator = new SourceEnumerator();
			ReferenceEnumerator referenceEnumerator = new ReferenceEnumerator();

			Project project = new Project();
			ProjectCompiler projectCompiler = new ProjectCompiler(sourceEnumerator, referenceEnumerator, compiler, Factory.DefaultOutput);

			projectCompiler.Compile(project);

			Assert.That(compiler.References, Contains.Item("System.dll"));
		}

		[Test]
		public void WhenCompilingItPassesAllProjectReferences()
		{
			Compiler compiler = new Compiler();
			SourceEnumerator sourceEnumerator = new SourceEnumerator();
			ReferenceEnumerator referenceEnumerator = new ReferenceEnumerator();

			Project project = new Project();
			ProjectCompiler projectCompiler = new ProjectCompiler(sourceEnumerator, referenceEnumerator, compiler, Factory.DefaultOutput);

			projectCompiler.Compile(project);

			Assert.That(compiler.References, Contains.Item(Factory.DefaultOutput + "\\MyProject.dll"));
		}

		[Test]
		public void WhenCompilingItPassesAllPackageReferences()
		{
			Compiler compiler = new Compiler();
			SourceEnumerator sourceEnumerator = new SourceEnumerator();
			ReferenceEnumerator referenceEnumerator = new ReferenceEnumerator();

			Project project = new Project();
			ProjectCompiler projectCompiler = new ProjectCompiler(sourceEnumerator, referenceEnumerator, compiler, Factory.DefaultOutput);

			projectCompiler.Compile(project);

			Assert.That(compiler.References, Contains.Item(Factory.DefaultPackages + "\\NUnit-2.6.3\\net40\\nunit.framework.dll"));
		}

		[Test]
		public void WhenCompiledItReturnsCreatedResource()
		{
			Compiler compiler = new Compiler();
			SourceEnumerator sourceEnumerator = new SourceEnumerator();
			ReferenceEnumerator referenceEnumerator = new ReferenceEnumerator();

			Project project = new Project();
			ProjectCompiler projectCompiler = new ProjectCompiler(sourceEnumerator, referenceEnumerator, compiler, Factory.DefaultOutput);

			IResource resource = projectCompiler.Compile(project);

			Assert.That(resource, Is.Not.Null);
		}

		private static class Factory
		{
			public const String DefaultOutput = "c:\\output";
			public const String DefaultPackages = "c:\\packages";
		}

		private class Project : ProjectStub, IProject
		{
			public const String DefaultName = "abc";
			public const String DefaultPath = "c:\\projects\\abc";

			public ProjectMetadata Metadata
			{
				get { return new ProjectMetadata(DefaultPath, DefaultName); }
			}

			public override IReferenceCollection References
			{
				get { return new ReferenceCollection(); }
			}

			public override IProjectReferenceCollection ProjectReferences
			{
				get { return new ProjectReferenceCollection(); }
			}

			public override IPackageReferenceCollection PackageReferences
			{
				get { return new PackageReferenceCollection(); }
			}

			private class ReferenceCollection : IReferenceCollection
			{
				public IEnumerable<Reference> AsEnumerable()
				{
					return new[] { new Reference("System") };
				}

				public bool Contains(Reference reference)
				{
					return reference.Name == "System";
				}
			}

			private class ProjectReferenceCollection : IProjectReferenceCollection
			{
				public IEnumerable<ProjectReference> AsEnumerable()
				{
					return new[] { new ProjectReference("MyProject") };
				}

				public bool Contains(ProjectReference reference)
				{
					return reference.Name == "MyProject";
				}
			}

			private class PackageReferenceCollection : IPackageReferenceCollection
			{
				public IEnumerable<PackageReference> AsEnumerable()
				{
					yield return new PackageReference("NUnit", "2.6.3");
				}

				public bool Contains(PackageReference reference)
				{
					return reference.Name == "NUnit" && reference.Version == "2.6.3";
				}
			}
		}

		private class Compiler : ICompiler
		{
			public String Name { get; set; }
			public String[] Sources { get; set; }
			public String[] References { get; set; }

			public IResource Compile(ICompilable target)
			{
				this.Name = target.Name;
				this.Sources = target.GetSources().ToArray();
				this.References = target.GetReferences().ToArray();

				return new Resource();
			}			
		}

		private class Resource : IResource
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
		}

		private class SourceEnumerator : ISourceEnumerator
		{
			public String Path { get; set; }

			public String[] FindSources(String path)
			{
				this.Path = path;

				return new [] { "c:\\projects\\abc\\a.cs", "c:\\projects\\abc\\b.cs" };
			}
		}

		private class ReferenceEnumerator : IReferenceEnumerator
		{
			public String Package { get; set; }
			public String Version { get; set; }

			public String[] FindReferences(String package, String version)
			{
				return new [] { Factory.DefaultPackages + "\\NUnit-2.6.3\\net40\\nunit.framework.dll" };
			}
		}
	}
}