using System;
using System.Text;
using System.Runtime.InteropServices;

namespace noide
{
	public interface INative
	{
        INativeSynchronization Synchronization { get; }

        INativeDirectoryManagement DirectoryManagement { get; }

        INativeProcessAndThread ProcessAndThread { get; }

        INativePipes Pipes { get; }

        INativeHandlesAndObjects HandlesAndObjects { get; }

        INativeFileManagement FileManagement { get; }
	}

    public interface INativeSynchronization
    {
        int GetMaximumWaitObjects();

        int WaitForMultipleObjects(
            int nCount,
            IntPtr[] lpHandles,
            int bWaitAll,
            int dwMilliseconds);
    }

    public interface INativeDirectoryManagement
    {
        IntPtr FindFirstChangeNotification(
                String lpPathName,
                int bWatchSubtree,
                int dwNotifyFilter);

        int FindNextChangeNotification(
            IntPtr hChangeHandle);
    }

    public interface INativeProcessAndThread
    {
        bool CreateProcess(
            string lpApplicationName,
            StringBuilder lpCommandLine,
            IntPtr lpProcessAttributes,
            IntPtr lpThreadAttributes,
            bool bInheritHandles,
            int dwCreationFlags,
            IntPtr lpEnvironment,
            string lpCurrentDirectory,
            STARTUPINFO lpStartupInfo,
            PROCESSINFORMATION lpProcessInformation);

        bool GetExitCodeProcess(
            IntPtr hProcess, 
            out int exitCode);
    }

    public interface INativePipes
    {
        bool CreatePipe(
            out IntPtr hReadPipe,
            out IntPtr hWritePipe,
            SECURITY_ATTRIBUTES lpPipeAttributes,
            int nSize);
    }

    public interface INativeHandlesAndObjects
    {
        bool SetHandleInformation(
            IntPtr hObject,
            int dwMask,
            int dwFlags);

        bool CloseHandle(
            IntPtr hObject);
    }

    public interface INativeFileManagement
    {
        bool ReadFile(
            IntPtr handle, 
            byte[] bytes,
            int numBytesToRead,
            out int numBytesRead,
            IntPtr overlapped);
    }

	[StructLayout(LayoutKind.Sequential)]
    public class STARTUPINFO
    {
    	public int cb;
    	public IntPtr lpReserved;
    	public IntPtr lpDesktop;
    	public IntPtr lpTitle;
    	public int dwX;
    	public int dwY;
    	public int dwXSize;
    	public int dwYSize;
    	public int dwXCountChars;
    	public int dwYCountChars;
    	public int dwFillAttribute;
    	public int dwFlags;
    	public short wShowWindow;
    	public short cbReserved2;
    	public IntPtr lpReserved2;
    	public IntPtr hStdInput;
    	public IntPtr hStdOutput;
    	public IntPtr hStdError;
    }
    
    [StructLayout(LayoutKind.Sequential)]
    public class PROCESSINFORMATION
    {
    	public IntPtr hProcess;
    	public IntPtr hThread;
    	public int dwProcessId;
    	public int dwThreadId;
    }


    [StructLayout(LayoutKind.Sequential)]
    public class SECURITY_ATTRIBUTES
    {
        public int nLength = 12;
        public IntPtr lpSecurityDescriptor;
        public bool bInheritHandle;
    }
}