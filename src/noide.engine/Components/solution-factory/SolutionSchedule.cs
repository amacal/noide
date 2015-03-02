using System.Collections.Generic;

namespace noide
{
    partial class SolutionFactory
    {
        private class SolutionSchedule : ISolutionSchedule
        {
        	private readonly IProjectSchedule schedule;
        	private readonly IPackageCollection packages;
        	private readonly HashSet<PackageReference> completed;
        	private readonly HashSet<PackageReference> working;
        	private readonly HashSet<PackageReference> failed;
        	private readonly HashSet<PackageReference> scheduled;
        	private readonly HashSet<IProject> waiting;
        	private readonly HashSet<IProject> ready;

        	public SolutionSchedule(IProjectSchedule schedule, IPackageCollection packages)
        	{
        		this.schedule = schedule;
        		this.packages = packages;       		
        		this.completed = new HashSet<PackageReference>();
        		this.working = new HashSet<PackageReference>();
        		this.failed = new HashSet<PackageReference>();
        		this.scheduled = new HashSet<PackageReference>();
        		this.waiting = new HashSet<IProject>();
        		this.ready = new HashSet<IProject>();
        	}

			public bool IsCompleted()
			{
				return this.schedule.IsCompleted();
			}

			public void Succeed(IProject project)
			{
				this.schedule.Succeed(project);
			}

			public void Fail(IProject project)
			{
				this.schedule.Fail(project);
			}

			public void Succeed(IPackage package)
			{
				PackageMetadata metadata = package.Metadata;
				PackageReference reference = new PackageReference(metadata.Name, metadata.Version);

				this.working.Remove(reference);
				this.scheduled.Remove(reference);
				this.completed.Add(reference);

				this.FlushWaitingPackages();
			}

			public void Fail(IPackage package)
			{
				PackageMetadata metadata = package.Metadata;
				PackageReference reference = new PackageReference(metadata.Name, metadata.Version);

				this.working.Remove(reference);
				this.scheduled.Remove(reference);
				this.failed.Add(reference);

				this.FlushWaitingPackages();
			}

			private void FlushWaitingPackages()
			{
				foreach (IProject project in this.waiting)
				{
					bool isFailed = false;
					bool isSuccessful = false;
					bool isFinalized = true;

					foreach (PackageReference reference in project.PackageReferences.AsEnumerable())
					{
						if (this.completed.Contains(reference) == true)
						{
							isSuccessful = true;
						}
						else if (this.failed.Contains(reference) == true)
						{
							isFailed = true;
						}
						else
						{
							isFinalized = false;
						}
					}

					if (isFinalized == true)
					{
						if (isSuccessful == true && isFailed == false)
						{
							this.ready.Add(project);
						}
						else
						{
							this.schedule.Fail(project);
						}
					}
				}

				foreach (IProject project in this.ready)
				{
					this.waiting.Remove(project);
				}
			}

			public IReadOnlyCollection<IPackage> GetPackages()
			{
				List<IPackage> packages = new List<IPackage>();

				foreach (PackageReference reference in this.scheduled)
				{
					if (this.working.Add(reference) == true)
					{
						packages.Add(this.packages.GetPackage(reference.Name, reference.Version));
					}
				}

				return packages.AsReadOnly();
			}

			public IReadOnlyCollection<IProject> GetProjects()
			{
				List<IProject> satisfied = new List<IProject>();

				foreach (IProject project in this.ready)
				{
					satisfied.Add(project);
				}

				this.ready.Clear();

				foreach (IProject project in this.schedule.Next())
				{
					bool withoutPackage = true;
					bool stopped = false;

					foreach (PackageReference reference in project.PackageReferences.AsEnumerable())
					{
						withoutPackage = false;

						if (this.completed.Contains(reference) == false)
						{
							stopped = true;
							this.scheduled.Add(reference);
						}
					}

					if (withoutPackage == true || stopped == false)
					{
						satisfied.Add(project);
					}
					else
					{
						this.waiting.Add(project);		
					}
				}

				return satisfied.AsReadOnly();
			}
        }
    }
}