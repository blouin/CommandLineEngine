using System;
using System.Collections.Generic;
using System.Text;
using CommandLineEngine.Operation;
using CommandLineEngine.Parser;

namespace UnitTests.Execute
{
    class FormatterTest : CommandLineEngine.IHelpFormatter
    {
        public void PrintHelp(Configuration configuration, Command command, Items operationMessages)
        {
            if (command != null) throw new CommandException();
            else throw new ConfigurationException();
        }

        internal class ConfigurationException : ApplicationException { }
        internal class CommandException : ApplicationException { }
    }
}
