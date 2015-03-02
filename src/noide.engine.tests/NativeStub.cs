using System;
using Onlysharp;

namespace noide.tests
{
	public class NativeStub
	{	
        public virtual INativeProcessAndThread ProcessAndThread
        {
			get { throw new NotSupportedException(); }
        }

		public virtual INativeSynchronization Synchronization
		{
			get { throw new NotSupportedException(); }
		}

        public virtual INativeDirectoryManagement DirectoryManagement
        {
            get { throw new NotSupportedException(); }
        }

        public virtual INativePipes Pipes
        {
            get { throw new NotSupportedException(); }
        }

        public virtual INativeHandlesAndObjects HandlesAndObjects
        {
            get { throw new NotSupportedException(); }
        }

        public virtual INativeFileManagement FileManagement
        {
            get { throw new NotSupportedException(); }
        }
	}
}