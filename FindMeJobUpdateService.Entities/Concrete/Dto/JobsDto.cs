using System;
using System.Collections.Generic;
using System.Text;

namespace FindMeJobUpdateService.Entities.Concrete.Dto
{
    public class JobsDto
    {
        public string id { get; set; }
        public string type { get; set; }
        public string url { get; set; }
        public string created_at { get; set; }
        public string company { get; set; }
        public string company_url { get; set; }
        public string location { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string how_to_apply { get; set; }
        public string company_logo { get; set; }
    }
}
