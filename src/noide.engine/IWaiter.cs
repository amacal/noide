using System;

namespace noide
{
	public interface IWaiter<T>
	{
		void Add(IWaitable<T> target);

		void Remove(IntPtr handle);

		bool IsCompleted();

		IHeartBeat<T> Next();
	}
}