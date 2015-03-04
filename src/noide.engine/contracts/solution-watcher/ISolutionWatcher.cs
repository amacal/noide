using System;

namespace noide
{
	public interface ISolutionWatcher
	{
		void Watch(IStop stop, String path);
	}
}