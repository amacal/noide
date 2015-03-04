using System.Collections.Generic;
using System.Linq;

namespace noide
{
	partial class SolutionWatcher
	{
        private class HeartBeatFilter : IHeartBeatFilter<IProject>
        {
            private int total;
            private readonly HashSet<IProject> successful;

            public HeartBeatFilter()
            {
                this.total = 0;
                this.successful = new HashSet<IProject>();
            }

            public IHeartBeat<IProject> Filter(IHeartBeat<IProject> beat)
            {
                IHeartBeat<IProject> result = new HeartBeat();

                if (beat.IsSuccessful)
                {
                    this.successful.Add(beat.Payload);
                    this.total = 0;
                }

                if (this.total++ > 5)
                {
                    this.total = 0;

                    if (this.successful.Count > 0)
                    {
                        IProject project = this.successful.First();

                        result = new HeartBeat(project);
                        this.successful.Remove(project);
                    }
                }

                return result;
            }
        }
	}
}