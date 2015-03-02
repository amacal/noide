namespace noide
{
	public interface IWatcherFactory
	{
		IWatcher<T> Create<T>(IHeartBeatFilter<T> filter);
	}
}