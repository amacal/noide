using System;

namespace noide
{
	partial class SolutionWatcher
	{
        private class Watchable : IWatchable<IProject>
        {
            private readonly IProject project;

            public Watchable(IProject project)
            {
                this.project = project;
            }

            public String Path
            {
                get { return this.project.Metadata.Path; }
            }

            public IProject Payload
            {
                get { return this.project; }
            }
        }
	}
}