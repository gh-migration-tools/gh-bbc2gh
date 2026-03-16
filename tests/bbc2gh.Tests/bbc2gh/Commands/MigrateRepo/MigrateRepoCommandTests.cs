using BitbucketCloudToGitHub.Commands.MigrateRepo;

namespace BitbucketCloudToGitHub.Tests.bbc2gh.Commands.MigrateRepo;

public class MigrateRepoCommandTests
{
    [Fact]
    public void Should_Have_Options()
    {
        var command = new MigrateRepoCommand();
        Assert.NotNull(command);
        Assert.Equal("migrate-repo", command.Name);
        Assert.Equal(22, command.Options.Count);

        TestHelpers.VerifyCommandOption(command.Options, "bbc-exporter-version", false);
        TestHelpers.VerifyCommandOption(command.Options, "bitbucket-access-token", false);
        TestHelpers.VerifyCommandOption(command.Options, "bitbucket-api-token", false);
        TestHelpers.VerifyCommandOption(command.Options, "bitbucket-email", false);
        TestHelpers.VerifyCommandOption(command.Options, "bitbucket-user", false);
        TestHelpers.VerifyCommandOption(command.Options, "bitbucket-app-password", false);
        TestHelpers.VerifyCommandOption(command.Options, "bitbucket-workspace", true);
        TestHelpers.VerifyCommandOption(command.Options, "bitbucket-repo", true);
        TestHelpers.VerifyCommandOption(command.Options, "bitbucket-open-prs-only", false);
        TestHelpers.VerifyCommandOption(command.Options, "bitbucket-prs-from-date", false);
        TestHelpers.VerifyCommandOption(command.Options, "bitbucket-skip-commit-lookup", false);
        TestHelpers.VerifyCommandOption(command.Options, "bitbucket-debug", false);
        TestHelpers.VerifyCommandOption(command.Options, "github-pat", false);
        TestHelpers.VerifyCommandOption(command.Options, "github-org", true);
        TestHelpers.VerifyCommandOption(command.Options, "github-repo", true);
        TestHelpers.VerifyCommandOption(command.Options, "queue-only", false);
        TestHelpers.VerifyCommandOption(command.Options, "target-repo-visibility", false);
        TestHelpers.VerifyCommandOption(command.Options, "target-api-url", false);
        TestHelpers.VerifyCommandOption(command.Options, "verbose", false);
        TestHelpers.VerifyCommandOption(command.Options, "archive-url", false);
        TestHelpers.VerifyCommandOption(command.Options, "archive-path", false);
        TestHelpers.VerifyCommandOption(command.Options, "keep-archive", false);
    }
}
