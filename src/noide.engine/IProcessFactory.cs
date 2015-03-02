using System;

namespace noide
{
	public interface IProcessFactory
	{
		IProcess Execute(String workingDirectory, String commandLine);
	}
}