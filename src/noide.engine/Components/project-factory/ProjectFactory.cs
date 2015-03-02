using System;
using System.IO;

namespace noide
{
	public partial class ProjectFactory : IProjectFactory
	{
		private readonly IProjectReader projectReader;

		public ProjectFactory(IProjectReader projectReader)
		{
			this.projectReader = projectReader;
		}

		public IProject Create(String path)
		{
            String directory = Path.GetDirectoryName(path);
            String name = Path.GetFileName(directory);
            
            ProjectMetadata metadata = new ProjectMetadata(directory, name);
            Project project = new Project(metadata);

            project.Update(this.projectReader);
			return project;
		}
	}
}