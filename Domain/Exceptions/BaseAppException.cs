namespace IAMNC0000S03.Domain.Exceptions
{
    public abstract class BaseAppException : Exception
    {
        public string ErrorCode { get; }
        public int? StatusCode { get; }

        protected BaseAppException(string errorCode, string? message = null, int? statusCode = null) : base(message)
        {
            ErrorCode = errorCode;
            StatusCode = statusCode;
        }
    }
}
