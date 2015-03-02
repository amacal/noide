namespace noide
{
	public interface IWaiterFactory
	{
		IWaiter<T> Create<T>();
	}
}