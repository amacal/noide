using NUnit.Framework;

namespace noide.tests
{
	[TestFixture]
	public partial class CommandFactoryTests
	{
		private SolutionWatcher solutionWatcher;
		private SolutionMerger solutionMerger;

		[SetUp]
		public void SetUp()
		{
			this.solutionWatcher = new SolutionWatcher();
			this.solutionMerger = new SolutionMerger();
		}

		private ICommandFactory CreateCommandFactory()
		{
			return new CommandFactory(this.solutionWatcher, this.solutionMerger);
		}
	}
}