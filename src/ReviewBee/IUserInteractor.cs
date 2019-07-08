using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReviewBee
{
    public class UserInteractor
    {
        private IConsole _console;

        public UserInteractor(IConsole console)
        {
            _console = console;
        }

        public PullRequestFile InputHeadFileFromUser(IEnumerable<PullRequestFile> files)
        {
            var text = new StringBuilder();
            var counter = 1;
            foreach (var file in files)
            {
                text.Append($"{counter++}: {file.Name}\n");
            }

            _console.Write("Following are the choices for the head file.");
            _console.Write(text.ToString());
            _console.Write("Enter your choice");
            int choice=1;
            // do
            // {
            //     choice = _console.Read();
            // } while (!(choice > 0 && choice <= files.Count()));

            return files.ElementAt(choice-1);
        }
    }

    public interface IConsole
    {
        void Write(string text);
        int Read();
    }
}
