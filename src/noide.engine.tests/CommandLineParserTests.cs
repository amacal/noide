using System;
using NUnit.Framework;
using Onlysharp;

namespace noide.tests
{
	[TestFixture]
	public class CommandLineParserTests
	{
		[Test]
		public void WhenParsingNullItReturnsEmptyArray()
		{
			String commandLine = null;
			CommandLineParser parser = new CommandLineParser();

			String[] argv = parser.Parse(commandLine);

			Assert.That(argv, Is.Empty);
		}

		[Test]
		public void WhenParsingNothingItReturnsEmptyArray()
		{
			String commandLine = String.Empty;
			CommandLineParser parser = new CommandLineParser();

			String[] argv = parser.Parse(commandLine);

			Assert.That(argv, Is.Empty);
		}

		[Test]
		public void WhenParsingCommandItReturnsIt()
		{
			String commandLine = "dir";
			CommandLineParser parser = new CommandLineParser();

			String[] argv = parser.Parse(commandLine);

			Assert.That(argv, Is.EqualTo(new[] { "dir" }));
		}

		[Test]
		public void WhenParsingCommandWithSpacesAtTheEndItReturnsIt()
		{			
			String commandLine = "dir  ";
			CommandLineParser parser = new CommandLineParser();

			String[] argv = parser.Parse(commandLine);

			Assert.That(argv, Is.EqualTo(new[] { "dir" }));
		}

		[Test]
		public void WhenParsingCommandWithParametersItReturnsIt()
		{
			String commandLine = "dir --recursive";
			CommandLineParser parser = new CommandLineParser();

			String[] argv = parser.Parse(commandLine);

			Assert.That(argv, Is.EqualTo(new[] { "dir", "--recursive" }));			
		}

		[Test]
		public void WhenParsingCommandWithParametersSeparatedWithManySpaces()
		{
			String commandLine = "dir --recursive  --system";
			CommandLineParser parser = new CommandLineParser();

			String[] argv = parser.Parse(commandLine);

			Assert.That(argv, Is.EqualTo(new[] { "dir", "--recursive", "--system" }));
		}

		[Test]
		public void WhenParsingEscapedCommandItReturnsIt()
		{
			String commandLine = "\"dir\"";
			CommandLineParser parser = new CommandLineParser();

			String[] argv = parser.Parse(commandLine);

			Assert.That(argv, Is.EqualTo(new[] { "dir" }));			
		}

		[Test]
		public void WhenParsingEscapedCommandWithParametersItReturnsIt()
		{
			String commandLine = "\"dir\" --recursive";
			CommandLineParser parser = new CommandLineParser();

			String[] argv = parser.Parse(commandLine);

			Assert.That(argv, Is.EqualTo(new[] { "dir", "--recursive" }));			
		}

		[Test]
		public void WhenParsingEscapedCommandWithEscapedParametersSeparatedWithManySpaces()
		{
			String commandLine = "\"dir\" \"--recursive\"  \"--system\"";
			CommandLineParser parser = new CommandLineParser();

			String[] argv = parser.Parse(commandLine);

			Assert.That(argv, Is.EqualTo(new[] { "dir", "--recursive", "--system" }));
		}
	}
}