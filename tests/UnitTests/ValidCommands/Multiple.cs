﻿using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.ValidCommands
{
    class Multiple
    {
        [CommandLineEngine.Attributes.Command()]
        static void Command1() { }

        [CommandLineEngine.Attributes.Command()]
        static void Command2() { }
    }
}
