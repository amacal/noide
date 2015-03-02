using System;

namespace noide
{
	partial class ProjectFactory
	{
		private class Project : IProject
	    {
	        private ProjectMetadata metadata;
	        private ProjectDependencies dependencies;
	        private String type;
	       
	        public Project(ProjectMetadata metadata)
	        {
	            this.metadata = metadata;
	            this.dependencies = new ProjectDependencies();
	            this.type = "library";
	        }
	        
	        public ProjectMetadata Metadata
	        {
	            get { return this.metadata; }
	        }

	        public String Type
	        {
	        	get { return this.type; }
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
	                
	        public void Update(IProjectReader reader)
	        {
	            ProjectConfigurer configurer = new ProjectConfigurer(this.metadata);
	            
	            reader.Update(configurer);
	            
	            this.dependencies = new ProjectDependencies();
	            this.dependencies.AddReferences(configurer.References);
	            this.dependencies.AddProjects(configurer.Projects);
	            this.dependencies.AddPackages(configurer.Packages);

	            this.type = configurer.Type;
	        }
	    }
	}
}
