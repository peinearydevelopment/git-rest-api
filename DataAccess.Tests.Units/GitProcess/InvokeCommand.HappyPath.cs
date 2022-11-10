namespace PinaryDevelopment.Git.Server.DataAccess.Tests.Units.GitProcess
{
    using Moq;
    using DataAccess.Contracts;
    using static DataAccess.Contracts.GitResponseDto;

    [TestClass]
    [TestCategory("Units")]
    public class InvokeCommandHappyPath
    {
        private readonly string[] SuccessfulMessageIndicators = new[] { "" };
        private readonly string[] NoOpMessageIndicators = new[] { "" };
        private readonly string[] ErroredMessageIndicators = new[] { "" };
        private readonly Mock<IGitProcess> MockProcess;
        private readonly GitResponseDto Response;
        private static readonly int ExitCode = new Random().Next(int.MaxValue);
        private static readonly string Command = "init";

        public InvokeCommandHappyPath()
        {
            var indicators = new GitMessageIndicators(SuccessfulMessageIndicators, NoOpMessageIndicators, ErroredMessageIndicators);
            MockProcess = new Mock<IGitProcess>();

            MockProcess.Setup(m => m.InvokeCommand(Command, indicators)).Returns(new GitSuccessResponseDto(null));
            MockProcess.Setup(m => m.Start(Command)).Returns(true);
            MockProcess.Setup(m => m.BeginErrorReadLine());
            MockProcess.Setup(m => m.BeginOutputReadLine());
            MockProcess.Setup(m => m.WaitForExit());
            MockProcess.SetupGet(m => m.ExitCode).Returns(ExitCode);

            Response = GitProcessExtensions.InvokeCommand(MockProcess.Object, Command, indicators);

            /*
             * Doesn't work due to timing issues. `.Raise` seems to invoke the event immediately.
             * In order to test it, it would have to be invoked in the middle of the `InvokeCommand` invocation.
             * Leaving the test class without testing the message as that should be covered in the other tests, i.e. `./OutputData.KnownMessage.cs`
                var constructor = typeof(DataReceivedEventArgs).GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, new[] { typeof(string) }, null)!;
                var eventArgs = (DataReceivedEventArgs)constructor.Invoke(new object[] { "foobar" });
                MockProcess.Raise(e => e.OutputDataReceived += (object sender, DataReceivedEventArgs e) => {}, null, eventArgs);
            */
        }

        [TestMethod("1. Start is invoked only once.")]
        public void InvokesStartMethod()
        {
            MockProcess.Verify(m => m.Start(Command), Times.Once);
        }

        [TestMethod("2. BeginErrorReadLine is invoked only once.")]
        public void InvokesBeginErrorReadLineMethod()
        {
            MockProcess.Verify(m => m.BeginErrorReadLine(), Times.Once);
        }

        [TestMethod("3. BeginOutputReadLine is invoked only once.")]
        public void InvokesBeginOutputReadLineMethod()
        {
            MockProcess.Verify(m => m.BeginOutputReadLine(), Times.Once);
        }

        [TestMethod("4. WaitForExit is invoked only once.")]
        public void InvokesWaitForExitMethod()
        {
            MockProcess.Verify(m => m.WaitForExit(), Times.Once);
        }

        [TestMethod("5. Has expected exit code.")]
        public void HasExpectedExitCode()
        {
            Assert.AreEqual(ExitCode, Response.ExitCode);
        }
    }
}
