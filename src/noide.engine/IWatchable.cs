using System;

namespace noide
{
	public interface IWatchable<T>
	{
		String Path { get; }

		T Payload { get; }
	}
}