using System;
using System.IO;
using System.Text;
using System.Threading;

namespace noide
{
	public class PackageManager : IPackageManager
	{
		private readonly IProcessFactory processFactory;
		private readonly IFileService fileService;

		public PackageManager(IProcessFactory processFactory, IFileService fileService)
		{
			this.processFactory = processFactory;
			this.fileService = fileService;
		}

		public IResource Restore(IPackage package)
		{
			if (this.fileService.Exists(package.Metadata.Path) == true)
			{
				return new AlreadyRestored();
			}

			String workingDirectory = Path.GetDirectoryName(package.Metadata.Path);
            StringBuilder commandLine = new StringBuilder();

            this.AppendCommand(commandLine, workingDirectory);
            this.AppendPackage(commandLine, package.Metadata);
            this.AppendOutput(commandLine, workingDirectory);
            this.AppendOptions(commandLine);
            
			return new InProgress(this.processFactory.Execute(workingDirectory, commandLine.ToString()));
		}

		private void AppendCommand(StringBuilder builder, String path)
		{
			builder.Append("\"");
			builder.Append(Path.Combine(path, "nuget.exe"));
			builder.Append("\" install ");
		}

		private void AppendOutput(StringBuilder builder, String path)
		{
			builder.Append(" -OutputDirectory \"");
			builder.Append(path);
			builder.Append("\" ");
		}

		private void AppendOptions(StringBuilder builder)
		{
			builder.Append("-NoCache -NonInteractive -Verbosity quiet ");
		}

		private void AppendPackage(StringBuilder builder, PackageMetadata metadata)
		{
			builder.Append(metadata.Name);
			builder.Append(" -Version ");
			builder.Append(metadata.Version);
			builder.Append(" ");
		}
		
		private class AlreadyRestored : IResource
		{
			private readonly ManualResetEvent signal;

			public AlreadyRestored()
			{
				this.signal = new ManualResetEvent(true);
			}

			public IntPtr[] Handles
			{
				get { return new[] { this.signal.SafeWaitHandle.DangerousGetHandle() }; }
			}

			public bool Complete(IntPtr handle)
			{
				return true;
			}

			public bool IsSuccessful()
			{
				return true;
			}

			public void Release()
			{
				this.signal.Dispose();
			}
		}

		private class InProgress : IResource
		{
			private readonly IProcess process;

			public InProgress(IProcess process)
			{
				this.process = process;
			}

            public IntPtr[] Handles
            {
                get { return new[] { this.process.Handle, this.process.Output, this.process.StandardError }; }
            }

            public bool Complete(IntPtr handle)
            {			
                if (handle == this.process.Output)
                {
                    this.process.ReadOutput();
                }

                return handle == this.process.Handle;
            }

            public bool IsSuccessful()
            {
                return this.process.GetExitCode() == 0;
            }

            public void Release()
            {
                Console.Write(this.process.GetOutput());
                this.process.Release();
            }
		}
	}
}