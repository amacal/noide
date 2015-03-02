namespace noide
{
	public class WaiterFactory : IWaiterFactory
	{
		private readonly INative native;

		public WaiterFactory(INative native)
		{
			this.native = native;
		}

		public IWaiter<T> Create<T>()
		{
			return new Waiter<T>(this.native);
		}
	}
}