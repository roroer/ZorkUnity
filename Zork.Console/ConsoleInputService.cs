using System;

namespace Zork
{
    internal class ConsoleInputService : IInputService
    {
        public event EventHandler<string> InputReceived;

        public void ProcessInput() 
        {
            string inputString = Console.ReadLine();
            InputReceived?.Invoke(this, inputString);
        }
    }
}
