using System;

namespace noide.tests
{
	public class ProjectMetadataFake : IProjectMetadata
	{
		public String Name
		{
			get { return "abc"; }
		}

		public String Path
		{
			get { return "c:\\projects\\abc"; }
		}

		public String Type
		{
			get { return "library"; }
			set { throw new NotSupportedException(); }
		}
	}
}