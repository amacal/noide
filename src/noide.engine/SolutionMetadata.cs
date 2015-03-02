using System;

namespace noide
{
    public class SolutionMetadata
    {
        private readonly String path;
        
        public SolutionMetadata(String path)
        {
            this.path = path;
        }
        
        public String Path
        {
            get { return this.path; }
        }
    }
}
