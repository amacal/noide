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
			const string name = "abc";
			const string path = "c:\\projects\\abc";

			ProjectStub project = new ProjectStub(path, name);
			JsonReader reader = new JsonReader();
			ProjectReader updater = new ProjectReader(reader);

			updater.Update(project);

			Assert.That(reader.Path, Is.EqualTo("c:\\projects\\abc\\.project.json"));
		}

		[Test]
		public void WhenUpdatedItAddedGacReferences()
		{
			ProjectStub project = new ProjectStub();
			JsonReader reader = new JsonReader();
			ProjectReader updater = new ProjectReader(reader);

			updater.Update(project);

			Assert.That(project.References.Contains(new Reference("System")), Is.True);
		}

		[Test]
		public void WhenUpdatedItAddedProjectReferences()
		{
			ProjectStub project = new ProjectStub();
			JsonReader reader = new JsonReader();
			ProjectReader updater = new ProjectReader(reader);

			updater.Update(project);


			Assert.That(project.ProjectReferences.Contains(new ProjectReference("MyProject")), Is.True);
		}

		[Test]
		public void WhenUpdatedItAddedPackageReferences()
		{
			ProjectStub project = new ProjectStub();
			JsonReader reader = new JsonReader();
			ProjectReader updater = new ProjectReader(reader);

			updater.Update(project);

			Assert.That(project.PackageReferences.Contains(new PackageReference("NUnit", "2.6.3")), Is.True);
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