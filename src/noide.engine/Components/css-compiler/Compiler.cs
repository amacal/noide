using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace noide
{
    public partial class Compiler : ICompiler
    {
        private readonly IProcessFactory processFactory;

        public Compiler(IProcessFactory processFactory)
        {
            this.processFactory = processFactory;
        }
        
        public IResource Compile(ICompilable target)
        {
            String workingDirectory = Path.GetDirectoryName(target.Output);
            StringBuilder commandLine = new StringBuilder();

            this.AppendCommand(commandLine);
            this.AppendOptions(commandLine);
            this.AppendType(commandLine, target);
            this.AppendOutput(commandLine, target);
            this.AppendReferences(commandLine, target);
            this.AppendSources(commandLine, target);

            return new Resource(this.processFactory.Execute(workingDirectory, commandLine.ToString()));
        }

        private void AppendCommand(StringBuilder builder)
        {
            builder.Append("\"C:\\Windows\\Microsoft.NET\\Framework\\v4.0.30319\\csc.exe\" ") ;
        }

        private void AppendOptions(StringBuilder builder)
        {
            builder.Append("/nologo /unsafe /noconfig ");
        }

        private void AppendType(StringBuilder builder, ICompilable target)
        {
            builder.Append("/target:");
            builder.Append(target.Type);
            builder.Append(" ");
        }

        private void AppendOutput(StringBuilder builder, ICompilable target)
        {
            builder.Append("/out:");
            builder.Append(target.Name);
            
            if (target.Type == "exe")
            {
            	builder.Append(".exe ");
            }
            else
            {
            	builder.Append(".dll ");
            }
        }

        private void AppendReferences(StringBuilder builder, ICompilable target)
        {
            foreach (String reference in target.GetReferences())
            {
                builder.Append("\"/reference:");
                builder.Append(reference);
                builder.Append("\" ");
            }            
        }

        private void AppendSources(StringBuilder builder, ICompilable target)
        {
            foreach (String source in target.GetSources())
            {
                builder.Append("\"");
                builder.Append(source);
                builder.Append("\" ");
            }            
        }
    }
}
