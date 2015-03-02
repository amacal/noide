using System;
using System.Collections.Generic;

namespace noide
{
	partial class SolutionFactory
	{
	    private class Solution : ISolution
	    {
	        private readonly SolutionMetadata metadata;
	        private ProjectHierarchy hierarchy;
	        private PackageRepository repository;
	        private String output;

	        public Solution(SolutionMetadata metadata)
	        {
	            this.metadata = metadata;
	            this.output = this.metadata.Path;

	            this.hierarchy = new ProjectHierarchy(metadata.Path);
	            this.repository = new PackageRepository(metadata.Path);
	        }

	        public SolutionMetadata Metadata
	        {
	            get { return this.metadata; }
	        }

	        public IPackageCollection Packages
	        {
	        	get { return null; }
	        }

	        public IReadOnlyCollection<IProject> Projects
	        {
	            get { return this.hierarchy.Projects; }
	        }

	        public void Update(ISolutionReader reader)
	        {
	            SolutionConfigurer configurer = new SolutionConfigurer(this.metadata);

	            reader.Update(configurer);

	            this.ApplyOutput(configurer);
	            this.ApplyProjects(configurer);
	            this.ApplyPackages(configurer);
	        }

	        private void ApplyOutput(SolutionConfigurer configurer)
	        {
	            this.output = configurer.OutputPath;
	        }

	        private void ApplyProjects(SolutionConfigurer configurer)
	        {
	            this.hierarchy = new ProjectHierarchy(configurer.ProjectsPath);
	            this.hierarchy.AddProjects(configurer.Projects);
	        }

	        private void ApplyPackages(SolutionConfigurer configurer)
	        {
	            this.repository = new PackageRepository(configurer.PackagesPath);
	            this.repository.AddPackages(configurer.Packages);
	        }

	        public void Compile(ISolutionCompiler compiler)
	        {
	            compiler.Compile(this.CreateConfiguration(null));
	        }

	        public void Compile(ISolutionCompiler compiler, IProject trigger)
	        {
	            compiler.Compile(this.CreateConfiguration(trigger));
	        }

	        private SolutionData CreateConfiguration(IProject project)
	        {
	            return new SolutionData(this.repository, this.hierarchy, this.output, project);
	        }
	    }
	}
}