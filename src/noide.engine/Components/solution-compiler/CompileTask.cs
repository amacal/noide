using System;

namespace noide
{
	partial class SolutionCompiler
	{
        private class CompileTask : IExecutable
        {
            private readonly IProject project;

            public CompileTask(IProject project)
            {
                this.project = project;
            }

            public void Execute(IContext context)
            {
                context.Reporter.BeginCompiling(context.Round, this.project);
                IResource resource = context.Compiler.Compile(project);

                for (int i = 0; i < resource.Handles.Length; i++)
                {
                    context.Waiter.Add(new Waitable(this.project, resource, i));
                }
            }

            private class Waitable : ITask, IWaitable<ITask>
            {
                private readonly IProject project;
                private readonly IResource resource;
                private readonly IntPtr handle;

                public Waitable(IProject project, IResource resource, int index)
                {
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
                        this.CompleteCompiling(context.Reporter);
                        this.CompleteProject(context.Schedule);
                        this.ScheduleTesting(context);

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

                private void CompleteCompiling(IReporter reporter)
                {
                    reporter.CompleteCompiling(this.project, this.resource.IsSuccessful());                    
                }

                private void CompleteProject(IProcessingSchedule schedule)
                {
                    if (this.resource.IsSuccessful())
                    {
                        schedule.Succeed(this.project);
                    }
                    else
                    {
                        schedule.Fail(this.project);
                    }
                }

                public void ScheduleTesting(IContext context)
                {
                    if (this.CanExecuteTests(context) == true)
                    {
                        context.Schedule.Register(new TestTask(this.project, context.Tester.GetRunner(this.project)));
                    }
                }

                public IExecutable Continue(IContext context)
                {
                    return new NothingTask();
                }

                private bool CanExecuteTests(IContext context)
                {
                    return this.resource.IsSuccessful() == true
                        && context.Tester.IsTestable(this.project) == true;
                }

                public void Release()
                {
                    this.resource.Release();
                }
            }
        }
	}
}