namespace PinaryDevelopment.Git.Server.DataAccess.Tests.Units.RepositoriesDal
{
    using Microsoft.Extensions.Options;
    using Moq;
    using DataAccess.Contracts;
    using DataAccess;

    public class CreateRepositoryBaseCreateDirectoryThrowsException<T> where T : Exception, new()
    {
        private readonly Mock<IGitProcessFactory> MockGitProcessFactory;
        private readonly Mock<IDirectoryHelper> MockDirectoryHelper;
        private readonly Mock<IGitProcess> MockGitProcess;
        private readonly RepositoriesDal RepositoriesDal;
        private readonly Exception? Exception;

        public CreateRepositoryBaseCreateDirectoryThrowsException()
        {
            MockGitProcessFactory = new Mock<IGitProcessFactory>();
            MockDirectoryHelper = new Mock<IDirectoryHelper>();
            MockGitProcess = new Mock<IGitProcess>();
            var mockGitConfigurationOptions = new Mock<IOptions<GitConfigurationOptions>>();

            MockGitProcessFactory.Setup(m => m.GetProcess(It.IsAny<string>())).Returns(MockGitProcess.Object);

            MockDirectoryHelper.Setup(m => m.CreateDirectory(It.IsAny<string>())).Throws<T>();

            RepositoriesDal = new RepositoriesDal(MockGitProcessFactory.Object, MockDirectoryHelper.Object, mockGitConfigurationOptions.Object);

            try
            {
                RepositoriesDal.CreateRepository(Guid.NewGuid(), CancellationToken.None);
            }
            catch (Exception ex)
            {
                Exception = ex;
            }
        }

        [TestMethod("1. Expected exception should be thrown.")]
        public void ExceptionShouldBeThrown()
        {
            Assert.AreEqual(typeof(T), Exception?.GetType());
        }

        [TestMethod("2. Get process should not be invoked.")]
        public void GetProcessIsNotInvoked()
        {
            MockGitProcessFactory.Verify(m => m.GetProcess(It.IsAny<string>()), Times.Never());
        }

        [TestMethod("3. Create directory should be invoked.")]
        public void CreateDirectoryIsInvoked()
        {
            MockDirectoryHelper.Verify(m => m.CreateDirectory(It.IsAny<string>()), Times.Once());
        }

        [TestMethod("4. Invoke comamnd should not be invoked.")]
        public void InvokeCommandIsNotInvoked()
        {
            MockGitProcess.Verify(m => m.InvokeCommand(It.IsAny<string>(), It.IsAny<GitMessageIndicators>()), Times.Never());
        }
    }
}
