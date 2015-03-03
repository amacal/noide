namespace noide
{
	public interface IArgumentParser
	{
		IArgument[] Parse(string[] argv);
	}

	public interface IArgument
	{
		IOption Option { get; }

		string Value { get; }
	}

	public interface IOption
	{
		char Short { get; }

		string Long { get; }
	}
}