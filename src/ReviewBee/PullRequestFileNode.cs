using System.Collections.Generic;
using GitInteractions;

namespace ReviewBee
{
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
