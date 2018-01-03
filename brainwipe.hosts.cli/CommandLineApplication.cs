using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace brainwipe.hosts.cli
{
    internal interface ICommand {}

    public class CommandLineApplication : McMaster.Extensions.CommandLineUtils.CommandLineApplication, ICommand
    {
        protected readonly IServiceProvider serviceProvider;
        protected const int Ok = 0; 

        public CommandLineApplication()
        {}

        public CommandLineApplication(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            RegisterCommands();
        }

        private void RegisterCommands()
        {
            foreach (var command in serviceProvider.GetServices<ICommand>())
            {
                var commandLineApp = command as CommandLineApplication;

                if (commandLineApp == null)
                {
                    throw new InvalidCastException("Commands must inherit from ICommand and CommandLineApplication");
                }

                Commands.Add(commandLineApp);
            }
        }

    }
}
