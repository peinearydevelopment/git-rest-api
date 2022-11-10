namespace PinaryDevelopment.Git.Server.DataAccess.Contracts
{
    using System.Diagnostics;

    public interface IProcess
    {
        event DataReceivedEventHandler? OutputDataReceived;
        event DataReceivedEventHandler? ErrorDataReceived;
        bool Start(string commandText);
        void BeginErrorReadLine();
        void BeginOutputReadLine();
        void WaitForExit();
        int ExitCode { get; }
    }
}
