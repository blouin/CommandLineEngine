using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.InvalidCommands
{
    class CommandReservedKeyword
    {
        [CommandLineEngine.Attributes.Command("--help")]
        public void Command1() { }
    }
}
