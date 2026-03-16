using BitbucketCloudToGitHub.Cli.Commands;
using FluentAssertions;
using GitHub.Migration.Cli;

namespace BitbucketCloudToGitHub.Tests.bbc2gh.Cli.Commands;

public class CliCommandsTests
{
    private readonly string _newLine = Environment.NewLine;
    private readonly string _separator = $"{Environment.NewLine}  ";

    [Fact]
    public void Should_Return_Command_For_BbcExporter_Export()
    {
        // Arrange
        var command = GetBbcExporterExportCommand();

        const string expectedResult = """
                                      gh bbc-exporter export
                                        --bbc-api-url bbc-api-url
                                        --access-token access-token
                                        --api-token api-token
                                        --email email
                                        --user user
                                        --app-password app-password
                                        --workspace workspace
                                        --repo repo
                                        --temp-dir temp-dir
                                        --output output
                                        --open-prs-only
                                        --prs-from-date prs-from-date
                                        --skip-commit-lookup
                                        --debug
                                      """;

        // Act
        var result = command.ToCommandString(separator: _separator).ReplaceLineEndings(_newLine);

        // Assert
        result.Should().Be(expectedResult.ReplaceLineEndings(_newLine));
    }

    [Fact]
    public void Should_Return_Redacted_Command_For_BbcExporter_Export()
    {
        // Arrange
        var command = GetBbcExporterExportCommand();

        const string expectedResult = """
                                      gh bbc-exporter export
                                        --bbc-api-url bbc-api-url
                                        --access-token ***
                                        --api-token ***
                                        --email email
                                        --user user
                                        --app-password ***
                                        --workspace workspace
                                        --repo repo
                                        --temp-dir temp-dir
                                        --output output
                                        --open-prs-only
                                        --prs-from-date prs-from-date
                                        --skip-commit-lookup
                                        --debug
                                      """;

        // Act
        var result = command.ToCommandString(redacted: true, separator: _separator).ReplaceLineEndings(_newLine);

        // Assert
        result.Should().Be(expectedResult.ReplaceLineEndings(_newLine));
    }

    [Fact]
    public void Should_Return_Command_For_Gh_Extension_Install()
    {
        // Arrange
        var command = new GhExtensionInstallCommand
        {
            Repository = "gh-migration-tools/gh-bbc2gh",
            Force = true,
            Pin = "v1.0.0"
        };

        const string expectedResult = """
                                      gh extension install
                                        gh-migration-tools/gh-bbc2gh
                                        --force
                                        --pin v1.0.0
                                      """;

        // Act
        var result = command.ToCommandString(separator: _separator).ReplaceLineEndings(_newLine);

        // Assert
        result.Should().Be(expectedResult.ReplaceLineEndings(_newLine));
    }

    private static BbcExporterExportCommand GetBbcExporterExportCommand() =>
        new()
        {
            BbcApiUrl = "bbc-api-url",
            AccessToken = "access-token",
            ApiToken = "api-token",
            Email = "email",
            User = "user",
            AppPassword = "app-password",
            Workspace = "workspace",
            Repo = "repo",
            TempDir = "temp-dir",
            Output = "output",
            OpenPrsOnly = true,
            PrsFromDate = "prs-from-date",
            SkipCommitLookup = true,
            Debug = true
        };
}
