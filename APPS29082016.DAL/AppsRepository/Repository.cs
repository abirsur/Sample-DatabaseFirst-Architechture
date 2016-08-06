using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Reflection;
using System.Threading.Tasks;
using APPS29082016.DAL.AppsRepository.Contract;
using APPS29082016.DAL.EfRepository;
using APPS29082016.DAL.Response;

namespace APPS29082016.DAL.AppsRepository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        #region Private Properties
        /// <summary>
        /// Database first DbContext
        /// </summary>
        private EfRepositoryEntities DbContext { get; } = new EfRepositoryEntities();

       /// <summary>
        /// Generic class object
        /// </summary>
        private TEntity ObjectEnttity { get; set; } 
        #endregion

        #region FUNC Operations

        /// <summary>
        /// Func : Get All Entity List
        /// </summary>
        public Func<List<object>, List<TEntity>> EntityList { get; } = objEntity =>
        {
            List<TEntity> tObjList = new List<TEntity>();
            foreach (var item in objEntity)
            {
                tObjList.Add((TEntity)item);
            }
            return tObjList;
        };

        /// <summary>
        /// Func : Get Entity by FieldName and FieldValue
        /// </summary>
        public Func<List<TEntity>, string, string, List<TEntity>> EntityTypeByKeyAndValue { get; } = (objEntityList, objKeyName, objKeyValue) =>
         {
             List<TEntity> entityList = new List<TEntity>();
             object tEntityObj = new object();
             foreach (var items in objEntityList)
             {
                 bool isDataFound = false;
                 PropertyInfo[] infos = items.GetType().GetProperties();
                 Dictionary<string, string> dix = new Dictionary<string, string>();
                 foreach (PropertyInfo info in infos)
                 {
                     if (info.Name.ToLower() == objKeyName.ToLower())
                     {
                         if (info.GetValue(items, null).ToString() == objKeyValue)
                         {
                             isDataFound = true;
                         }
                         break;
                     }
                 }
                 if (isDataFound == true)
                 {
                     entityList.Add((TEntity)items);
                 }
             }
             return entityList;
         };

        /// <summary>
        /// Func : Get Entity by Keyword
        /// </summary>
        public Func<List<TEntity>, string, List<TEntity>> EntitiesByKeyWord { get; } =
            (objEntityList, objKeyValue) =>
            {
                List<TEntity> entityList = new List<TEntity>();
                foreach (var items in objEntityList)
                {
                    bool isDataFound = false;
                    PropertyInfo[] infos = items.GetType().GetProperties();
                    foreach (PropertyInfo info in infos)
                    {
                        if (info.GetValue(items, null).ToString() == objKeyValue)
                        {
                            isDataFound = true;
                        }
                        break;
                    }
                    if (isDataFound)
                    {
                        entityList.Add((TEntity)items);
                    }
                }
                return entityList;
            };

        #endregion

        #region Public Methods

        /// <summary>
        /// Get all Entity as 
        /// </summary>
        /// <returns>></returns>
        public ObjectListResponse<TEntity> GetAll()
        {
            try
            {
                Task<List<object>> objectList = DbContext.Set(typeof (TEntity)).ToListAsync();
                if (objectList != null)
                {
                    return new ObjectListResponse<TEntity>(EntityList(objectList.Result), 2);
                }
                return new ObjectListResponse<TEntity>(null,-2);
            }
            catch (Exception ex)
            {
                return new ObjectListResponse<TEntity>(ex);
            }
        }

        /// <summary>
        /// Get entity by FieldName and FieldValue. Can also used for case of Search and Duplicate Entry
        /// </summary>
        /// <param name="fieldName"> FieldName </param>
        /// <param name="fieldValue"> Fieldvalue </param>
        /// <returns>TEntity</returns>
        public ObjectListResponse<TEntity> GetEntityByKeyAndValue(string fieldName, string fieldValue)
        {
            try
            {
                List<TEntity> objectList = GetAll().EntityList;
                if (objectList != null)
                {
                    return new ObjectListResponse<TEntity>(EntityTypeByKeyAndValue(objectList,fieldName,fieldValue), 2);
                }
                return new ObjectListResponse<TEntity>(null, -2);
            }
            catch (Exception ex)
            {
                return new ObjectListResponse<TEntity>(ex);
            }
        }

        /// <summary>
        /// Get entity by Keyword
        /// </summary>
        /// <param name="keyWord"> Search keyword </param>
        /// <returns></returns>
        public ObjectListResponse<TEntity> GetEntityByKeyword(string keyWord)
        {
            try
            {
                List<TEntity> objectList = GetAll().EntityList;
                if (objectList != null)
                {
                    return new ObjectListResponse<TEntity>(EntitiesByKeyWord(objectList, keyWord), 2);
                }
                return new ObjectListResponse<TEntity>(null, -2);
            }
            catch (Exception ex)
            {
                return new ObjectListResponse<TEntity>(ex);
            }

        }

        /// <summary>
        /// Edit any entity in List
        /// note: use it carefully 
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="fieldValue"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Task<OperationResponse> EditEntity(string fieldName, string fieldValue, TEntity entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Create a new Entity
        /// </summary>
        /// <param name="tEntity"></param>
        /// <returns>bool</returns>
        public OperationResponse AddEntity(TEntity tEntity)
        {
            try
            {
                object entity = tEntity;
                DbContext.Set(typeof(TEntity)).Add(entity);
                DbContext.SaveChanges();
                return new OperationResponse(2);
            }
            catch (Exception ex)
            {
                return new OperationResponse(ex);
            }
        }

        #endregion

        /// <summary>
        /// Dispose database context
        /// </summary>
        public void Dispose()
        {
            DbContext.Dispose();
        }
    }
}
