using System;
using System.Text;
using NUnit.Framework;
using Onlysharp;

namespace noide.tests
{
	[TestFixture]
	public class WaiterTests
	{
		[Test]
		public void WhenGettingNextFromEmptyWaiterItReturnsUnsuccessfulHeartBeat()
		{
			Native native = new Native();
			Waiter<int> waiter = new Waiter<int>(native);

			IHeartBeat<int> heartBeat = waiter.Next();

			Assert.That(heartBeat.IsSuccessful, Is.False);
		}

		[Test]
		public void WhenGettingNextFromOneItemItReturnsIt()
		{
			Waitable waitable = new Waitable { Handle = new IntPtr(10), Payload = 13 };
			Native native = new Native();
			Waiter<int> waiter = new Waiter<int>(native);

			waiter.Add(waitable);
			IHeartBeat<int> heartBeat = waiter.Next();

			Assert.That(heartBeat.Payload, Is.EqualTo(13));
		}

		[Test]
		public void WhenGotNextFromOneItemItIsCompleted()
		{
			Waitable waitable = new Waitable { Handle = new IntPtr(10) };
			Native native = new Native();
			Waiter<int> waiter = new Waiter<int>(native);

			waiter.Add(waitable);
			waiter.Next();

			Assert.That(waiter.IsCompleted(), Is.True);
		}

		[Test]
		public void WhenGettingNextFromTwoItemsItCanHandleTheSignalFromTheSecond()
		{
			Waitable first = new Waitable { Handle = new IntPtr(10), Payload = 12 };
			Waitable second = new Waitable { Handle = new IntPtr(20), Payload = 13 };

			Native native = new Native { Result = 1 };
			Waiter<int> waiter = new Waiter<int>(native);

			waiter.Add(first);
			waiter.Add(second);

			IHeartBeat<int> heartBeat = waiter.Next();

			Assert.That(heartBeat.Payload, Is.EqualTo(13));
		}

		[Test]
		public void WhenGettingNextFromItemsExceedingMaxmiumCountItCanSplitCalls()
		{
			Waitable first = new Waitable { Handle = new IntPtr(10), Payload = 12 };
			Waitable second = new Waitable { Handle = new IntPtr(20), Payload = 13 };
			Waitable third = new Waitable { Handle = new IntPtr(30), Payload = 14 };

			Native native = new Native { Result = 0x0102 };
			Waiter<int> waiter = new Waiter<int>(native);

			waiter.Add(first);
			waiter.Add(second);
			waiter.Add(third);

			waiter.Next();

			Assert.That(native.Called, Is.EqualTo(2));
			Assert.That(native.Maxiumum, Is.LessThanOrEqualTo(2));
		}

		[Test]
		public void WhenGettingNextFromOneItemItCallsNative()
		{
			Waitable waitable = new Waitable { Handle = new IntPtr(20) };
			Native native = new Native();
			Waiter<int> waiter = new Waiter<int>(native);

			waiter.Add(waitable);
			waiter.Next();

			Assert.That(native.Handles, Is.EqualTo(new[] { waitable.Handle }));
		}

		private class Waitable : IWaitable<int>
		{
			public IntPtr Handle { get; set; }

			public int Payload { get; set; }
		}

		private class Native : NativeStub, INative, INativeSynchronization
		{
			public int Called { get; set; }
			public int Result { get; set; }
			public int Maxiumum { get; set; }

			public IntPtr[] Handles { get; set; }

			public override INativeSynchronization Synchronization
			{
				get { return this; }
			}

			public int GetMaximumWaitObjects()
			{
				return 2;
			}

			public int WaitForMultipleObjects(
	            int nCount,
	            IntPtr[] lpHandles,
	            int bWaitAll,
	            int dwMilliseconds)
			{
				this.Called += 1;
				this.Handles = lpHandles;
				this.Maxiumum = Math.Max(nCount, this.Maxiumum);

				return this.Result;
			}
		}
	}
}