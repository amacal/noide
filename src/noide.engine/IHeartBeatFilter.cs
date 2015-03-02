namespace noide
{
	public interface IHeartBeatFilter<T>
	{
		IHeartBeat<T> Filter(IHeartBeat<T> beat);
	}
}