using System;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace UnitTests
{
    public class ExecutionTests
    {
        #region Derive command

        [Fact]
        public void SingleCommandNoName()
        {
            var a = new string[] { "" };
            var o = CommandLineEngine.CommandExecutor.Execute(a, typeof(Execute.OneCommand), helpFormatter: new Execute.FormatterTest());
            Assert.Equal(1, (int)o.Output);
        }

        [Fact]
        public void SingleCommandByName()
        {
            var a = new string[] { "Command1" };
            var o = CommandLineEngine.CommandExecutor.Execute(a, typeof(Execute.OneCommand), helpFormatter: new Execute.FormatterTest());
            Assert.Equal(1, (int)o.Output);
        }

        [Fact]
        public void TwoCommandNoName()
        {
            var a = new string[] { "" };
            Assert.Throws<Execute.FormatterTest.ConfigurationException>(() =>
                CommandLineEngine.CommandExecutor.Execute(a, typeof(Execute.TwoCommand), helpFormatter: new Execute.FormatterTest())
            );
            // Assert help is printed for configuration
        }

        [Fact]
        public void TwoCommandByName()
        {
            var a = new string[] { "Command2" };
            var o = CommandLineEngine.CommandExecutor.Execute(a, typeof(Execute.TwoCommand), helpFormatter: new Execute.FormatterTest());
            Assert.Equal(2, (int)o.Output);
        }

        [Fact]
        public void TwoCommandWithDefaultNoName()
        {
            var a = new string[] { "" };
            var o = CommandLineEngine.CommandExecutor.Execute(a, typeof(Execute.TwoCommandWithDefault), helpFormatter: new Execute.FormatterTest());
            Assert.Equal(2, (int)o.Output);
        }

        [Fact]
        public void TwoCommandWithDefaultWithName()
        {
            var a = new string[] { "Command1" };
            var o = CommandLineEngine.CommandExecutor.Execute(a, typeof(Execute.TwoCommandWithDefault), helpFormatter: new Execute.FormatterTest());
            Assert.Equal(1, (int)o.Output);
        }

        [Fact]
        public void UnknownCommand()
        {
            var a = new string[] { "CommandDoesNotExist" };
            Assert.Throws<Execute.FormatterTest.ConfigurationException>(() =>
                CommandLineEngine.CommandExecutor.Execute(a, typeof(Execute.TwoCommand), helpFormatter: new Execute.FormatterTest())
            );
        }

        [Fact]
        public void RequestHelpConfiguration()
        {
            var a = new string[] { "--help" };
            Assert.Throws<Execute.FormatterTest.ConfigurationException>(() =>
                CommandLineEngine.CommandExecutor.Execute(a, typeof(Execute.TwoCommand), helpFormatter: new Execute.FormatterTest())
            );
            // Assert help is printed for configuration
        }

        [Fact]
        public void RequestHelpCommand()
        {
            var a = new string[] { "command1", "--help" };
            Assert.Throws<Execute.FormatterTest.CommandException>(() =>
                CommandLineEngine.CommandExecutor.Execute(a, typeof(Execute.TwoCommand), helpFormatter: new Execute.FormatterTest())
            );
            // Assert help is printed for a command
        }

        [Fact]
        public void RequestHelpCustom()
        {
            var a = new string[] { "--qwerty" };
            Assert.Throws<Execute.FormatterTest.ConfigurationException>(() =>
                CommandLineEngine.CommandExecutor.Execute(a, typeof(Execute.TwoCommand), new Execute.FormatterTest(), (_) => _.HelpCommandNames = new[] { "--qwerty" })
            );
            // Assert help is printed for configuration
        }

        #endregion

        #region Default help formatters

        [Fact]
        public void DefaultFormatterConsoleConfiguration()
        {
            var a = new string[] { "command1", "--help" };
            CommandLineEngine.CommandExecutor.Execute(a, typeof(Execute.TwoCommand));
            // Nothing to assert, but ensure test does not throw exception
        }

        [Fact]
        public void DefaultFormatterConsoleCommand()
        {
            var a = new string[] { "--help" };
            CommandLineEngine.CommandExecutor.Execute(a, typeof(Execute.TwoCommand));
            // Nothing to assert, but ensure test does not throw exception
        }

        [Fact]
        public void DefaultFormatterTextConfiguration()
        {
            var s = new System.Text.StringBuilder();
            var a = new string[] { "command1", "--help" };
            CommandLineEngine.CommandExecutor.Execute(a, typeof(Execute.TwoCommand), helpFormatter: new CommandLineEngine.Formatters.Text(s));
            Assert.NotEqual(0, s.Length);
        }

        [Fact]
        public void DefaultFormatterTextCommand()
        {
            var s = new System.Text.StringBuilder();
            var a = new string[] { "--help" };
            CommandLineEngine.CommandExecutor.Execute(a, typeof(Execute.TwoCommand), helpFormatter: new CommandLineEngine.Formatters.Text(s));
            Assert.NotEqual(0, s.Length);
        }

        #endregion

        #region Custom rules (ParameterRules attribute)

        [Fact]
        public void ParameterCustomRuleValid()
        {
            var a = new string[] { "" };
            var c = CommandLineEngine.CommandExecutor.Execute(a, typeof(Execute.ParameterRulesCommandValid));
            Assert.NotEmpty(c.Messages.OfType<Execute.ParameterRulesCommandValid.CustomMessage>());
        }

        [Fact]
        public void ParameterCustomRuleInvalid()
        {
            var a = new string[] { "" };
            Assert.Throws<Execute.FormatterTest.CommandException>(() =>
                CommandLineEngine.CommandExecutor.Execute(a, typeof(Execute.ParameterRulesCommandInvalid), helpFormatter: new Execute.FormatterTest())
            );
            // Assert help is printed for a command
        }

        #endregion

        #region Arguments parse

        [Fact]
        public void CommandObjectTypes()
        {
            var a = new string[] {
                "--_string", "a string",
                "--_bool1", "true",
                "--_bool2", // Switch is valid for boolean
                "--_int", "9",
                "--_long", "8",
                "--_decimal", "7",
                "--_dateTime", "2019-01-01",
                "--_stringArray", "string array 1", "string array 2",
                "--_boolArray", "true", "false",
                "--_intArray", "9", "8",
                "--_longArray", "7", "6",
                "--_decimalArray", "5", "4",
                "--_dateTimeArray", "2018-01-01", "2019-01-01"

            };
            var o = CommandLineEngine.CommandExecutor.Execute(a, typeof(Execute.CommandObjectTypes));
            Assert.Equal(1, (int)o.Output);
        }

        #endregion

        #region Others

        [Fact]
        public void ParameterLongPrefixChange()
        {
            var a = new string[] { "*ln", "test-value" };
            var o = CommandLineEngine.CommandExecutor.Execute(a, typeof(Execute.BasicCommand), configurationBuilder: (_) =>
            {
                _.LongParameterPrefix = "*";
                _.ShortParameterPrefix = "&";
            });
            Assert.Equal(1, (int)o.Output);
        }

        [Fact]
        public void ParameterShortPrefixChange()
        {
            var a = new string[] { "&sn", "test-value" };
            var o = CommandLineEngine.CommandExecutor.Execute(a, typeof(Execute.BasicCommand), configurationBuilder: (_) =>
            {
                _.LongParameterPrefix = "*";
                _.ShortParameterPrefix = "&";
            });
            Assert.Equal(1, (int)o.Output);
        }

        [Fact]
        public void NonStaticCommand()
        {
            var a = new string[] { "" };
            var o = CommandLineEngine.CommandExecutor.Execute(a, typeof(Execute.NonStaticCommand));
            Assert.Equal(1, (int)o.Output);
        }

        [Fact]
        public void SystemParameterCommand()
        {
            var a = new string[] { "" };
            var o = CommandLineEngine.CommandExecutor.Execute(a, typeof(Execute.SystemParameterCommand));
            // Nothing to assert, but ensure test does not throw exception in method
        }

        [Fact]
        public void CustomParseCommand()
        {
            var a = new string[] { "" };
            var o = CommandLineEngine.CommandExecutor.Execute(a, typeof(Execute.CustomParseCommand), configurationBuilder: (_) =>
            {
                _.ParseArgumentsAction = (a1, b1, c1, d1) =>
                {
                    d1(b1.Parameters.First(), "p", new[] { "override" });
                    return true;
                };
            });
            Assert.Equal("override", (string)o.Output);
        }
        

        #endregion
    }
}
