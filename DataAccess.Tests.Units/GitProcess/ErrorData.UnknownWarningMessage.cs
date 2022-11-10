namespace PinaryDevelopment.Git.Server.DataAccess.Tests.Units.GitProcess
{
    using DataAccess.Contracts;
    using static DataAccess.Contracts.GitResponseDto;

    [TestClass]
    [TestCategory("Units")]
    public class UnknownWarningMessageErrorData
    {
        private readonly GitResponseDto _responseDto;

        public UnknownWarningMessageErrorData()
        {
            var simulatedErrorDataReceived = new[]
            {
                "warning: This is a warning",
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

        [TestMethod("2. Git response is marked as unknown.")]
        public void IsMarkedAsUnknown()
        {
            Assert.IsFalse(_responseDto.IsKnownMessage);
        }

        [TestMethod("3. Git response contains message.")]
        public void HasExpectedMessage()
        {
            Assert.AreEqual("This is a warningIs it helpful?", _responseDto.Message);
        }
    }
}
