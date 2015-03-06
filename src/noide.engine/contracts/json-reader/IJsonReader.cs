using System;

namespace noide
{
	public interface IJsonReader
	{
		TData Read<TData>(String path);
	}
}