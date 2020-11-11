using FindMeJobUpdateService.Core.DataAccess;
using FindMeJobUpdateService.Entities.Concrete;

namespace FindMeJobUpdateService.DataAccess.Abstract
{
    public interface IJobsDal:IEntityRepositoryMongo<Jobs>
    {
        
    }
}