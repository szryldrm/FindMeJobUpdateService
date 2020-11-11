using System.Collections.Generic;
using System.Threading.Tasks;
using FindMeJobUpdateService.Core.Utilities.Results;
using FindMeJobUpdateService.Entities.Concrete;

namespace FindMeJobUpdateService.Business.Abstract
{
    public interface IJobsService
    {
        IDataResult<List<Jobs>> GetList();
        Task<IResult> UpdateService();
        IResult Add(Jobs jobs);
    }
}
