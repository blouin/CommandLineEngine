using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.Parse.InParse.ValidCommands
{
    class ParameterWithSpaces
    {
        static void Command1([CommandLineEngine.Attributes.Parameter("p 1")] int p) { }
    }
}
