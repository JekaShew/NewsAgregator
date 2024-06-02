using Microsoft.AspNetCore.Mvc;
using NewsAgregator.Abstract.ComplaintInterfaces;
using NewsAgregator.Abstract.WeatherInterfaces;
using NewsAgregator.ViewModels.Data;

namespace NewsAgregator.Web.Controllers.WeatherControllers
{
    [Route("/api/weather")]
    public class WeatherController : Controller
    {
        private readonly IWeatherServices _weatherServices;

        public WeatherController(IWeatherServices weatherServices)
        {
            _weatherServices = weatherServices;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(WeatherVM weatherVM)
        {
            await _weatherServices.AddWeather(weatherVM);
            return Ok();
        }

        [HttpGet("takeall")]
        public async Task<IActionResult> TakeAll()
        {
            var result = await _weatherServices.TakeWeathers();
            if (result != null)
                return Ok(result);
            else
                return NotFound();
        }

        [HttpGet("takebyid")]
        public async Task<IActionResult> TakeById(Guid id)
        {
            var result = await _weatherServices.TakeWeatherById(id);
            if (result != null)
                return Ok(result);
            else
                return NotFound();
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _weatherServices.DeleteWeather(id);
            return Ok();
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update(WeatherVM weatherVM)
        {
            await _weatherServices.UpdateWeather(weatherVM);
            return Ok();
        }
    }
}
