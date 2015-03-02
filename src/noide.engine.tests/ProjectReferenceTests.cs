using System;
using NUnit.Framework;
using Onlysharp;

namespace noide.tests
{
	[TestFixture]
	public class ProjectReferenceTests
	{
		[Test]
		public void WhenCreatingNewInstanceItExposesPassedName()
		{
			const String name = "name";

			ProjectReference reference = new ProjectReference(name);

			Assert.That(reference.Name, Is.EqualTo(name));
		}

		[Test]
		public void WhenComparingTwoEqualInstancesItReturnsTrue()
		{
			ProjectReference first = new ProjectReference("name");
			ProjectReference second = new ProjectReference("name");

			bool equal = first.Equals(second);

			Assert.That(equal, Is.True);
		}

		[Test]
		public void WhenComparingTwoUnequalInstancesItReturnsFalse()
		{
			ProjectReference first = new ProjectReference("name");
			ProjectReference second = new ProjectReference("other");

			bool equal = first.Equals(second);

			Assert.That(equal, Is.False);
		}
	}
}