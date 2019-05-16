using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.Parse.InParse.ValidCommands
{
    class ParameterNoDefaultHidden
    {
        static void Command1(
            [CommandLineEngine.Attributes.Parameter()]
            [CommandLineEngine.Attributes.ParameterHidden()]
            int p) { }
    }
}
