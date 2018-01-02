using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace brainwipe.hosts
{
    public static class HostsFile
    {
        public static HostMap[] Entries => GetMaps();

        private static HostMap[] GetMaps()
        {
            var maps = new List<HostMap>();
            foreach (var line in HostFileLines)
            {
                if (HostMap.IsMap(line))
                {
                    HostMap.TryParse(line, out var map);
                    maps.Add(map);
                }
            }
            return maps.ToArray();
        }

        private static string HostsPath => Path.Combine(
            Environment.GetEnvironmentVariable("SystemRoot"), 
            @"system32\drivers\etc\hosts");


        private static IEnumerable<string> HostFileLines => File.ReadAllLines(HostsPath);

    }
}
