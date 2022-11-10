namespace PinaryDevelopment.Git.Server.DataAccess.Contracts
{
    public interface IGitProcess : IProcess
    {
        GitResponseDto InvokeCommand(string commandText, GitMessageIndicators messageIndicators);
    }
}
