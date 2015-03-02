using System;
using System.IO;

namespace noide
{
	public class SourceEnumerator : ISourceEnumerator
	{
		public String[] FindSources(String path)
		{
			return Directory.GetFiles(path, "*.cs", SearchOption.AllDirectories);
		}
	}
}