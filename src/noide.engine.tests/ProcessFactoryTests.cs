using System;
using System.ComponentModel;
using System.Text;
using NUnit.Framework;
using Onlysharp;

namespace noide.tests
{
	[TestFixture]
	public class ProcessFactoryTests
	{
		[Test]
		public void WhenExecutingItAssignsValidProcessHandle()
		{
			Native native = new Native();
			ProcessFactory factory = new ProcessFactory(native);
			IProcess process = factory.Execute(Environment.CurrentDirectory, "ls");

			Assert.That(process.Handle, Is.EqualTo(new IntPtr(20)));
		}

		[Test]
		public void WhenExecutingItAssignsValidThreadHandle()
		{
			Native native = new Native();
			ProcessFactory factory = new ProcessFactory(native);
			IProcess process = factory.Execute(Environment.CurrentDirectory, "ls");

			Assert.That(process.Thread, Is.EqualTo(new IntPtr(21)));
		}

		[Test]
		public void WhenExecutingItAssignsValidOutputHandle()
		{
			Native native = new Native();
			ProcessFactory factory = new ProcessFactory(native);
			IProcess process = factory.Execute(Environment.CurrentDirectory, "ls");

			Assert.That(process.Output, Is.EqualTo(new IntPtr(13)));
		}

		[Test]
		[ExpectedException(typeof(Win32Exception))]
		public void WhenExecutingNotAvailableCommandLineToolItThrowsNativeException()
		{
			Native native = new Native { CreateProcessResult = false };
			ProcessFactory factory = new ProcessFactory(native);
			IProcess process = factory.Execute(Environment.CurrentDirectory, "dir");
		}

		private class Native : NativeStub, INative, INativeProcessAndThread, INativePipes, INativeHandlesAndObjects
		{
			public bool CreateProcessResult = true;

	        public override INativeProcessAndThread ProcessAndThread
	        {
	            get { return this; }
	        }

	        public override INativePipes Pipes
	        {
	        	get { return this; }
	        }

	        public override INativeHandlesAndObjects HandlesAndObjects
	        {
	        	get { return this; }
	        }

			public bool CreateProcess(
				string lpApplicationName,
	        	StringBuilder lpCommandLine,
	        	IntPtr lpProcessAttributes,
	        	IntPtr lpThreadAttributes,
	        	bool bInheritHandles,
	        	int dwCreationFlags,
	        	IntPtr lpEnvironment,
	        	string lpCurrentDirectory,
	        	STARTUPINFO lpStartupInfo,
	        	PROCESSINFORMATION lpProcessInformation)
        	{
        		lpProcessInformation.hProcess = new IntPtr(20);
        		lpProcessInformation.hThread = new IntPtr(21);

        		return CreateProcessResult;
        	}

        	public bool GetExitCodeProcess(IntPtr hProcess, out int exitCode)
        	{
        		throw new NotSupportedException();
        	}

        	public bool CreatePipe(
	            out IntPtr hReadPipe,
	            out IntPtr hWritePipe,
	            SECURITY_ATTRIBUTES lpPipeAttributes,
	            int nSize)
        	{
        		hReadPipe = new IntPtr(13);
        		hWritePipe = new IntPtr(27);

        		return true;
        	}

        	public bool SetHandleInformation(
	            IntPtr hObject,
	            int dwMask,
	            int dwFlags)
	        {
	            return true;
	        }

	        public bool CloseHandle(
	        	IntPtr hObject)
	        {
	        	return true;
	        }
        }
	}
}