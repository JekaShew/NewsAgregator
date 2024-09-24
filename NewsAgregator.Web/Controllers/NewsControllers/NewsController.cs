using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NewsAgregator.Abstract.ComplaintInterfaces;
using NewsAgregator.Abstract.NewsInterfaces;
using NewsAgregator.ViewModels.Data;
using NewsAgregator.Web.Controllers.AccountControllers;
using NewsAgregator.Web.Filters;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NewsAgregator.Web.Controllers.NewsControllers
{
    [Route("/api/news")]
    public class NewsController : Controller
    {
        private readonly INewsServices _newsServices;
        private readonly ILogger<NewsController> _logger;

        public NewsController(INewsServices newsServices, ILogger<NewsController> logger)
        {
            _newsServices = newsServices;
            _logger = logger;
        }

        [HttpGet("getparameters")]
        public async Task<IActionResult> GetParameters()
        {
            try
            {
                var result = await _newsServices.GetNewsParametersAsync();

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(NewsVM newsVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _newsServices.AddNewsAsync(newsVM);
                    return Ok();
                }
                else return BadRequest(ModelState);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("aggregate")]
        public async Task<IActionResult> Aggregate()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _newsServices.AggregateNewsAsync();
                    return Ok();
                }
                else return BadRequest(ModelState);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("getrssdata")]
        public async Task<IActionResult> GetRssData()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _newsServices.GetRssDataAsync();
                    return Ok();
                }
                else return BadRequest(ModelState);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("scraptext")]
        public async Task<IActionResult> ScrapText()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _newsServices.UpdateNewsTextByScrappedData();
                    return Ok();
                }
                else return BadRequest(ModelState);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("updatenewsrate")]
        public async Task<IActionResult> UpdateNewsRate()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _newsServices.UpdateNewsRateAsync();
                    return Ok();
                }
                else return BadRequest(ModelState);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet("takeall")]
        [Authorize]
        [PolicyAuthorization(Permission = "CanReadNews")]
        public async Task<IActionResult> TakeAll()
        {
            try
            {
                var result = await _newsServices.TakeNewsesAsync();
                if (result.Count != 0)
                    return Ok(result);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("taketopnewses")]
        public async Task<IActionResult> TakeTopNewses()
        {
            try
            {
                var result = await _newsServices.TakeTopNewsesAsync();
                if (result.Count != 0)
                    return Ok(result);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("takesuitablenewses")]
        [Authorize]
        public async Task<IActionResult> TakeSuitableNewses()
        {
            try
            {
                var result = await _newsServices.TakeSuitableNewsesAsync();
                if (result.Count != 0)
                    return Ok(result);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("takebyid/{id}")]
        public async Task<IActionResult> TakeById(Guid id)
        {
            try
            {
                var result = await _newsServices.TakeNewsByIdAsync(id);
                if (result != null)
                    return Ok(result);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _newsServices.DeleteNewsAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update(NewsVM newsVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _newsServices.UpdateNewsAsync(newsVM);
                    return Ok();
                }
                else return BadRequest(ModelState);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }
    }
}
