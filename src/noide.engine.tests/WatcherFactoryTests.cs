using System;
using NUnit.Framework;
using Onlysharp;

namespace noide.tests
{
	[TestFixture]
	public class WatcherFactoryTests
	{
		[Test]
		public void WhenCreatingItReturnsNotNull()
		{
			Native native = new Native();
			WaiterFactory waiterFactory = new WaiterFactory();
			WatcherFactory watcherFactory = new WatcherFactory(native, waiterFactory);

			HeartBeatFilter filter = new HeartBeatFilter();
			IWatcher<int> watcher = watcherFactory.Create<int>(filter);

			Assert.That(watcher, Is.Not.Null);
		}

		private class WaiterFactory : IWaiterFactory
		{
			public IWaiter<T> Create<T>()
			{
				return new Waiter<T>();
			}
		}

		private class Waiter<T> : IWaiter<T>
		{
			public void Add(IWaitable<T> target)
			{
			}

			public void Remove(IntPtr handle)
			{
			}

			public bool IsCompleted()
			{
				return true;
			}

			public IHeartBeat<T> Next()
			{
				return new HeartBeat<T>();
			}
		}

		private class HeartBeat<T> : IHeartBeat<T>
		{
			public bool IsSuccessful
			{
				get { return false; }
			}

			public T Payload
			{
				get { return default(T); }
			}
		}

		private class HeartBeatFilter : IHeartBeatFilter<int>
		{
			public IHeartBeat<int> Filter(IHeartBeat<int> beat)
			{
				return beat;
			}
		}

		private class Native : NativeStub, INative
		{
		}
	}
}