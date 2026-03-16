using OctoshiftCLI.Commands.DownloadLogs;

namespace BitbucketCloudToGitHub.Commands.DownloadLogs;

public sealed class DownloadLogsCommand : DownloadLogsCommandBase
{
    public DownloadLogsCommand() => AddOptions();
}
