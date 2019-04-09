using System;
using System.Collections.Generic;
using System.Text;
using CommandLineEngine.Operation;
using CommandLineEngine.Operation.Types;
using CommandLineEngine.Parser;

namespace TestConsole
{
    public sealed class ParameterRuleTest : CommandLineEngine.Attributes.ParameterRuleAttribute
    {
        public override bool Evaluate(InputArguments inputArguments, OperationResult operationResult)
        {
            operationResult.Messages.Add(new Warning("FYI: a custom rule running..."));
            return true;
        }
    }
}
