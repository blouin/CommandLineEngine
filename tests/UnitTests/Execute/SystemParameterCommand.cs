using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.Execute
{
    class SystemParameterCommand
    {
        [CommandLineEngine.Attributes.Command()]
        static int Command1(
            CommandLineEngine.Operation.OperationResult or,
            CommandLineEngine.Parser.InputArguments ia)
        {
            if (or == null) throw new ApplicationException();
            if (ia == null) throw new ApplicationException();
            return 1;
        }
    }
}
