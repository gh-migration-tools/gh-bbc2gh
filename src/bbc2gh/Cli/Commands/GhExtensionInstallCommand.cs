using GitHub.Migration.Cli;

namespace BitbucketCloudToGitHub.Cli.Commands;

public sealed class GhExtensionInstallCommand : ICliCommand
{
    public string CliCommand => "gh extension install";

    [CliCommandArgument]
    public required string Repository { get; init; }

    [CliCommandOption("--force")]
    public bool Force { get; set; }

    [CliCommandOption("--pin")]
    public string? Pin { get; set; }
}
