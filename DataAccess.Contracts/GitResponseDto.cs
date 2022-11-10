namespace PinaryDevelopment.Git.Server.DataAccess.Contracts
{
    public record GitResponseDto
    {
        public record GitErrorResponseDto(string? Message, bool IsKnownMessage = false) : GitResponseDto(Message, IsKnownMessage);

        public record GitWarningResponseDto(string? Message, bool IsKnownMessage = false) : GitResponseDto(Message, IsKnownMessage);

        public record GitSuccessResponseDto(string? Message, bool IsKnownMessage = false) : GitResponseDto(Message, IsKnownMessage);

        public int ExitCode { get; set; }
        public string? Message { get; init; }
        public bool IsKnownMessage { get; init; }

        private GitResponseDto(string? Message, bool IsKnownMessage)
        {
            this.IsKnownMessage = IsKnownMessage;
            this.Message = Message;
        }
    }
}
