using System;
using System.Linq;
using NUnit.Framework;
using Onlysharp;

namespace noide.tests
{
	[TestFixture]
	public partial class SolutionTests
	{
		private static class Factory
		{
			public const String DefaultPath = "C:\\Projects\\abc";

			public static SolutionMetadata CreateMetadata()
			{
				return new SolutionMetadata(DefaultPath);
			}
		}
	}
}