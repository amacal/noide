using System;

namespace noide
{
	public class ConsoleReporter : IReporter
	{
		public void Beat()
		{
			Console.Write(".");
		}

		public void Trigger(IProject project)
		{			
            Console.WriteLine();
            Console.WriteLine("0 - {0} - triggered", project.Metadata.Name);
		}

		public void BeginRestoring(int round, IPackage package)
		{
			Console.WriteLine("{1} - {0} - restoring", package.Metadata.Name, round);
		}

		public void CompleteRestoring(IPackage package, bool isSuccessful)
		{
			Console.WriteLine("  - {0} - restoring | successful: {1}", package.Metadata.Name, isSuccessful);
		}

		public void BeginCompiling(int round, IProject project)
		{
			Console.WriteLine("{1} - {0} - compiling", project.Metadata.Name, round);
		}

		public void CompleteCompiling(IProject project, bool isSuccessful)
		{
			Console.WriteLine("  - {0} - compiling | successful: {1}", project.Metadata.Name, isSuccessful);
		}

		public void BeginTesting(int round, IProject project)
		{
			Console.WriteLine("{1} - {0} - testing", project.Metadata.Name, round);
		}

		public void CompleteTesting(IProject project, ITestingResult result)
		{
			Console.WriteLine("  - {0} - testing | successful: {1}", project.Metadata.Name, result.IsSuccessful());

			foreach (ITestingCase testignCase in result.GetCases())
			{
				Console.WriteLine("  * " + testignCase.Name);

				foreach (String remark in testignCase.GetRemarks())
				{
					Console.Write("    ");
					Console.WriteLine(remark);
				}
			}
		}
	}
}