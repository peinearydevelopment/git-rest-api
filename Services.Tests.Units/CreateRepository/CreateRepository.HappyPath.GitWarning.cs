namespace PinaryDevelopment.Git.Server.Services.Tests.Units.CreateRepository
{
    using Moq;
    using DataAccess.Contracts;
    using Microsoft.Extensions.Logging;
    using static Contracts.GitResponse;
    using static DataAccess.Contracts.GitResponseDto;
    using Contracts;

    [TestClass]
    [TestCategory("Units")]
    public class CreateRepositoryHappyPathGitWarning
    {
        private readonly Mock<IRepositoriesDal> MockRepositoriesDal;
        private readonly Mock<ILogger<RepositoriesService>> MockLogger;
        private readonly RepositoriesService RepositoriesService;
        private readonly Guid ClientId = Guid.NewGuid();
        private readonly string Message = "This is just a warning message.";
        private readonly GitResponse GitResponse;

        public CreateRepositoryHappyPathGitWarning()
        {
            MockRepositoriesDal = new Mock<IRepositoriesDal>();
            MockLogger = new Mock<ILogger<RepositoriesService>>();

            MockRepositoriesDal
                .Setup(m => m.CreateRepository(ClientId, It.IsAny<CancellationToken>()))
                .Returns(new GitWarningResponseDto(Message));

            RepositoriesService = new RepositoriesService(MockRepositoriesDal.Object, MockLogger.Object);

            GitResponse = RepositoriesService.CreateRepository(ClientId, CancellationToken.None);
        }

        [TestMethod("1. Expected response type should be returned.")]
        public void ExpectedResponseTypeIsReturned()
        {
            Assert.AreEqual(typeof(GitSuccessResponse), GitResponse.GetType());
        }

        [TestMethod("2. Expected message should be returned.")]
        public void ExpectedMessageIsReturned()
        {
            Assert.AreEqual(Message, GitResponse.Message);
        }

        [TestMethod("3. Create repository should be invoked.")]
        public void CreateRepositoryIsInvoked()
        {
            MockRepositoriesDal.Verify(m => m.CreateRepository(ClientId, It.IsAny<CancellationToken>()), Times.Once());
        }

        [TestMethod("4. Log should be invoked.")]
        public void LogIsInvoked()
        {
            MockLogger.Verify(m => m.Log(LogLevel.Warning, It.IsAny<EventId>(), It.Is<It.IsAnyType>((o, t) => string.Equals(Message, o.ToString(), StringComparison.InvariantCultureIgnoreCase)), It.IsAny<Exception?>(), It.IsAny<Func<It.IsAnyType, Exception?, string>>()), Times.Once());
        }
    }
}
