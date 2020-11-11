using FindMeJobUpdateService.Entities.Concrete;
using FindMeJobUpdateService.Entities.Concrete.Dto;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace FindMeJobUpdateService.Business.Utilities.Mapping
{
    public static class Mapping
    {
        public static Jobs MapClass(JobsDto dto)
        {
            Jobs jobs = new Jobs();

            jobs.githubId = dto.id;
            jobs.type = dto.type;
            jobs.url = dto.url;
            jobs.created_at = DateTime.ParseExact(dto.created_at, "ddd MMM d HH:mm:ss UTC yyyy", CultureInfo.InvariantCulture);
            jobs.company = dto.company;
            jobs.company_url = dto.company_url;
            jobs.location = dto.location;
            jobs.title = dto.title;
            jobs.description = dto.description;
            jobs.how_to_apply = dto.how_to_apply;
            jobs.company_logo = dto.company_logo;
            jobs.JobsPage = "Github";
            return jobs;
        }
    }
}
