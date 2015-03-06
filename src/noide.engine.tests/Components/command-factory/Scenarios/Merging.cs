using System;
using NUnit.Framework;

namespace noide.tests
{
	partial class CommandFactoryTests
	{
		[Test]
		public void WhenMergingCommandIsExecutedItPassesPathToTheSolutionMerger()
		{
			ICommandFactory commandFactory = this.CreateCommandFactory();
			IArgument[] arguments = new IArgument[] { new MergeVerb(), new MergePath() };
			ICommand command = commandFactory.Create(arguments);

			command.Execute();

			Assert.That(this.solutionMerger.Path, Is.EqualTo("c:\\projects\\myproject"));
		}

		private class MergeVerb : IArgument
		{
			public IOption Option
			{
				get { return null; }
			}

			public String Value
			{
				get { return "merge"; }
			}
		}

		private class MergePath : IArgument, IOption
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