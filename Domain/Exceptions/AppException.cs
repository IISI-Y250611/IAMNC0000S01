namespace IAMNC0000S03.Domain.Exceptions
{
    public class AppException : BaseAppException
    {
        public AppException(string errorCode, string? message = null, int? statusCode = null)
            : base(errorCode, message, statusCode)
        {
            //ErrorCode = errorCode;
            //StatusCode = statusCode;
        }

        public override string ToString()
        {
            return $"[AppException] Code = {ErrorCode}, Message = {Message}";
        }
    }
}