using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

namespace noide
{
    public class Runner : ITester
    {
        private readonly IProcessFactory processFactory;
        private readonly IFileService fileService;
        private readonly IPackage package;

        public Runner(IProcessFactory processFactory, IFileService fileService, IPackage package)
        {
            this.processFactory = processFactory;
            this.fileService = fileService;
            this.package = package;
        }

        public IPackageReferenceCollection PackageReferences
        {
            get { return new References(this.package); }
        }

        public IResource<ITestingResult> Test(ITestable target)
        {
            String executable = this.package.FindFile(this.fileService, "nunit-console.exe");
            String workingDirectory = Path.GetDirectoryName(target.Path);
            StringBuilder commandLine = new StringBuilder();

            this.AppendCommand(commandLine, executable);
            this.AppendTarget(commandLine, target);
            this.AppendOptions(commandLine);
            this.AppendPrivateBinPath(commandLine, executable);

            return new Resource(this.processFactory.Execute(workingDirectory, commandLine.ToString()));
        }

        private void AppendCommand(StringBuilder builder, String executable)
        {
            builder.Append("\"");
            builder.Append(executable);
            builder.Append("\" ");
        }

        private void AppendTarget(StringBuilder builder, ITestable target)
        {
            builder.Append("\"");
            builder.Append(target.Path);
            builder.Append("\" ");
        }

        private void AppendOptions(StringBuilder builder)
        {
            builder.Append("/nologo /process:Separate /domain:None /nothread /noresult /noshadow /nodots ");
        }

        private void AppendPrivateBinPath(StringBuilder builder, String executable)
        {
            builder.Append("\"/privatebinpath:");
            builder.Append(Path.GetDirectoryName(executable));
            builder.Append("\" ");
        }

        private class References : IPackageReferenceCollection
        {
            private readonly IPackage package;

            public References(IPackage package)
            {
                this.package = package;
            }

            public int Count
            {
                get { return 1; }
            }

            public bool Contains(PackageReference reference)
            {
                return this.package.Metadata.Name == reference.Name
                    && this.package.Metadata.Version == reference.Version;
            }

            public void Clear()
            {
            }

            public void Add(PackageReference reference)
            {
            }

            public IEnumerable<PackageReference> AsEnumerable()
            {
                yield return new PackageReference(this.package.Metadata.Name, this.package.Metadata.Version);
            }
        }

        private class Resource : IResource<ITestingResult>
        {
            private readonly IProcess process;

            public Resource(IProcess process)
            {
                this.process = process;
            }

            public IntPtr[] Handles
            {
                get { return new[] { this.process.Handle, this.process.Output }; }
            }

            public bool Complete(IntPtr handle)
            {
                if (handle == this.process.Output)
                {
                    this.process.ReadOutput();
                }

                return handle == this.process.Handle;
            }

            public bool IsSuccessful()
            {
                return this.process.GetExitCode() == 0;
            }

            public void Release()
            {
                this.process.Release();
            }

            public ITestingResult Payload
            {
                get { return new Result(this.process.GetOutput(), this.IsSuccessful()); }
            }
        }

        private class Result : ITestingResult
        {
            private static Regex regex;

            static Result()
            {
                regex = new Regex(@"^[0-9]+\) (Test Error|Test Failure) : (?<name>[^:]+)");
            }
            
            private readonly String[] lines;
            private readonly bool isSuccessful;

            public Result(String output, bool isSuccessful)
            {
                this.isSuccessful = isSuccessful;
                this.lines = output.Replace("\r\n", "\n").Split('\n');
            }

            public bool IsSuccessful()
            {
                return this.isSuccessful;
            }

            public ITestingCase[] GetCases()
            {
                List<ITestingCase> cases = new List<ITestingCase>();

                for (int i = 0; i < this.lines.Length; i++)                
                {
                    Match match = regex.Match(this.lines[i]);

                    if (match.Success == true)
                    {
                        cases.Add(this.CreateCase(match.Groups["name"].Value, ref i));
                    }
                }

                return cases.ToArray();
            }

            private ITestingCase CreateCase(string name, ref int index)
            {
                List<String> remarks = new List<String>();

                for (int i = index + 1; i < this.lines.Length; i++, index++)
                {
                    if (regex.IsMatch(this.lines[i]) == true)
                    {
                        break;
                    }

                    if (String.IsNullOrEmpty(this.lines[i]) == false)
                    {
                        remarks.Add(this.lines[i].Trim());
                    }
                }

                return new Case(name, remarks.ToArray());
            }
        }

        private class Case : ITestingCase
        {
            private readonly String name;
            private readonly String[] remarks;

            public Case(String name, String[] remarks)
            {
                this.name = name;
                this.remarks = remarks;
            }

            public String Name
            {
                get { return this.name; }
            }

            public String[] GetRemarks()
            {
                return this.remarks;
            }
        }
    }
}
