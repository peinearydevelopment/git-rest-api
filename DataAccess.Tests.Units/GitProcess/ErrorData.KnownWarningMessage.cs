namespace PinaryDevelopment.Git.Server.DataAccess.Tests.Units.GitProcess
{
    using DataAccess.Contracts;
    using static DataAccess.Contracts.GitResponseDto;

    [TestClass]
    [TestCategory("Units")]
    public class KnownWarningMessageErrorData
    {
        private readonly GitResponseDto _responseDto;

        public KnownWarningMessageErrorData()
        {
            var simulatedErrorDataReceived = new[]
            {
                "warning: This is only a warning",
                "Is it helpful?",
            };

            var process = new FakeGitProcess(simulatedErrorDataReceived: simulatedErrorDataReceived);
            _responseDto = process.InvokeCommand("init", new GitMessageIndicators(noOpMessageIndicators: new[] { "This is only a warning" }));
        }

        [TestMethod("1. Git response is Warning Response type.")]
        public void IsOfExpectedResponseType()
        {
            Assert.AreEqual(typeof(GitWarningResponseDto), _responseDto.GetType());
        }

        [TestMethod("2. Git response is marked as known.")]
        public void IsMarkedAsKnown()
        {
            Assert.IsTrue(_responseDto.IsKnownMessage);
        }

        [TestMethod("3. Git response contains message.")]
        public void HasExpectedMessage()
        {
            Assert.AreEqual("This is only a warningIs it helpful?", _responseDto.Message);
        }
    }
}
