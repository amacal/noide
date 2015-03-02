using System;
using System.Collections.Generic;

namespace noide
{
	public class Watcher<T> : IWatcher<T>
	{
		private readonly IHeartBeatFilter<T> filter;
		private readonly INative native;
		private readonly IWaiter<Folder> waiter;
		private readonly List<IntPtr> handles;
		private readonly List<IWatchable<T>> items;

		public Watcher(IHeartBeatFilter<T> filter, INative native, IWaiterFactory waiterFactory)
		{
			this.filter = filter;
			this.native = native;
			this.waiter = waiterFactory.Create<Folder>();
			this.handles = new List<IntPtr>();
			this.items = new List<IWatchable<T>>();
		}

		public void Add(IWatchable<T> target)
		{
			IntPtr handle = this.native.DirectoryManagement.FindFirstChangeNotification(target.Path, 1, 0x00000010);

			this.handles.Add(handle);
			this.items.Add(target);

			this.waiter.Add(new Waitable(handle, new Folder { Value = target.Payload, Handle = handle } ));
		}

		public IHeartBeat<T> Next()
		{
			IHeartBeat<Folder> beat = this.waiter.Next();

			if (beat.IsSuccessful == false)
			{
				return this.filter.Filter(new HeartBeat());
			}

			this.native.DirectoryManagement.FindNextChangeNotification(beat.Payload.Handle);
			this.waiter.Add(new Waitable(beat.Payload.Handle, new Folder { Value = beat.Payload.Value, Handle = beat.Payload.Handle } ));

			return this.filter.Filter(new HeartBeat(beat.Payload.Value));
		}

		private class Waitable : IWaitable<Folder>
		{
			private readonly IntPtr handle;
			private readonly Folder payload;

			public Waitable(IntPtr handle, Folder payload)
			{
				this.handle = handle;
				this.payload = payload;
			}

			public IntPtr Handle
			{
				get { return this.handle; }
			}

			public Folder Payload
			{
				get { return this.payload; }
			}
		}

		private struct Folder
		{
			public T Value;

			public IntPtr Handle;
		}

		private class HeartBeat : IHeartBeat<T>
		{
			private readonly bool isSuccessful;
			private readonly T payload;

			public HeartBeat()
			{
				this.isSuccessful = false;
			}

			public HeartBeat(T payload)
			{
				this.isSuccessful = true;
				this.payload = payload;
			}

			public bool IsSuccessful
			{
				get { return this.isSuccessful; }
			}

			public T Payload
			{
				get { return this.payload; }
			}
		}
	}
}