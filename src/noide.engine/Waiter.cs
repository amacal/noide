using System;
using System.Collections.Generic;
using System.Linq;

namespace noide
{
	public class Waiter<T> : IWaiter<T>
	{
		private readonly INative native;
		private readonly List<IntPtr> handles;
		private readonly List<IWaitable<T>> items;

		public Waiter(INative native)
		{
			this.native = native;
			this.handles = new List<IntPtr>();
			this.items = new List<IWaitable<T>>();
		}

		public void Add(IWaitable<T> target)
		{
			this.handles.Add(target.Handle);
			this.items.Add(target);
		}

		public void Remove(IntPtr handle)
		{
			for (int i = this.handles.Count - 1; i >= 0; i--)
			{
				if (this.handles[i] == handle)
				{
					this.handles.RemoveAt(i);
					this.items.RemoveAt(i);
				}
			}
		}

		public bool IsCompleted()
		{
			return this.handles.Count == 0;
		}

		public IHeartBeat<T> Next()
		{
			IHeartBeat<T> beat = new HeartBeat();
			int maximum = this.native.Synchronization.GetMaximumWaitObjects();

			if (this.items.Count > 0)
			{
				for (int page = 0; page <= (this.handles.Count - 1) / maximum; page++)
				{
					IntPtr[] handles = this.handles.Skip(page * maximum).Take((page + 1) * maximum).ToArray();
					IWaitable<T>[] items = this.items.Skip(page * maximum).Take((page + 1) * maximum).ToArray();

		            int wait = this.WaitForMultipleObjects(handles);

		            if (wait != 0x0102 && wait >= 0)
		            {
		            	IWaitable<T> result = items[wait];

		            	this.handles.RemoveAt(page * maximum + wait);
		            	this.items.RemoveAt(page * maximum + wait);

		            	beat = new HeartBeat(result.Payload);
		            	break;
		            }
	        	}
        	}

			return beat;
		}

		private int WaitForMultipleObjects(IntPtr[] handles)
		{
			return this.native.Synchronization.WaitForMultipleObjects(handles.Length, handles, 0, 100);
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