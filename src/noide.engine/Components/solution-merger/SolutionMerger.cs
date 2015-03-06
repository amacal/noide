using System;
using System.Collections.Generic;

namespace noide
{
	public partial class SolutionMerger : ISolutionMerger
	{
		private readonly ISolutionFactory solutionFactory;
		private readonly ISourceEnumerator sourceEnumerator;

		public SolutionMerger(ISolutionFactory solutionFactory, ISourceEnumerator sourceEnumerator)
		{
			this.solutionFactory = solutionFactory;
			this.sourceEnumerator = sourceEnumerator;
		}

		public void Merge(String path)
		{
			ISolution solution = this.solutionFactory.Create(path);
			Target target = new Target(solution, this.sourceEnumerator);
		}
	}
}