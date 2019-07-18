using System.IO;

namespace RoslynAPI
{
    public interface ISemanticGraphGetter
    {
        void GetSemanticGraph(DirectoryInfo path);
    }
}