using NUnit.Framework;

namespace noide.tests
{
	partial class ProjectFactoryTests
	{
		[Test]
		public void WhenCreatingNewProjectItUsesPathToUpdateTheProject()
		{
			ProjectReader reader = new ProjectReader();
			ProjectFactory factory = new ProjectFactory(reader);

			factory.Create("c:\\projects\\source\\MyProject\\.project.json");

			Assert.That(reader.Path, Is.EqualTo("c:\\projects\\source\\MyProject"));
		}
	}
}