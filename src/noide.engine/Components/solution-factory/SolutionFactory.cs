using System;

namespace noide
{
	public partial class SolutionFactory : ISolutionFactory
	{
		private readonly ISolutionReader solutionReader;

		public SolutionFactory(ISolutionReader solutionReader)
		{
			this.solutionReader = solutionReader;
		}

		public ISolution Create(String path)
		{
			Solution solution = new Solution(new SolutionMetadata(path));
            solution.Update(this.solutionReader);
			return solution;
		}
	}
}