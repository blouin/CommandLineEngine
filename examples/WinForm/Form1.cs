using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CommandLineEngine.Operation;
using CommandLineEngine.Parser;

namespace TestWinForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // Get text formatter
            var f = new CommandLineEngine.Formatters.Text(null);

            // Print help
            CommandLineEngine.CommandParser.PrintHelp(helpFormatter: f);
            textBox1.Text = f.StringBuilder.ToString();

            // Passing NULL to args, so we get from Environment
            var o = CommandLineEngine.CommandExecutor.Execute(null, 
                helpFormatter: f, 
                configurationBuilder: c => { 
                    // Program information
                    c.Program.Name = "Sample application";
                    c.Program.HelpUrl = "https://github.com/blouin/CommandLineEngine";
                    c.ExitOnError = false;
                });
        }

        [CommandLineEngine.Attributes.Command()]
        static int SingleCommand(
            [CommandLineEngine.Attributes.Parameter("arg1", "a1", "Help for argument 1")]
            string arg1 = "DefaultArg2Value",
            [CommandLineEngine.Attributes.Parameter("arg2", "a2", "Help for argument 2")]
            string arg2 = "DefaultArg2Value"
        )
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Command ran successfully.");
            sb.AppendLine($" Arg1: {arg1}");
            sb.AppendLine($" Arg2: {arg2}");
            MessageBox.Show(sb.ToString());

            return 1;
        }
    }
}
