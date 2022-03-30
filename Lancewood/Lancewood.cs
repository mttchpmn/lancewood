using PackageManager;
using Shell;

namespace Lancewood;

public class Lancewood
{
    private readonly IShell _shell;
    private readonly IPackageManager _packageManager;
    private readonly LancewoodConfig? _config;
    private readonly TextWriter _outputStream;

    public Lancewood(TextWriter outputStream)
    {
        _outputStream = outputStream;
        _shell = ShellFactory.GetShell();
        _packageManager = PackageManagerFactory.GetPackageManager();
    }

    Task LoadConfig(string filePath)
    {
        throw new NotImplementedException();
    }

    public async Task InstallPackages()
    {
        if (_config == null)
        {
            await _outputStream.WriteLineAsync("No Lancewood config loaded. Aborting...");
            return;
        }

        if (_config.Packages == null || !_config.Packages.Any())
        {
            await _outputStream.WriteLineAsync("No packages to install");
        }

        foreach (var package in _config.Packages)
        {
            await _outputStream.WriteAsync($"Installing {package.FriendlyName}...");
            // await _packageManager.InstallPackage(package.Name);
        }
    }

    public async Task SymlinkFiles()
    {
        if (_config == null)
        {
            await _outputStream.WriteLineAsync("No Lancewood config loaded. Aborting...");
            return;
        }

        if (_config.Dotfiles == null || !_config.Dotfiles.Any())
        {
            await _outputStream.WriteLineAsync("No dotfiles to symlink");
        }

        foreach (var dotfile in _config.Dotfiles)
        {
            await _outputStream.WriteAsync($"Symlinking {dotfile.Name}...");
            // await _packageManager.InstallPackage(package.Name);
        }
    }

    public async Task OpenWebLinks()
    {
        if (_config == null)
        {
            await _outputStream.WriteLineAsync("No Lancewood config loaded. Aborting...");
            return;
        }

        if (_config.WebLinks == null || !_config.WebLinks.Any())
        {
            await _outputStream.WriteLineAsync("No web links to open");
        }

        foreach (var link in _config.WebLinks)
        {
            await _outputStream.WriteAsync($"Opening web link for {link.Name}...");
            // await _packageManager.InstallPackage(package.Name);
        }
    }

    public async Task RunArbitraryCommands()
    {
        if (_config == null)
        {
            await _outputStream.WriteLineAsync("No Lancewood config loaded. Aborting...");
            return;
        }

        if (_config.Commands == null || !_config.Commands.Any())
        {
            await _outputStream.WriteLineAsync("No packages to install");
        }

        foreach (var command in _config.Commands)
        {
            await _outputStream.WriteAsync($"Executing {command.Name}...");
            // await _packageManager.InstallPackage(package.Name);
        }
    }
}