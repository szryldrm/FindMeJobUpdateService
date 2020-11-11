using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using FindMeJobUpdateService.Business.Abstract;

namespace FindMeJobUpdateService.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private IJobsService _jobsService;

        public JobsController(IJobsService jobsService)
        {
            _jobsService = jobsService;
        }

        [HttpGet("getlist")]
        public IActionResult GetList()
        {
            var result = _jobsService.GetList();
            if (result.Success)
            {
                return Ok(JsonConvert.SerializeObject(result.Data));
            }
            return BadRequest(result.Message);
        }

        [HttpGet("updatelist")]
        public IActionResult UpdateList()
        {
            var result = _jobsService.UpdateService();
            if (result.Result.Success)
            {
                return Ok(JsonConvert.SerializeObject(result.Result.Message));
            }
            return BadRequest(result.Result.Message);
        }
    }
}
