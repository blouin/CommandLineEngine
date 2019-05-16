using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.Parse.ValidCommands
{
    class AttributeParameterHidden
    {
        [CommandLineEngine.Attributes.Command()]
        static void Command1(
            [CommandLineEngine.Attributes.Parameter()]
        int p1,
            [CommandLineEngine.Attributes.Parameter()]
        int p2 = 0,
            [CommandLineEngine.Attributes.Parameter()]
            [CommandLineEngine.Attributes.ParameterHidden()]
        int p3 = 0
            )
        { }
    }
}
