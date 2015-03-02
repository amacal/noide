using System;

namespace noide
{
	public interface IResource
	{
		IntPtr[] Handles { get; }

		bool Complete(IntPtr handle);

		bool IsSuccessful();

		void Release();
	}

	public interface IResource<T> : IResource
	{
		T Payload { get; }
	}
}