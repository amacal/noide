using System;
using System.Collections.Generic;
using System.IO;

namespace noide
{
	public class ProjectTester : IProjectTester
	{
		private readonly IPackageEnumerator packageEnumerator;
		private readonly IRunnerEnumerator runnerEnumerator;
		private readonly String output;

		public ProjectTester(IPackageEnumerator packageEnumerator, IRunnerEnumerator runnerEnumerator, String output)
		{
			this.packageEnumerator = packageEnumerator;
			this.runnerEnumerator = runnerEnumerator;
			this.output = output;
		}

		public bool IsTestable(IProject project)
		{
			return this.runnerEnumerator.FindRunner(this.packageEnumerator, project) != null;
		}

		public ITester GetRunner(IProject project)
		{
			return this.runnerEnumerator.FindRunner(this.packageEnumerator, project);
		}

		public IResource<ITestingResult> Test(ITester runner, IProject project)
		{
			Target target = new Target(this.output, project, this.packageEnumerator);
			IResource<ITestingResult> resource = runner.Test(target);

			return resource;
		}

		private class Target : ITestable
		{
			private readonly String output;
			private readonly IProject project;
			private readonly IReferenceEnumerator referenceEnumerator;

			public Target(String output, IProject project, IReferenceEnumerator referenceEnumerator)
			{
				this.output = output;
				this.project = project;
				this.referenceEnumerator = referenceEnumerator;
			}

			public String Name
			{
				get { return this.project.Metadata.Name; }
			}

			public String Path
			{
				get { return System.IO.Path.Combine(this.output, this.project.Metadata.Name + ".dll"); }
			}

			public IReadOnlyCollection<String> GetReferences()
			{
				List<String> references = new List<String>();

				foreach (ProjectReference reference in this.project.ProjectReferences.AsEnumerable())
				{
					references.Add(System.IO.Path.Combine(this.output, reference.Name + ".dll"));
				}

				foreach (PackageReference reference in this.project.PackageReferences.AsEnumerable())
				{
					references.AddRange(this.referenceEnumerator.FindReferences(reference.Name, reference.Version));
				}

				return references.AsReadOnly();
			}
		}
	}
}