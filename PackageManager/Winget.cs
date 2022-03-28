using System.Diagnostics;
using System.Security.Principal;

namespace PackageManager;

public class Winget : IPackageManager
{
    private const string ExecutableName = "winget.exe";
    private const string PackageNotFoundText = "No package found matching input criteria.";
    private const string SuccessText = "Successfully installed";

    public async Task<PackageInstallStatus> InstallPackage(string packageName)
    {
        if (!IsWingetInstalled())
        {
            throw new InvalidOperationException(
                "Winget is not installed on your system. Please install it and re-run Lancewood.");
        }
        
        var args = $"install --exact --force {packageName}";
        var startInfo = new ProcessStartInfo(ExecutableName, args)
        {
            UseShellExecute = false,
            RedirectStandardOutput = true
        };

        using var exeProcess = Process.Start(startInfo);

        if (exeProcess == null)
        {
            throw new InvalidOperationException("Unable to initialise package manager");
        }

        var result = PackageInstallStatus.Failed;

        while (!exeProcess.StandardOutput.EndOfStream)
        {
            var line = await exeProcess.StandardOutput.ReadLineAsync();
            Console.WriteLine(line);

            switch (line)
            {
                case PackageNotFoundText:
                    Console.WriteLine($"Package: {packageName} not found");
                    result = PackageInstallStatus.PackageNotFound;
                    break;
                case SuccessText:
                    result = PackageInstallStatus.Successful;
                    break;
            }
        }

        await exeProcess.WaitForExitAsync();

        return result;
    }
    
    private bool IsWingetInstalled()
    {
        return File.Exists(GetInstallPath());
    }

    private string GetInstallPath()
    {
        var installPath = $@"C:\Users\{Environment.UserName}\AppData\Local\Microsoft\WindowsApps\winget.exe";
        
        return installPath;
    }
}