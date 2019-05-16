using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.Parse.ValidCommands
{
    class AttributeParameter
    {
        [CommandLineEngine.Attributes.Command()]
        static void Command1(
            [CommandLineEngine.Attributes.Parameter(
                name: ParseTests.name,
                shortName: ParseTests.shortName,
                description: ParseTests.description)] int p1) { }
    }
}
