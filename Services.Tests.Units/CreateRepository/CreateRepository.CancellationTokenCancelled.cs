namespace PinaryDevelopment.Git.Server.Services.Tests.Units.RepositoriesDal
{
    using Moq;
    using DataAccess.Contracts;
    using Microsoft.Extensions.Logging;

    [TestClass]
    [TestCategory("Units")]
    public class CreateRepositoryCancellationTokenCancelled
    {
        private readonly Mock<IRepositoriesDal> MockRepositoriesDal;
        private readonly Mock<ILogger<RepositoriesService>> MockLogger;
        private readonly RepositoriesService RepositoriesService;
        private readonly Exception? Exception;

        public CreateRepositoryCancellationTokenCancelled()
        {
            MockRepositoriesDal = new Mock<IRepositoriesDal>();
            MockLogger = new Mock<ILogger<RepositoriesService>>();

            RepositoriesService = new RepositoriesService(MockRepositoriesDal.Object, MockLogger.Object);
            var cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.Cancel();

            try
            {
                RepositoriesService.CreateRepository(Guid.NewGuid(), cancellationTokenSource.Token);
            }
            catch (Exception ex)
            {
                Exception = ex;
            }
        }

        [TestMethod("1. Operation canceled exception should be thrown.")]
        public void OperationCanceledExceptionShouldBeThrown()
        {
            Assert.AreEqual(typeof(OperationCanceledException), Exception?.GetType());
        }

        [TestMethod("2. Create repository should not be invoked.")]
        public void CreateRepositoryIsNotInvoked()
        {
            MockRepositoriesDal.Verify(m => m.CreateRepository(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Never());
        }

        [TestMethod("3. Log should not be invoked.")]
        public void LogIsNotInvoked()
        {
            MockLogger.Verify(m => m.Log(It.IsAny<LogLevel>(), It.IsAny<EventId>(), It.IsAny<string>(), It.IsAny<Exception?>(), It.IsAny<Func<string, Exception?, string>>()), Times.Never());
        }
    }
}
