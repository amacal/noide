namespace noide
{
	public interface IArgumentParser
	{
		IArgument[] Parse(string[] argv);
	}
}