using System;

namespace noide
{
	partial class SolutionCompiler
	{
        private class TestTask : IExecution
        {
            private readonly IProject project;
            private readonly ITester runner;

            public TestTask(IProject project, ITester runner)
            {
                this.project = project;
                this.runner = runner;
            }

            public IPackageReferenceCollection PackageReferences
            {
                get { return this.runner.PackageReferences; }
            }

            public void Execute(IContext context)
            {
                context.Reporter.BeginTesting(context.Round + 1, this.project);
                IResource<ITestingResult> resource = context.Tester.Test(this.runner, this.project);

                for (int i = 0; i < resource.Handles.Length; i++)
                {
                    context.Waiter.Add(new Waitable(this, this.project, resource, i));
                }
            }

            private class Waitable : ITask, IWaitable<ITask>
            {
                private readonly IExecution execution;
                private readonly IProject project;
                private readonly IResource<ITestingResult> resource;
                private readonly IntPtr handle;

                public Waitable(IExecution execution, IProject project, IResource<ITestingResult> resource, int index)
                {
                    this.execution = execution;
                    this.project = project;
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
                        this.CompleteTesting(context.Reporter);
                        this.CompleteExecution(context.Schedule);

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

                private void CompleteTesting(IReporter reporter)
                {
                    reporter.CompleteTesting(this.project, this.resource.Payload);                    
                }

                private void CompleteExecution(IProcessingSchedule schedule)
                {
                    if (this.resource.IsSuccessful())
                    {
                        schedule.Succeed(this.execution);
                    }
                    else
                    {
                        schedule.Fail(this.execution);
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