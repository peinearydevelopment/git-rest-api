namespace PinaryDevelopment.Git.Server.Tests.Units.GitProcess
{
    using DataAccess.Contracts;
    using DataAccess.Tests.Units;
    using static DataAccess.Contracts.GitResponseDto;

    [TestClass]
    [TestCategory("Units")]
    public class KnownMessageOutputData
    {
        private readonly GitResponseDto _responseDto;

        public KnownMessageOutputData()
        {
            var simulatedOutputDataReceived = new[]
            {
                "success: This is not an error",
                "Nothing to see here",
                "Move on"
            };

            var process = new FakeGitProcess(simulatedOutputDataReceived: simulatedOutputDataReceived);
            _responseDto = process.InvokeCommand("init", new GitMessageIndicators(successfulMessageIndicators: new[] { "Nothing to see here" } ));
        }

        [TestMethod("1. Git response is Success Response type.")]
        public void IsOfExpectedResponseType()
        {
            Assert.AreEqual(typeof(GitSuccessResponseDto), _responseDto.GetType());
        }

        [TestMethod("2. Git response is marked as known.")]
        public void IsMarkedAsKnown()
        {
            Assert.IsTrue(_responseDto.IsKnownMessage);
        }

        [TestMethod("3. Git response contains message.")]
        public void HasExpectedMessage()
        {
            Assert.AreEqual("Nothing to see hereMove on", _responseDto.Message);
        }
    }
}
