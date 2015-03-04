using System;
using NUnit.Framework;

namespace noide.tests
{
	partial class CommandFactoryTests
	{
		[Test]
		public void WhenWatchingCommandIsExecutedItPassesPathToTheSolutionWatcher()
		{
			ICommandFactory commandFactory = this.CreateCommandFactory();
			IArgument[] arguments = new IArgument[] { new WatchVerb(), new WatchPath() };
			ICommand command = commandFactory.Create(arguments);

			command.Execute();

			Assert.That(this.solutionWatcher.Path, Is.EqualTo("c:\\projects\\myproject"));
		}

		private class WatchVerb : IArgument
		{
			public IOption Option
			{
				get { return null; }
			}

			public String Value
			{
				get { return "watch"; }
			}
		}

		private class WatchPath : IArgument, IOption
		{
			public IOption Option
			{
				get { return this; }
			}

			public String Value
			{
				get { return "c:\\projects\\myproject"; }
			}

			public char Short
			{
				get { return default(char); }
			}

			public String Long
			{
				get { return "path"; }
			}
		}
	}
}