using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.InvalidCommands
{
    public class CommandWithSpaces
    {
        [CommandLineEngine.Attributes.Command("c 1")]
        static void Command1() { }

    }
}
