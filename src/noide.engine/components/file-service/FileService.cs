using System;
using System.IO;

namespace noide
{
	public class FileService : IFileService
	{
		public bool	Exists(String path)
		{
			return Directory.Exists(path);
		}

		public String FindFile(String path, String filename)
		{
			foreach (String found in Directory.GetFiles(path, filename, SearchOption.AllDirectories))
			{
				return found;
			}

			return null;
		}
	}
}