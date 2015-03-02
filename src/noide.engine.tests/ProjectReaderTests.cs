using System;
using NUnit.Framework;
using Onlysharp;

namespace noide.tests
{
	[TestFixture]
	public class ProjectReaderTests
	{
		[Test]
		public void WhenUpdatingItUsesPassedPath()
		{
			ProjectConfigurer configurer = Factory.CreateConfigurer();
			JsonReader reader = new JsonReader();
			ProjectReader updater = new ProjectReader(reader);

			updater.Update(configurer);

			Assert.That(reader.Path, Is.EqualTo(Factory.DefaultPath + "\\.project.json"));
		}

		[Test]
		public void WhenUpdatedItAddedGacReferences()
		{
			ProjectConfigurer configurer = Factory.CreateConfigurer();
			JsonReader reader = new JsonReader();
			ProjectReader updater = new ProjectReader(reader);

			updater.Update(configurer);

			Assert.That(configurer.References, Contains.Item(new Reference("System")));
		}

		[Test]
		public void WhenUpdatedItAddedProjectReferences()
		{
			ProjectConfigurer configurer = Factory.CreateConfigurer();
			JsonReader reader = new JsonReader();
			ProjectReader updater = new ProjectReader(reader);

			updater.Update(configurer);

			Assert.That(configurer.Projects, Contains.Item(new ProjectReference("MyProject")));
		}

		[Test]
		public void WhenUpdatedItAddedPackageReferences()
		{
			ProjectConfigurer configurer = Factory.CreateConfigurer();
			JsonReader reader = new JsonReader();
			ProjectReader updater = new ProjectReader(reader);

			updater.Update(configurer);

			Assert.That(configurer.Packages, Contains.Item(new PackageReference("NUnit", "2.6.3")));
		}

		private static class Factory
		{
			public const String DefaultName = "abc";
			public const String DefaultPath = "C:\\Projects\\abc";

			public static ProjectConfigurer CreateConfigurer()
			{
				return new ProjectConfigurer(new ProjectMetadata(DefaultPath, DefaultName));
			}
		}

		private class JsonReader : IJsonReader
		{
			public String Path { get; set; }

			public TData Read<TData>(String path)
			{
				this.Path = path;

				return (TData)(Object) new ProjectReader.ProjectData
				{
					references = new[] { "System" },
					dependencies = new[] { "MyProject" },
					packages = new[] { new ProjectReader.PackageData { name = "NUnit", version = "2.6.3" } }
				};
			}
		}
	}
}