using System;
using NUnit.Framework;
using Onlysharp;

namespace noide.tests
{
	[TestFixture]
	public class SolutionReaderTests
	{
		[Test]
		public void WhenUpdatingItUsesPassedPath()
		{
			JsonReader reader = new JsonReader();
			ProjectFactory factory = new ProjectFactory();
			ProjectEnumerator enumerator = new ProjectEnumerator();

			SolutionConfigurer configurer = Factory.CreateConfigurer();
			SolutionReader updater = new SolutionReader(reader, enumerator, factory);

			updater.Update(configurer);

			Assert.That(reader.Path, Is.EqualTo(Factory.DefaultPath + "\\.solution.json"));
		}

		[Test]
		public void WhenUpdatedItContainsReadProjectsPath()
		{
			JsonReader reader = new JsonReader();
			ProjectFactory factory = new ProjectFactory();
			ProjectEnumerator enumerator = new ProjectEnumerator();

			SolutionConfigurer configurer = Factory.CreateConfigurer();
			SolutionReader updater = new SolutionReader(reader, enumerator, factory);

			updater.Update(configurer);

			Assert.That(configurer.ProjectsPath, Is.EqualTo(Factory.DefaultPath + "\\src"));
		}

		[Test]
		public void WhenUpdatedItContainsReadPackagesPath()
		{
			JsonReader reader = new JsonReader();
			ProjectFactory factory = new ProjectFactory();
			ProjectEnumerator enumerator = new ProjectEnumerator();

			SolutionConfigurer configurer = Factory.CreateConfigurer();
			SolutionReader updater = new SolutionReader(reader, enumerator, factory);

			updater.Update(configurer);

			Assert.That(configurer.PackagesPath, Is.EqualTo(Factory.DefaultPath + "\\lib"));
		}

		[Test]
		public void WhenUpdatedItContainsReadOutputPath()
		{
			JsonReader reader = new JsonReader();
			ProjectFactory factory = new ProjectFactory();
			ProjectEnumerator enumerator = new ProjectEnumerator();

			SolutionConfigurer configurer = Factory.CreateConfigurer();
			SolutionReader updater = new SolutionReader(reader, enumerator, factory);

			updater.Update(configurer);

			Assert.That(configurer.OutputPath, Is.EqualTo(Factory.DefaultPath + "\\out"));
		}

		[Test]
		public void WhenUpdatedItContainsFoundProjects()
		{
			JsonReader reader = new JsonReader();
			ProjectFactory factory = new ProjectFactory();
			ProjectEnumerator enumerator = new ProjectEnumerator();

			SolutionConfigurer configurer = Factory.CreateConfigurer();
			SolutionReader updater = new SolutionReader(reader, enumerator, factory);

			updater.Update(configurer);

			Assert.That(configurer.Projects, Has.Count.EqualTo(1));
		}

		[Test]
		public void WhenUpdatedItContainsFoundPackages()
		{
			JsonReader reader = new JsonReader();
			ProjectFactory factory = new ProjectFactory();
			ProjectEnumerator enumerator = new ProjectEnumerator();

			SolutionConfigurer configurer = Factory.CreateConfigurer();
			SolutionReader updater = new SolutionReader(reader, enumerator, factory);

			updater.Update(configurer);

			Assert.That(configurer.Packages, Has.Count.EqualTo(1));			
		}

		private static class Factory
		{
			public const String DefaultPath = "C:\\Projects\\abc";

			public static SolutionConfigurer CreateConfigurer()
			{
				return new SolutionConfigurer(new SolutionMetadata(DefaultPath));
			}
		}

		private class ProjectEnumerator : IProjectEnumerator
		{
			public String[] FindProjects(String path)
			{
				return new[] { "src\\MyProject\\.project.json" };
			}
		}

		private class Project : ProjectStub, IProject
		{
			private readonly String path;

			public Project(String path)
			{
				this.path = path;
			}

			public ProjectMetadata Metadata
			{
				get { return new ProjectMetadata(this.path, "Project"); }
			}
		}

		private class ProjectFactory : IProjectFactory
		{
			public IProject Create(String path)
			{
				return new Project(path);
			}
		}

		private class JsonReader : IJsonReader
		{
			public String Path { get; set; }

			public TData Read<TData>(String path)
			{
				this.Path = path;

				return (TData)(Object) new SolutionReader.SolutionData
				{
					folders = new SolutionReader.FolderData
					{
						output = "out",
						sources = "src",
						packages = "lib"
					},
					packages = new[]
					{
						new SolutionReader.PackageData
						{
							name = "NUnit",
							version = "2.6.3",
							references = new[] { "lib\\net40\\NetJSON.dll" }
						}
					}
				};
			}
		}
	}
}