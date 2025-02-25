using System.Net;

namespace CodeQueue.Domain.Models
{
    public class DefaultResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public ErrorModel Error { get; set; }
    }

    public class ErrorModel
    {
        public string StackTrace { get; set; }
        public string Message { get; set; }

        public ErrorModel() { }

        public ErrorModel(Exception ex)
        {
            StackTrace = ex.StackTrace;
            Message = ex.Message;
        }
    }
}
