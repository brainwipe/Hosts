using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;

namespace brainwipe.hosts
{
    // By Rob Lang, From https://github.com/brainwipe/Hosts
    public static class HostsFile
    {
        public static HostMap[] Entries => GetMaps();

        public static void Add(string mapLine)
        {
            HostMap.TryParse(mapLine, out var map);
            Add(map);
        }

        public static void Add(HostMap myMap)
        {
            foreach (var line in HostFileLines)
            {
                if (HostMap.TryParse(line, out var map))
                {
                    if (map.Equals(myMap))
                    {
                        return;
                    }
                }
            }
            var hostFileLines = HostFileLines.Append(myMap.ToString());
            SaveHostFile(hostFileLines);
        }

        public static void RemoveByIp(string ip)
        {
            if (IPAddress.TryParse(ip, out var ipAddress))
            {
                RemoveByIp(ipAddress);
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(ip), "IP not removed, format was invalid");
            }
        }

        public static void RemoveByIp(IPAddress ip)
        {
            var hostFileLines = HostFileLines.ToList();

            foreach (var line in hostFileLines)
            {
                if (HostMap.TryParse(line, out var map))
                {
                    if (map.Address.Equals(ip))
                    {
                        hostFileLines.Remove(line);
                        break;
                    }
                }
            }
            SaveHostFile(hostFileLines);
        }

        public static void RemoveAll()
        {
            List<string> hostOutput = new List<string>();
            foreach (var line in HostFileLines)
            {
                if (!HostMap.IsMap(line))
                {
                    hostOutput.Add(line);
                }
            }
            SaveHostFile(hostOutput);
        }

        public static void RemoveByHostName(string hostname)
        {
            var hostFileLines = HostFileLines.ToList();

            foreach (var line in hostFileLines)
            {
                if (HostMap.TryParse(line, out var map))
                {
                    if (map.HostName == hostname)
                    {
                        hostFileLines.Remove(line);
                        break;
                    }
                }
            }
            SaveHostFile(hostFileLines);
        }

        private static HostMap[] GetMaps()
        {
            var maps = new List<HostMap>();
            foreach (var line in HostFileLines)
            {
                if (HostMap.TryParse(line, out var map))
                {
                    maps.Add(map);
                }
            }
            return maps.ToArray();
        }

        private static string HostsPath => Path.Combine(
            Environment.GetEnvironmentVariable("SystemRoot"), 
            @"system32\drivers\etc\hosts");


        private static IEnumerable<string> HostFileLines => File.ReadAllLines(HostsPath);

        private static void SaveHostFile(IEnumerable<string> lines) => File.WriteAllText(HostsPath, string.Join(Environment.NewLine, lines));
    }
}
