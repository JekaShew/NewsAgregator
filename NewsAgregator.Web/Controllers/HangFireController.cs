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
                "NewsAggregationGetRssData",
                () => _newsServices.GetRssDataAsync(),
                "0 * * * *"
                );

            RecurringJob.AddOrUpdate(
                "NewsAggregationUpdateNewsTextByScrappedData",
                () => _newsServices.UpdateNewsTextByScrappedData(),
                "10 * * * *"
                );

            RecurringJob.AddOrUpdate(
               "NewsAggregationUpdateNewsRate",
               () => _newsServices.UpdateNewsRateAsync(),
               "30 * * * *"
               );

            RecurringJob.AddOrUpdate(
              "NewsAggregationDeleteOldNews",
              () => _newsServices.DeleteOldNewsesAsync(),
              "45 12 * * *"
              );

            RecurringJob.AddOrUpdate(
              "NewsAggregationDeleteNewsWithBadRate",
              () => _newsServices.DeleteNewsWithBadRateAsync(),
              "45 13 * * *"
              );


            return Ok();
        }
    }
}
