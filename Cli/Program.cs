using Lancewood;
using PackageManager;
using Shell;
using Spectre.Console;

var lancewood = new Lancewood.Lancewood(Console.Out);

const string runAll = "Run all bootstrapping actions";
const string installPackages = "Install packages";
const string openWebLinks = "Open web links";
const string symlinkDotfiles = "Symlink dotfiles to OS";
const string executeArbitraryCommands = "Execute arbitrary commands";
const string editConfig = "Open Lancewood config in editor";
const string exit = "Quit";

string action;

do
{
    AnsiConsole.Clear();
    PrintTitle();
    
    action = AnsiConsole.Prompt(
        new SelectionPrompt<String>()
            .Title("Please select desired action:")
            .AddChoices(new[]
            {
                runAll,
                installPackages,
                openWebLinks,
                symlinkDotfiles,
                executeArbitraryCommands,
                editConfig,
                exit
            })
    );

    switch (action)
    {
        case installPackages:
            lancewood.InstallPackages();
            var shouldContinue = AnsiConsole.Confirm("Would you like to continue bootstrapping?");
            if (!shouldContinue)
                action = exit;
            break;
        default:
            break;
    }
} while (!action.Equals(exit));

void PrintTitle()
{
    AnsiConsole.Write(
        new FigletText("Lancewood")
            .Centered()
            .Color(Color.Aqua));

    AnsiConsole.Write(
        new Markup("[dim]Your best friend for bootstrapping your new OS. Fast.[/]")
            .Centered()
    );
    AnsiConsole.WriteLine();
    AnsiConsole.Write(new Rule().RuleStyle("grey").LeftAligned());
}