namespace ReviewBee
{
    public class PullRequestFile
    {
        public PullRequestFile(string fileName, string patchContent)
        {
            FileName = fileName;
            PatchContent = patchContent;
        }

        public string FileName { get; }
        public string PatchContent { get; }
    }
}
