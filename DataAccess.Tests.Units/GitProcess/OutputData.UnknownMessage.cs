namespace PinaryDevelopment.Git.Server.Tests.Units.GitProcess
{
    using DataAccess.Contracts;
    using DataAccess.Tests.Units;
    using static DataAccess.Contracts.GitResponseDto;

    [TestClass]
    [TestCategory("Units")]
    public class UnknownMessageOutputData
    {
        private readonly GitResponseDto _responseDto;

        public UnknownMessageOutputData()
        {
            var simulatedOutputDataReceived = new[]
            {
                "This is not an error",
                "Nothing to see here",
                "Move on"
            };

            var process = new FakeGitProcess(simulatedOutputDataReceived: simulatedOutputDataReceived);
            _responseDto = process.InvokeCommand("init", new GitMessageIndicators());
        }

        [TestMethod("1. Git response is Success Response type.")]
        public void IsOfExpectedResponseType()
        {
            Assert.AreEqual(typeof(GitSuccessResponseDto), _responseDto.GetType());
        }

        [TestMethod("2. Git response is marked as unknown.")]
        public void IsMarkedAsUnknown()
        {
            Assert.IsFalse(_responseDto.IsKnownMessage);
        }

        [TestMethod("3. Git response contains message.")]
        public void HasExpectedMessage()
        {
            Assert.AreEqual("This is not an errorNothing to see hereMove on", _responseDto.Message);
        }
    }
}
