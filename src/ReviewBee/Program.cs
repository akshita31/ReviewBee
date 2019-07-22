using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using GitInteractions;

namespace ReviewBee
{
    class Program
    {
        static void Main(string[] args)
        {
            var githubClient = new OctokitGitClient(Default.ReviewBeeDirectory);

            // Download a sample repo.
            Task.WaitAll(githubClient.DownloadGithubRepo(Default.RepoOwner, Default.RepoName, ".", "master"));

            var visualiser = new PullRequestVisualiser(githubClient, new RealConsole());
            Task.WaitAll(visualiser.Visualise(Default.RepoOwner, Default.RepoName, Default.PrNumber));
        }
    }

    public static class Default
    {
        public static string RepoName = "omnisharp-vscode";
        public static int PrNumber = 3089;
        public static string RepoOwner = "omnisharp";

        private static string UserProfile = Environment.GetEnvironmentVariable(
                RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                    ? "USERPROFILE"
                    : "HOME");
        public static DirectoryInfo ReviewBeeDirectory = new DirectoryInfo(Path.Combine(UserProfile, ".reviewBee"));
    }

    public class RealConsole : IConsole
    {
        public int Read()
        {
            return Convert.ToInt32(Console.ReadLine());
        }

        public void Write(string text)
        {
            Console.Write(text);
        }
    }
}
