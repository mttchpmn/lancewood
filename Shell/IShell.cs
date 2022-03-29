namespace Shell;

public interface IShell
{
    Task SymlinkFile(string targetPath, string sourcePath);
    
    Task OpenWebBrowser(string url);

    Task ExecuteArbitraryCommand(string command);
}