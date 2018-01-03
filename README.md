# Hosts
C# .NET Core automation for managing the Windows 7,8,10 Hosts file. Add/remove and list host maps.

There is no nuget package for this code, it's intended to be used via the command line or by copying the code from the brainwipe.hosts library into your own cli tooling.

## Command Line Options
To run, you need to use dotnet core. Build the solution and navigate to `/bin/debug/netcoreapp2.0`.

### List

  dotnet hosts.dll list

Gives (comments are ignored):

  127.0.0.1   localhost.com
  127.0.0.1   myotherdomain.com

### Add Map
   
  dotnet hosts.dll add "127.1.1.1 newdomain.com"

### Remove Map
You have two options, by IP or by domain. If you do by IP then all the maps with the IP are removed.
	
  dotnet hosts.dll remove -i "127.1.1.1"

  dotnet hosts.dll remove -n "newdomain.com"

You can also remove all hosts. This is useful if you prefer tear-down and build-up processes.

  dotnet hosts.dll remove -a

## Using the Code
Copy `/brainwipe.hosts/HostMap.cs` and `/brainwipe.hosts/HostsFile.cs` into your application. You can then use the `HostsFile` class directly. 

If you need inspiration then check out how it's used in `brainwipe.hosts.cli/Commands/`.

### Examples

  HostsFile.Add("127.0.0.1 mynewdomain.com");
  HostsFile.RemoveByIp("127.0.0.1");
  HostsFile.RemoveByHostName("mynewdomain.com");
  HostsFile.RemoveAll();

  foreach (var entry in HostsFile.Entries)
  {
     Debug.WriteLine(entry.ToString());
  }
  
