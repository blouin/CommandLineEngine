﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CommandLineEngine {
    using System;
    using System.Reflection;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("CommandLineEngine.Resources", typeof(Resources).GetTypeInfo().Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Command with spaces in the names are not allowed. Command &apos;{0}&apos;..
        /// </summary>
        internal static string CommandCanNotHaveSpace {
            get {
                return ResourceManager.GetString("CommandCanNotHaveSpace", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to There can be at most one default command..
        /// </summary>
        internal static string CommandDefaultMostOne {
            get {
                return ResourceManager.GetString("CommandDefaultMostOne", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Duplicate command named &apos;{0}&apos; found. Ensure that all methods with the [Command] attribute have unique names..
        /// </summary>
        internal static string CommandDuplicate {
            get {
                return ResourceManager.GetString("CommandDuplicate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Command name is &apos;{0}&apos; and help switch is set to &apos;{1}&apos;..
        /// </summary>
        internal static string CommandInformation {
            get {
                return ResourceManager.GetString("CommandInformation", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Command &apos;{0}&apos; has invalid parameters. Can not be executed..
        /// </summary>
        internal static string CommandInvalid {
            get {
                return ResourceManager.GetString("CommandInvalid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Command with reserved keyword found. Reserved kewords are &apos;{0}&apos;..
        /// </summary>
        internal static string CommandReservedKeywords {
            get {
                return ResourceManager.GetString("CommandReservedKeywords", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Command &apos;{0}&apos; is executable with specified parameters..
        /// </summary>
        internal static string CommandValid {
            get {
                return ResourceManager.GetString("CommandValid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Creating default instance of &apos;{0}&apos; using the constructor with no arguments..
        /// </summary>
        internal static string CreatingDefaultInstance {
            get {
                return ResourceManager.GetString("CreatingDefaultInstance", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid developpement configuration. See OperationMessages property on this exception for details..
        /// </summary>
        internal static string DevelopperException {
            get {
                return ResourceManager.GetString("DevelopperException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Error executing command..
        /// </summary>
        internal static string ErrorExecutingCommand {
            get {
                return ResourceManager.GetString("ErrorExecutingCommand", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Error validating arguments before executing..
        /// </summary>
        internal static string ErrorValidatingCommand {
            get {
                return ResourceManager.GetString("ErrorValidatingCommand", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Executing command &apos;{0}&apos;..
        /// </summary>
        internal static string ExecutingCommand {
            get {
                return ResourceManager.GetString("ExecutingCommand", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Executing command &apos;{0}&apos; from arguments..
        /// </summary>
        internal static string ExecutingCommandFromArguments {
            get {
                return ResourceManager.GetString("ExecutingCommandFromArguments", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Get command from arguments..
        /// </summary>
        internal static string GetFromArguments {
            get {
                return ResourceManager.GetString("GetFromArguments", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Available commands:.
        /// </summary>
        internal static string Help_AvailableCommands {
            get {
                return ResourceManager.GetString("Help_AvailableCommands", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Default value: {0}.
        /// </summary>
        internal static string Help_DefaultValue {
            get {
                return ResourceManager.GetString("Help_DefaultValue", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Help for command &apos;{0}&apos;.
        /// </summary>
        internal static string Help_ForCommand {
            get {
                return ResourceManager.GetString("Help_ForCommand", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to See: {0}.
        /// </summary>
        internal static string Help_HelpUrl {
            get {
                return ResourceManager.GetString("Help_HelpUrl", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No description available..
        /// </summary>
        internal static string Help_NoDescription {
            get {
                return ResourceManager.GetString("Help_NoDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Options:.
        /// </summary>
        internal static string Help_Options {
            get {
                return ResourceManager.GetString("Help_Options", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Parameters:.
        /// </summary>
        internal static string Help_Parameters {
            get {
                return ResourceManager.GetString("Help_Parameters", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Usage: {0} [commandName] [arguments].
        /// </summary>
        internal static string Help_Usage {
            get {
                return ResourceManager.GetString("Help_Usage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid.
        /// </summary>
        internal static string Invalid {
            get {
                return ResourceManager.GetString("Invalid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No commands found. Ensure that at least one method is marked with the [Command] attribute..
        /// </summary>
        internal static string NoCommandFound {
            get {
                return ResourceManager.GetString("NoCommandFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Parameter with spaces in the names are not allowed. Parameter &apos;{0}&apos; found for command &apos;{1}&apos;..
        /// </summary>
        internal static string ParameterCanNotHaveSpace {
            get {
                return ResourceManager.GetString("ParameterCanNotHaveSpace", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Duplicate parameter named &apos;{0}&apos; found for command &apos;{1}&apos;. Ensure that all parameters with the [Parameter] attribute have unique names..
        /// </summary>
        internal static string ParameterDuplicate {
            get {
                return ResourceManager.GetString("ParameterDuplicate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Parameter &apos;{0}&apos; value missing..
        /// </summary>
        internal static string ParameterMissing {
            get {
                return ResourceManager.GetString("ParameterMissing", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Parameter with reserved keyword found for command &apos;{1}&apos;. Reserved kewords are &apos;{0}&apos;..
        /// </summary>
        internal static string ParameterReservedKeywords {
            get {
                return ResourceManager.GetString("ParameterReservedKeywords", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Parameters can not be parsed..
        /// </summary>
        internal static string ParametersNotParsable {
            get {
                return ResourceManager.GetString("ParametersNotParsable", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Hidden parameter named &apos;{0}&apos; found for command &apos;{1}&apos; does not have a default value specified. Ensure that all parameters with the [Hidden] attribute have default values..
        /// </summary>
        internal static string ParameterVisible {
            get {
                return ResourceManager.GetString("ParameterVisible", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unknown argument &apos;{0}&apos;..
        /// </summary>
        internal static string UnknownArgument {
            get {
                return ResourceManager.GetString("UnknownArgument", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unknown command name..
        /// </summary>
        internal static string UnknownCommand {
            get {
                return ResourceManager.GetString("UnknownCommand", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unknown command name, but a single exists. Executing command &apos;{0}&apos;..
        /// </summary>
        internal static string UnknownCommandSingle {
            get {
                return ResourceManager.GetString("UnknownCommandSingle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Valid.
        /// </summary>
        internal static string Valid {
            get {
                return ResourceManager.GetString("Valid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Validating command &apos;{0}&apos;..
        /// </summary>
        internal static string ValidatingCommand {
            get {
                return ResourceManager.GetString("ValidatingCommand", resourceCulture);
            }
        }
    }
}
