using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.InvalidCommands
{
    class CommandSameName
    {
        [CommandLineEngine.Attributes.Command("c1")]
        static void Command1() { }

        [CommandLineEngine.Attributes.Command("c1")]
        static void Command2() { }
    }
}
