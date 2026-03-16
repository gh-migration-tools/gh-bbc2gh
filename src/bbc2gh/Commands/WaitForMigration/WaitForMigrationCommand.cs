using OctoshiftCLI.Commands.WaitForMigration;

namespace BitbucketCloudToGitHub.Commands.WaitForMigration;

public sealed class WaitForMigrationCommand : WaitForMigrationCommandBase
{
    public WaitForMigrationCommand() => AddOptions();
}
