using System.Diagnostics;

namespace Shell;

public class Posh : IShell
{
    public Task SymlinkFile(string targetPath, string sourcePath)
    {
        var command =
            $"New-Item -ItemType SymbolicLink -Force -Path {targetPath} -Target {sourcePath}";
        
        return ExecuteArbitraryCommand(command);
    }

    public Task OpenWebBrowser(string url)
    {
        if (String.IsNullOrWhiteSpace(url))
        {
            throw new InvalidOperationException("URL cannot be empty");
        }
        
        var command = $"MicrosoftEdge.exe {url}";
        
        return ExecuteArbitraryCommand(command);
    }

    public async Task ExecuteArbitraryCommand(string command)
    {
        var args = $"-Command {command}";
        var startInfo = new ProcessStartInfo("pwsh.exe", args)
        {
           // UseShellExecute = false,
           // RedirectStandardOutput = true
        };

        using var exe = Process.Start(startInfo);

        if (exe == null)
        {
            throw new InvalidOperationException("Unable to launch shell");
        }

        await exe.WaitForExitAsync();
    }
}