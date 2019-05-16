using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.Parse.InParse.ValidCommands
{
    class ParameterReservedKeyword
    {
        static void Command1([CommandLineEngine.Attributes.Parameter("--help")] int p) { }
    }
}
