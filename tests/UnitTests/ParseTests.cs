using System;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace UnitTests
{
    public class ParseTests
    {
        internal const string name = "test-name";
        internal const string shortName = "test-shortName";
        internal const string description = "test-description";
        internal const string helpUrl = "test-helpUrl";

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

        [Fact]
        public void CommandAttribute()
        {
            var c = CommandLineEngine.CommandParser.Parse(typeof(ValidCommands.AttributeCommand)).Commands.First();
            Assert.Equal(c.Name, ParseTests.name);
            Assert.Equal(c.Description, ParseTests.description);
            Assert.Equal(c.HelpUrl, ParseTests.helpUrl);
        }

        #endregion

        #region Attribute Parameter tests

        [Fact]
        public void ParameterAttribute()
        {
            var c = CommandLineEngine.CommandParser.Parse(typeof(ValidCommands.AttributeParameter)).Commands.First();
            var p = c.Parameters.First();
            Assert.Equal(p.Name, ParseTests.name);
            Assert.Equal(p.ShortName, ParseTests.shortName);
            Assert.Equal(p.Description, ParseTests.description);
        }

        #endregion

        #region Attribute ParameterVisibility tests

        [Fact]
        public void ParameterVisibilityAttribute()
        {
            var c = CommandLineEngine.CommandParser.Parse(typeof(ValidCommands.AttributeParameterHidden)).Commands.First();
            var p = c.Parameters.ToArray();

            Assert.Equal(3, p.Length);
            Assert.True(p[0].Visible);
            Assert.False(p[0].HasDefaultValue);
            Assert.True(p[1].Visible);
            Assert.True(p[1].HasDefaultValue);
            Assert.False(p[2].Visible);
            Assert.True(p[2].HasDefaultValue);
        }

        #endregion

        #region Validation tests

        [Fact]
        public void OneCommandMinimum()
        {
            Assert.Throws<CommandLineEngine.CommandLineEngineDevelopperException>(
                   () => CommandLineEngine.CommandParser.Parse(typeof(InvalidCommands.CommandNone)));
        }

        [Fact]
        public void OneDefaultCommandMaximum()
        {
            Assert.Throws<CommandLineEngine.CommandLineEngineDevelopperException>(
                   () => CommandLineEngine.CommandParser.Parse(typeof(InvalidCommands.CommandManyDefault)));
        }

        [Fact]
        public void CommandWithSpacesFail()
        {
            Assert.Throws<CommandLineEngine.CommandLineEngineDevelopperException>(
                () => CommandLineEngine.CommandParser.Parse(typeof(InvalidCommands.CommandWithSpaces)));
        }

        [Fact]
        public void CommandReservedKeywordFail()
        {
            Assert.Throws<CommandLineEngine.CommandLineEngineDevelopperException>(
                () => CommandLineEngine.CommandParser.Parse(typeof(InvalidCommands.CommandReservedKeyword)));
        }

        [Fact]
        public void CommandSameNameFail()
        {
            Assert.Throws<CommandLineEngine.CommandLineEngineDevelopperException>(
                () => CommandLineEngine.CommandParser.Parse(typeof(InvalidCommands.CommandSameName)));
        }

        [Fact]
        public void ParameterWithSpacesFail()
        {
            Assert.Throws<CommandLineEngine.CommandLineEngineDevelopperException>(
                () => CommandLineEngine.CommandParser.Parse(typeof(InvalidCommands.ParameterWithSpaces)));
        }

        [Fact]
        public void ParameterReservedKeywordFail()
        {
            Assert.Throws<CommandLineEngine.CommandLineEngineDevelopperException>(
                () => CommandLineEngine.CommandParser.Parse(typeof(InvalidCommands.ParameterReservedKeyword)));
        }

        [Fact]
        public void ParameterSameNameFail()
        {
            Assert.Throws<CommandLineEngine.CommandLineEngineDevelopperException>(
                () => CommandLineEngine.CommandParser.Parse(typeof(InvalidCommands.ParameterSameName)));
        }

        [Fact]
        public void ParameterNoDefaultHiddenFail()
        {
            Assert.Throws<CommandLineEngine.CommandLineEngineDevelopperException>(
                () => CommandLineEngine.CommandParser.Parse(typeof(InvalidCommands.ParameterNoDefaultHidden)));
        }

        #endregion

        #region Configuration tests

        [Fact]
        public void UpdateProgramInformation()
        {
            var c = CommandLineEngine.CommandParser.Parse(typeof(ValidCommands.Single), configurationBuilder: cb =>
                {
                    cb.Program.Name = ParseTests.name;
                    cb.Program.HelpUrl = ParseTests.helpUrl;
                    cb.Program.Description = ParseTests.description;
                });

            Assert.Equal(ParseTests.name, c.Program.Name);
            Assert.Equal(ParseTests.helpUrl, c.Program.HelpUrl);
            Assert.Equal(ParseTests.description, c.Program.Description);
        }

        [Fact]
        public void UpdateParameterPrefix()
        {
            var c = CommandLineEngine.CommandParser.Parse(typeof(ValidCommands.Single), configurationBuilder: cb =>
            {
                cb.LongParameterPrefix = "*****";
                cb.ShortParameterPrefix = "/////";
            });

            Assert.Equal("*****", c.LongParameterPrefix);
            Assert.Equal("/////", c.ShortParameterPrefix);
        }

        [Fact]
        public void UpdateHelpCommandNames()
        {
            var c = CommandLineEngine.CommandParser.Parse(typeof(ValidCommands.Single), configurationBuilder: cb =>
            {
                cb.HelpCommandNames = new[] { "update-help" };
            });

            Assert.Single(c.HelpCommandNames);
            Assert.Equal("update-help", c.HelpCommandNames[0]);
        }

        [Fact]
        public void CustomConfigurationActionValid()
        {
            var c = CommandLineEngine.CommandParser.Parse(typeof(ValidCommands.Single), configurationBuilder: cb =>
            {
                cb.ConfigurationValidationAction = (_, or) =>
                {
                    or.Messages.Add(new CommandLineEngine.Operation.Types.Warning("test-message"));
                    return true;
                };
            });

            //Assert.Single(c.HelpCommandNames);
            //Assert.Equal("update-help", c.HelpCommandNames[0]);
        }

        #endregion
    }
}
