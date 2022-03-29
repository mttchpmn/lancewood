// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using PackageManager;
using Shell;

Console.WriteLine("--------------------| LANCEWOOD - OS Bootstrapper |--------------------");
var packageManager = PackageManagerFactory.GetPackageManager();

// var status = await packageManager.InstallPackage("Vim.vim");
// Console.WriteLine($"STATUS: {status}");

var shell = ShellFactory.GetShell();

// shell.ExecuteArbitraryCommand("ls");
// shell.OpenWebBrowser("");
shell.SymlinkFile(@"C:\Users\matt\Documents\test.txt", @"C:\Users\matt\test.txt");