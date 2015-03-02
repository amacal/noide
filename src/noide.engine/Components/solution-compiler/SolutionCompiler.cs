namespace noide
{
    public partial class SolutionCompiler : ISolutionCompiler
    {
        private readonly IPackageManagerFactory packageManagerFactory;
        private readonly IProjectCompilerFactory projectCompilerFactory;
        private readonly IProjectTesterFactory projectTesterFactory;
        private readonly IWaiterFactory waiterFactory;
        private readonly IReporterFactory reporterFactory;

        public SolutionCompiler(
            IPackageManagerFactory packageManagerFactory,
            IProjectCompilerFactory projectCompilerFactory, 
            IProjectTesterFactory projectTesterFactory, 
            IWaiterFactory waiterFactory,
            IReporterFactory reporterFactory)
        {
            this.packageManagerFactory = packageManagerFactory;
            this.projectCompilerFactory = projectCompilerFactory;
            this.projectTesterFactory = projectTesterFactory;
            this.waiterFactory = waiterFactory;
            this.reporterFactory = reporterFactory;
        }

        public void Compile(ISolutionData solutionData)
        {
            this.Compile(solutionData, solutionData.Order());
        }
        
        private void Compile(ISolutionData solutionData, ISolutionSchedule schedule)
        {
            Context context = this.CreateContext(solutionData, schedule);
            
            while (this.IsCompleted(context) == false && context.Round < 10)
            {
                this.SchedulePackages(context);
                this.ScheduleProjects(context);
                this.ScheduleExecutions(context);
                this.Beat(context);
            }
        }

        private bool IsCompleted(IContext context)
        {
            return context.Schedule.IsCompleted() && context.Waiter.IsCompleted();
        }

        private void SchedulePackages(IContext context)
        {
            foreach (IPackage package in context.Schedule.GetPackages())
            {
                new RestoreTask(package).Execute(context);
            }
        }

        private void ScheduleProjects(IContext context)
        {
            foreach (IProject project in context.Schedule.GetProjects())
            {
                new CompileTask(project).Execute(context);
            }            
        }

        private void ScheduleExecutions(IContext context)
        {
            foreach (IExecution execution in context.Schedule.GetExecutions())
            {
                execution.Execute(context);
            }
        }

        private void Beat(Context context)
        {
            IHeartBeat<ITask> beat = context.Waiter.Next();

            if (beat.IsSuccessful == true)
            {
                if (beat.Payload.Complete(context) == true)
                {
                    beat.Payload.Continue(context).Execute(context);
                    beat.Payload.Release();
                    context.Round++;
                }
            }            
        }

        private Context CreateContext(ISolutionData solutionData, ISolutionSchedule schedule)
        {
            IPackageEnumerator packageEnumerator = new PackageEnumerator(solutionData);

            return new Context
            {
                Round = 1,
                Schedule = new ProcessingSchedule(schedule, solutionData.GetPackage),
                Reporter = this.reporterFactory.Create(),
                Waiter = this.waiterFactory.Create<ITask>(),
                Packager = this.packageManagerFactory.Create(),
                Compiler = this.projectCompilerFactory.Create(packageEnumerator, solutionData.Output),
                Tester = this.projectTesterFactory.Create(packageEnumerator, solutionData.Output)
            };
        }
    }
}
