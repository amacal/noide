using System;

namespace noide
{
    public class Reference
    {
        private readonly String name;
        
        public Reference(String name)
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
            Reference second = obj as Reference;

            return second != null && second.Name == this.Name;
        }
    }
}
