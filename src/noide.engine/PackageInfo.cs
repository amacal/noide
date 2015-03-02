using System;
using System.Collections.Generic;

namespace noide
{
    public class PackageInfo
    {
        private readonly String name;
        private readonly String version;
        private readonly String[] references;
        
        public PackageInfo(String name, String version, String[] references)
        {
            this.name = name;
            this.version = version;
            this.references = references;
        }
        
        public String Name
        {
            get { return this.name; }
        }
        
        public String Version
        {
            get { return this.version; }
        }
        
        public IReadOnlyCollection<string> GetReferences()
        {
            return this.references;
        }
    }
}
