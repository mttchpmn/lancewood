using System.Text.Json;
using PackageManager;
using Shell;

namespace Lancewood;

public class Engine
{
    private readonly LancewoodConfig _config;
    private readonly TextWriter _outputStream;
    private readonly IPackageManager _packageManager;
    private readonly IShell _shell;
    private readonly string _filePath;

    public Engine(String configPath, TextWriter outputStream)
    {
        _filePath = configPath;
        _config = LoadConfig(configPath);
        _outputStream = outputStream;
        _packageManager = PackageManagerFactory.GetPackageManager();
        _shell = ShellFactory.GetShell();
    }

    private LancewoodConfig LoadConfig(string filePath)
    {
        var fileContent = File.ReadAllText(filePath);
        var jsonSerializerOptions = new JsonSerializerOptions()
            {PropertyNamingPolicy = JsonNamingPolicy.CamelCase, WriteIndented = true};
        var config = JsonSerializer.Deserialize<LancewoodConfig>(fileContent, jsonSerializerOptions);

        return config;
    }

    public async Task InstallPackages()
    {
        if (!_config.Packages.Any())
        {
            await _outputStream.WriteLineAsync("No packages to install");
        }

        foreach (var package in _config.Packages)
        {
            await _outputStream.WriteAsync($"Installing {package.FriendlyName}...");
            await _packageManager.InstallPackage(package.Name);
            await _outputStream.WriteLineAsync($"Done.");
        }
    }

    public async Task SymlinkFiles()
    {
        if (!_config.Dotfiles.Any())
        {
            await _outputStream.WriteLineAsync("No dotfiles to symlink");
        }

        foreach (var dotfile in _config.Dotfiles)
        {
            await _outputStream.WriteAsync($"Symlinking {dotfile.Name}...");
            await _shell.SymlinkFile(dotfile.TargetPath, dotfile.SourcePath);
            await _outputStream.WriteLineAsync($"Done.");
        }
    }

    public async Task OpenWebLinks()
    {
        if (!_config.WebLinks.Any())
        {
            await _outputStream.WriteLineAsync("No web links to open");
        }

        foreach (var link in _config.WebLinks)
        {
            await _outputStream.WriteAsync($"Opening web link for {link.Name}..........");
            await _shell.OpenWebBrowser(link.Url);
            await _outputStream.WriteLineAsync($"Done.");
        }
    }

    public async Task RunArbitraryCommands()
    {
        if (!_config.Commands.Any())
        {
            await _outputStream.WriteLineAsync("No packages to install");
        }

        foreach (var command in _config.Commands)
        {
            await _outputStream.WriteAsync($"Executing {command.Name}...");
            await _shell.ExecuteArbitraryCommand(command.Command);
            await _outputStream.WriteLineAsync($"Done.");
        }
    }

    public async Task EditConfig()
    {
        // This will open the file in the default editor
        await _shell.ExecuteArbitraryCommand(_filePath);
    }
}