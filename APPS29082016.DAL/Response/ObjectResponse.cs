using System;
using APPS29082016.DAL.Response.Common;

namespace APPS29082016.DAL.Response
{
    public class ObjectResponse<TEntity> : ResponseBase where TEntity : class
    {
        public TEntity Entity { get; set; }

        public ObjectResponse(TEntity entity, int statusCode)
        {
            if (statusCode > 0)
            {
                this.StatusCode = statusCode;
                this.Entity = entity;
                this.StatusMessage = CommonResponse.GetStatusMessage(statusCode);
            }
            if (statusCode < 0)
            {
                this.StatusCode = statusCode;
                this.Entity = null;
                this.StatusMessage = CommonResponse.GetStatusMessage(statusCode);
            }
        }

        public ObjectResponse(Exception exception)
        {
            this.Entity = null;
            this.StatusCode = -99;
            this.StatusMessage = CommonResponse.GetStatusMessage(-99);
            this.ExceptionMessage = exception.Message;
            this.Stacktrace = exception.StackTrace;
            this.ExceptionFound = true;
        }
    }
}