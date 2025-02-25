using System.Net;

namespace CodeQueue.Domain.Models.Exceptions
{
    public class ResponseException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }

        public ResponseException()
        {
            StatusCode = HttpStatusCode.BadRequest;
            Message = string.Empty;
        }

        public ResponseException(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
            Message = string.Empty;
        }

        public ResponseException(HttpStatusCode statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message;
        }
    }
}
