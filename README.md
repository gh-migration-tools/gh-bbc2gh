# gh-bbc2gh

[![License](https://img.shields.io/badge/license-MIT-blue.svg)](https://github.com/gh-migration-tools/gh-bbc2gh/blob/main/LICENSE)
[![CI](https://github.com/gh-migration-tools/gh-bbc2gh/actions/workflows/CI.yml/badge.svg)](https://github.com/gh-migration-tools/gh-bbc2gh/actions/workflows/CI.yml)

**gh-bbc2gh** is a GitHub CLI extension for Bitbucket Cloud to GitHub migrations. This extension uses under the hood the [`gh-bbc-exporter`](https://github.com/katiem0/gh-bbc-exporter) by [@katiem0](https://github.com/katiem0/) to create an Bitbucket Cloud export archive.

By using the [Octoshift](https://github.com/gh-migration-tools/Octoshift) library this extension is build upon the same codebase and provides the same set of commands as the `gh-gei`, `gh-ado2gh` or `gh-bbs2gh` extensions.

## Setup

### Install extension

```
gh extension install gh-migration-tools/gh-bbc2gh
```

### Update extension

```
gh extension upgrade gh-migration-tools/gh-bbc2gh
```

### Skipping version checks

When the CLI is launched, it logs if a newer version of the CLI is available. You can skip this check by setting the `GEI_SKIP_VERSION_CHECK` environment variable to `true`.

```powershell
# Windows PowerShell
$env:GEI_SKIP_VERSION_CHECK = "true"
```

```bash
# macOS/Linux
export GEI_SKIP_VERSION_CHECK=true
```

### Skipping GitHub status checks

When the CLI is launched, it logs a warning if there are any ongoing [GitHub incidents](https://www.githubstatus.com/) that might affect your use of the CLI. You can skip this check by setting the `GEI_SKIP_STATUS_CHECK` environment variable to `true`.

```powershell
# Windows PowerShell
$env:GEI_SKIP_STATUS_CHECK = "true"
```

```bash
# macOS/Linux
export GEI_SKIP_STATUS_CHECK=true
```

### Configuring multipart upload chunk size

Set the `GITHUB_OWNED_STORAGE_MULTIPART_MEBIBYTES` environment variable to change the archive upload part size. Provide the value in mebibytes (MiB); For example:

```powershell
# Windows PowerShell
$env:GITHUB_OWNED_STORAGE_MULTIPART_MEBIBYTES = "10"
```

```bash
# macOS/Linux
export GITHUB_OWNED_STORAGE_MULTIPART_MEBIBYTES=10
```

This sets the chunk size to 10 MiB (10,485,760 bytes). The minimum supported value is 5 MiB, and the default remains 100 MiB.

This might be needed to improve upload reliability in environments with proxies or very slow connections.

## Usage

To get an overview of all commands run:

```
gh bbc2gh --help
```

### Migrate repo

To get an overview of all options for the Bitbucket Cloud to GitHub migration run:

```
gh bbc2gh migrate-repo --help
```

```
Description:
  Import a Bitbucket Cloud archive to GitHub.
  Note: Expects GH_PAT env variable or --github-pat option to be set.

Usage:
  gh-bbc2gh migrate-repo [options]

Options:
  --bbc-exporter-version <bbc-exporter-version>           Pin the version of the gh-bbc-exporter extension.
  --bitbucket-access-token <bitbucket-access-token>       Bitbucket workspace access token for authentication.
  --bitbucket-api-token <bitbucket-api-token>             Bitbucket API token for authentication.
  --bitbucket-email <bitbucket-email>                     Atlassian account email for API token authentication.
  --bitbucket-user <bitbucket-user>                       Bitbucket username for basic authentication.
  --bitbucket-app-password <bitbucket-app-password>       Bitbucket app password for basic authentication.
  --bitbucket-workspace <bitbucket-workspace> (REQUIRED)  Bitbucket workspace name.
  --bitbucket-repo <bitbucket-repo> (REQUIRED)            Name of the repository to export from Bitbucket Cloud.
  --bitbucket-open-prs-only                               Export only open pull requests and ignore closed/merged ones.
  --bitbucket-prs-from-date <bitbucket-prs-from-date>     Export pull requests created on or after this date (format: YYYY-MM-DD).
  --bitbucket-skip-commit-lookup                          Skip Bitbucket API lookups to retrieve commit SHAs (use local lookup only).
  --bitbucket-debug                                       Enable debug logging.
  --github-pat <github-pat>                               The GitHub personal access token to be used for the migration. If not set will be read from GH_PAT environment variable.
  --github-org <github-org> (REQUIRED)
  --github-repo <github-repo> (REQUIRED)
  --queue-only                                            Only queues the migration, does not wait for it to finish. Use the wait-for-migration command to subsequently wait for it to finish and view the
                                                          status.
  --target-repo-visibility <internal|private|public>      The visibility of the target repo. Defaults to private. Valid values are public, private, or internal.
  --target-api-url <target-api-url>                       The URL of the target API, if not migrating to github.com. Defaults to https://api.github.com
  --verbose
  --archive-url <archive-url>                             URL used to download Bitbucket Cloud migration archive. Only needed if you want to manually retrieve the archive instead of letting this CLI do that
                                                          for you.
  --archive-path <archive-path>                           Path to Bitbucket Cloud migration archive on disk.
  --keep-archive                                          Keeps the export archive after successfully uploading it. By default, it will be automatically deleted.
  -?, -h, --help                                          Show help and usage information
```

All options with the `--bitbucket` prefix are passed down to the `gh-bbc-exporter` extension.

```
gh bbc2gh migrate-repo
  --bitbucket-workspace BBC_WORKSPACE
  --bitbucket-repo BBC_REPO
  --bitbucket-access-token BBC_TOKEN
  --github-org GH_ORG
  --github-repo GH_REPO
  --github-pat GH_PAT
  --verbose
```
