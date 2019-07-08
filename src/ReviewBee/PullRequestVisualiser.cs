namespace ReviewBee
{
    public class PullRequestVisualiser
    {
        private IGitClient _gitClient;

        public PullRequestVisualiser(IGitClient gitClient)
        {
            _gitClient = gitClient;
        }

        public void Visualise(string owner, string name, int prNumber)
        {
            var pullRequestFiles = _gitClient.GetPullRequestFiles(owner, name, prNumber);
            pullRequestFiles.ForEach(file =>{

            });
        }
    }
}
