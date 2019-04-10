# CommandLineEngine
![AppVeyor build](https://img.shields.io/appveyor/ci/blouin/commandlineengine.svg)
![AppVeyor tests](https://img.shields.io/appveyor/tests/blouin/commandlineengine.svg)

This library uses conventions in your code in order to allow command-line functionnality (parsing and executing commands), such as creating a CLI. When the code is executed by the user, the first argument is the command name, followed by the arguments. The library will find the command and execute it passing in the parameters recieved. Otherwise, it will print help information.

To use the library in your application, install the [NuGet package](https://www.nuget.org/packages/CommandLineEngine/).

Features:
* Minimal attributes required for basic functionnality
* Automatic mapping of command and parameter name
* Automatic recognition of optional parameters
* Automatically generated help page (can also be customized)
* Ability to hide optional parameter from help (such as `debug` parameters)
* Many advanced customizations available

## Getting started
In your code, simply add the `[Command]` attribute on all methods or functions you wish to identify a command. By default, all parameters in the function will become arguments that can be passed on the command line. 
Then, in the Main method, call `CommandExecutor.Execute(args)`;

### Basic functionnality
```c#
class Program
{
    static void Main(string[] args)
    {
        CommandLineEngine.CommandExecutor.Execute(args);
    }

    [Command]
    static void SayHello(string name)
    {
        Console.WriteLine($"Hello {name}");
    }

    [Command]
    static void SayGoodbye(string name)
    {
        Console.WriteLine($"Goodbye {name}!");
    }
}
```
Assuming the code compiles to MyApp.exe, we can simply call
```
MyApp.exe SayHello --name UserName
Output: Hello UserName!
```

It is also possible to request help by passing the `--help` tag, either globally or for a particular command
```
MyApp.exe --help
or
MyApp.exe SayHello --help
```

## Basic customizations
The following attributes allow you to customize the command and the parameters
### Command Attribute
Property | Use
------------ | -------------
Name | Allows you to override the name of the command, by default, uses the method name.
Description | Allows you to specify a description for the method, will be shown in help.
HelpUrl | Allows you to specify a URL for more information, will be shown in help.
### Parameter Attribute
Property | Use
------------ | -------------
Name | Allows you to override the name of the parameter, by default, uses the parameter name. Passed as `--name value`.
ShortName | Allows you to specify a a short name for the parameter. Passed as `-shortName value`.
Description | Allows you to specify a description for the parameter, will be shown in help.


## Advanced customizations
See [wiki](https://github.com/blouin/CommandLineEngine/wiki) for more customizations available
