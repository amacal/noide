using System;

namespace noide
{
	public interface IProcess
	{
		IntPtr Handle { get; }

		IntPtr Thread { get; }

		IntPtr Output { get; }

		IntPtr StandardError { get; }

		int GetExitCode();

		void ReadOutput();

		String GetOutput();

		void Release();
	}
}