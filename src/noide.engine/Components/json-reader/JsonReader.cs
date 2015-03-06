using System;
using System.IO;

namespace noide
{
	public class JsonReader : IJsonReader
	{
		public TData Read<TData>(String path)
		{
            String content = File.ReadAllText(path);
            TData data = NetJSON.NetJSON.Deserialize<TData>(content);

            return data;
		}
	}
}