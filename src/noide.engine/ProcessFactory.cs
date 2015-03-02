using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;

namespace noide
{
	public class ProcessFactory : IProcessFactory
	{
		private readonly INative native;

		public ProcessFactory(INative native)
		{
			this.native = native;
		}

		public IProcess Execute(String workingDirectory, String commandLine)
		{
			IntPtr hOutputReadPipe, hOutputWritePipe;
			IntPtr hErrorReadPipe, hErrorWritePipe;

			SECURITY_ATTRIBUTES attributes = new SECURITY_ATTRIBUTES
			{
				nLength = Marshal.SizeOf(typeof(SECURITY_ATTRIBUTES)),
				bInheritHandle = true
			};

			if (this.native.Pipes.CreatePipe(out hOutputReadPipe, out hOutputWritePipe, attributes, 0) == false)
				throw new Win32Exception();

			if (this.native.HandlesAndObjects.SetHandleInformation(hOutputReadPipe, 0x00000001, 0) == false)
				throw new Win32Exception();

			if (this.native.Pipes.CreatePipe(out hErrorReadPipe, out hErrorWritePipe, attributes, 0) == false)
				throw new Win32Exception();

			if (this.native.HandlesAndObjects.SetHandleInformation(hErrorReadPipe, 0x00000001, 0) == false)
				throw new Win32Exception();

            STARTUPINFO startup = new STARTUPINFO
            {
            	cb = Marshal.SizeOf(typeof(STARTUPINFO)),
            	dwFlags = 0x00000100,
            	hStdOutput = hOutputWritePipe,
            	hStdError = hErrorWritePipe,
            };
            
            PROCESSINFORMATION process = new PROCESSINFORMATION
            {
            };

            bool status = this.native.ProcessAndThread.CreateProcess(
            	null, 
            	new StringBuilder(commandLine),
            	IntPtr.Zero,
            	IntPtr.Zero,
            	true,
            	0,
            	IntPtr.Zero,
            	workingDirectory,
            	startup,
            	process);

            if (status == false)
            	throw new Win32Exception();

			if (this.native.HandlesAndObjects.CloseHandle(hOutputWritePipe) == false)
				throw new Win32Exception();

			if (this.native.HandlesAndObjects.CloseHandle(hErrorWritePipe) == false)
				throw new Win32Exception();

            return new Process(this.native, process.hProcess, process.hThread, hOutputReadPipe, hErrorReadPipe);
		}

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