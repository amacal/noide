using System;
using System.Collections.Generic;

namespace noide
{
    partial class SolutionFactory
    {
        private class SolutionData : ISolutionData
        {
            private readonly PackageRepository repository;
            private readonly ProjectHierarchy hierarchy;
            private readonly String output;
            private readonly IProject project;

            public SolutionData(PackageRepository repository, ProjectHierarchy hierarchy, String output, IProject project)
            {
                this.repository = repository;
                this.hierarchy = hierarchy;
                this.output = output;
                this.project = project;
            }

            public String Output
            {
                get { return this.output; }
            }

            public ISolutionSchedule Order()
            {
                IProjectSchedule schedule;

                if (this.project != null)
                {
                    schedule = this.hierarchy.Order(this.project);
                }
                else
                {
                    schedule = this.hierarchy.Order();
                }

                return new SolutionSchedule(schedule, this.repository);
            }

            public IPackage GetPackage(String name, String version)
            {
                return this.repository.GetPackage(name, version);
            }

            public IReadOnlyCollection<String> GetReferences(String name, String version)
            {
                return this.repository.GetReferences(name, version);
            }
        }
    }
}