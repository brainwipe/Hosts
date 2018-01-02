using System;
using System.Diagnostics;

namespace brainwipe.hosts.cli
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            WriteLine("Hosts Example Commandline");
            WriteLine("Entries:");
            foreach (var entry in HostsFile.Entries)
            {
                WriteLine(entry.ToString());
            }
        }

        private static void WriteLine(string value)
        {
            Debug.WriteLine(value);
            Console.WriteLine(value);
        }
    }
}
