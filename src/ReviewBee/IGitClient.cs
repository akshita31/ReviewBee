using System.Collections.Generic;

namespace ReviewBee
{
    public interface IGitClient
    {
        IEnumerable<PullRequestFile> GetPullRequestFiles(string owner, string name, int pullRequestNumber);
    }
}
