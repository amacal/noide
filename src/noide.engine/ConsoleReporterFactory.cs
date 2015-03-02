namespace noide
{
	public class ConsoleReporterFactory : IReporterFactory
	{
		public IReporter Create()
		{
			return new ConsoleReporter();
		}
	}
}