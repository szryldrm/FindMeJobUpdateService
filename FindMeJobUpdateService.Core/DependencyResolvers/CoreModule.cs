using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FindMeJobUpdateService.Core.CrossCuttingConcerns.Caching;
using FindMeJobUpdateService.Core.CrossCuttingConcerns.Caching.Redis;
using FindMeJobUpdateService.Core.Utilities.IoC;

namespace FindMeJobUpdateService.Core.DependencyResolvers
{
    public class CoreModule : ICoreModule
    {
        private IConfiguration _configuration;

        public CoreModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Load(IServiceCollection services)
        {
            services.AddSingleton<ICacheManager, RedisCacheManager>();
            services.AddSingleton<Stopwatch>();
        }
    }
}
