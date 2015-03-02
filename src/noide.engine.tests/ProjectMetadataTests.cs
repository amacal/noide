using System;
using NUnit.Framework;
using Onlysharp;

namespace noide.tests
{
	[TestFixture]
	public class ProjectMetadataTests
	{
		[Test]
		public void WhenCreatingNewInstanceItExposesPassedParameters()
		{
			const String path = "c:\\projects\\abc";
			const String name = "name";

			ProjectMetadata metadata = new ProjectMetadata(path, name);

			Assert.That(metadata.Path, Is.EqualTo(path));
			Assert.That(metadata.Name, Is.EqualTo(name));
		}
	}
}