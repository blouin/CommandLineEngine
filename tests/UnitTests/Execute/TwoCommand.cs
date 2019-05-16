using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.Execute
{
    class TwoCommand
    {
        [CommandLineEngine.Attributes.Command()]
        static int Command1()
        {
            return 1;
        }

        [CommandLineEngine.Attributes.Command()]
        static int Command2()
        {
            return 2;
        }
    }
}
