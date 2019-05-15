using System;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace UnitTests
{
    public class ExecutionTests
    {
        [Fact]
        public void ExecuteSimple()
        {
            var o = CommandLineEngine.CommandExecutor.Execute(null, typeof(ValidCommands.Single));
            Assert.Equal(999, (int)o.Output);
        }
    }
}
