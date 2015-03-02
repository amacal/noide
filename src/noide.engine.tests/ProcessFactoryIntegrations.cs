using System;
using System.Text;
using NUnit.Framework;
using Onlysharp;

namespace noide.tests
{
	[TestFixture]
	[Category("Integration")]
	public class ProcessFactoryIntegrations
	{
		[Test]
		public void WhenExecutingItProvidesOutput()
		{
			Native native = new Native();
			ProcessFactory factory = new ProcessFactory(native);

			IProcess process = factory.Execute(Environment.CurrentDirectory, "ping");
			String output = process.GetOutput();

			Assert.That(output, Is.Not.Empty);
		}
	}
}