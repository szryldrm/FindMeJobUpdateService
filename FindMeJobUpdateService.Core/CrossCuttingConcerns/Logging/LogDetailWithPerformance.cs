using System;
using System.Collections.Generic;
using System.Text;

namespace FindMeJobUpdateService.Core.CrossCuttingConcerns.Logging
{
    public class LogDetailWithPerformance : LogDetail
    {
        public int Interval { get; set; }
    }
}
