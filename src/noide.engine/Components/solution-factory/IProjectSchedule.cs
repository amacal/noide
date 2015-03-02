using System.Collections.Generic;

namespace noide
{
    partial class SolutionFactory
    {
		private interface IProjectSchedule
		{
			bool IsCompleted();

			void Succeed(IProject project);

			void Fail(IProject project);

			IReadOnlyCollection<IProject> Next();
		}
	}
}