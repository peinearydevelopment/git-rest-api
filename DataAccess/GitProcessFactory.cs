namespace PinaryDevelopment.Git.Server.DataAccess
{
    using Contracts;

    public class GitProcessFactory : IGitProcessFactory
    {
        public IGitProcess GetProcess(string repositoryDirectory)
        {
            return new GitProcess(repositoryDirectory);
        }
    }
}
