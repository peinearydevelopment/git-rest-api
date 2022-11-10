namespace PinaryDevelopment.Git.Server.DataAccess
{
    using Contracts;
    using Microsoft.Extensions.Options;

    public class RepositoriesDal : IRepositoriesDal
    {
        private const string RepositoryBaseDirectory = "/repos";
        private static string RepositoryDirectory(Guid clientId) => $"{RepositoryBaseDirectory}/{clientId}";
        private readonly IGitProcessFactory GitProcessFactory;
        private readonly IDirectoryHelper DirectoryHelper;
        private readonly GitConfigurationOptions GitConfigurationOptions;

        public RepositoriesDal(IGitProcessFactory gitProcessFactory, IDirectoryHelper directoryHelper, IOptions<GitConfigurationOptions> gitConfigurationOptions)
        {
            GitProcessFactory = gitProcessFactory;
            DirectoryHelper = directoryHelper;
            GitConfigurationOptions = gitConfigurationOptions.Value;
        }

        public GitResponseDto CreateRepository(Guid clientId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var repositoryDirectory = RepositoryDirectory(clientId);
            DirectoryHelper.CreateDirectory(repositoryDirectory);

            var messageIndicators = new GitMessageIndicators(
                successfulMessageIndicators: new[] { "Initialized empty Git repository in" },
                noOpMessageIndicators: new[] { "re-init: ignored" },
                erroredMessageIndicators: new[] { "unknown option" }
            );

            var process = GitProcessFactory.GetProcess(repositoryDirectory);

            return process.InvokeCommand($"init --initial-branch={GitConfigurationOptions.DefaultBranchName}", messageIndicators);
        }
    }
}
