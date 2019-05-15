using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.InvalidCommands
{
    class ParameterSameName
    {
        static void Command1(
            [CommandLineEngine.Attributes.Parameter("--p1")] int p1,
            [CommandLineEngine.Attributes.Parameter("--p1")] int p2) { }
    }
}
