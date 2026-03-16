using OctoshiftCLI.Commands;
using OctoshiftCLI.Commands.MigrateRepo;
using OctoshiftCLI.Extensions;

namespace BitbucketCloudToGitHub.Commands.MigrateRepo;

public sealed class MigrateRepoCommandArgs : MigrateRepoCommandArgsBase
{
    public string? BbcExporterVersion { get; set; }
    public string? BitbucketApiUrl { get; set; }
    [Secret]
    public string? BitbucketAccessToken { get; set; }
    [Secret]
    public string? BitbucketApiToken { get; set; }
    public string? BitbucketEmail { get; set; }
    public string? BitbucketUser { get; set; }
    [Secret]
    public string? BitbucketAppPassword { get; set; }
    public string BitbucketWorkspace { get; set; } = null!;
    public string BitbucketRepo { get; set; } = null!;
    public string BitbucketTempDir { get; set; } = null!;
    public string BitbucketOutput { get; set; } = null!;
    public bool BitbucketOpenPrsOnly { get; set; }
    public string? BitbucketPrsFromDate { get; set; }
    public bool BitbucketSkipCommitLookup { get; set; }
    public bool BitbucketDebug { get; set; }

    public override bool ShouldGenerateArchive() =>
        BitbucketWorkspace.HasValue() && BitbucketRepo.HasValue() && !ArchiveUrl.HasValue();
}
