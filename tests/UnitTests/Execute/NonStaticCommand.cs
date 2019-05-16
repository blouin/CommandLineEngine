using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.Execute
{
    class NonStaticCommand
    {
        [CommandLineEngine.Attributes.Command()]
        int Command1()
        {
            return 1;
        }
    }
}
