namespace ReviewBee
{
    public class PullRequestGraph
    {
        public PullRequestGraph(PullRequestFileNode rootNode)
        {
            Root = rootNode;
        }

        public PullRequestFileNode Root { get; }
    }
}
