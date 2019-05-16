using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.Execute
{
    class TwoCommandWithDefault
    {
        [CommandLineEngine.Attributes.Command()]
        static int Command1()
        {
            return 1;
        }

        [CommandLineEngine.Attributes.Command()]
        [CommandLineEngine.Attributes.CommandDefault()]
        static int Command2()
        {
            return 2;
        }
    }
}
