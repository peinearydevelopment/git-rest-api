namespace PinaryDevelopment.Git.Server.DataAccess
{
    using DataAccess.Contracts;
    using System.Diagnostics;
    using System.Text.RegularExpressions;
    using static Contracts.GitResponseDto;

    public static class GitProcessExtensions
    {
        public static GitResponseDto InvokeCommand(this IGitProcess process, string commandText, GitMessageIndicators messageIndicators)
        {
            var processErrorData = string.Empty;
            var processOutputData = string.Empty;

            process.ErrorDataReceived += (object sender, DataReceivedEventArgs e) => processErrorData += e.Data;
            process.OutputDataReceived += (object sender, DataReceivedEventArgs e) => processOutputData += e.Data;

            process.Start(commandText);
            process.BeginErrorReadLine();
            process.BeginOutputReadLine();
            process.WaitForExit();

            GitResponseDto responseDto;
            if (!string.IsNullOrWhiteSpace(processErrorData))
            {
                responseDto = ProcessErrorData(processErrorData, messageIndicators.ErroredMessageIndicators, messageIndicators.NoOpMessageIndicators);
            }
            else
            {
                responseDto = ProcessOutputData(processOutputData, messageIndicators.SuccessfulMessageIndicators);
            }

            responseDto.ExitCode = process.ExitCode;
            return responseDto;
        }

        private static GitResponseDto ProcessErrorData(string errorData, string[] erroredMessageIndicators, string[] noOpMessageIndicators)
        {
            if (Regex.IsMatch(errorData, "(error: )", RegexOptions.Compiled))
            {
                foreach (var erroredMessageIndicator in erroredMessageIndicators)
                {
                    var pattern = $"(error: )({erroredMessageIndicator}.*)";
                    if (Regex.IsMatch(errorData, pattern))
                    {
                        return new GitErrorResponseDto(Message: Regex.Matches(errorData, pattern)[0].Groups[2].Value, IsKnownMessage: true);
                    }
                }

                return new GitErrorResponseDto(Message: Regex.Matches(errorData, "(error: )(.*)", RegexOptions.Compiled)[0].Groups[2].Value);
            }

            if (Regex.IsMatch(errorData, "(warning: )", RegexOptions.Compiled))
            {
                foreach (var noOpMessageIndicator in noOpMessageIndicators)
                {
                    var pattern = $"(warning: )({noOpMessageIndicator}.*)";
                    if (Regex.IsMatch(errorData, pattern))
                    {
                        return new GitWarningResponseDto(Message: Regex.Matches(errorData, pattern)[0].Groups[2].Value, IsKnownMessage: true);
                    }
                }

                return new GitWarningResponseDto(Message: Regex.Matches(errorData, "(warning: )(.*)", RegexOptions.Compiled)[0].Groups[2].Value);
            }

            return new GitErrorResponseDto(Message: errorData);
        }

        private static GitResponseDto ProcessOutputData(string outputData, string[] succeededMessageIndicators)
        {
            foreach (var succeededMessageIndicator in succeededMessageIndicators)
            {
                var pattern = $"({succeededMessageIndicator}.*)";
                if (Regex.IsMatch(outputData, pattern))
                {
                    return new GitSuccessResponseDto(Message: Regex.Matches(outputData, pattern)[0].Groups[1].Value, IsKnownMessage: true);
                }
            }

            return new GitSuccessResponseDto(Message: outputData);
        }
    }
}
