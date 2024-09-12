using Microsoft.AspNetCore.Mvc;
using NewsAgregator.Abstract.WeatherInterfaces;
using NewsAgregator.Services.WeatherServices;
using NewsAgregator.ViewModels.Data;
using NewsAgregator.Web.Controllers.AccountControllers;

namespace NewsAgregator.Web.Controllers.WeatherControllers
{
    [Route("/api/weatherstatus")]
    public class WeatherStatusController : Controller
    {
        private readonly IWeatherStatusServices _weatherStatusServices;
        private readonly ILogger<WeatherStatusController> _logger;

        public WeatherStatusController(IWeatherStatusServices weatherStatusServices, ILogger<WeatherStatusController> logger)
        {
            _weatherStatusServices = weatherStatusServices;
            _logger = logger;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(WeatherStatusVM weatherStatusVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _weatherStatusServices.AddWeatherStatusAsync(weatherStatusVM);
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
                var result = await _weatherStatusServices.TakeWeatherStatusesAsync();
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
                var result = await _weatherStatusServices.TakeWeatherStatusByIdAsync(id);
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
                await _weatherStatusServices.DeleteWeatherStatusAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }

        }

        [HttpPost("update")]
        public async Task<IActionResult> Update(WeatherStatusVM weatherStatusVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _weatherStatusServices.UpdateWeatherStatusAsync(weatherStatusVM);
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
