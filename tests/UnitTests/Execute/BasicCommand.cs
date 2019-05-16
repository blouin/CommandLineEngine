using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.Execute
{
    class BasicCommand
    {
        [CommandLineEngine.Attributes.Command()]
        static int Command1(
            [CommandLineEngine.Attributes.Parameter("ln", "sn")]
            string p)
        {
            return 1;
        }
    }
}
