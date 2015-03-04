using System;

namespace noide
{
	public interface IProjectCompilerFactory
	{
		IProjectCompiler Create(IReferenceEnumerator referenceEnumerator, String output);
	}
}