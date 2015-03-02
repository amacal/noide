namespace noide
{
	public interface IWatcher<T>
	{
		void Add(IWatchable<T> target);

		IHeartBeat<T> Next();
	}
}