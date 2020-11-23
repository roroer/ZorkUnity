using System;

namespace Zork
{
    internal class ConsoleOutputService : IOutputService
    {
        public void Write(string value) {
            Console.Write(value);
        }

        public void Write(object value) {
            Console.Write(value);
        }

        public void WriteLine(string value) {
            Console.WriteLine(value);
        }

        public void WriteLine(object value) {
            Console.WriteLine(value.ToString());
        }
    }
}
