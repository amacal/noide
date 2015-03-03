using System;
using NUnit.Framework;

namespace noide.engine.tests
{
	partial class ArgumentParserTests
	{
		[Test]
		public void WhenParsingNoDataItReturnsEmptyArray()
		{
			String[] argv = new String[0];
			IArgumentParser parser = new ArgumentParser();

			IArgument[] arguments = parser.Parse(argv);

			Assert.That(arguments, Is.Empty);
		}

		[Test]
		public void WhenParsingValueItReturnsIt()
		{
			String[] argv = new String[] { "value" };
			IArgumentParser parser = new ArgumentParser();

			IArgument[] arguments = parser.Parse(argv);

			Assert.That(arguments[0].Value, Is.EqualTo("value"));
		}

		[Test]
		public void WhenParsingShortOptionItReturnsIt()
		{
			String[] argv = new String[] { "-v" };
			IArgumentParser parser = new ArgumentParser();

			IArgument[] arguments = parser.Parse(argv);

			Assert.That(arguments[0].Option.Short, Is.EqualTo('v'));
		}

		[Test]
		public void WhenParsingShortOptionFollowedByValueItReturnsIt()
		{
			String[] argv = new String[] { "-v", "value" };
			IArgumentParser parser = new ArgumentParser();

			IArgument[] arguments = parser.Parse(argv);

			Assert.That(arguments[0].Option.Short, Is.EqualTo('v'));
			Assert.That(arguments[0].Value, Is.EqualTo("value"));
		}

		[Test]
		public void WhenParsingTwoConnectedShortOptionsItReturnsThem()
		{
			String[] argv = new String[] { "-va" };
			IArgumentParser parser = new ArgumentParser();

			IArgument[] arguments = parser.Parse(argv);

			Assert.That(arguments[0].Option.Short, Is.EqualTo('v'));
			Assert.That(arguments[1].Option.Short, Is.EqualTo('a'));
		}

		[Test]
		public void WhenParsingTwoConnectedShortOptionsFollowedByValueItReturnsTheValueAttachedToTheLastOption()
		{
			String[] argv = new String[] { "-va", "value" };
			IArgumentParser parser = new ArgumentParser();

			IArgument[] arguments = parser.Parse(argv);

			Assert.That(arguments[0].Value, Is.Null);
			Assert.That(arguments[1].Value, Is.EqualTo("value"));			
		}

		[Test]
		public void WhenParsingLongOptionItReturnsIt()
		{
			String[] argv = new String[] { "--version" };
			IArgumentParser parser = new ArgumentParser();

			IArgument[] arguments = parser.Parse(argv);

			Assert.That(arguments[0].Option.Long, Is.EqualTo("version"));
		}

		[Test]
		public void WhenParsingLongOptionFollowedByValueItReturnsIt()
		{
			String[] argv = new String[] { "--version", "value" };
			IArgumentParser parser = new ArgumentParser();

			IArgument[] arguments = parser.Parse(argv);

			Assert.That(arguments[0].Option.Long, Is.EqualTo("version"));
			Assert.That(arguments[0].Value, Is.EqualTo("value"));
		}
	}
}