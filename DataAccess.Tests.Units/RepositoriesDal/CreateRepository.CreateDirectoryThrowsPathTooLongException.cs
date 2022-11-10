namespace PinaryDevelopment.Git.Server.DataAccess.Tests.Units.RepositoriesDal
{
    [TestClass]
    [TestCategory("Units")]
    public class CreateRepositoryCreateDirectoryThrowsPathTooLongException : CreateRepositoryBaseCreateDirectoryThrowsException<PathTooLongException>
    {
    }
}
