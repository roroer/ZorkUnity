using System.IO;
using Newtonsoft.Json;

namespace Zork
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string defaultGameFilename = "Zork.json";
            string gameFilename = (args.Length > 0 ? args[(int)CommandLineArguments.GameFilename] : defaultGameFilename);

            ConsoleOutputService output = new ConsoleOutputService();
            ConsoleInputService input = new ConsoleInputService();

            Game game = JsonConvert.DeserializeObject<Game>(File.ReadAllText(gameFilename));
            game.Initialize(input, output);

            while (game.IsRunning)
            {
                output.Write("\n>");
                input.ProcessInput();
            }

            game.Shutdown();
        }

        private enum CommandLineArguments
        {
            GameFilename = 0
        }
    }
}