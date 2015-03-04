namespace noide
{
	public interface ICompiler
	{
		IResource Compile(ICompilable target);
	}
}