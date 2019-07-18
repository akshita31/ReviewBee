using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Octokit;

namespace ReviewBee
{
    public class OctokitGitClient : IGitClient
    {
        private GitHubClient _client;

        public OctokitGitClient()
        {
            _client = new GitHubClient(new ProductHeaderValue("my-cool-app"));
        }

        public async Task<IEnumerable<PullRequestFile>> GetPullRequestFiles(string owner, string name, int pullRequestNumber)
        {
           
            var prFiles = await _client.PullRequest.Files(owner, name,pullRequestNumber);
           return prFiles.Select(file => new PullRequestFile(file.FileName, file.Patch));
        }

        public async Task DownloadGithubRepo(string owner, string name, string path,string commitId)
        {
            var id = (await _client.Repository.Get(owner, name)).Id;
            IReadOnlyList<RepositoryContent> repoContent = await _client.Repository.Content.GetAllContentsByRef(id, path, commitId);
            
            Console.WriteLine("hello");
            Console.WriteLine(repoContent.Count);
        }
    }
}
