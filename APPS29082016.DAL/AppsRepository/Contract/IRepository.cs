using System;
using System.Threading.Tasks;
using APPS29082016.DAL.Response;

namespace APPS29082016.DAL.AppsRepository.Contract
{
    public interface IRepository<TEntity>:IDisposable where TEntity:class
    {
        ObjectListResponse<TEntity> GetAll();
        OperationResponse AddEntity(TEntity tEntity);
        ObjectListResponse<TEntity> GetEntityByKeyAndValue(string keyName, string keyValue);
        ObjectListResponse<TEntity> GetEntityByKeyword(string keyWord);
        Task<OperationResponse> EditEntity(string fieldName, string fieldValue, TEntity entity);
        
    }
}
