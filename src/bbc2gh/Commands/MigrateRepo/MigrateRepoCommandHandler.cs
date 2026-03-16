using BitbucketCloudToGitHub.Cli.Commands;
using GitHub.Migration.Cli;
using OctoshiftCLI;
using OctoshiftCLI.Commands.MigrateRepo;
using OctoshiftCLI.Extensions;
using OctoshiftCLI.Services;

namespace BitbucketCloudToGitHub.Commands.MigrateRepo;

public sealed class MigrateRepoCommandHandler : MigrateRepoCommandHandlerBase<MigrateRepoCommandArgs>
{
    private readonly OctoLogger _logger;
    private readonly ICliClient _cliClient;

    public MigrateRepoCommandHandler(
        OctoLogger logger,
        GithubApi githubApi,
        EnvironmentVariableProvider environmentVariableProvider,
        FileSystemProvider fileSystemProvider,
        WarningsCountLogger warningsCountLogger,
        ICliClient cliClient)
        : base(logger, githubApi, environmentVariableProvider, fileSystemProvider, warningsCountLogger)
    {
        ArgumentNullException.ThrowIfNull(cliClient);

        _logger = logger;
        _cliClient = cliClient;
    }

    protected override async Task ValidateOptionsAsync(MigrateRepoCommandArgs args)
    {
        ArgumentNullException.ThrowIfNull(args);

        // Validate args
        if (args.ShouldGenerateArchive())
        {
            if (string.IsNullOrWhiteSpace(args.ArchivePath))
            {
                args.ArchivePath = ".";
            }

            Directory.CreateDirectory(args.ArchivePath);
        }

        // Validate dependencies
        await InstallBbcExporterAsync(args.BbcExporterVersion).ConfigureAwait(false);
    }

    protected override async Task<string> GenerateArchiveAsync(MigrateRepoCommandArgs args)
    {
        ArgumentNullException.ThrowIfNull(args);

        if (!string.IsNullOrWhiteSpace(args.BitbucketOutput))
        {
            args.ArchivePath = args.BitbucketOutput;
        }

        var command = new BbcExporterExportCommand
        {
            BbcApiUrl = args.BitbucketApiUrl,
            AccessToken = args.BitbucketAccessToken,
            ApiToken = args.BitbucketApiToken,
            Email = args.BitbucketEmail,
            User = args.BitbucketUser,
            AppPassword = args.BitbucketAppPassword,
            Workspace = args.BitbucketWorkspace,
            Repo = args.BitbucketRepo,
            TempDir = args.BitbucketTempDir,
            Output = args.BitbucketOutput,
            OpenPrsOnly = args.BitbucketOpenPrsOnly,
            PrsFromDate = args.BitbucketPrsFromDate,
            SkipCommitLookup = args.BitbucketSkipCommitLookup,
            Debug = args.BitbucketDebug
        };

        _logger.LogInformation("Export started.");

        var exitCode = await _cliClient
            .RunCommandAsync(
                command,
                args.ArchivePath)
            .ConfigureAwait(false);

        if (exitCode != 0)
        {
            throw new OctoshiftCliException("GitLab export failed");
        }

        var archiveFilePath = GetArchiveFilePath(args.ArchivePath!);
        _logger.LogInformation($"Export completed. Your migration archive should be ready at {archiveFilePath}");

        return archiveFilePath;
    }

    private static string GetArchiveFilePath(string archivePath) =>
        Directory
            .GetFiles(archivePath, "bitbucket-export-*.tar.gz")
            .OrderDescending()
            .First();

    protected override string GetRepoUrl(MigrateRepoCommandArgs args)
    {
        ArgumentNullException.ThrowIfNull(args);

        return args.BitbucketWorkspace.HasValue() && args.BitbucketRepo.HasValue()
            ? $"https://github.com/{args.BitbucketWorkspace.EscapeDataString()}/{args.BitbucketRepo.EscapeDataString()}"
            : "https://not-used";
    }

    private async Task InstallBbcExporterAsync(string? version)
    {
        const string repository = "katiem0/gh-bbc-exporter";
        var command = new GhExtensionInstallCommand { Repository = repository };

        if (!string.IsNullOrWhiteSpace(version))
        {
            command.Pin = version;
        }
        else
        {
            command.Force = true;
        }

        var exitCode = await _cliClient
            .RunCommandAsync(command)
            .ConfigureAwait(false);

        if (exitCode != 0)
        {
            throw new OctoshiftCliException($"The installation of the {repository} extension failed.");
        }
    }
}
