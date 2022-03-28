using System.Runtime.InteropServices;

namespace PackageManager;

public static class PackageManagerFactory
{
    public static IPackageManager GetPackageManager()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            return new Winget();
        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            throw new NotSupportedException("Mac OS not yet supported");
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            throw new NotSupportedException("Linux not yet supported");

        throw new NotSupportedException("Platform not supported");
    }
}