using System;
using System.Collections.Generic;
using System.IO;

namespace noide
{
	public class ProjectCompiler : IProjectCompiler
	{
		private readonly IReferenceEnumerator referenceEnumerator;
		private readonly ISourceEnumerator sourceEnumerator;
		private readonly ICompiler compiler;
		private readonly String output;

		public ProjectCompiler(ISourceEnumerator sourceEnumerator, IReferenceEnumerator referenceEnumerator, ICompiler compiler, String output)
		{
			this.referenceEnumerator = referenceEnumerator;
			this.sourceEnumerator = sourceEnumerator;
			this.compiler = compiler;
			this.output = output;
		}

		public IResource Compile(IProject project)
		{
			Target target = new Target(this.output, project, this.referenceEnumerator, this.sourceEnumerator);
			IResource resource = this.compiler.Compile(target);

			return resource;
		}

		private class Target : ICompilable
		{
			private readonly String output;
			private readonly IProject project;
			private readonly IReferenceEnumerator referenceEnumerator;
			private readonly ISourceEnumerator sourceEnumerator;

			public Target(String output, IProject project, IReferenceEnumerator referenceEnumerator, ISourceEnumerator sourceEnumerator)
			{
				this.output = output;
				this.project = project;
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
				return this.sourceEnumerator.FindSources(this.project.Metadata.Path);
			}

			public IReadOnlyCollection<String> GetReferences()
			{
				List<String> references = new List<String>();

				foreach (Reference reference in this.project.References.AsEnumerable())
				{
					references.Add(reference.Name + ".dll");
				}

				foreach (ProjectReference reference in this.project.ProjectReferences.AsEnumerable())
				{
					references.Add(Path.Combine(this.output, reference.Name + ".dll"));
				}

				foreach (PackageReference reference in this.project.PackageReferences.AsEnumerable())
				{
					references.AddRange(this.referenceEnumerator.FindReferences(reference.Name, reference.Version));
				}

				return references.AsReadOnly();
			}

			public IReadOnlyCollection<String> GetResources()
			{
				return new String[0];
			}
		}
	}
}