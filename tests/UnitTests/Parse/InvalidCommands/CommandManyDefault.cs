using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.Parse.InParse.ValidCommands
{
    class CommandManyDefault
    {
        [CommandLineEngine.Attributes.Command()]
        [CommandLineEngine.Attributes.CommandDefault()]
        public void Command1() { }

        [CommandLineEngine.Attributes.Command()]
        [CommandLineEngine.Attributes.CommandDefault()]
        public void Command2() { }
    }
}
