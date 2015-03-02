using System;
using System.Collections.Generic;

namespace noide.tests
{
	public class CommandLineParser
	{
		public String[] Parse(String commandLine)
		{
			List<String> result = new List<String>();

			int begin = 0;
			bool escape = false;

			if (String.IsNullOrEmpty(commandLine) == false)
			{
				for (int i = 0; i < commandLine.Length; i++)
				{
					if (commandLine[i] == '"' && escape == false)
					{
						escape = true;
						begin++;
						continue;
					}

					if (commandLine[i] == '"' && escape == true)
					{
						escape = false;
						result.Add(commandLine.Substring(begin, i - begin));
						begin = i + 1;
						continue;
					}

					if (commandLine[i] == ' ' && i == begin)
					{
						begin++;
						continue;
					}

					if (commandLine[i] == ' ' && i > begin)
					{
						result.Add(commandLine.Substring(begin, i - begin));
						begin = i + 1;
						continue;
					}
				}

				if (begin < commandLine.Length)
				{
					result.Add(commandLine.Substring(begin, commandLine.Length - begin));
				}
			}

			return result.ToArray();
		}
	}
}