namespace Lancewood;

public record LancewoodConfig(
    IEnumerable<Package> Packages,
    IEnumerable<WebLink> WebLinks,
    IEnumerable<Dotfile> Dotfiles,
    IEnumerable<ArbitraryCommand> Commands);


public record Package(string Name, string FriendlyName);

public record WebLink(string Name, string Url);

public record Dotfile(string Name, string SourcePath, string TargetPath);

public record ArbitraryCommand(string Name, string Command);
