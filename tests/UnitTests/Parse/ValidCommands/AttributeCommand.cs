using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.Parse.ValidCommands
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
