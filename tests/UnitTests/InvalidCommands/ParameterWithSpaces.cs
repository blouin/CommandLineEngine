﻿using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.InvalidCommands
{
    class ParameterWithSpaces
    {
        static void Command1([CommandLineEngine.Attributes.Parameter("p 1")] int p) { }
    }
}
