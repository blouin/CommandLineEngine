using System;
using System.Collections.Generic;
using System.Text;
using CommandLineEngine.Operation;
using CommandLineEngine.Parser;

namespace UnitTests.Execute
{
    class ParameterRulesCommandValid
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
                operationResult.Messages.Add(new CustomMessage());
                return true;
            }
        }

        internal class CustomMessage : CommandLineEngine.Operation.Item
        {
            internal CustomMessage() : base("custom-message") { }
            public override ItemType Type => ItemType.Information;
        }
    }
}
