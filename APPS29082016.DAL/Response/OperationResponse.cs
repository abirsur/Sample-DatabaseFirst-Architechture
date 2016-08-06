using System;
using APPS29082016.DAL.Response.Common;

namespace APPS29082016.DAL.Response
{
    public class OperationResponse : ResponseBase
    {
        public OperationResponse(int statusCode)
        {
            this.StatusCode = statusCode;
            this.StatusMessage = CommonResponse.GetStatusMessage(statusCode);
        }

        public OperationResponse(Exception exception)
        {
            this.StatusCode = -99;
            this.StatusMessage = CommonResponse.GetStatusMessage(-99);
            this.ExceptionMessage = exception.Message;
            this.Stacktrace = exception.StackTrace;
            this.ExceptionFound = true;
        }

        
    }
}
