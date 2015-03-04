using System;

namespace noide
{
	public interface IFileService
	{
		bool Exists(String path);

		String FindFile(String path, String filename);
	}
}