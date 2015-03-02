using System;

namespace noide
{
    public class ProjectMetadata
    {
        private readonly String path;
        private readonly String name;
        
        public ProjectMetadata(String path, String name)
        {
            this.path = path;
            this.name = name;
        }
        
        public String Path
        {
            get { return this.path; }
        }
        
        public String Name
        {
            get { return this.name; }
        }
    }
}
