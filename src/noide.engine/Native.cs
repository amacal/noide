using System;
using System.Text;
using System.Runtime.InteropServices;

namespace noide
{
	public unsafe class Native : 
        INative, 
        INativeSynchronization, 
        INativeDirectoryManagement, 
        INativeProcessAndThread, 
        INativePipes,
        INativeHandlesAndObjects,
        INativeFileManagement
	{
        public INativeSynchronization Synchronization
        {
            get { return this; }
        }

		public int GetMaximumWaitObjects()
		{
			return 64;
		}

		public int WaitForMultipleObjects(
            int nCount,
            IntPtr[] lpHandles,
            int bWaitAll,
            int dwMilliseconds)
		{
			fixed (IntPtr* handles = lpHandles)
			{
				return _WaitForMultipleObjects(nCount, handles, bWaitAll, dwMilliseconds);
			}
		}

		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, EntryPoint = "WaitForMultipleObjects")]
        private static extern int _WaitForMultipleObjects(
            int nCount,
            IntPtr* lpHandles,
            int bWaitAll,
            int dwMilliseconds);

        public INativeDirectoryManagement DirectoryManagement
        {
            get { return this; }
        }

        public IntPtr FindFirstChangeNotification(
                String lpPathName,
                int bWatchSubtree,
                int dwNotifyFilter)
        {
            return _FindFirstChangeNotification(lpPathName, bWatchSubtree, dwNotifyFilter);
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, EntryPoint = "FindFirstChangeNotification")]
        private static extern IntPtr _FindFirstChangeNotification(
            String lpPathName,
            int bWatchSubtree,
            int dwNotifyFilter
        );

        public int FindNextChangeNotification(
            IntPtr hChangeHandle)
        {
            return _FindNextChangeNotification(hChangeHandle);
        }
        
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, EntryPoint = "FindNextChangeNotification")]
        private static extern int _FindNextChangeNotification(
            IntPtr hChangeHandle
        );

        public INativeProcessAndThread ProcessAndThread
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
        	return _CreateProcess(lpApplicationName, lpCommandLine, lpProcessAttributes, lpThreadAttributes, bInheritHandles, dwCreationFlags, lpEnvironment, lpCurrentDirectory, lpStartupInfo, lpProcessInformation);
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, EntryPoint = "CreateProcess", SetLastError = true)]
        private static extern bool _CreateProcess(
        	string lpApplicationName,
        	StringBuilder lpCommandLine,
        	IntPtr lpProcessAttributes,
        	IntPtr lpThreadAttributes,
        	bool bInheritHandles,
        	int dwCreationFlags,
        	IntPtr lpEnvironment,
        	[MarshalAs(UnmanagedType.LPTStr)]
        	string lpCurrentDirectory,
        	STARTUPINFO lpStartupInfo,
        	PROCESSINFORMATION lpProcessInformation);

        public bool GetExitCodeProcess(IntPtr hProcess, out int exitCode)
        {
        	return _GetExitCodeProcess(hProcess, out exitCode);
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, EntryPoint = "GetExitCodeProcess")]
        private static extern bool _GetExitCodeProcess(
        	IntPtr hProcess,
        	out int exitCode);

        public bool CreatePipe(
            out IntPtr hReadPipe,
            out IntPtr hWritePipe,
            SECURITY_ATTRIBUTES lpPipeAttributes,
            int nSize)
        {
            return _CreatePipe(out hReadPipe, out hWritePipe, lpPipeAttributes, nSize);
        }

        public INativePipes Pipes
        {
            get { return this; }
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, EntryPoint = "CreatePipe")]
        public static extern bool _CreatePipe(
            out IntPtr hReadPipe, 
            out IntPtr hWritePipe, 
            SECURITY_ATTRIBUTES lpPipeAttributes, 
            int nSize);

        public INativeHandlesAndObjects HandlesAndObjects
        {
            get { return this; }
        }

        public bool SetHandleInformation(
            IntPtr hObject,
            int dwMask,
            int dwFlags)
        {
            return _SetHandleInformation(hObject, dwMask, dwFlags);
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, EntryPoint = "SetHandleInformation")]
        private static extern bool _SetHandleInformation(
            IntPtr hObject,
            int dwMask,
            int dwFlags);

        public bool CloseHandle(
            IntPtr hObject)
        {
            return _CloseHandle(hObject);
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, EntryPoint = "CloseHandle", SetLastError = true)]
        private static extern bool _CloseHandle(
            IntPtr hObject);

        public INativeFileManagement FileManagement
        {
            get { return this; }
        }

        public bool ReadFile(
            IntPtr handle, 
            byte[] bytes,
            int numBytesToRead,
            out int numBytesRead,
            IntPtr overlapped)
        {
            fixed (byte* lpBytes = bytes)
            {
                return _ReadFile(handle, lpBytes, numBytesToRead, out numBytesRead, overlapped);
            }
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, EntryPoint = "ReadFile", SetLastError = true)]
        private static extern bool _ReadFile(
            IntPtr hFile, 
            byte* lpBuffer,
            int nNumberOfBytesToRead,
            out int lpNumberOfBytesRead,
            IntPtr lpOverlapped);
	}
}