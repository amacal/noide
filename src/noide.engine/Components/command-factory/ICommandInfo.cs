namespace noide
{
	partial class CommandFactory
	{
		private interface ICommandInfo
		{
			bool CanHandle(IArgument[] arguments);

			ICommand Create(IArgument[] arguments);
		}
	}
}