namespace noide
{
	partial class SolutionCompiler
	{
        private interface ITask
        {
            bool Complete(IContext context);

            IExecutable Continue(IContext context);

            void Release();
        }
	}
}