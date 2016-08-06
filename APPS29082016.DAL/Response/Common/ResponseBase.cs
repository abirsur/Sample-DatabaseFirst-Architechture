using System;

namespace APPS29082016.DAL.Response.Common
{
    public abstract class ResponseBase
    {
        public int StatusCode { get; set; }
        public string StatusMessage { get; set; }
        public bool ExceptionFound { get; set; }
        public string ExceptionMessage { get; set; }
        public string Stacktrace { get; set; }
        public TimeSpan TimeSpan {
            get { return DateTime.UtcNow.TimeOfDay; }
        }
    }

   
}
