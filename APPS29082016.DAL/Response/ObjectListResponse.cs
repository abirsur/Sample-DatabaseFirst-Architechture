using System;
using System.Collections.Generic;
using APPS29082016.DAL.Response.Common;

namespace APPS29082016.DAL.Response
{
    public class ObjectListResponse<TEntity> : ResponseBase where TEntity : class
    {
        public List<TEntity> EntityList { get; set; }
        public int RecordCount { get; set; }

        public ObjectListResponse(List<TEntity> entityList, int statusCode)
        {
            if (statusCode>0)
            {
                this.StatusCode = statusCode;
                this.EntityList = entityList;
                this.StatusMessage = CommonResponse.GetStatusMessage(statusCode);
                this.RecordCount = entityList.Count;
            }
            if (statusCode < 0)
            {
                this.StatusCode = statusCode;
                this.EntityList = new List<TEntity>();
                this.StatusMessage = CommonResponse.GetStatusMessage(statusCode);
                this.RecordCount = 0;
            }
        }

        public ObjectListResponse(Exception exception)
        {
            this.StatusCode = -99;
            this.EntityList = new List<TEntity>();
            this.StatusMessage = CommonResponse.GetStatusMessage(-99);
            this.RecordCount = 0;
            this.ExceptionMessage = exception.Message;
            this.Stacktrace = exception.StackTrace;
            this.ExceptionFound = true;
        }
    }
}