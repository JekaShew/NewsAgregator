using Microsoft.AspNetCore.Mvc;
using NewsAgregator.Abstract.WeatherInterfaces;
using NewsAgregator.Services.WeatherServices;
using NewsAgregator.ViewModels.Data;

namespace NewsAgregator.Web.Controllers.WeatherControllers
{
    [Route("/api/weatherstatus")]
    public class WeatherStatusController : Controller
    {
        private readonly IWeatherStatusServices _weatherStatusServices;

        public WeatherStatusController(IWeatherStatusServices weatherStatusServices)
        {
            _weatherStatusServices = weatherStatusServices;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(WeatherStatusVM weatherStatusVM)
        {
            await _weatherStatusServices.AddWeatherStatus(weatherStatusVM);
            return Ok();
        }

        [HttpGet("takeall")]
        public async Task<IActionResult> TakeAll()
        {
            var result = await _weatherStatusServices.TakeWeatherStatuses();
            if (result != null)
                return Ok(result);
            else
                return NotFound();
        }

        [HttpGet("takebyid")]
        public async Task<IActionResult> TakeById(Guid id)
        {
            var result = await _weatherStatusServices.TakeWeatherStatusById(id);
            if (result != null)
                return Ok(result);
            else
                return NotFound();
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _weatherStatusServices.DeleteWeatherStatus(id);
            return Ok();
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update(WeatherStatusVM weatherStatusVM)
        {
            await _weatherStatusServices.UpdateWeatherStatus(weatherStatusVM);
            return Ok();
        }

    }

   
}
