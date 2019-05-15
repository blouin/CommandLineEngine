using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.InvalidCommands
{
    class ParameterReservedKeyword
    {
        static void Command1([CommandLineEngine.Attributes.Parameter("--help")] int p) { }
    }
}
