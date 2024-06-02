using Microsoft.AspNetCore.Mvc;
using NewsAgregator.Abstract.ComplaintInterfaces;
using NewsAgregator.Abstract.NewsInterfaces;
using NewsAgregator.ViewModels.Data;

namespace NewsAgregator.Web.Controllers.NewsControllers
{
    [Route("/api/news")]
    public class NewsController : Controller
    {
        private readonly INewsServices _newsServices;

        public NewsController(INewsServices newsServices)
        {
            _newsServices = newsServices;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(NewsVM newsVM)
        {
            await _newsServices.AddNews(newsVM);
            return Ok();
        }

        [HttpGet("takeall")]
        public async Task<IActionResult> TakeAll()
        {
            var result = await _newsServices.TakeNewses();
            if (result != null)
                return Ok(result);
            else
                return NotFound();
        }

        [HttpGet("takebyid")]
        public async Task<IActionResult> TakeById(Guid id)
        {
            var result = await _newsServices.TakeNewsById(id);
            if (result != null)
                return Ok(result);
            else
                return NotFound();
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _newsServices.DeleteNews(id);
            return Ok();
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update(NewsVM newsVM)
        {
            await _newsServices.UpdateNews(newsVM);
            return Ok();
        }
    }
}
