namespace PinaryDevelopment.Git.Server.Contracts
{
    public record GitResponse
    {
        public record GitErrorResponse(string? Message) : GitResponse(Message);

        public record GitSuccessResponse(string? Message) : GitResponse(Message);

        public string? Message { get; init; }

        private GitResponse(string? Message)
        {
            this.Message = Message;
        }
    }
}
