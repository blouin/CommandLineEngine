using System;
using System.Text;
using CommandLineEngine.Operation.Types;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            CommandLineEngine.CommandExecutor.Execute(args, configurationBuilder: c =>
            {
                // Program information
                c.Program.Name = "Sample application";
                c.Program.HelpUrl = "https://github.com/blouin/CommandLineEngine";

                // The defaults
                //c.LongParameterPrefix = "--";
                //c.ShortParameterPrefix = "-";
                //c.HelpCommandNames = new[] { "--help", "-h", "?" };

                // Custom validation
                c.ConfigurationValidationAction = (o) => 
                    {
                        o.Messages.Add(new Information("FYI: custom validation of Configuration..."));
                        return true;
                    };
            });

        }

        [CommandLineEngine.Attributes.Command()]
        static int SingleCommand(
            [CommandLineEngine.Attributes.Parameter("arg1", "a1", "Help for argument 1")]
            string arg1 = "DefaultArg1Value",
            [CommandLineEngine.Attributes.Parameter("arg2", "a2", "Help for argument 2")]
            string arg2 = "DefaultArg2Value"
        )
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Command ran successfully.");
            sb.AppendLine($" Arg1: {arg1}");
            sb.AppendLine($" Arg2: {arg2}");
            Console.WriteLine(sb.ToString());

            return 1;
        }
    }
}
