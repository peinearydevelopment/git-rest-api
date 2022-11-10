namespace PinaryDevelopment.Git.Server.DataAccess.Contracts
{
    public class GitMessageIndicators
    {
        public string[] SuccessfulMessageIndicators { get; private set; }
        public string[] NoOpMessageIndicators { get; private set; }
        public string[] ErroredMessageIndicators { get; private set; }

        public GitMessageIndicators(string[]? successfulMessageIndicators = null, string[]? noOpMessageIndicators = null, string[]? erroredMessageIndicators = null)
        {
            SuccessfulMessageIndicators = successfulMessageIndicators ?? Array.Empty<string>();
            NoOpMessageIndicators = noOpMessageIndicators ?? Array.Empty<string>();
            ErroredMessageIndicators = erroredMessageIndicators ?? Array.Empty<string>();
        }
    }
}
