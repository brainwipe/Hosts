using System;
using System.Linq;
using System.Net;

namespace brainwipe.hosts
{
    // By Rob Lang, From https://github.com/brainwipe/Hosts
    public class HostMap
    {
        private readonly IPAddress address;
        private readonly string hostName;

        public HostMap(IPAddress address, string hostName)
        {
            this.address = address ?? throw new ArgumentNullException(nameof(address));
            this.hostName = hostName ?? throw new ArgumentNullException(nameof(hostName));
        }

        public HostMap(string address, string hostName) : this(IPAddress.Parse(address), hostName)
        {
        }

        public IPAddress Address => address;

        public string HostName => hostName;

        public override string ToString()
        {
            return $"{address}\t{hostName}";
        }

        public static bool IsMap(string map)
        {
            if (string.IsNullOrEmpty(map))
            {
                return false;
            }

            if (map.First() == '#')
            {
                return false;
            }

            return TryParse(map, out var hostMap);
        }

        public static bool TryParse(string map, out HostMap output)
        {
            output = null;
            var parts = map
                .Split(' ', '\t')
                .Where(x => !string.IsNullOrEmpty(x)).ToArray();

            if (parts.Length < 2)
            {
                return false;
            }

            if (string.IsNullOrEmpty(parts[1].Trim()))
            {
                return false;
            }

            if (IPAddress.TryParse(parts[0], out var parseIP))
            {
                output = new HostMap(parseIP, parts[1]);
                return true;
            }

            return false;
        }

        public override bool Equals(object obj)
        {
            var other = (HostMap)obj;
            return Equals(address, other.address) && string.Equals(hostName, other.hostName);
        }

        protected bool Equals(HostMap other)
        {
            return Equals(address, other.address) && string.Equals(hostName, other.hostName);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((address != null ? address.GetHashCode() : 0) * 397) ^ (hostName != null ? hostName.GetHashCode() : 0);
            }
        }
    }
}
