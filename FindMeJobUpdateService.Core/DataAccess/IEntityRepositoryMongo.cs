using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using FindMeJobUpdateService.Core.Entities;

namespace FindMeJobUpdateService.Core.DataAccess
{
    public interface IEntityRepositoryMongo<T> where T:class, IDocument, new()
    {
        bool Add(T entity);  
        bool Update(T entity);  
        bool Delete(string id);
        bool UpdateOne(FilterDefinition<T> filter, UpdateDefinition<T> update, UpdateOptions options=null);
        T Get(FilterDefinition<T> filter);  
        List<T> GetList(FilterDefinition<T> filter=null);
        List<T> GetProjectionList(ProjectionDefinition<T> project, FilterDefinition<T> filter = null);
    }
}
