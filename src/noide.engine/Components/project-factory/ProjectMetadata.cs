using System;

namespace noide
{
    partial class ProjectFactory
    {
        private class ProjectMetadata : IProjectMetadata
        {
            private readonly String path;
            private readonly String name;
            private String type;
            
            public ProjectMetadata(String path, String name)
            {
                this.path = path;
                this.name = name;
                this.type = "library";
            }
            
            public String Path
            {
                get { return this.path; }
            }
            
            public String Name
            {
                get { return this.name; }
            }

            public String Type
            {
                get { return this.type; }
                set { this.type = value; }
            }
        }
    }
}
