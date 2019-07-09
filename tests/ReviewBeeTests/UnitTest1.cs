using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using ReviewBee;
using Xunit;

namespace ReviewBeeTests
{
    public class PullRequestVisualiserTests
    {
        [Fact]
        public async void Returns_the_graph_with_the_files()
        {
            var owner = "owner";
            var repoName = "repoName";
            var prNumber = 1;
            var gitClient = new FakeGitClient(){
                ("rootFile","someContent")
            };
            var console = new FakeConsole(1);

            var visualiser = new PullRequestVisualiser(gitClient, console);
            var obtainedGraph = await visualiser.Visualise(owner, repoName, prNumber);
            obtainedGraph.Root.Name.Should().Be("rootFile");
        }
    }

    public class FakeConsole : IConsole
    {
        private int _readOutput;

        public FakeConsole(int readOutput)
        {
            _readOutput =readOutput;
        }

        public int Read()
        {
            return _readOutput;
        }

        public void Write(string text)
        {
        }
    }

    public class FakeGitClient : IGitClient, IEnumerable
    {
        private List<PullRequestFile> _prFiles;

        public FakeGitClient()
        {
            _prFiles = new List<PullRequestFile>();
        }

        public void Add((string fileName, string patchContent) file)
        {
            var prFile = new PullRequestFile(file.fileName, file.patchContent);
            _prFiles.Add(prFile);
        }

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<PullRequestFile>> GetPullRequestFiles(string owner, string name, int pullRequestNumber)
        {
            return _prFiles;
        }
    }
}
