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

        public async Task DownloadGithubRepo(string owner, string name, string path,string commitId)
        {

            var client = new GitHubClient(new ProductHeaderValue("my-cool-app"));
            var id = (await client.Repository.Get(owner, name)).Id;
            IReadOnlyList<RepositoryContent> repoContent = await client.Repository.Content.GetAllContentsByRef(id, path, commitId);
            
            Console.WriteLine("hello");
            Console.WriteLine(repoContent.Count);
        }
    }
}
