using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.ValidCommands
{
    public class Multiple
    {
        [CommandLineEngine.Attributes.Command("command1", "Description for command1")]
        static int Command1(
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

        [CommandLineEngine.Attributes.Command("command2", "Description for command2")]
        static int Command2(
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
