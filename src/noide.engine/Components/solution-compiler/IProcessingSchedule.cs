using System.Collections.Generic;

namespace noide
{
	partial class SolutionCompiler
	{
		private interface IProcessingSchedule
		{
			bool IsCompleted();

			void Succeed(IPackage package);

			void Fail(IPackage package);

			void Succeed(IProject project);

			void Fail(IProject project);

			void Succeed(IExecution execution);

			void Fail(IExecution execution);

			IReadOnlyCollection<IPackage> GetPackages();

			IReadOnlyCollection<IProject> GetProjects();

			IReadOnlyCollection<IExecution> GetExecutions();

			void Register(IExecution execution);
		}
	}
}