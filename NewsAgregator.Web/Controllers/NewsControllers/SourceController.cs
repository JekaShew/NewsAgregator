using Microsoft.AspNetCore.Mvc;
using NewsAgregator.Abstract.NewsInterfaces;
using NewsAgregator.ViewModels.Data;

namespace NewsAgregator.Web.Controllers.NewsControllers
{
    public class SourceController : Controller
    {
        private readonly ISourceServices _sourceServices;
        private readonly ILogger<SourceController> _logger;

        public SourceController(ISourceServices sourceServices, ILogger<SourceController> logger)
        {
            _sourceServices = sourceServices;
            _logger = logger;
        }


        [HttpGet("takeall")]
        public async Task<IActionResult> TakeAll()
        {
            try
            {
                var result = await _sourceServices.TakeSourcesAsync();
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
        public async Task<IActionResult> TakeById([FromRoute]Guid id)
        {
            try
            {
                var result = await _sourceServices.TakeSourceByIdAsync(id);
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
        public async Task<IActionResult> Delete([FromRoute]Guid id)
        {
            try
            {
                await _sourceServices.DeleteSourceAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }

        }

        [HttpPost("update")]
        public async Task<IActionResult> Update(SourceVM sourceVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _sourceServices.UpdateSourceAsync(sourceVM);
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
