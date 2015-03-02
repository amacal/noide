using System;

namespace noide
{
	public class PackageMetadata
	{
		private readonly String path;
		private readonly String name;
		private readonly String version;

		public PackageMetadata(String path, String name, String version)
		{
			this.path = path;
			this.name = name;
			this.version = version;
		}

		public String Path
		{
			get { return this.path; }
		}

		public String Name
		{
			get { return this.name; }
		}

		public String Version
		{
			get { return this.version; }
		}
	}
}