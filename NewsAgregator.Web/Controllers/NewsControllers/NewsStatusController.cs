using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsAgregator.Abstract.ComplaintInterfaces;
using NewsAgregator.Abstract.NewsInterfaces;
using NewsAgregator.ViewModels.Data;
using NewsAgregator.Web.Controllers.AccountControllers;

namespace NewsAgregator.Web.Controllers.NewsControllers
{
    [Route("/api/newsstatus")]
    public class NewsStatusController : Controller
    {
        private readonly INewsStatusServices _newsStatusServices;
        private readonly ILogger<NewsStatusController> _logger;

        public NewsStatusController(INewsStatusServices newsStatusServices, ILogger<NewsStatusController> logger)
        {
            _newsStatusServices = newsStatusServices;
            _logger = logger;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(NewsStatusVM newsStatusVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _newsStatusServices.AddNewsStatusAsync(newsStatusVM);
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
        public async Task<IActionResult> TakeAll()
        {
            try
            {
                var result = await _newsStatusServices.TakeNewsStatusesAsync();
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
                var result = await _newsStatusServices.TakeNewsStatusByIdAsync(id);
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
                await _newsStatusServices.DeleteNewsStatusAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update(NewsStatusVM newsStatusVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _newsStatusServices.UpdateNewsStatusAsync(newsStatusVM);
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
