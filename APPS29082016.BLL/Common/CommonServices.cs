using APPS29082016.DAL.AppsRepository.Contract;
using APPS29082016.DAL.Response;

namespace APPS29082016.BLL.Common
{
    public class CommonServices
    {
        /// <summary>
        /// if no valus found return true else false
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="fieldName"></param>
        /// <param name="fieldValue"></param>
        /// <returns></returns>
        public bool IsDuplicateEntry<TEntity>(IRepository<TEntity> repository, string fieldName, string fieldValue)
            where TEntity : class
        {

            if (string.IsNullOrEmpty(fieldName) || string.IsNullOrEmpty(fieldValue)) return false;
            ObjectListResponse<TEntity> objectListResponse =
                repository.GetEntityByKeyAndValue(
                    fieldName,
                    fieldValue);
            return !objectListResponse.ExceptionFound &&
                   objectListResponse.StatusCode == -2;
        }

        
    }
}
