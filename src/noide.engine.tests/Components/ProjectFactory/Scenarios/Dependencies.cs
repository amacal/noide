using System.Collections.Generic;
using NUnit.Framework;

namespace noide.tests
{
	partial class ProjectFactoryTests
	{
		[Test]
		public void WhenCreatedNewProjectItContainsReadReferences()
		{
			ProjectReader reader = new ProjectReader();
			ProjectFactory factory = new ProjectFactory(reader);

			IProject project = factory.Create("c:\\projects\\source\\MyProject\\.project.json");			

			Assert.That(project.References.Contains(new Reference("System")), Is.True);
		}

		[Test]
		public void WhenCreatedNewProjectItDoesntContainNotReadReference()
		{
			ProjectReader reader = new ProjectReader();
			ProjectFactory factory = new ProjectFactory(reader);

			IProject project = factory.Create("c:\\projects\\source\\MyProject\\.project.json");

			Assert.That(project.References.Contains(new Reference("System.Data")), Is.False);
		}

		[Test]
		public void WhenCreatedNewProjectItEnumeratesAllReadReferences()
		{
			ProjectReader reader = new ProjectReader();
			ProjectFactory factory = new ProjectFactory(reader);

			IProject project = factory.Create("c:\\projects\\source\\MyProject\\.project.json");
			IEnumerable<Reference> references = project.References.AsEnumerable();

			Assert.That(references, Is.EquivalentTo(new[] { new Reference("System") }));
		}

		[Test]
		public void WhenCreatedNewProjectItContainsReadProjectReferences()
		{
			ProjectReader reader = new ProjectReader();
			ProjectFactory factory = new ProjectFactory(reader);

			IProject project = factory.Create("c:\\projects\\source\\MyProject\\.project.json");			

			Assert.That(project.ProjectReferences.Contains(new ProjectReference("MyProject")), Is.True);
		}

		[Test]
		public void WhenCreatedNewProjectItContainsReadPackageReferences()
		{
			ProjectReader reader = new ProjectReader();
			ProjectFactory factory = new ProjectFactory(reader);

			IProject project = factory.Create("c:\\projects\\source\\MyProject\\.project.json");			

			Assert.That(project.PackageReferences.Contains(new PackageReference("NUnit", "2.6.3")), Is.True);
		}

		[Test]
		public void WhenCreatedNewProjectItDoesntContainsNotReadPackageReferences()
		{
			ProjectReader reader = new ProjectReader();
			ProjectFactory factory = new ProjectFactory(reader);

			IProject project = factory.Create("c:\\projects\\source\\MyProject\\.project.json");

			Assert.That(project.PackageReferences.Contains(new PackageReference("NUnit", "2.6.4")), Is.False);
		}

		[Test]
		public void WhenCreatedNewProjectItEnumeratesAllReadPackageReferences()
		{
			ProjectReader reader = new ProjectReader();
			ProjectFactory factory = new ProjectFactory(reader);

			IProject project = factory.Create("c:\\projects\\source\\MyProject\\.project.json");
			IEnumerable<PackageReference> references = project.PackageReferences.AsEnumerable();

			Assert.That(references, Is.EquivalentTo(new[] { new PackageReference("NUnit", "2.6.3") }));
		}
	}
}