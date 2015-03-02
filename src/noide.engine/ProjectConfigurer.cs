using System;
using System.Collections.Generic;

namespace noide
{
    public class ProjectConfigurer
    {
        private readonly ProjectMetadata metadata;
        private String type;

        private List<Reference> references;
        private List<ProjectReference> projects;
        private List<PackageReference> packages;

        public ProjectConfigurer(ProjectMetadata metadata)
        {
            this.metadata = metadata;
            this.type = "library";

            this.references = new List<Reference>();
            this.projects = new List<ProjectReference>();
            this.packages = new List<PackageReference>();
        }
        
        public ProjectMetadata Metadata
        {
            get { return this.metadata; }
        }

        public String Type
        {
            get { return this.type; }
        }
        
        public IReadOnlyCollection<Reference> References
        {
            get { return this.references.AsReadOnly(); }
        }
        
        public IReadOnlyCollection<ProjectReference> Projects
        {
            get { return this.projects.AsReadOnly(); }
        }
        
        public IReadOnlyCollection<PackageReference> Packages
        {
            get { return this.packages.AsReadOnly(); }
        }

        public void SetType(String type)
        {
            this.type = type;
        }
        
        public void AddReference(Reference reference)
        {
            this.references.Add(reference);
        }
        
        public void AddProject(ProjectReference project)
        {
            this.projects.Add(project);
        }
        
        public void AddPackage(PackageReference package)
        {
            this.packages.Add(package);
        }
    }
}
