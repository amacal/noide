using System;
using NUnit.Framework;
using Onlysharp;

namespace noide.tests
{
	[TestFixture]
	public class ReferenceTests
	{
		[Test]
		public void WhenCreatingNewInstanceItExposesPassedName()
		{
			const String name = "name";

			Reference reference = new Reference(name);

			Assert.That(reference.Name, Is.EqualTo(name));
		}

		[Test]
		public void WhenComparingTwoEqualInstancesItReturnsTrue()
		{
			Reference first = new Reference("name");
			Reference second = new Reference("name");

			bool equal = first.Equals(second);

			Assert.That(equal, Is.True);
		}

		[Test]
		public void WhenComparingTwoUnequalInstancesItReturnsFalse()
		{
			Reference first = new Reference("name");
			Reference second = new Reference("other");

			bool equal = first.Equals(second);

			Assert.That(equal, Is.False);
		}
	}
}