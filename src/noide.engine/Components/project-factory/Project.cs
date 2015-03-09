using System;

namespace noide
{
	partial class ProjectFactory
	{
		private class Project : IProject
	    {
	        private ProjectMetadata metadata;
	        private ProjectDependencies dependencies;
	       
	        public Project(ProjectMetadata metadata)
	        {
	            this.metadata = metadata;
	            this.dependencies = new ProjectDependencies();
	        }
	        
	        public IProjectMetadata Metadata
	        {
	            get { return this.metadata; }
	        }

	        public IReferenceCollection References
	        {
	            get { return this.dependencies.References; }
	        }
	        
	        public IProjectReferenceCollection ProjectReferences
	        {
	            get { return this.dependencies.Projects; }
	        }
	        
	        public IPackageReferenceCollection PackageReferences
	        {
	            get { return this.dependencies.Packages; }
	        }
	    }
	}
}
