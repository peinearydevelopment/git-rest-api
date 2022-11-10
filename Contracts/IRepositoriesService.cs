namespace PinaryDevelopment.Git.Server.Contracts
{
    public interface IRepositoriesService
    {
        GitResponse CreateRepository(Guid clientId, CancellationToken cancellationToken);
    }
}
