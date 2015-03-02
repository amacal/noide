using System;
using System.IO;

namespace noide
{
	public class ProjectEnumerator : IProjectEnumerator
	{
		public String[] FindProjects(String path)
		{
			return Directory.GetFiles(path, ".project.json", SearchOption.AllDirectories);
		}
	}
}