using System;

namespace noide
{
	public interface IProjectFactory
	{
		IProject Create(String path);
	}
}