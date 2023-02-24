namespace Core.Results
{
    public class Result:IResult
    {
        public bool IsSuccess { get; }

        public string Message { get; }
        public Result(bool isSuccess, string message) : this(isSuccess)
        {
            Message = message;
        }
        public Result(bool isSuccess)
        {
            this.IsSuccess = isSuccess;
        }
    }
}
