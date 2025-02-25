using System.Net;

namespace CodeQueue.Domain.Models
{
    public class DefaultResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public ExceptionModel? Exception { get; set; }
        public object Payload { get; set; }

        public DefaultResponse()
        {
            StatusCode = HttpStatusCode.OK;
        }
        
        public DefaultResponse(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
        }

        public DefaultResponse(HttpStatusCode statusCode, object payload)
        {
            StatusCode = statusCode;
            Payload = payload;
        }
    }

    public class ExceptionModel
    {
        public string StackTrace { get; set; }
        public string Message { get; set; }

        public ExceptionModel() { }

        public ExceptionModel(Exception ex)
        {
            StackTrace = ex.StackTrace;
            Message = ex.Message;
        }
    }
}
