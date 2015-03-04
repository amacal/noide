using System;

namespace noide
{
	public interface ISourceEnumerator
	{
		String[] FindSources(String path);
	}
}