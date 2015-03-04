using NUnit.Framework;

namespace noide.tests
{
	[TestFixture]
	public partial class CommandFactoryTests
	{
		private SolutionWatcher solutionWatcher;

		[SetUp]
		public void SetUp()
		{
			this.solutionWatcher = new SolutionWatcher();
		}

		private ICommandFactory CreateCommandFactory()
		{
			return new CommandFactory(this.solutionWatcher);
		}
	}
}