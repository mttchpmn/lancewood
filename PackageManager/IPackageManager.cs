namespace PackageManager;

public interface IPackageManager
{
    Task<PackageInstallStatus> InstallPackage(string packageName);
}