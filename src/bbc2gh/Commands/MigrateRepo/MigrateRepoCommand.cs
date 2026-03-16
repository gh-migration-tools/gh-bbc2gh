using GitHub.Migration.Cli;
using Microsoft.Extensions.DependencyInjection;
using OctoshiftCLI.Commands;
using OctoshiftCLI.Contracts;
using OctoshiftCLI.Services;
using System.CommandLine;

namespace BitbucketCloudToGitHub.Commands.MigrateRepo;

public sealed class MigrateRepoCommand : CommandBase<MigrateRepoCommandArgs, MigrateRepoCommandHandler>
{
    public MigrateRepoCommand() : base(
        name: "migrate-repo",
        description: "Import a Bitbucket Cloud archive to GitHub." +
                     Environment.NewLine +
                     "Note: Expects GH_PAT env variable or --github-pat option to be set.")
    {
        AddOption(BbcExporterVersion);
        AddOption(BitbucketAccessToken);
        AddOption(BitbucketApiToken);
        AddOption(BitbucketEmail);
        AddOption(BitbucketUser);
        AddOption(BitbucketAppPassword);
        AddOption(BitbucketWorkspace);
        AddOption(BitbucketRepo);
        AddOption(BitbucketOpenPrsOnly);
        AddOption(BitbucketPrsFromDate);
        AddOption(BitbucketSkipCommitLookup);
        AddOption(BitbucketDebug);
        AddOption(GithubPat);
        AddOption(GithubOrg);
        AddOption(GithubRepo);
        AddOption(QueueOnly);
        AddOption(TargetRepoVisibility.FromAmong("public", "private", "internal"));
        AddOption(TargetApiUrl);
        AddOption(Verbose);
        AddOption(ArchiveUrl);
        AddOption(ArchivePath);
        AddOption(KeepArchive);
    }

    public Option<string> BbcExporterVersion { get; } = new(
        name: "--bbc-exporter-version",
        description: "Pin the version of the gh-bbc-exporter extension.");

    public Option<string> BitbucketAccessToken { get; } = new(
        name: "--bitbucket-access-token",
        description: "Bitbucket workspace access token for authentication.");

    public Option<string> BitbucketApiToken { get; } = new(
        name: "--bitbucket-api-token",
        description: "Bitbucket API token for authentication.");

    public Option<string> BitbucketEmail { get; } = new(
        name: "--bitbucket-email",
        description: "Atlassian account email for API token authentication.");

    public Option<string> BitbucketUser { get; } = new(
        name: "--bitbucket-user",
        description: "Bitbucket username for basic authentication.");

    public Option<string> BitbucketAppPassword { get; } = new(
        name: "--bitbucket-app-password",
        description: "Bitbucket app password for basic authentication.");

    public Option<string> BitbucketWorkspace { get; } = new(
        name: "--bitbucket-workspace",
        description: "Bitbucket workspace name.")
    {
        IsRequired = true
    };

    public Option<string> BitbucketRepo { get; } = new(
        name: "--bitbucket-repo",
        description: "Name of the repository to export from Bitbucket Cloud.")
    {
        IsRequired = true
    };

    public Option<bool> BitbucketOpenPrsOnly { get; } = new(
        name: "--bitbucket-open-prs-only",
        description: "Export only open pull requests and ignore closed/merged ones.");

    public Option<string> BitbucketPrsFromDate { get; } = new(
        name: "--bitbucket-prs-from-date",
        description: "Export pull requests created on or after this date (format: YYYY-MM-DD).");

    public Option<bool> BitbucketSkipCommitLookup { get; } = new(
        name: "--bitbucket-skip-commit-lookup",
        description: "Skip Bitbucket API lookups to retrieve commit SHAs (use local lookup only).");

    public Option<bool> BitbucketDebug { get; } = new(
        name: "--bitbucket-debug",
        description: "Enable debug logging.");

    public Option<string> GithubPat { get; } = new(
        name: "--github-pat",
        description: "The GitHub personal access token to be used for the migration. If not set will be read from GH_PAT environment variable.");

    public Option<string> GithubOrg { get; } = new("--github-org")
    {
        IsRequired = true
    };

    public Option<string> GithubRepo { get; } = new("--github-repo")
    {
        IsRequired = true
    };

    public Option<bool> QueueOnly { get; } = new(
        name: "--queue-only",
        description: "Only queues the migration, does not wait for it to finish. Use the wait-for-migration command to subsequently wait for it to finish and view the status.");

    public Option<string> TargetRepoVisibility { get; } = new(
        name: "--target-repo-visibility",
        description: "The visibility of the target repo. Defaults to private. Valid values are public, private, or internal.");

    public Option<string> TargetApiUrl { get; } = new(
        name: "--target-api-url",
        description: "The URL of the target API, if not migrating to github.com. Defaults to https://api.github.com");

    public Option<bool> Verbose { get; } = new("--verbose");

    public Option<string> ArchiveUrl { get; } = new(
        name: "--archive-url",
        description: "URL used to download Bitbucket Cloud migration archive. Only needed if you want to manually retrieve the archive instead of letting this CLI do that for you.");

    public Option<string> ArchivePath { get; } = new(
        name: "--archive-path",
        description: "Path to Bitbucket Cloud migration archive on disk.");

    public Option<bool> KeepArchive { get; } = new(
        name: "--keep-archive",
        description: "Keeps the export archive after successfully uploading it. By default, it will be automatically deleted.");

    public override MigrateRepoCommandHandler BuildHandler(MigrateRepoCommandArgs args, IServiceProvider sp)
    {
        ArgumentNullException.ThrowIfNull(args);
        ArgumentNullException.ThrowIfNull(sp);

        var log = sp.GetRequiredService<OctoLogger>();
        var githubApiFactory = sp.GetRequiredService<ITargetGithubApiFactory>();
        var githubApi = githubApiFactory.Create(args.TargetApiUrl, null, args.GithubPat);
        var environmentVariableProvider = sp.GetRequiredService<EnvironmentVariableProvider>();
        var fileSystemProvider = sp.GetRequiredService<FileSystemProvider>();
        var warningsCountLogger = sp.GetRequiredService<WarningsCountLogger>();
        var cliClient = sp.GetRequiredService<ICliClient>();

        return new MigrateRepoCommandHandler(
            log,
            githubApi,
            environmentVariableProvider,
            fileSystemProvider,
            warningsCountLogger,
            cliClient);
    }
}
