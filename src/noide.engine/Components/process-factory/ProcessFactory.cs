using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;

namespace noide
{
	public partial class ProcessFactory : IProcessFactory
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
	}
}