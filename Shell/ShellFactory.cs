using System.Runtime.InteropServices;

namespace Shell;

public static class ShellFactory
{
   public static IShell GetShell()
   {
       if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
           return new Posh();
        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            throw new NotSupportedException("Mac OS not yet supported");
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            throw new NotSupportedException("Linux not yet supported");

        throw new NotSupportedException("Platform not supported");
   }
}