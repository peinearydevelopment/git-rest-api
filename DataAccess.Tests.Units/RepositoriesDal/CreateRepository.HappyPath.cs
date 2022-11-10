namespace PinaryDevelopment.Git.Server.DataAccess.Tests.Units.RepositoriesDal
{
    using Microsoft.Extensions.Options;
    using Moq;
    using DataAccess.Contracts;
    using DataAccess;

    [TestClass]
    [TestCategory("Units")]
    public class CreateRepositoryHappyPath
    {
        private readonly Mock<IGitProcessFactory> MockGitProcessFactory;
        private readonly Mock<IDirectoryHelper> MockDirectoryHelper;
        private readonly Mock<IGitProcess> MockGitProcess;
        private readonly RepositoriesDal RepositoriesDal;
        private readonly Exception? Exception = null;
        private readonly Guid ClientId = Guid.NewGuid();
        private readonly string DefaultBranchName = "Default";

        public CreateRepositoryHappyPath()
        {
            MockGitProcessFactory = new Mock<IGitProcessFactory>();
            MockDirectoryHelper = new Mock<IDirectoryHelper>();
            MockGitProcess = new Mock<IGitProcess>();
            var mockGitConfigurationOptions = new Mock<IOptions<GitConfigurationOptions>>();
            mockGitConfigurationOptions.SetupGet(m => m.Value).Returns(new GitConfigurationOptions { DefaultBranchName = DefaultBranchName });

            MockGitProcessFactory.Setup(m => m.GetProcess($"/repos/{ClientId}")).Returns(MockGitProcess.Object);

            RepositoriesDal = new RepositoriesDal(MockGitProcessFactory.Object, MockDirectoryHelper.Object, mockGitConfigurationOptions.Object);

            try
            {
                RepositoriesDal.CreateRepository(ClientId, CancellationToken.None);
            }
            catch (Exception ex)
            {
                Exception = ex;
            }
        }

        [TestMethod("1. Exception should not be thrown.")]
        public void ExceptionShouldNotBeThrown()
        {
            Assert.IsNull(Exception);
        }

        [TestMethod("2. Get process should be invoked.")]
        public void GetProcessIsInvoked()
        {
            MockGitProcessFactory.Verify(m => m.GetProcess($"/repos/{ClientId}"), Times.Once());
        }

        [TestMethod("3. Create directory should not be invoked.")]
        public void CreateDirectoryIsNotInvoked()
        {
            MockDirectoryHelper.Verify(m => m.CreateDirectory($"/repos/{ClientId}"), Times.Once());
        }

        [TestMethod("4. Invoke comamnd should not be invoked.")]
        public void InvokeCommandIsNotInvoked()
        {
            MockGitProcess.Verify(m => m.InvokeCommand($"init --initial-branch={DefaultBranchName}", It.IsAny<GitMessageIndicators>()), Times.Once());
        }
    }
}
