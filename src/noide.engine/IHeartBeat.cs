namespace noide
{
	public interface IHeartBeat<T>
	{
		bool IsSuccessful { get; }

		T Payload { get; }
	}
}