using System;
using NUnit.Framework;
using Onlysharp;

namespace noide.tests
{
	[TestFixture]
	public class PackageReferenceTests
	{
		[Test]
		public void WhenCreatingNewInstanceItExposesPassedName()
		{
			const String name = "package";
			const String version = "2.6.3";

			PackageReference reference = new PackageReference(name, version);

			Assert.That(reference.Name, Is.EqualTo(name));
			Assert.That(reference.Version, Is.EqualTo(version));
		}

		[Test]
		public void WhenComparingTwoEqualInstancesItReturnsTrue()
		{
			PackageReference first = new PackageReference("package", "2.6.3");
			PackageReference second = new PackageReference("package", "2.6.3");

			bool equal = first.Equals(second);

			Assert.That(equal, Is.True);
		}

		[Test]
		public void WhenComparingTheSamePackagesButInDifferentVersionItReturnsFalse()
		{
			PackageReference first = new PackageReference("package", "2.6.3");
			PackageReference second = new PackageReference("package", "2.6.4");

			bool equal = first.Equals(second);

			Assert.That(equal, Is.False);
		}

		[Test]
		public void WhenComparingCompletelyDifferentPackagesItReturnsFalse()
		{
			PackageReference first = new PackageReference("package1", "2.6.3");
			PackageReference second = new PackageReference("package2", "2.6.4");

			bool equal = first.Equals(second);

			Assert.That(equal, Is.False);
		}
	}
}