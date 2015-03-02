namespace noide
{
	public class WatcherFactory : IWatcherFactory
	{
		private readonly INative native;
		private readonly IWaiterFactory waiterFactory;

		public WatcherFactory(INative native, IWaiterFactory waiterFactory)
		{
			this.native = native;
			this.waiterFactory = waiterFactory;
		}

		public IWatcher<T> Create<T>(IHeartBeatFilter<T> filter)
		{
			return new Watcher<T>(filter, this.native, this.waiterFactory);
		}
	}
}

namespace Onlysharp
{
	class A
	{
	}
}