namespace PinaryDevelopment.Git.Server.Services
{
    using Microsoft.Extensions.Logging;
    using Contracts;
    using DataAccess.Contracts;
    using static DataAccess.Contracts.GitResponseDto;
    using static Contracts.GitResponse;

    public class RepositoriesService : IRepositoriesService
    {
        private readonly ILogger<RepositoriesService> _logger;
        private readonly IRepositoriesDal _repositoriesDal;

        public RepositoriesService(IRepositoriesDal repositoriesDal, ILogger<RepositoriesService> logger)
        {
            _logger = logger;
            _repositoriesDal = repositoriesDal;
        }

        public GitResponse CreateRepository(Guid clientId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var gitResponseDto = _repositoriesDal.CreateRepository(clientId, cancellationToken);

            GitResponse LogGitErrorAndReturn()
            {
                _logger.LogError(gitResponseDto.Message);
                return new GitErrorResponse(gitResponseDto.Message);
            }

            GitResponse LogGitWarningAndReturn()
            {
                _logger.LogWarning(gitResponseDto.Message);
                return new GitSuccessResponse(gitResponseDto.Message);
            }

            return gitResponseDto switch
            {
                GitSuccessResponseDto
                    => new GitSuccessResponse(gitResponseDto.Message),
                GitWarningResponseDto
                    => LogGitWarningAndReturn(),
                GitErrorResponseDto or _
                    => LogGitErrorAndReturn(),
            };
        }
    }
}
