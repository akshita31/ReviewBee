using System;
using System.Threading.Tasks;

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
            var visualiser = new PullRequestVisualiser(githubClient, new RealConsole());
            Task.WaitAll(visualiser.Visualise(owner, name, prNumber));
        }
    }

    public class RealConsole : IConsole
    {
        public int Read()
        {
           return Console.Read();
        }

        public void Write(string text)
        {
            Console.Write(text);
        }
    }
}
