
namespace CommandLineEngine
{
    public interface IHelpFormatterWithOption : IHelpFormatter
    {
        /// <summary>
        /// Gets if we quit the program after displaying help
        /// </summary>
        bool ExitWithError { get; }
    }
}
