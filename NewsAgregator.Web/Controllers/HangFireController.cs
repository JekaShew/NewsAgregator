using Hangfire;
using Microsoft.AspNetCore.Mvc;
using NewsAgregator.Abstract.NewsInterfaces;

namespace NewsAgregator.Web.Controllers
{
    [Route("api/hangfire")]
    [ApiController]
    public class HangFireController : Controller
    {
        private readonly INewsServices _newsServices;
        public HangFireController(INewsServices newsServices)
        {
            _newsServices = newsServices;
        }

        [HttpPost("setjobs")]
        public async Task<IActionResult> SetRecurringJobs()
        {
            RecurringJob.AddOrUpdate(
                "NewsAggregation",
                () => _newsServices.AggregateNewsAsync(),
                " "
                );
            return Ok();
        }
    }
}
