using System;
using System.IO;

namespace noide
{
    public class SolutionReader : ISolutionReader
    {
        private readonly IJsonReader jsonReader;
        private readonly IProjectEnumerator projectEnumerator;
        private readonly IProjectFactory projectFactory;

        public SolutionReader(IJsonReader jsonReader, IProjectEnumerator projectEnumerator, IProjectFactory projectFactory)
        {
            this.jsonReader = jsonReader;
            this.projectEnumerator = projectEnumerator;
            this.projectFactory = projectFactory;
        }

        public void Update(SolutionConfigurer configurer)
        {
            String path = Path.Combine(configurer.Metadata.Path, ".solution.json");
            SolutionData data = this.jsonReader.Read<SolutionData>(path);

            this.ConfigureOutput(configurer, data);
            this.ConfigureProjects(configurer, data);
            this.ConfigurePackages(configurer, data);
        }

        private void ConfigureOutput(SolutionConfigurer configurer, SolutionData data)
        {
            configurer.SetOutput(data.folders.output);
        }

        private void ConfigureProjects(SolutionConfigurer configurer, SolutionData data)
        {
            String sources = Path.Combine(configurer.Metadata.Path, data.folders.sources);
            String[] proj = this.projectEnumerator.FindProjects(sources);
            IProject[] projects = new IProject[proj.Length];
            
            for (int i = 0; i < proj.Length; i++)
            {
                projects[i] = this.projectFactory.Create(proj[i]);
            }
            
            configurer.SetProjects(data.folders.sources, projects);            
        }

        private void ConfigurePackages(SolutionConfigurer configurer, SolutionData data)
        {
            PackageInfo[] packages = new PackageInfo[data.packages.Length];
            
            for (int i = 0; i < data.packages.Length; i++)
            {
                packages[i] = new PackageInfo(data.packages[i].name, data.packages[i].version, data.packages[i].references);
            }
            
            configurer.SetPackages(data.folders.packages, packages);            
        }
        
        public class SolutionData
        {
            public FolderData folders { get; set; }
            public PackageData[] packages { get; set; }
        }
    
        public class PackageData
        {
            public string name { get; set; }
            public string version { get; set; }
            public string[] references { get; set; }
        }
    
        public class FolderData
        {
            public string output { get; set; }
            public string sources { get; set; }
            public string packages { get; set; }
        }
    }
}
