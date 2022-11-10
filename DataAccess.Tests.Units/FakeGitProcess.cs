namespace PinaryDevelopment.Git.Server.DataAccess.Tests.Units
{
    using Contracts;
    using System.Diagnostics;
    using System.Reflection;

    public class FakeGitProcess : IGitProcess
    {
        private string[] SimulatedErrorDataReceived { get; }
        private string[] SimulatedOutputDataReceived { get; }

        public int ExitCode { get; }

        public event DataReceivedEventHandler? OutputDataReceived;
        public event DataReceivedEventHandler? ErrorDataReceived;

        public void BeginErrorReadLine()
        {
            if (SimulatedErrorDataReceived.Any())
            {
                // https://stackoverflow.com/questions/7582087/how-to-mock-serialdatareceivedeventargs#answer-7583964
                var constructor = typeof(DataReceivedEventArgs).GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, new[] { typeof(string) }, null)!;

                foreach (var simulatedErrorData in SimulatedErrorDataReceived)
                {
                    var eventArgs = (DataReceivedEventArgs)constructor.Invoke(new object[] { simulatedErrorData });
                    ErrorDataReceived?.Invoke(this, eventArgs);
                }
            }
        }

        public void BeginOutputReadLine()
        {
            if (SimulatedOutputDataReceived.Any())
            {
                var constructor = typeof(DataReceivedEventArgs).GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, new[] { typeof(string) }, null)!;

                foreach (var simulatedOutputData in SimulatedOutputDataReceived)
                {
                    var eventArgs = (DataReceivedEventArgs)constructor.Invoke(new object[] { simulatedOutputData });
                    OutputDataReceived?.Invoke(this, eventArgs);
                }
            }
        }

        public bool Start(string commandText) => true;

        public void WaitForExit() {}

        public GitResponseDto InvokeCommand(string commandText, GitMessageIndicators messageIndicators)
        {
            return GitProcessExtensions.InvokeCommand(this, commandText, messageIndicators);
        }

        public FakeGitProcess(string[]? simulatedErrorDataReceived = null, string[]? simulatedOutputDataReceived = null)
        {
            SimulatedErrorDataReceived = simulatedErrorDataReceived ?? Array.Empty<string>();
            SimulatedOutputDataReceived = simulatedOutputDataReceived ?? Array.Empty<string>();
        }
    }
}
