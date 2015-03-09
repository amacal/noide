using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace noide
{
    partial class ProjectFactory
    {
        public class ProjectDependencies
        {
            private List<Reference> references;
            private List<ProjectReference> projects;
            private List<PackageReference> packages;

            public ProjectDependencies()
            {
                this.references = new List<Reference>();
                this.projects = new List<ProjectReference>();
                this.packages = new List<PackageReference>();
            }
            
            public IReferenceCollection References
            {
                get { return new ReferenceCollection(this.references); }
            }
            
            public IProjectReferenceCollection Projects
            {
                get { return new ProjectReferenceCollection(this.projects); }
            }
            
            public IPackageReferenceCollection Packages
            {
                get { return new PackageReferenceCollection(this.packages); }
            }

            public void AddReferences(IReadOnlyCollection<Reference> references)
            {
                this.references.AddRange(references);
            }
            
            public void AddProjects(IReadOnlyCollection<ProjectReference> projects)
            {
                this.projects.AddRange(projects);
            }
            
            public void AddPackages(IReadOnlyCollection<PackageReference> packages)
            {
                this.packages.AddRange(packages);
            }
        }
    }
}
