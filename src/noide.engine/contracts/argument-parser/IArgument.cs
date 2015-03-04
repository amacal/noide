namespace noide
{
	public interface IArgument
	{
		IOption Option { get; }

		string Value { get; }
	}
}