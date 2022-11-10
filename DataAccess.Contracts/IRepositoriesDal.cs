namespace PinaryDevelopment.Git.Server.DataAccess.Contracts
{
    public interface IRepositoriesDal
    {
        GitResponseDto CreateRepository(Guid clientId, CancellationToken cancellationToken);
    }
}
