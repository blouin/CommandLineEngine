using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.Parse.InParse.ValidCommands
{
    class CommandReservedKeyword
    {
        [CommandLineEngine.Attributes.Command("--help")]
        public void Command1() { }
    }
}
