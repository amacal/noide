using System;

namespace noide
{
	partial class Compiler
	{
        private class Resource : IResource
        {
            private readonly IProcess process;

            public Resource(IProcess process)
            {
                this.process = process;
            }

            public IntPtr[] Handles
            {
                get { return new[] { this.process.Handle, this.process.Output }; }
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