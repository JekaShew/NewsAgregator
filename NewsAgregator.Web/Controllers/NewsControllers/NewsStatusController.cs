using Microsoft.AspNetCore.Mvc;
using NewsAgregator.Abstract.ComplaintInterfaces;
using NewsAgregator.Abstract.NewsInterfaces;
using NewsAgregator.ViewModels.Data;

namespace NewsAgregator.Web.Controllers.NewsControllers
{
    [Route("/api/newsstatus")]
    public class NewsStatusController : Controller
    {
        private readonly INewsStatusServices _newsStatusServices;

        public NewsStatusController(INewsStatusServices newsStatusServices)
        {
            _newsStatusServices = newsStatusServices;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(NewsStatusVM newsStatusVM)
        {
            await _newsStatusServices.AddNewsStatus(newsStatusVM);
            return Ok();
        }

        [HttpGet("takeall")]
        public async Task<IActionResult> TakeAll()
        {
            var result = await _newsStatusServices.TakeNewsStatuses();
            if (result != null)
                return Ok(result);
            else
                return NotFound();
        }

        [HttpGet("takebyid")]
        public async Task<IActionResult> TakeById(Guid id)
        {
            var result = await _newsStatusServices.TakeNewsStatusById(id);
            if (result != null)
                return Ok(result);
            else
                return NotFound();
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _newsStatusServices.DeleteNewsStatus(id);
            return Ok();
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update(NewsStatusVM newsStatusVM)
        {
            await _newsStatusServices.UpdateNewsStatus(newsStatusVM);
            return Ok();
        }
    }
}
