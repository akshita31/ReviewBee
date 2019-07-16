using System;
using System.Threading.Tasks;
using Octokit;

namespace ReviewBee
{
    class Program
    {
        static void Main(string[] args)
        {
            string owner = "omnisharp";
            string name = "omnisharp-vscode";
            int prNumber = 3089;
            var githubClient = new GithubClient();

            // Download a sample repo.
            Task.WaitAll(githubClient.DownloadGithubRepo(owner, name, ".", "master"));

            var visualiser = new PullRequestVisualiser(githubClient, new RealConsole());
            Task.WaitAll(visualiser.Visualise(owner, name, prNumber));
        }
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
