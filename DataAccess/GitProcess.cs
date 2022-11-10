namespace PinaryDevelopment.Git.Server.DataAccess
{
    using Contracts;
    using System.Diagnostics;

    public class GitProcess : IGitProcess
    {
        private readonly Process Process;

        public GitProcess(string repositoryDirectory)
        {
            Process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "git",
                    RedirectStandardInput = true,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    WorkingDirectory = repositoryDirectory
                },
                EnableRaisingEvents = true
            };
        }

        public int ExitCode { get => Process.ExitCode; }

        public event DataReceivedEventHandler? OutputDataReceived
        {
            add => Process.OutputDataReceived += value;
            remove => Process.OutputDataReceived -= value;
        }

        public event DataReceivedEventHandler? ErrorDataReceived
        {
            add => Process.ErrorDataReceived += value;
            remove => Process.ErrorDataReceived -= value;
        }

        public void BeginErrorReadLine()
        {
            Process.BeginErrorReadLine();
        }

        public void BeginOutputReadLine()
        {
            Process.BeginOutputReadLine();
        }

        public GitResponseDto InvokeCommand(string commandText, GitMessageIndicators messageIndicators)
        {
            return GitProcessExtensions.InvokeCommand(this, commandText, messageIndicators);
        }

        public bool Start(string commandText)
        {
            Process.StartInfo.Arguments = $"{commandText}";

            return Process.Start();
        }

        public void WaitForExit()
        {
            Process.WaitForExit();
        }
    }
}
