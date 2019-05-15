using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTestsData
{
    public class Multiple
    {
        [CommandLineEngine.Attributes.Command()]
        static void Command1() { }

        [CommandLineEngine.Attributes.Command()]
        static void Command2() { }
    }
}
