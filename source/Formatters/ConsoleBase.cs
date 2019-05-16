using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CommandLineEngine.Formatters
{
    /// <summary>
    /// Base console formatter object
    /// </summary>
    public abstract class ConsoleBase : FormatterBase
    {
        #region Private Attributes

        /// <summary>
        /// The console width
        /// </summary>
        private int consoleWidth;

        #endregion

        #region Class Construction

        /// <summary>
        /// Creates the base formatter
        /// </summary>
        protected ConsoleBase()
        {
            try { consoleWidth = System.Console.WindowWidth; }
            catch { consoleWidth = 80; } // Default value
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Utility function to write text to output
        /// </summary>
        protected void WriteLine()
        {
            WriteLine(1, String.Empty, null);
        }

        /// <summary>
        /// Utility function to write text to output
        /// </summary>
        /// <param name="text">Text to output</param>
        protected virtual void WriteLine(string text)
        {
            WriteLine(2, text, null);
        }

        /// <summary>
        /// Utility function to write text to output
        /// </summary>
        /// <param name="text">Text to output</param>
        /// <param name="color">Color text to output</param>
        protected void WriteLine(string text, System.ConsoleColor? color)
        {
            WriteLine(2, text, color);
        }

        /// <summary>
        /// Utility function to write text to output
        /// </summary>
        /// <param name="lineOffset">Length of tab for new lines</param>
        /// <param name="text">Text to output</param>
        protected void WriteLine(int lineOffset, string text)
        {
            WriteLine(lineOffset, text, null);
        }

        /// <summary>
        /// Utility function to write text to output
        /// </summary>
        /// <param name="lineOffset">Length of tab for new lines</param>
        /// <param name="text">Text to output</param>
        /// <param name="color">Color text to output</param>
        protected virtual void WriteLine(int lineOffset, string text, System.ConsoleColor? color)
        {
            int length = consoleWidth - lineOffset - 1;

            List<string> textLines = new List<string>();
            if (text.Length <= consoleWidth)
            {
                textLines.Add(text);
            }
            else
            {
                text = text.Replace(Environment.NewLine, String.Empty);

                // Remove first line
                textLines.Add(text.Substring(0, consoleWidth - 1));
                text = text.Remove(0, consoleWidth - 1);

                // Add rest in lines
                string regex = @"(?<=\G.{" + length + "})";
                var lines = Regex.Split(text, regex, RegexOptions.Singleline);
                foreach (var line in lines)
                {
                    var sb = new StringBuilder();
                    for (int i = 0; i < lineOffset; i++)
                    {
                        sb.Append(" ");
                    }
                    if (line[0] == ' ')
                    {
                        sb.Append(line.Remove(0, 1));
                    }
                    else
                    {
                        sb.Append(line);
                    }
                    textLines.Add(sb.ToString());
                }
            }

            // Add lines
            foreach (var textLine in textLines)
            {
                if (color.HasValue)
                {
                    System.Console.ForegroundColor = color.Value;
                }
                System.Console.WriteLine(textLine);
                System.Console.ResetColor();
            }
        }

        #endregion
    }
}