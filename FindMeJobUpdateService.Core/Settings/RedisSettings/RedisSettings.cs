using System;
using System.Collections.Generic;
using System.Text;

namespace FindMeJobUpdateService.Core.Settings.RedisSettings
{
    public class RedisSettings : IRedisSettings
    {
        public string RedisHostIP { get; set; }
        public string RedisPort { get; set; }
    }
}
