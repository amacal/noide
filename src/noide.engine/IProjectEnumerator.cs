using System;

namespace noide
{
	public interface IProjectEnumerator
	{
		String[] FindProjects(String path);
	}
}