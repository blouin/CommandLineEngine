using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.InvalidCommands
{
    class ParameterNoDefaultHidden
    {
        static void Command1(
            [CommandLineEngine.Attributes.Parameter()]
            [CommandLineEngine.Attributes.ParameterHidden()]
            int p) { }
    }
}
