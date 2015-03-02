using System.Collections.Generic;

namespace noide
{
	public interface ISolutionSchedule
	{
		bool IsCompleted();

		void Succeed(IPackage package);

		void Fail(IPackage package);

		void Succeed(IProject project);

		void Fail(IProject project);

		IReadOnlyCollection<IPackage> GetPackages();

		IReadOnlyCollection<IProject> GetProjects();
	}
}