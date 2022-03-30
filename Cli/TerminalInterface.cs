﻿using System.Diagnostics;
using Lancewood;
using Spectre.Console;

namespace Cli;

public class TerminalInterface
{
    const string RunAll = "Run all bootstrapping actions";
    const string InstallPackages = "Install packages";
    const string OpenWebLinks = "Open web links";
    const string SymlinkDotfiles = "Symlink dotfiles to OS";
    const string ExecuteArbitraryCommands = "Execute arbitrary commands";
    const string EditConfig = "Open Lancewood config in editor";
    const string Exit = "Quit";

    private readonly Engine _engine;

    public TerminalInterface(string[] args)
    {
        if (!args.Any())
        {
            AnsiConsole.MarkupLine("[red]Please provide a path to your Lancewood config file.[/]");
            Environment.Exit(1);
        }

        var configPath = args.First();
        _engine = new Engine(configPath, Console.Out);
    }

    public async Task Run()
    {
        string action;

        do
        {
            PrintTitle();

            action = AnsiConsole.Prompt(
                new SelectionPrompt<String>()
                    .Title("Please select desired action:")
                    .AddChoices(new[]
                    {
                        RunAll,
                        InstallPackages,
                        OpenWebLinks,
                        SymlinkDotfiles,
                        ExecuteArbitraryCommands,
                        EditConfig,
                        Exit
                    })
            );

            switch (action)
            {
                case RunAll:
                    AnsiConsole.MarkupLine("Not yet implemented");
                    if (!ShouldContinue())
                        action = Exit;
                    break;
                case InstallPackages:
                    await _engine.InstallPackages();
                    if (!ShouldContinue())
                        action = Exit;
                    break;
                case OpenWebLinks:
                    await _engine.OpenWebLinks();
                    if (!ShouldContinue())
                        action = Exit;
                    break;
                case SymlinkDotfiles:
                    await _engine.SymlinkFiles();
                    if (!ShouldContinue())
                        action = Exit;
                    break;
                case ExecuteArbitraryCommands:
                    await _engine.RunArbitraryCommands();
                    if (!ShouldContinue())
                        action = Exit;
                    break;
                case EditConfig:
                    await _engine.EditConfig();
                    if (!ShouldContinue())
                        action = Exit;
                    break;
                default:
                    AnsiConsole.MarkupLine("[red]Action not supported.[/]");
                    break;
            }
        } while (!action.Equals(Exit));
    }

    private void PrintTitle()
    {
        AnsiConsole.Clear();
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

    private bool ShouldContinue()
    {
        return AnsiConsole.Confirm("Would you like to continue bootstrapping?");
    }
}