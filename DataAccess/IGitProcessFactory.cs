namespace PinaryDevelopment.Git.Server.DataAccess
{
    using Contracts;

    public interface IGitProcessFactory
    {
        IGitProcess GetProcess(string repositoryDirectory);
    }
}
