namespace SmartBudgett.Core.Results
{
    public class Result : IResult
    {
        public Result(bool success)
        {
            Success = success;
            Message = string.Empty;
        }

        public Result(bool success, string message)
        {
            Success = success;
            Message = message;
        }

        public bool Success { get; }

        public string Message { get; }
    }
}