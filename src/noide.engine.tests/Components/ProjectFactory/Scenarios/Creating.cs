using NUnit.Framework;

namespace noide.tests
{
	partial class ProjectFactoryTests
	{
		[Test]
		public void WhenCreatingNewProjectItExposesMetadataFromGivenPath()
		{
			ProjectReader reader = new ProjectReader();
			ProjectFactory factory = new ProjectFactory(reader);

			IProject project = factory.Create("c:\\projects\\source\\MyProject\\.project.json");

			Assert.That(project.Metadata.Name, Is.EqualTo("MyProject"));
			Assert.That(project.Metadata.Path, Is.EqualTo("c:\\projects\\source\\MyProject"));
		}
	}
}