using System.Collections.Generic;
using System.Threading.Tasks;

namespace GitInteractions
{
    public interface IGitClient
    {
        Task<IEnumerable<PullRequestFile>> GetPullRequestFiles(string owner, string name, int pullRequestNumber);
    }
}
