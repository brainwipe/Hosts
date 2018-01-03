using System;
using System.Collections.Generic;
using System.Text;

namespace brainwipe.hosts.cli.Commands
{
    public class List : CommandLineApplication
    {
        public List()
        {
            Name = "list";
            Description = "Lists all the maps in the Hosts file";
            HelpOption("-? | -h | --help");
            OnExecute((Func<int>)RunCommand);
        }

        private int RunCommand()
        {
            foreach (var entry in HostsFile.Entries)
            {
                Program.WriteLine(entry.ToString());
            }
            return Ok;
        }

    }
}
