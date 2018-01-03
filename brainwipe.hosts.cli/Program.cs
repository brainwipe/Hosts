using System;
using System.Diagnostics;
using System.Linq;
using brainwipe.hosts.cli.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace brainwipe.hosts.cli
{
    class Program
    {
        private static void Main(string[] args)
        {
            var serviceProvider = BuildServiceProvider();
            var app = new CommandLineApplication(serviceProvider);
            app.Execute(args);
        }

        private static IServiceProvider BuildServiceProvider()
        {
            var serviceCollection = new ServiceCollection();
            var allCommandTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(x => typeof(ICommand).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract && x != typeof(CommandLineApplication));

            foreach (var commandType in allCommandTypes)
            {
                serviceCollection.AddSingleton(typeof(ICommand), commandType);
            }
            return serviceCollection.BuildServiceProvider();
        }

        public static void WriteLine(string value)
        {
            Debug.WriteLine(value);
            Console.WriteLine(value);
        }
    }
}
