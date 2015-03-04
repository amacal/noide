namespace noide
{
	partial class SolutionWatcher
	{
        private class HeartBeat : IHeartBeat<IProject>
        {
            private readonly IProject project;

            public HeartBeat()
            {
            }

            public HeartBeat(IProject project)
            {
                this.project = project;
            }

            public bool IsSuccessful
            {
                get { return this.project != null; }
            }

            public IProject Payload
            {
                get { return this.project; }
            }
        }
	}
}