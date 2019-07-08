using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReviewBee
{
    public class PullRequestVisualiser
    {
        private IGitClient _gitClient;
        private UserInteractor _userInteractor;

        public PullRequestVisualiser(IGitClient gitClient, IConsole console)
        {
            _gitClient = gitClient;
            _userInteractor = new UserInteractor(console);
        }

        public async Task<PullRequestGraph> Visualise(string owner, string name, int prNumber)
        {
            var pullRequestFiles = await _gitClient.GetPullRequestFiles(owner, name, prNumber);
            //once we get the list of all files ask the user to choose the head file
            var headFile = _userInteractor.InputHeadFileFromUser(pullRequestFiles);
            var graph = new PullRequestGraph(new PullRequestFileNode(headFile));
            return graph;
        }
    }

    public class PullRequestGraph
    {
        public PullRequestGraph(PullRequestFileNode rootNode)
        {
            Root = rootNode;
        }

        public PullRequestFileNode Root { get; }
    }

    public class PullRequestFileNode
    {
        public PullRequestFileNode(PullRequestFile file)
        {
            _file = file;
        }

        public string Name
        {
            get
            {
                return _file.Name;
            }
        }

        public string PatchContent
        {
            get
            {
                return _file.PatchContent;
            }
        }

        public List<PullRequestFileNode> Children { get; }
        private PullRequestFile _file;
    }
}
