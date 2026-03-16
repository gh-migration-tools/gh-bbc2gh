using GitHub.Migration.Cli;

namespace BitbucketCloudToGitHub.Cli.Commands;

public sealed class BbcExporterExportCommand : ICliCommand
{
    public string CliCommand => "gh bbc-exporter export";

    [CliCommandOption("--bbc-api-url")]
    public string? BbcApiUrl { get; set; }

    [CliCommandOption("--access-token", isSecret: true)]
    public string? AccessToken { get; set; }

    [CliCommandOption("--api-token", isSecret: true)]
    public string? ApiToken { get; set; }

    [CliCommandOption("--email")]
    public string? Email { get; set; }

    [CliCommandOption("--user")]
    public string? User { get; set; }

    [CliCommandOption("--app-password", isSecret: true)]
    public string? AppPassword { get; set; }

    [CliCommandOption("--workspace")]
    public string? Workspace { get; set; }

    [CliCommandOption("--repo")]
    public string? Repo { get; set; }

    [CliCommandOption("--temp-dir")]
    public string? TempDir { get; set; }

    [CliCommandOption("--output")]
    public string? Output { get; set; }

    [CliCommandOption("--open-prs-only")]
    public bool OpenPrsOnly { get; set; }

    [CliCommandOption("--prs-from-date")]
    public string? PrsFromDate { get; set; }

    [CliCommandOption("--skip-commit-lookup")]
    public bool SkipCommitLookup { get; set; }

    [CliCommandOption("--debug")]
    public bool Debug { get; set; }
}
