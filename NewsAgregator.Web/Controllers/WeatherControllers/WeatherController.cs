﻿using Microsoft.AspNetCore.Mvc;
using NewsAgregator.Abstract.ComplaintInterfaces;
using NewsAgregator.Abstract.WeatherInterfaces;
using NewsAgregator.ViewModels.Data;
using NewsAgregator.Web.Controllers.AccountControllers;

namespace NewsAgregator.Web.Controllers.WeatherControllers
{
    [Route("/api/weather")]
    public class WeatherController : Controller
    {
        private readonly IWeatherServices _weatherServices;
        private readonly ILogger<WeatherController> _logger;

        public WeatherController(IWeatherServices weatherServices, ILogger<WeatherController> logger)
        {
            _weatherServices = weatherServices;
            _logger = logger;
        }

        [HttpGet("getparameters")]
        public async Task<IActionResult> GetParameters()
        {
            try
            {
                var result = await _weatherServices.GetWeatherParameters();

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(WeatherVM weatherVM)
        {
            try
            {
                await _weatherServices.AddWeather(weatherVM);
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
                var result = await _weatherServices.TakeWeathers();
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
                var result = await _weatherServices.TakeWeatherById(id);
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
                await _weatherServices.DeleteWeather(id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update(WeatherVM weatherVM)
        {
            try
            {
                await _weatherServices.UpdateWeather(weatherVM);
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
