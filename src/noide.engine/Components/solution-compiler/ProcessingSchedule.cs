using System;
using System.Collections.Generic;

namespace noide
{
	partial class SolutionCompiler
	{
		private class ProcessingSchedule : IProcessingSchedule
		{
			private readonly ISolutionSchedule schedule;

			private readonly List<IExecution> executions;
			private readonly List<IExecution> ready;
			private readonly HashSet<IExecution> waiting;
			private readonly HashSet<PackageReference> working;
			private readonly HashSet<PackageReference> scheduled;
			private readonly HashSet<PackageReference> completed;
			private readonly HashSet<PackageReference> failed;
			private readonly Func<String, String, IPackage> getPackage;

			public ProcessingSchedule(ISolutionSchedule schedule, Func<String, String, IPackage> getPackage)
			{
				this.schedule = schedule;
				this.getPackage = getPackage;

				this.executions = new List<IExecution>();
				this.ready = new List<IExecution>();
				this.waiting = new HashSet<IExecution>();
				this.working = new HashSet<PackageReference>();
				this.scheduled = new HashSet<PackageReference>();
				this.completed = new HashSet<PackageReference>();
				this.failed = new HashSet<PackageReference>();
			}

			public bool IsCompleted()
			{
				return this.schedule.IsCompleted() && this.executions.Count == 0 && this.waiting.Count == 0 && this.ready.Count == 0;
			}

			public void Succeed(IPackage package)
			{
				PackageMetadata metadata = package.Metadata;
				PackageReference reference = new PackageReference(metadata.Name, metadata.Version);

				this.scheduled.Remove(reference);
				this.completed.Add(reference);
				this.schedule.Succeed(package);

				this.FlushWaitingPackages();
			}

			public void Fail(IPackage package)
			{
				PackageMetadata metadata = package.Metadata;
				PackageReference reference = new PackageReference(metadata.Name, metadata.Version);

				this.scheduled.Remove(reference);
				this.failed.Add(reference);
				this.schedule.Fail(package);

				this.FlushWaitingPackages();
			}

			public void Succeed(IProject project)
			{
				this.schedule.Succeed(project);
			}

			public void Fail(IProject project)
			{
				this.schedule.Fail(project);
			}

			public void Succeed(IExecution execution)
			{
				this.waiting.Remove(execution);
			}

			public void Fail(IExecution execution)
			{
				this.waiting.Remove(execution);
			}

			private void FlushWaitingPackages()
			{
				foreach (IExecution execution in this.waiting)
				{
					bool isFailed = false;
					bool isSuccessful = false;
					bool isFinalized = true;

					foreach (PackageReference reference in execution.PackageReferences.AsEnumerable())
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
							this.ready.Add(execution);
						}
						else
						{
							this.Fail(execution);
						}
					}
				}

				foreach (IExecution execution in this.ready)
				{
					this.waiting.Remove(execution);
				}
			}

			public IReadOnlyCollection<IPackage> GetPackages()
			{
				List<IPackage> result = new List<IPackage>();

				result.AddRange(this.schedule.GetPackages());

				foreach (PackageReference reference in this.scheduled)
				{
					if (this.working.Add(reference) == true)
					{
						result.Add(this.getPackage(reference.Name, reference.Version));
					}
				}				

				return result.AsReadOnly();
			}

			public IReadOnlyCollection<IProject> GetProjects()
			{
				return this.schedule.GetProjects();
			}

			public IReadOnlyCollection<IExecution> GetExecutions()
			{
				List<IExecution> satisfied = new List<IExecution>();

				foreach (IExecution execution in this.ready)
				{
					satisfied.Add(execution);
				}

				this.ready.Clear();

				foreach (IExecution execution in this.executions)
				{
					bool withoutPackage = true;
					bool stopped = false;

					foreach (PackageReference reference in execution.PackageReferences.AsEnumerable())
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
						satisfied.Add(execution);
					}
					else
					{
						this.waiting.Add(execution);		
					}
				}

				foreach (IExecution execution in this.waiting)
				{
					this.executions.Remove(execution);
				}

				foreach (IExecution execution in satisfied)
				{
					this.executions.Remove(execution);
				}

				return satisfied.AsReadOnly();
			}

			public void Register(IExecution execution)
			{
				this.executions.Add(execution);
			}
		}
	}
}