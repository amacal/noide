using System;

namespace noide.tests
{
	public class ProjectMetadataStub : IProjectMetadata
	{
		private readonly String name;
		private readonly String path;
		private String type;

		public ProjectMetadataStub(String path, String name)
		{
			this.name = name;
			this.path = path;
			this.type = "library";
		}

		public String Name
		{
			get { return this.name; }
		}

		public String Path
		{
			get { return this.path; }
		}

		public String Type
		{
			get { return this.type; }
			set { this.type = value; }
		}
	}
}