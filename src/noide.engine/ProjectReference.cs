using System;

namespace noide
{
    public class ProjectReference
    {
        private readonly String name;
        
        public ProjectReference(String name)
        {
            this.name = name;
        }
        
        public String Name
        {
            get { return this.name; }
        }

        public override int GetHashCode()
        {
            return this.name.GetHashCode();
        }

        public override bool Equals(Object obj)
        {
            ProjectReference second = obj as ProjectReference;

            return second != null && second.Name == this.Name;
        }
    }
}
