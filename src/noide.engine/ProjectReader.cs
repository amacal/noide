using System;
using System.IO;

namespace noide
{
    public class ProjectReader : IProjectReader
    {
        private readonly IJsonReader jsonReader;

        public ProjectReader(IJsonReader jsonReader)
        {
            this.jsonReader = jsonReader;
        }

        public void Update(ProjectConfigurer configurer)
        {
            String path = Path.Combine(configurer.Metadata.Path, ".project.json");
            ProjectData data = this.jsonReader.Read<ProjectData>(path);

            if (data != null)
            {
                if (data.metadata != null)
                {
                    if (data.metadata.type != null)
                    {
                        configurer.SetType(data.metadata.type);
                    }
                }

                if (data.references != null)
                {
                    foreach (String reference in data.references)
                    {
                        configurer.AddReference(new Reference(reference));
                    }
                }
            
                if (data.dependencies != null)
                {
                    foreach (String dependency in data.dependencies)
                    {
                        configurer.AddProject(new ProjectReference(dependency));
                    }
                }
            
                if (data.packages != null)
                {
                    foreach (PackageData package in data.packages)
                    {
                        configurer.AddPackage(new PackageReference(package.name, package.version));
                    }
                }
            }
        }
       
        public class ProjectData
        {
            public MetadataData metadata { get; set; }
            public String[] references { get; set; }
            public String[] dependencies { get; set; }
            public PackageData[] packages { get; set; }
        }
        
        public class MetadataData
        {
            public String type { get; set; }
        }

        public class PackageData
        {
            public String name { get; set; }
            public String version { get; set; }
        }
    }
}
