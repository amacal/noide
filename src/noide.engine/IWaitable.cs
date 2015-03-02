using System;

namespace noide
{
	public interface IWaitable<T>
	{
		IntPtr Handle { get; }

		T Payload { get; }
	}
}