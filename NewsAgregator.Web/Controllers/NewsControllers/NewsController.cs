using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NewsAgregator.Abstract.ComplaintInterfaces;
using NewsAgregator.Abstract.NewsInterfaces;
using NewsAgregator.ViewModels.Data;
using NewsAgregator.Web.Controllers.AccountControllers;
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

        [HttpPost("updatenewsrate")]
        public async Task<IActionResult> UpdateNewsRate()
        {
            //var url = "http://localhost:1337/v1/chat/completions";

            //var body = new
            //{
            //    model = "gpt-3.5-turbo",
            //    stream = false,
            //    messages = new[]
            //    {
            //        new { role = "assistant", content = "Test passed" }
            //    }
            //};

            //using (var client = new HttpClient())
            //{
            //    var request = new HttpRequestMessage(HttpMethod.Post,
            //        url);

            //    request.Headers.Add("Accept", "application/json");
            //    request.Content = JsonContent.Create(new[]
            //    {
            //        new { Body = body }
            //    });

            //    var response = await client.SendAsync(request);

            //    if (response.IsSuccessStatusCode)
            //    {
            //        var responseString = await response.Content.ReadAsStringAsync();


            //        //var lemmas = //JsonConvert.DeserializeObject<>

            //        //lemmasDictionary

            //        //based on dict calculate rate of your article

            //        return Ok(responseString);
            //    }
            //    else
            //    {
            //        await _newsServices.UpdateNewsRateAsync();
            //        return StatusCode(500);
            //    }
            //}


            await _newsServices.UpdateNewsRateAsync();
            return Ok();
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

        [HttpGet("takeall")]
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
