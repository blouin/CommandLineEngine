using System;
using System.Collections.Generic;
using System.Text;
using CommandLineEngine.Operation;
using CommandLineEngine.Parser;

namespace UnitTests.Execute
{
    class ParameterRulesCommandInvalid
    {
        [CommandLineEngine.Attributes.Command()]
        static int Command1(
            [Rule]
            string p = "default-value")
        {
            return 1;
        }

        class Rule : CommandLineEngine.Attributes.ParameterRuleAttribute
        {
            public override bool Evaluate(InputArguments inputArguments, OperationResult operationResult)
            {
                return false;
            }
        }
    }
}
