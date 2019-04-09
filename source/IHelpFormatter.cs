using CommandLineEngine.Parser;

namespace CommandLineEngine
{
    /// <summary>
    /// Formatter to output help
    /// </summary>
    public interface IHelpFormatter
    {
        /// <summary>
        /// Prints help to formatter
        /// </summary>
        /// <param name="commands">Full configuration</param>
        /// <param name="command">Specific command to output</param>
        /// <param name="operationMessages">List of operation message</param>
        void PrintHelp(Configuration configuration, Command command, Operation.Items operationMessages);
    }
}
