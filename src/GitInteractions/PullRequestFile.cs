namespace GitInteractions
{
    public class PullRequestFile
    {
        public PullRequestFile(string name, string patchContent)
        {
            Name = name;
            PatchContent = patchContent;
        }

        public string Name { get; }
        public string PatchContent { get; }
    }
}
