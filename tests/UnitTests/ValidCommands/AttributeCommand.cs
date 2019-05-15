using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.ValidCommands
{
    class AttributeCommand
    {
        [CommandLineEngine.Attributes.Command(
            name: ParseTests.name,
            description: ParseTests.description,
            helpUrl: ParseTests.helpUrl)]
        static void Command1() { }
    }
}
