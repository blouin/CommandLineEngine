using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.Execute
{
    class OneCommand
    {
        [CommandLineEngine.Attributes.Command()]
        static int Command1()
        {
            return 1;
        }
    }
}
