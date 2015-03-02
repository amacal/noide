using System;
using NUnit.Framework;
using Onlysharp;

namespace noide.tests
{
	[TestFixture]
	public class ProjectCompilerFactoryTests
	{
		[Test]
		public void WhenGettingCompilerItReturnsNotNull()
		{
			Compiler compiler = new Compiler();
			SourceEnumerator sourceEnumerator = new SourceEnumerator();
			ReferenceEnumerator referenceEnumerator = new ReferenceEnumerator();
			ProjectCompilerFactory factory = new ProjectCompilerFactory(sourceEnumerator, compiler);

			IProjectCompiler result = factory.Create(referenceEnumerator, "c:\\output");

			Assert.That(result, Is.Not.Null);
		}

		private class SourceEnumerator : ISourceEnumerator
		{
			public String[] FindSources(String path)
			{
				return new String[0];
			}
		}

		private class ReferenceEnumerator : IReferenceEnumerator
		{
			public String[] FindReferences(String package, String version)
			{
				return new String[0];
			}
		}

		private class Compiler : ICompiler
		{
			public IResource Compile(ICompilable target)
			{
				return new Resource();
			}
		}

		private class Resource : IResource
		{
			public IntPtr[] Handles
			{
				get { return new[] { new IntPtr(20) }; }
			}

            public bool Complete(IntPtr handle)
			{
				return false;
			}

			public bool IsSuccessful()
			{
				return false;
			}

			public void Release()
			{
			}
		}
	}
}