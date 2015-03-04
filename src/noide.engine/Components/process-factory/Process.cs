using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace noide
{
	partial class ProcessFactory
	{
		private class Process : IProcess
		{
			private static readonly int bufferSize;
			private static readonly byte[] buffer;

			static Process()
			{
				bufferSize = 1024;
				buffer = new byte[bufferSize];
			}

			private readonly INative native;
			private readonly IntPtr hProcess;
			private readonly IntPtr hThread;
			private readonly IntPtr hOutputPipe;
			private readonly IntPtr hErrorPipe;
			private readonly List<IntPtr> handles;
			private readonly StringBuilder output;

			public Process(INative native, IntPtr hProcess, IntPtr hThread, IntPtr hOutputPipe, IntPtr hErrorPipe)
			{
				this.native = native;
				this.hProcess = hProcess;
				this.hThread = hThread;
				this.hOutputPipe = hOutputPipe;
				this.hErrorPipe = hErrorPipe;

				this.handles = new List<IntPtr>{ hProcess, hThread, hOutputPipe, hErrorPipe };
				this.output = new StringBuilder();
			}

			public IntPtr Handle
			{
				get { return this.hProcess; }
			}

			public IntPtr Thread
			{
				get { return this.hThread; }
			}

			public IntPtr Output
			{
				get { return this.hOutputPipe; }
			}

			public IntPtr StandardError
			{
				get { return this.hErrorPipe; }
			}

			public int GetExitCode()
			{
				int exitCode;
				
				if (this.native.ProcessAndThread.GetExitCodeProcess(this.hProcess, out exitCode) == false)
					throw new Win32Exception();
				
				return exitCode;
			}

			public void ReadOutput()
			{
			    this.AppendStringBuildFromPipe(this.hOutputPipe, this.output);
			}

			public void ProcessStandardError()
			{
				this.AppendStringBuildFromPipe(this.hErrorPipe, this.output);
			}

			public String GetOutput()
			{	
			    this.AppendStringBuildFromPipe(this.hOutputPipe, this.output);
				this.AppendStringBuildFromPipe(this.hErrorPipe, this.output);

				return this.output.ToString();
			}
			
			private void AppendStringBuildFromPipe(IntPtr hPipe, StringBuilder data)
			{
				int read = 0;
				Encoding encoding = Encoding.UTF8;		

				do
				{
					if (this.native.FileManagement.ReadFile(hPipe, buffer, bufferSize, out read, IntPtr.Zero) == false)
					{
						if (Marshal.GetLastWin32Error() != 0x6d)
							throw new Win32Exception();
					}

					if (read > 0)
					{
						data.Append(encoding.GetString(buffer, 0, read));
					}
				}
				while (read > 0);
			}

			public void Release()
			{
				this.ReleaseProcess();
				this.ReleaseThread();
				this.ReleaseOutputPipe();
				this.ReleaseErrorPipe();
			}

			private void ReleaseProcess()
			{
				this.handles.Remove(this.hProcess);
				
				if (this.native.HandlesAndObjects.CloseHandle(this.hProcess) == false)
					throw new Win32Exception();			
			}

			private void ReleaseThread()
			{
				this.handles.Remove(this.hThread);
				
				if (this.native.HandlesAndObjects.CloseHandle(this.hThread) == false)
					throw new Win32Exception();
			}

			private void ReleaseOutputPipe()
			{
				this.handles.Remove(this.hOutputPipe);
				
				if (this.native.HandlesAndObjects.CloseHandle(this.hOutputPipe) == false)
					throw new Win32Exception();				
			}

			private void ReleaseErrorPipe()
			{
				this.handles.Remove(this.hErrorPipe);
				
				if (this.native.HandlesAndObjects.CloseHandle(this.hErrorPipe) == false)
					throw new Win32Exception();				
			}
		}
	}
}