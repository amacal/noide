using System;
using NUnit.Framework;

namespace noide.tests
{
	partial class SolutionTests
	{
		[Test]
		public void WhenCreatingNewSolutionItExposesPassedPath()
		{
			SolutionReader reader = new SolutionReader();
			SolutionFactory factory = new SolutionFactory(reader);
			ISolution solution = factory.Create(Factory.DefaultPath);

			Assert.That(solution.Metadata.Path, Is.EqualTo(Factory.DefaultPath));
		}

		[Test]
		public void WhenCreatingNewSolutionItContainsLoadedProjects()
		{
			SolutionReader reader = new SolutionReader();
			SolutionFactory factory = new SolutionFactory(reader);
			ISolution solution = factory.Create(Factory.DefaultPath);

			Assert.That(solution.Projects, Has.Count.EqualTo(2));
		}

		[Test]
		public void WhenCreatingItUpdatesSolutionWithPathExcludingJsonFile()
		{
			SolutionReader reader = new SolutionReader();
			SolutionFactory factory = new SolutionFactory(reader);
			ISolution solution = factory.Create(Factory.DefaultPath);

			Assert.That(reader.Path, Is.EqualTo(Factory.DefaultPath));
		}
	}
}