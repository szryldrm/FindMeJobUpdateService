using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Caching.Distributed;
using MongoDB.Bson;
using MongoDB.Driver;
using FindMeJobUpdateService.Business.Abstract;
using FindMeJobUpdateService.Core.Utilities.Results;
using FindMeJobUpdateService.DataAccess.Abstract;
using FindMeJobUpdateService.Entities.Concrete;
using FindMeJobUpdateService.Business.Contants;
using FindMeJobUpdateService.Core.Aspects.Autofac.Caching;
using FindMeJobUpdateService.Core.Aspects.Autofac.Logging;
using FindMeJobUpdateService.Core.Aspects.Autofac.Performans;
using FindMeJobUpdateService.Core.Aspects.Autofac.Transaction;
using FindMeJobUpdateService.Core.CrossCuttingConcerns.Caching;
using FindMeJobUpdateService.Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using FindMeJobUpdateService.Entities.Concrete.Dto;
using System.Net.Http;
using Newtonsoft.Json;
using FindMeJobUpdateService.Business.Utilities.Mapping;
using System.Threading.Tasks;

namespace FindMeJobUpdateService.Business.Concrete
{
    public class JobsManager : IJobsService
    {
        private readonly IJobsDal _jobsDal;

        public JobsManager(IJobsDal jobsDal)
        {
            _jobsDal = jobsDal;
        }

        [TransactionScopeAspect(Priority = 1)]
        [CacheAspect(duration: 60, Priority = 2)]
        [LogAspect(typeof(DatabaseLogger), Priority = 3)]
        public IDataResult<List<Jobs>> GetList()
        {
            var value = _jobsDal.GetList().OrderByDescending(x => x.created_at).ToList();

            return value != null
                ? (IDataResult<List<Jobs>>)new SuccessDataResult<List<Jobs>>(value)
                : new ErrorDataResult<List<Jobs>>(Messages.ListNotFound);
        }

        [TransactionScopeAspect(Priority = 1)]
        [LogAspect(typeof(DatabaseLogger), Priority = 2)]
        //[CacheRemoveAspect("*IWarrantyService.Get*", Priority = 3)]
        //[LogAspect(typeof(FileLogger), Priority = 4)]
        [CacheFlushAspect(Priority = 5)]
        public IResult Add(Jobs jobs)
        {
            return _jobsDal.Add(jobs)
                ? (IResult)new SuccessResult(Messages.RecordIsAdded)
                : new ErrorResult(Messages.RecordIsNotAdded);
        }

        [TransactionScopeAspect(Priority = 1)]
        [LogAspect(typeof(DatabaseLogger), Priority = 2)]
        [CacheFlushAspect(Priority = 3)]
        public async Task<IResult> UpdateService()
        {
            try
            {
                var DBList = _jobsDal.GetList();
                List<JobsDto> githubJobList = new List<JobsDto>();
                List<JobsDto> tempList = new List<JobsDto>();
                int i = 1;

                while (true)
                {
                    using (var client = new HttpClient())
                    {
                        var result = await client.GetAsync("https://jobs.github.com/positions.json?page=" + i);

                        if (result.IsSuccessStatusCode)
                        {
                            string resultContentString = await result.Content.ReadAsStringAsync();
                            githubJobList = JsonConvert.DeserializeObject<List<JobsDto>>(resultContentString);
                        }
                    }

                    if (githubJobList.Count > 0)
                    {
                        foreach (var job in githubJobList)
                        {
                            tempList.Add(job);
                        }

                        i++;
                    }
                    else
                    {
                        tempList.Reverse();

                        foreach (var githubJob in tempList)
                        {
                            var value = DBList.FirstOrDefault(x => x.githubId == githubJob.id);
                            if (value == null)
                            {
                                Add(Mapping.MapClass(githubJob));
                            }
                        }

                        break;
                    }
                }

                return new SuccessResult();
            }
            catch (Exception ex)
            {
                return new ErrorResult(ex.Message);
                throw;
            }
        }
    }
}