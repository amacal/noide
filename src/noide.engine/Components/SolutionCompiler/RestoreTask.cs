using System;

namespace noide
{
	partial class SolutionCompiler
	{
        private class RestoreTask : IExecutable
        {
            private readonly IPackage package;

            public RestoreTask(IPackage package)
            {
                this.package = package;
            }

            public void Execute(IContext context)
            {
                context.Reporter.BeginRestoring(context.Round, this.package);
                IResource resource = context.Packager.Restore(this.package);

                for (int i = 0; i < resource.Handles.Length; i++)
                {
                    context.Waiter.Add(new Waitable(this.package, resource, i));
                }
            }

            private class Waitable : ITask, IWaitable<ITask>
            {
                private readonly IPackage package;
                private readonly IResource resource;
                private readonly IntPtr handle;

                public Waitable(IPackage package, IResource resource, int index)
                {
                    this.package = package;
                    this.resource = resource;
                    this.handle = resource.Handles[index];
                }

                public IntPtr Handle
                {
                    get { return this.handle; }
                }

                public ITask Payload
                {
                    get { return this; }
                }

                public bool Complete(IContext context)
                {
                    if (this.resource.Complete(this.handle) == true)
                    {
                        this.RemoveOtherHandles(context);
                        this.CompleteRestoring(context.Reporter);
                        this.CompletePackage(context.Schedule);

                        return true;
                    }
                    else
                    {
                        context.Waiter.Add(this);
                        return false;
                    }
                }

                private void RemoveOtherHandles(IContext context)
                {
                    foreach (IntPtr handle in this.resource.Handles)
                    {
                        if (handle != this.handle)
                        {
                            context.Waiter.Remove(handle);
                        }
                    }
                }

                private void CompleteRestoring(IReporter reporter)
                {
                    reporter.CompleteRestoring(this.package, this.resource.IsSuccessful());
                }

                private void CompletePackage(IProcessingSchedule schedule)
                {
                    if (this.resource.IsSuccessful())
                    {
                        schedule.Succeed(this.package);
                    }
                    else
                    {
                        schedule.Fail(this.package);
                    }
                }

                public IExecutable Continue(IContext context)
                {
                    return new NothingTask();
                }

                public void Release()
                {
                    this.resource.Release();
                }
            }
        }
    }
}