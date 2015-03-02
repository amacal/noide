using System;

namespace noide
{
    public class PackageReference
    {
        private readonly String name;
        private readonly String version;
        
        public PackageReference(String name, String version)
        {
            this.name = name;
            this.version = version;
        }
        
        public String Name
        {
            get { return this.name; }
        }
        
        public String Version
        {
            get { return this.version; }
        }

        public override int GetHashCode()
        {
            return this.name.GetHashCode();
        }

        public override bool Equals(Object obj)
        {
            PackageReference other = obj as PackageReference;

            return other != null && other.Name == this.Name && other.Version == this.Version;
        }
    }
}
