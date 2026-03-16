using OctoshiftCLI.Services;

namespace BitbucketCloudToGitHub.Services;

public sealed class VersionChecker : VersionCheckerBase
{
    protected override string ProductName => "bbc2gh";
    protected override string LatestVersionFileUrl => "https://raw.githubusercontent.com/gh-migration-tools/gh-bbc2gh/main/LATEST-VERSION.txt";

    public VersionChecker(HttpClient httpClient, OctoLogger logger)
        : base(httpClient, logger)
    {
    }
}
