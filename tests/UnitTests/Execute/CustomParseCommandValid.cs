using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.Execute
{
    class CustomParseCommand
    {
        [CommandLineEngine.Attributes.Command()]
        static string Command1(
            string p = "default-value")
        {
            return p;
        }
    }
}
