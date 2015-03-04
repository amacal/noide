using System;

namespace noide
{
	public interface IProjectCompiler
	{
		IResource Compile(IProject project);
	}
}