using System;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace UnitTests
{
    public class ParseTests
    {
        #region Number of tests

        [Fact]
        public void SingleCommand()
        {
            var c = CommandLineEngine.CommandParser.Parse(typeof(ValidCommands.Single));
            Assert.Single(c.Commands);
        }

        [Fact]
        public void MultipleCommands()
        {
            var c = CommandLineEngine.CommandParser.Parse(typeof(ValidCommands.Multiple));
            Assert.Equal(2, c.Commands.Count());
        }

        [Fact]
        public void SingleCommandExternal()
        {
            var c = CommandLineEngine.CommandParser.Parse(typeof(UnitTestsData.Single));
            Assert.Single(c.Commands);
        }

        [Fact]
        public void MultipleCommandsExternal()
        {
            var c = CommandLineEngine.CommandParser.Parse(typeof(UnitTestsData.Multiple));
            Assert.Equal(2, c.Commands.Count());
        }

        #endregion

        #region Attribute Command tests

        #endregion

        #region Attribute Parameter tests

        #endregion

        #region Attribute ParameterVisibility tests

        #endregion

        #region Attribute ParameterRule tests

        #endregion

        #region Validation tests

        [Fact]
        public void CommandWithSpaces()
        {
            Assert.Throws<CommandLineEngine.CommandLineEngineDevelopperException>(
                () => CommandLineEngine.CommandParser.Parse(typeof(InvalidCommands.CommandWithSpaces)));
        }

        [Fact]
        public void ParameterWithSpaces()
        {
            Assert.Throws<CommandLineEngine.CommandLineEngineDevelopperException>(
                () => CommandLineEngine.CommandParser.Parse(typeof(InvalidCommands.ParameterWithSpaces)));
        }

        [Fact]
        public void TwoCommandsSameName()
        {
            Assert.Throws<CommandLineEngine.CommandLineEngineDevelopperException>(
                () => CommandLineEngine.CommandParser.Parse(typeof(InvalidCommands.TwoCommandsSameName)));
        }

        #endregion
    }
}
