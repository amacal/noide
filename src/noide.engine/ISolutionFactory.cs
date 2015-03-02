using System;

namespace noide
{
	public interface ISolutionFactory
	{
		ISolution Create(String path);
	}
}