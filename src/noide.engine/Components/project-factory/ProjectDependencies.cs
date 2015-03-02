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
                get { return new ReferenceCollection(this.references.AsReadOnly()); }
            }
            
            public IProjectReferenceCollection Projects
            {
                get { return new ProjectReferenceCollection(this.projects.AsReadOnly()); }
            }
            
            public IPackageReferenceCollection Packages
            {
                get { return new PackageReferenceCollection(this.packages.AsReadOnly()); }
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

            private class ReferenceCollection : IReferenceCollection
            {   
                private readonly ReadOnlyCollection<Reference> references;

                public ReferenceCollection(ReadOnlyCollection<Reference> references)
                {
                    this.references = references;
                }

                public IEnumerable<Reference> AsEnumerable()
                {
                    return this.references;
                }

                public bool Contains(Reference reference)
                {
                    return this.references.Contains(reference);
                }
            }

            private class ProjectReferenceCollection : IProjectReferenceCollection
            {
                private readonly ReadOnlyCollection<ProjectReference> references;

                public ProjectReferenceCollection(ReadOnlyCollection<ProjectReference> references)
                {
                    this.references = references;
                }

                public IEnumerable<ProjectReference> AsEnumerable()
                {
                    return this.references;
                }

                public bool Contains(ProjectReference reference)
                {
                    return this.references.Contains(reference);
                }
            }

            private class PackageReferenceCollection : IPackageReferenceCollection
            {
                private readonly ReadOnlyCollection<PackageReference> references;

                public PackageReferenceCollection(ReadOnlyCollection<PackageReference> references)
                {
                    this.references = references;
                }

                public IEnumerable<PackageReference> AsEnumerable()
                {
                    return this.references;
                }

                public bool Contains(PackageReference reference)
                {
                    return this.references.Contains(reference);
                }
            }
        }
    }
}
