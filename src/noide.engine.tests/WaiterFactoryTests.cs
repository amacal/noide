using System;
using System.Text;
using NUnit.Framework;
using Onlysharp;

namespace noide.tests
{
	[TestFixture]
	public class WaiterFactoryTests
	{
		[Test]
		public void WhenCreatingItReturnsNotNull()
		{
			Native native = new Native();
			WaiterFactory factory = new WaiterFactory(native);

			IWaiter<int> waiter = factory.Create<int>();

			Assert.That(waiter, Is.Not.Null);
		}

		private class Native : NativeStub, INative
		{
		}
	}
}