namespace PinaryDevelopment.Git.Server.DataAccess.Tests.Units.RepositoriesDal
{
    using Microsoft.Extensions.Options;
    using Moq;
    using DataAccess.Contracts;
    using DataAccess;

    [TestClass]
    [TestCategory("Units")]
    public class CreateRepositoryCancellationTokenDisposed
    {
        private readonly Mock<IGitProcessFactory> MockGitProcessFactory;
        private readonly Mock<IDirectoryHelper> MockDirectoryHelper;
        private readonly Mock<IGitProcess> MockGitProcess;
        private readonly RepositoriesDal RepositoriesDal;
        private readonly Exception? Exception;

        public CreateRepositoryCancellationTokenDisposed()
        {
            MockGitProcessFactory = new Mock<IGitProcessFactory>();
            MockDirectoryHelper = new Mock<IDirectoryHelper>();
            MockGitProcess = new Mock<IGitProcess>();
            var mockGitConfigurationOptions = new Mock<IOptions<GitConfigurationOptions>>();

            MockGitProcessFactory.Setup(m => m.GetProcess(It.IsAny<string>())).Returns(MockGitProcess.Object);

            RepositoriesDal = new RepositoriesDal(MockGitProcessFactory.Object, MockDirectoryHelper.Object, mockGitConfigurationOptions.Object);
            var cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.Dispose();

            try
            {
                RepositoriesDal.CreateRepository(Guid.NewGuid(), cancellationTokenSource.Token);
            }
            catch (Exception ex)
            {
                Exception = ex;
            }
        }

        [TestMethod("1. Object disposed exception should be thrown.")]
        public void ObjectDisposedExceptionShouldBeThrown()
        {
            Assert.AreEqual(typeof(ObjectDisposedException), Exception?.GetType());
        }

        [TestMethod("2. Get process should not be invoked.")]
        public void GetProcessIsNotInvoked()
        {
            MockGitProcessFactory.Verify(m => m.GetProcess(It.IsAny<string>()), Times.Never());
        }

        [TestMethod("3. Create directory should not be invoked.")]
        public void CreateDirectoryIsNotInvoked()
        {
            MockDirectoryHelper.Verify(m => m.CreateDirectory(It.IsAny<string>()), Times.Never());
        }

        [TestMethod("4. Invoke comamnd should not be invoked.")]
        public void InvokeCommandIsNotInvoked()
        {
            MockGitProcess.Verify(m => m.InvokeCommand(It.IsAny<string>(), It.IsAny<GitMessageIndicators>()), Times.Never());
        }
    }
}
