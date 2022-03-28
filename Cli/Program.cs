// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using PackageManager;

Console.WriteLine("--------------------| LANCEWOOD - OS Bootstrapper |--------------------");
var packageManager = PackageManagerFactory.GetPackageManager();

var status = await packageManager.InstallPackage("vim.vim");

Console.WriteLine($"STATUS: {status}");