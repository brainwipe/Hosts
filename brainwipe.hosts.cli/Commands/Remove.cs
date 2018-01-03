using System;
using System.Collections.Generic;
using System.Text;
using McMaster.Extensions.CommandLineUtils;

namespace brainwipe.hosts.cli.Commands
{
    public class Remove : CommandLineApplication
    {
        private readonly CommandOption ip;
        private readonly CommandOption host;
        private readonly CommandOption all;

        public Remove()
        {
            Name = "remove";
            Description = "Removes a map by either IP or hostname";
            HelpOption("-? | -h | --help");
            ip = Option("-i | --i", "The IP of the map to remove", CommandOptionType.SingleValue);
            host = Option("-n | --n", "The host of the map to remove", CommandOptionType.SingleValue);
            all = Option("-a | --a", "Remove all maps (keeps comments)", CommandOptionType.NoValue);
            OnExecute((Func<int>)RunCommand);
        }

        private int RunCommand()
        {
            if (all.HasValue())
            {
                HostsFile.RemoveAll();
                return Ok;
            }

            if (ip.HasValue() && host.HasValue())
            {
                Program.WriteLine("When removing a map, specify either ip or host name, not both");
                return 1;
            }
            if (!ip.HasValue() && !host.HasValue())
            {
                Program.WriteLine("When removing a map, specify at least ip or host name");
                return 1;
            }

            if (ip.HasValue())
            {
                HostsFile.RemoveByIp(ip.Value());
            }
            
            if (host.HasValue())
            {
                HostsFile.RemoveByHostName(host.Value());
            }

            return Ok;
        }
    }
}
