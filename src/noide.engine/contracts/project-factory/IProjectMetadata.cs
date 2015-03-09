using System;

namespace noide
{
	public interface IProjectMetadata
	{
		String Name { get; }

		String Path { get; }

		String Type { get; set; }
	}
}