using System;
using NUnit.Framework;

namespace noide.tests
{
	partial class SolutionTests
	{
		[Test]
		public void WhenCompilingItPassesOutputPathToCompiler()
		{
			SolutionCompiler compiler = new SolutionCompiler();
			SolutionReader reader = new SolutionReader();
			SolutionFactory factory = new SolutionFactory(reader);
			ISolution solution = factory.Create(Factory.DefaultPath);

			solution.Compile(compiler);

			Assert.That(compiler.Output, Is.EqualTo(Factory.DefaultPath + "\\output"));			
		}
	}
}