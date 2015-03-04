using System;

namespace noide
{
    public static class Program
    {
        public static void Main(String[] args)
        {
            Bootstrapper bootstrapper = new Bootstrapper();

            IArgumentParser parser = bootstrapper.CreateArgumentParser();
            ICommandFactory factory = bootstrapper.CreateCommandFactory();

            IArgument[] arguments = parser.Parse(args);
            ICommand command = factory.Create(arguments);

            command.Execute();
        }
    }
}