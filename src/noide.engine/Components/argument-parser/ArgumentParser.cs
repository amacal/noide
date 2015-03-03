using System;
using System.Collections.Generic;

namespace noide
{
	public class ArgumentParser : IArgumentParser
	{
		public IArgument[] Parse(string[] argv)
		{
			int offset = 0;
			List<IArgument> arguments = new List<IArgument>();

			while (offset < argv.Length)
			{
				if (argv[offset].StartsWith("--") == true)
				{
					this.HandleLongOption(arguments, argv, ref offset);					
				}
				else if (argv[offset].StartsWith("-") == true)
				{	
					this.HandleShortOption(arguments, argv, ref offset);
				}
				else
				{
					this.HandleValue(arguments, argv, ref offset);
				}
			}

			return arguments.ToArray();
		}

		private void HandleShortOption(ICollection<IArgument> arguments, string[] argv, ref int offset)
		{
			Argument argument = null;

			for (int i = 1; i < argv[offset].Length; i++)
			{
				argument = new Argument
				{
					Option = new Option
					{
						Short = argv[offset][i]
					}
				};

				arguments.Add(argument);
			}

			offset++;

			if (offset < argv.Length && argv[offset].StartsWith("-") == false)
			{
				argument.Value = argv[offset++];
			}
		}

		private void HandleLongOption(ICollection<IArgument> arguments, string[] argv, ref int offset)
		{
			String value = null;
			Option option = new Option
			{
				Long = argv[offset++].Substring(2)
			};

			if (offset < argv.Length && argv[offset].StartsWith("-") == false)
			{
				value = argv[offset++];
			}

			arguments.Add(new Argument
			{
				Option = option,
				Value = value
			});
		}
		
		private void HandleValue(ICollection<IArgument> arguments, string[] argv, ref int offset)
		{
			arguments.Add(new Argument
			{
				Value = argv[offset++]
			});
		}

		private class Argument : IArgument
		{
			public IOption Option { get; set; }
			public String Value { get; set; }
		}

		private class Option : IOption
		{
			public Char Short { get; set; }
			public String Long { get; set; }
		}
	}
}