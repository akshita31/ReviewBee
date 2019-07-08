using System;
using System.Collections.Generic;
using ReviewBee;
using Xunit;

namespace ReviewBeeTests
{
    public class PullRequestVisualiserTests
    {
        [Fact]
        public void Returns_the_graph_with_the_files()
        {
            var owner = "owner";
            var repoName = "repoName";
            var prNumber = 1;
            var gitClient = new FakeGitClient();

            var visualiser = new PullRequestVisualiser(gitClient);
            var obtainedGraph = visualiser.Visualise(owner, repoName, prNumber);
            obtainedGraph.
        }
    }

    public class FakeGitClient : IGitClient
    {
        public IEnumerable<PullRequestFile> GetPullRequestFiles(string owner, string name, int pullRequestNumber)
        {
            throw new NotImplementedException();
        }
    }
}
