using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Octokit;

namespace ReviewBee
{
    public class GithubClient : IGitClient
    {
        public async Task<IEnumerable<PullRequestFile>> GetPullRequestFiles(string owner, string name, int pullRequestNumber)
        {
            var client = new GitHubClient(new ProductHeaderValue("my-cool-app"));
            var prFiles = await client.PullRequest.Files(owner, name,pullRequestNumber);
           return prFiles.Select(file => new PullRequestFile(file.FileName, file.Patch));
        }
    }
}
