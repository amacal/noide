using System;
using System.Collections.Generic;
using NUnit.Framework;
using Onlysharp;

namespace noide.tests
{
	[TestFixture]
	public class WatcherTests
	{
		[Test]
		public void WhenGettingNextFromEmptyWatcherItReturnsUnsuccessfulHeartBeat()
		{
			Native native = new Native();
			WaiterFactory waiterFactory = new WaiterFactory();
			HeartBeatFilter filter = new HeartBeatFilter();
			Watcher<int> watcher = new Watcher<int>(filter, native, waiterFactory);

			IHeartBeat<int> heartBeat = watcher.Next();

			Assert.That(heartBeat.IsSuccessful, Is.False);
		}

		[Test]
		public void WhenGettingNextFromOneItemItReturnsIt()
		{
			Watchable watchable = new Watchable { Path = "c:\\projects\\abc", Payload = 13 };

			Native native = new Native();
			WaiterFactory waiterFactory = new WaiterFactory();
			HeartBeatFilter filter = new HeartBeatFilter();
			Watcher<int> watcher = new Watcher<int>(filter, native, waiterFactory);

			watcher.Add(watchable);
			IHeartBeat<int> heartBeat = watcher.Next();

			Assert.That(heartBeat.Payload, Is.EqualTo(13));
		}

		[Test]
		public void WhenAddingWatchableItRegistersItUsingNative()
		{			
			Watchable watchable = new Watchable { Path = "c:\\projects\\abc", Payload = 13 };

			Native native = new Native();
			WaiterFactory waiterFactory = new WaiterFactory();
			HeartBeatFilter filter = new HeartBeatFilter();
			Watcher<int> watcher = new Watcher<int>(filter, native, waiterFactory);

			watcher.Add(watchable);

			Assert.That(native.Paths, Contains.Item("c:\\projects\\abc"));
		}

		[Test]
		public void WhenGettingNextFromOneItemItCallsNative()
		{
			Watchable watchable = new Watchable { Path = "c:\\projects\\abc", Payload = 13 };

			Native native = new Native();
			WaiterFactory waiterFactory = new WaiterFactory();
			HeartBeatFilter filter = new HeartBeatFilter();
			Watcher<int> watcher = new Watcher<int>(filter, native, waiterFactory);

			watcher.Add(watchable);
			watcher.Next();

			Assert.That(native.Handles, Contains.Item(new IntPtr(20)));
		}

		private class Watchable : IWatchable<int>
		{
			public String Path { get; set; }

			public int Payload { get; set; }
		}

		private class Native : NativeStub, INative, INativeDirectoryManagement
		{
			public ICollection<String> Paths = new List<String>();
			public ICollection<IntPtr> Handles = new List<IntPtr>();

	        public override INativeDirectoryManagement DirectoryManagement
	        {
	            get { return this; }
	        }

            public IntPtr FindFirstChangeNotification(
                String lpPathName,
                int bWatchSubtree,
                int dwNotifyFilter)
            {
            	this.Paths.Add(lpPathName);
            	return new IntPtr(20);
            }

	        public int FindNextChangeNotification(
	            IntPtr hChangeHandle)
	        {
	        	this.Handles.Add(hChangeHandle);
	        	return 1;
	        }
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
			public Queue<IWaitable<T>> Items = new Queue<IWaitable<T>>();

			public void Add(IWaitable<T> target)
			{
				this.Items.Enqueue(target);
			}

			public void Remove(IntPtr handle)
			{
			}
			
			public bool IsCompleted()
			{
				return this.Items.Count == 0;
			}

			public IHeartBeat<T> Next()
			{
				if (this.Items.Count > 0)
				{
					return new HeartBeat<T>
					{
						IsSuccessful = true,
						Payload = this.Items.Dequeue().Payload
					};
				}

				return new HeartBeat<T>();
			}
		}

		private class HeartBeat<T> : IHeartBeat<T>
		{
			public bool IsSuccessful { get; set; }

			public T Payload { get; set; }
		}

		private class HeartBeatFilter : IHeartBeatFilter<int>
		{
			public IHeartBeat<int> Filter(IHeartBeat<int> beat)
			{
				return beat;
			}
		}
	}
}