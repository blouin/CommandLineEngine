using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.ValidCommands
{
    class Single
    {
        [CommandLineEngine.Attributes.Command()]
        static int Command1() { return 999; }
    }
}
