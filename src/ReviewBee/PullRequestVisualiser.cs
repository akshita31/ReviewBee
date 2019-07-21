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
}
