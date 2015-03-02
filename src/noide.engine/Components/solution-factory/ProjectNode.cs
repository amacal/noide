using System;
using System.Collections.Generic;

namespace noide
{
    partial class SolutionFactory
    {
        public class ProjectNode
        {
            private readonly IProject target;
            private readonly List<ProjectNode> before;
            private readonly List<ProjectNode> after;
            
            public ProjectNode(IProject target)
            {
                this.target = target;
                this.before = new List<ProjectNode>();
                this.after = new List<ProjectNode>();
            }
            
            public IProject Target
            {
                get { return this.target; }
            }
            
            public void AddBefore(ProjectNode node)
            {
                this.before.Add(node);
            }

            public void AddAfter(ProjectNode node)
            {
                this.after.Add(node);
            }
            
            public IReadOnlyCollection<ProjectNode> GetBefore()
            {
                return this.before.AsReadOnly();
            }

            public IReadOnlyCollection<ProjectNode> GetAfter()
            {
                return this.after.AsReadOnly();
            }
            
            public bool HasNothingBefore()
            {
                return this.before.Count == 0;
            }

            public bool HasNothingBefore(Predicate<ProjectNode> predicate)
            {
                return this.before.Find(predicate) == null;
            }

            public bool HasNothingAfter()
            {
                return this.after.Count == 0;
            }
        }
    }
}
