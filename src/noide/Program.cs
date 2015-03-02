using System;

namespace noide
{
    public static class Program
    {
        public static void Main(String[] args)
        {
            Bootstrapper bootstrapper = new Bootstrapper();
            ISolutionWatcher watcher = bootstrapper.CreateSolutionWatcher();

            watcher.Watch(new Never(), args[0]);
        }

        private class Never : IStop
        {
        	public bool IsRequested()
        	{
        		return false;
        	}
        }
    }
}