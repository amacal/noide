namespace noide
{
	public interface ICommandFactory
	{
		ICommand Create(IArgument[] arguments);
	}
}