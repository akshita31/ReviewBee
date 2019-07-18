using System.IO;
using System.Linq;
using Buildalyzer;
using Buildalyzer.Workspaces;

namespace RoslynAPI
{
    public class RoslynSemanticGraphGetter : ISemanticGraphGetter
    {
        public void GetSemanticGraph(DirectoryInfo directory)
        {
            //directory where solution
            if (!directory.Exists)
            {
                throw new DirectoryNotFoundException($"Could not find directory {directory.FullName}");
            }

            var solutionFile = directory.EnumerateFiles("*.sln", SearchOption.AllDirectories).FirstOrDefault();
            if (solutionFile != null)
            {
                var manager = new AnalyzerManager(solutionFile.FullName);
                foreach (var prj in manager.Projects.Values)
                {
                    // GetWorkspace returns Microsoft.CodeAnalysis.AdhocWorkspace which can be used with Roslyn
                    var workspace = prj.GetWorkspace();
                    workspace.CurrentSolution.GetProjectDependencyGraph();
                    var sln = workspace.CurrentSolution;

                    //await AnalyzeProject(sln.Projects.First());
                }
            }
        }
    }
}
