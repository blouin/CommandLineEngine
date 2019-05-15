using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.ValidCommands
{
    class Single
    {
        [CommandLineEngine.Attributes.Command()]
        static void Command1() { }
    }
}
