namespace PinaryDevelopment.Git.Server.DataAccess.Tests.Units.GitProcess
{
    using DataAccess.Contracts;
    using static DataAccess.Contracts.GitResponseDto;

    [TestClass]
    [TestCategory("Units")]
    public class UnknownMessageErrorData
    {
        private readonly GitResponseDto _responseDto;

        public UnknownMessageErrorData()
        {
            var simulatedErrorDataReceived = new[]
            {
                "wrng: Seems like this is an error",
                "What's going on?",
                "Anyone there?"
            };

            var process = new FakeGitProcess(simulatedErrorDataReceived: simulatedErrorDataReceived);
            _responseDto = process.InvokeCommand("init", new GitMessageIndicators(erroredMessageIndicators: new[] { "This is an error" }));
        }

        [TestMethod("1. Git response is Error Response type.")]
        public void IsOfExpectedResponseType()
        {
            Assert.AreEqual(typeof(GitErrorResponseDto), _responseDto.GetType());
        }

        [TestMethod("2. Git response is marked as unknown.")]
        public void IsMarkedAsUnknown()
        {
            Assert.IsFalse(_responseDto.IsKnownMessage);
        }

        [TestMethod("3. Git response contains message.")]
        public void HasExpectedMessage()
        {
            Assert.AreEqual("wrng: Seems like this is an errorWhat's going on?Anyone there?", _responseDto.Message);
        }
    }
}
