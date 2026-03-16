using OctoshiftCLI.Commands.AbortMigration;

namespace BitbucketCloudToGitHub.Commands.AbortMigration;

public sealed class AbortMigrationCommand : AbortMigrationCommandBase
{
    public AbortMigrationCommand() => AddOptions();
}
