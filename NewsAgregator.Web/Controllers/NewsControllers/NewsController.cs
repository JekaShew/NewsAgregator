using Microsoft.AspNetCore.Mvc;
using NewsAgregator.Abstract.ComplaintInterfaces;
using NewsAgregator.Abstract.NewsInterfaces;
using NewsAgregator.ViewModels.Data;
using NewsAgregator.Web.Controllers.AccountControllers;

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
                var result = await _newsServices.GetNewsParameters();

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
                    await _newsServices.AddNews(newsVM);
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
        public async Task<IActionResult> TakeAll()
        {
            try
            {
                var result = await _newsServices.TakeNewses();
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

        [HttpGet("takebyid/{id}")]
        public async Task<IActionResult> TakeById(Guid id)
        {
            try
            {
                var result = await _newsServices.TakeNewsById(id);
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
                await _newsServices.DeleteNews(id);
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
                    await _newsServices.UpdateNews(newsVM);
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
