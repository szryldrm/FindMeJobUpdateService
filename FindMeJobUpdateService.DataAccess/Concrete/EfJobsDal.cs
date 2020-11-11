using FindMeJobUpdateService.Core.DataAccess.EntityFramework;
using FindMeJobUpdateService.Core.Settings.MongoDbSettings;
using FindMeJobUpdateService.DataAccess.Abstract;
using FindMeJobUpdateService.Entities.Concrete;

namespace FindMeJobUpdateService.DataAccess.Concrete
{
    public class EfJobsDal : EfEntityRepositoryBaseMongo<Jobs>, IJobsDal
    {
        public EfJobsDal(IMongoDbSettings settings) : base(settings)
        {
        }
    }
}