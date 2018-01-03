using System;
using System.Collections.Generic;
using System.Text;
using McMaster.Extensions.CommandLineUtils;

namespace brainwipe.hosts.cli.Commands
{
    public class Add : CommandLineApplication
    {
        private readonly CommandOption map;


        public Add()
        {
            Name = "add";
            Description = "Adds a new entry to the Hosts file";
            HelpOption("-? | -h | --help");
            map = Option("-m | --m", "The new map as it would appear in the file: 127.0.0.1 localhost", CommandOptionType.SingleValue);
            OnExecute((Func<int>)RunCommand);
        }

        private int RunCommand()
        {
            if (HostMap.IsMap(map.Value()))
            {
                HostsFile.Add(map.Value());
            }
            else
            {
                Program.WriteLine($"Error: The input '{map.Value()}' was not a valid map.");
                return 1;
            }
            return Ok;
        }
    }
}
