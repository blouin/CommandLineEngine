using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using CommandLineEngine.Attributes;

namespace CommandLineEngine
{
    /// <summary>
    /// Class used to run commands
    /// </summary>
    public static class CommandParser
    {
        /// <summary>
        /// Parses the specified types and retrieves all possible commands
        /// </summary>
        /// <param name="configurationBuilder">Configuration builder action, or nothing to use defaults</param>
        /// <returns>Configuration retrieved from parse operation</returns>
        public static Parser.Configuration Parse(Action<Parser.Configuration> configurationBuilder = null)
        {
            return Parse(new Type[] { }, configurationBuilder);
        }

        /// <summary>
        /// Parses the specified types and retrieves all possible commands
        /// </summary>
        /// <param name="type">Types in which to get all assemblies, or nothing to scan all assembly</param>
        /// <param name="configurationBuilder">Configuration builder action, or nothing to use defaults</param>
        /// <returns>Configuration retrieved from parse operation</returns>
        public static Parser.Configuration Parse(Type type, Action<Parser.Configuration> configurationBuilder = null)
        {
            return Parse(new[] { type }, configurationBuilder);
        }

        /// <summary>
        /// Parses the specified types and retrieves all possible commands
        /// </summary>
        /// <param name="types">List of types in which to get all assemblies, or nothing to scan all assembly</param>
        /// <param name="configurationBuilder">Configuration builder action, or nothing to use defaults</param>
        /// <returns>Configuration retrieved from parse operation</returns>
        public static Parser.Configuration Parse(Type[] types, Action<Parser.Configuration> configurationBuilder = null)
        {
            // Create configuration
            var configuration = new Parser.Configuration();
            configurationBuilder?.Invoke(configuration);

            // Ensure we got types, otherwise, extract from entry assembly
            types = types == null || types.Count() == 0 ?
                Assembly.GetEntryAssembly().DefinedTypes.Select(i => i.AsType()).ToArray() :
                types;

            // Extract all commands
            var commandsParsed = types
                .SelectMany(t =>
                    t
                        .GetTypeInfo()
                        .GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static)
                        .Select(i => new
                            {
                                MethodInfo = i,
                                Attr = i.GetCustomAttribute<CommandAttribute>(),
                                Default = i.GetCustomAttribute<CommandDefaultAttribute>()
                            })
                        .Where(i => i.Attr != null)
                        .Select(i => new { C = new Parser.Command(configuration, i.MethodInfo, i.Attr), D = i.Default })
                )
                .ToArray();

            // Save commands
            configuration.Commands = commandsParsed
                .Select(i => i.C)
                .ToArray();

            // For each command, extract parameters
            foreach (var cmd in configuration.Commands)
            {
                var cmdParameters = cmd.MethodInfo.GetParameters()
                    .Select(i => new
                        {
                            ParameterInfo = i,
                            Attr = i.GetCustomAttribute<ParameterAttribute>(),
                            Hidden = i.GetCustomAttribute<ParameterHiddenAttribute>(),
                            Rules = i.GetCustomAttributes<ParameterRuleAttribute>(),
                            System =
                                typeof(Parser.InputArguments).GetTypeInfo().IsAssignableFrom(i.ParameterType) ||
                                typeof(Operation.OperationResult).GetTypeInfo().IsAssignableFrom(i.ParameterType)
                        })
                    .Select(i => new { S = i.System, P = new Parser.CommandParameter(cmd, i.ParameterInfo, i.Rules, i.Attr, i.Hidden) });
                cmd.Parameters = cmdParameters.Where(i => !i.S).Select(i => i.P).ToArray();
                cmd.SystemParameters = cmdParameters.Where(i => i.S).Select(i => i.P).ToArray();
            }

            // Validate the configuration
            var vr = configuration.Validate(commandsParsed.Count(c => c.D != null));
            if (!vr.Valid)
            {
                throw new CommandLineEngineDevelopperException(vr);
            }

            // Get default command
            configuration.DefaultCommand = commandsParsed.FirstOrDefault(i => i.D != null)?.C;

            return configuration;
        }

        /// <summary>
        /// Prints help for all possible commands in the specified types
        /// </summary>
        /// <param name="helpFormatter">Help formatting engine, or nothing to format the messages to console</param>
        /// <param name="configurationBuilder">Configuration builder action, or nothing to use defaults</param>
        public static void PrintHelp(IHelpFormatter helpFormatter = null, Action<Parser.Configuration> configurationBuilder = null)
        {
            PrintHelp(new Type[] { }, helpFormatter, configurationBuilder);
        }

        /// <summary>
        /// Prints help for all possible commands in the specified types
        /// </summary>
        /// <param name="type">Types in which to get all assemblies, or nothing to scan all assembly</param>
        /// <param name="helpFormatter">Help formatting engine, or nothing to format the messages to console</param>
        /// <param name="configurationBuilder">Configuration builder action, or nothing to use defaults</param>
        public static void PrintHelp(Type type, IHelpFormatter helpFormatter = null, Action<Parser.Configuration> configurationBuilder = null)
        {
            PrintHelp(new[] { type }, helpFormatter, configurationBuilder);
        }

        /// <summary>
        /// Prints help for all possible commands in the specified types
        /// </summary>
        /// <param name="types">List of types in which to get all assemblies, or nothing to scan all assembly</param>
        /// <param name="helpFormatter">Help formatting engine, or nothing to format the messages to console</param>
        /// <param name="configurationBuilder">Configuration builder action, or nothing to use defaults</param>
        public static void PrintHelp(Type[] types, IHelpFormatter helpFormatter = null, Action<Parser.Configuration> configurationBuilder = null)
        {
            // Extract available commands based on information gathered & print help
            var configuration = Parse(types, configurationBuilder);
            PrintHelpInternal(configuration, null, helpFormatter, new Operation.Items(new Operation.OperationResult()), false);
        }

        #region Internal Functions

        internal static void PrintHelpInternal(Parser.Configuration configuration, Parser.Command command, IHelpFormatter helpFormatter, Operation.Items operationMessages, bool error = true)
        {
            var f = (helpFormatter ?? new Formatters.Console());

            // Print help using the help formatter, or create a default console messenger for this
            f.PrintHelp(configuration, command, operationMessages);
            PrintDebug(operationMessages);

            if (error && configuration.ExitOnError)
            {
                Environment.Exit(Int32.MinValue);
            }
        }

        internal static void PrintDebug(Operation.Items operationMessages)
        {
#if DEBUG
            // Spit out all messages for debug mode
            operationMessages.ForEach(i => Debug.WriteLine($"{i.Message} ({i.Type.ToString()})"));
#endif
        }

        #endregion
    }
}
