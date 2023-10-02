using System;

namespace Sample.Common.Response
{
    public class ErrorItem
    {
        public string Message { get; set; }
        public Exception Exception { get; set; }

        public ErrorItem() : this(string.Empty, null)
        {
        }

        public ErrorItem(string message) : this(message, null)
        {
        }

        public ErrorItem(string message, Exception exception)
        {
            Message = message;
            Exception = exception;
        }
    }

    public class Response<T>
    {
        public bool Success { get; set; }
        public T Payload { get; set; }
        public ErrorItem Error { get; set; }
        public int ResponseCode { get; set; }

        public Response()
        {
            Success = true;
        }

        public bool IsValid()
        {
            return Success && Payload != null;
        }
    }
}
