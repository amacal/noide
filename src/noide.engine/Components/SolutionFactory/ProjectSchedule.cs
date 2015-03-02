using System.Collections.Generic;
using System.Linq;

namespace noide
{
    partial class SolutionFactory
    {
        private class ProjectSchedule : IProjectSchedule
        {
            private readonly Queue<ProjectNode> queue;
            private readonly HashSet<ProjectNode> visited;
            private readonly HashSet<ProjectNode> discovered;
            private readonly HashSet<ProjectNode> completed;
            private readonly HashSet<ProjectNode> processed;
            private readonly HashSet<ProjectNode> failed;
            
            public ProjectSchedule(IReadOnlyCollection<ProjectNode> before, IReadOnlyCollection<ProjectNode> after)
            {
                this.queue = new Queue<ProjectNode>(after);
                this.visited = new HashSet<ProjectNode>();
                this.discovered = new HashSet<ProjectNode>(after);
                this.completed = new HashSet<ProjectNode>();
                this.processed = new HashSet<ProjectNode>(before);
                this.failed = new HashSet<ProjectNode>();
            }
            
            public bool IsCompleted()
            {
                return this.discovered.Count == this.completed.Count + this.failed.Count;
            }
            
            public void Succeed(IProject project)
            {
                this.CloseSucceededProject(project);
                this.EnqueueAvailableProjects();
            }

            public void Fail(IProject project)
            {
                this.EnqueueAvailableProjects();
                this.CloseFailedProject(project);
            }


            private void CloseSucceededProject(IProject project)
            {            
                foreach (ProjectNode node in this.visited)
                {
                    if (node.Target == project)
                    {
                        this.completed.Add(node);
                        this.processed.Add(node);
                        this.failed.Remove(node);

                        break;
                    }
                }            
            }

            private void CloseFailedProject(IProject project)
            {            
                foreach (ProjectNode node in this.visited)
                {
                    if (node.Target == project)
                    {
                        this.completed.Remove(node);
                        this.failed.Add(node);

                        break;
                    }
                }            
            }

            private void EnqueueAvailableProjects()
            {
                foreach (ProjectNode node in this.completed.ToArray())
                {
                    foreach (ProjectNode after in node.GetAfter())
                    {
                        if (this.discovered.Contains(after) == false)
                        {
                            if (after.GetBefore().All(this.processed.Contains))
                            {
                                this.queue.Enqueue(after);
                                this.discovered.Add(after);
                            }
                        }
                    }
                }
            }
            
            public IReadOnlyCollection<IProject> Next()
            {
                ProjectNode node;
                List<IProject> result = new List<IProject>();

                while (this.queue.Count > 0)
                {
                    node = this.queue.Dequeue();

                    if (this.visited.Add(node) == true)
                    {
                        result.Add(node.Target);
                    }
                }

                return result;
            }
        }
    }
}