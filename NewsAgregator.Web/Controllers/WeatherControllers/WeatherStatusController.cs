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
                await _weatherStatusServices.AddWeatherStatus(weatherStatusVM);
                return Ok();
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
                var result = await _weatherStatusServices.TakeWeatherStatuses();
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
                var result = await _weatherStatusServices.TakeWeatherStatusById(id);
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
                await _weatherStatusServices.DeleteWeatherStatus(id);
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
                await _weatherStatusServices.UpdateWeatherStatus(weatherStatusVM);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }
    }

   
}
