using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace noide
{
	partial class SolutionMerger
	{
		private class Target : ICompilable
		{
			private readonly String output;
			private readonly IProject project;
			private readonly ISolution solution;
			private readonly IReferenceEnumerator referenceEnumerator;
			private readonly ISourceEnumerator sourceEnumerator;

			public Target(ISolution solution, ISourceEnumerator sourceEnumerator)
			{
				this.output = output;
				this.project = solution.Projects.First();
				this.solution = solution;
				this.referenceEnumerator = referenceEnumerator;
				this.sourceEnumerator = sourceEnumerator;
			}

			public String Name
			{
				get { return this.project.Metadata.Name; }
			}

			public String Output
			{
				get { return Path.Combine(this.output, this.project.Metadata.Name + ".dll"); }
			}

			public String Type
			{
				get { return this.project.Type; }
			}

			public IReadOnlyCollection<String> GetSources()
			{
				List<String> allSources = new List<String>();

				foreach (IProject project in this.solution.Projects)
				{
					String path = project.Metadata.Path;
					String[] sources = this.sourceEnumerator.FindSources(path);

					allSources.AddRange(sources);
				}

				return allSources.AsReadOnly();
			}

			public IReadOnlyCollection<String> GetReferences()
			{
				HashSet<String> references = new HashSet<String>();

				foreach (IProject project in this.solution.Projects)
				{
					foreach (Reference reference in project.References.AsEnumerable())
					{
						references.Add(reference.Name + ".dll");
					}

					foreach (PackageReference reference in project.PackageReferences.AsEnumerable())
					{
						foreach (String path in this.referenceEnumerator.FindReferences(reference.Name, reference.Version))
						{
							references.Add(path);
						}
					}
				}

				return references;
			}

			public IReadOnlyCollection<String> GetResources()
			{
				return new String[0];
			}
		}
	}
}