using System;
using System.Collections.Generic;

namespace noide
{
    public class SolutionConfigurer
    {
        private readonly SolutionMetadata metadata;
        
        private String outputPath;
        private String projectsPath;
        private String packagesPath;
        
        private List<IProject> projects;
        private List<PackageInfo> packages;
        
        public SolutionConfigurer(SolutionMetadata metadata)
        {
            this.metadata = metadata;

            this.outputPath = metadata.Path;
            this.projectsPath = metadata.Path;
            this.packagesPath = metadata.Path;
            
            this.projects = new List<IProject>();
            this.packages = new List<PackageInfo>();
        }
        
        public SolutionMetadata Metadata
        {
            get { return this.metadata; }
        }
        
        public String ProjectsPath
        {
            get { return this.projectsPath; }
        }
        
        public IReadOnlyCollection<IProject> Projects
        {
            get { return this.projects.AsReadOnly(); }
        }
        
        public String PackagesPath
        {
            get { return this.packagesPath; }
        }
        
        public IReadOnlyCollection<PackageInfo> Packages
        {
            get { return this.packages.AsReadOnly(); }
        }
        
        public String OutputPath
        {
            get { return this.outputPath; }
        }
        
        public void SetOutput(String path)
        {
            this.outputPath = System.IO.Path.Combine(this.metadata.Path, path);
        }
        
        public void SetProjects(String path, IReadOnlyCollection<IProject> projects)
        {
            this.projectsPath = System.IO.Path.Combine(this.metadata.Path, path);
            
            foreach (IProject project in projects)
            {
                this.projects.Add(project);
            }
        }
        
        public void SetPackages(String path, IReadOnlyCollection<PackageInfo> packages)
        {            
            this.packagesPath = System.IO.Path.Combine(this.metadata.Path, path);

            foreach (PackageInfo package in packages)
            {
                this.packages.Add(package);
            }
        }
    }
}
