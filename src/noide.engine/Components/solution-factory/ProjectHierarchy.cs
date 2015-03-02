using System;
using System.Collections.Generic;

namespace noide
{
    partial class SolutionFactory
    {
        private class ProjectHierarchy
        {
            private readonly String path;
            private readonly List<IProject> projects;
            private readonly Dictionary<String, ProjectNode> nodes;
            
            public ProjectHierarchy(String path)
            {
                this.path = path;
                this.projects = new List<IProject>();
                this.nodes = new Dictionary<String, ProjectNode>();
            }
            
            public String Path
            {
                get { return this.path; }
            }
            
            public IReadOnlyCollection<IProject> Projects
            {
                get { return this.projects.AsReadOnly(); }
            }
            
            public void AddProjects(IReadOnlyCollection<IProject> projects)
            {
                foreach (IProject project in projects)
                {
                    ProjectNode node = new ProjectNode(project);
                    HashSet<ProjectNode> adjusted = new HashSet<ProjectNode>();
                
                    foreach (ProjectReference reference in project.ProjectReferences.AsEnumerable())
                    {
                        ProjectNode referencedNode;
                        this.nodes.TryGetValue(reference.Name, out referencedNode);
                    
                        if (referencedNode != null)
                        {
                            node.AddBefore(referencedNode);
                            referencedNode.AddAfter(node);
                            adjusted.Add(referencedNode);
                        }
                    }
                
                    foreach (ProjectNode item in this.nodes.Values)
                    {
                        if (adjusted.Contains(item) == false)
                        {
                            foreach (ProjectReference reference in item.Target.ProjectReferences.AsEnumerable())
                            {
                                if (reference.Name == project.Metadata.Name)
                                {
                                    node.AddAfter(item);
                                    item.AddBefore(node);
                                }
                            }
                        }
                    }
                
                    this.nodes.Add(project.Metadata.Name, node);
                    this.projects.Add(project);
                }
            }
            
            public IProjectSchedule Order()
            {
                List<ProjectNode> queue = new List<ProjectNode>();
                
                foreach (ProjectNode node in this.nodes.Values)
                {
                    if (node.HasNothingBefore() == true)
                    {
                        queue.Add(node);
                    }
                }
                
                return new ProjectSchedule(new ProjectNode[0], queue);
            }
            
            public IProjectSchedule Order(IProject trigger)
            {
                ProjectNode node;
                List<ProjectNode> after = new List<ProjectNode>();
                
                this.nodes.TryGetValue(trigger.Metadata.Name, out node);
                
                if (node != null)
                {
                    after.Add(node);
                }
                
                Queue<ProjectNode> queue = new Queue<ProjectNode>(after);
                HashSet<ProjectNode> before = new HashSet<ProjectNode>(after);
                
                while (queue.Count > 0)
                {
                	node = queue.Dequeue();
                	
                	foreach (ProjectNode item in node.GetBefore())
                	{
                		if (before.Add(item))
                		{
                			queue.Enqueue(item);
                		}
                	}
                }
                
                return new ProjectSchedule(new List<ProjectNode>(before), after);
            }
        }
    }
}
