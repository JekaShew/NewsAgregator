using Microsoft.AspNetCore.Mvc;
using NewsAgregator.Abstract.ComplaintInterfaces;
using NewsAgregator.Data.Models;
using NewsAgregator.ViewModels.Data;

namespace NewsAgregator.Web.Controllers.ComplaintControllers
{
    [Route("/api/complainttype")]
    public class ComplaintTypeController : Controller
    {
        private readonly IComplaintTypeServices _complaintTypeServices;

        public ComplaintTypeController(IComplaintTypeServices complaintTypeServices)
        {
            _complaintTypeServices = complaintTypeServices;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(ComplaintTypeVM complaintTypeVM)
        {
            await _complaintTypeServices.AddComplaintType(complaintTypeVM);
            return Ok();
        }

        [HttpGet("takeall")]
        public async Task<IActionResult> TakeAll()
        {
            var result = await _complaintTypeServices.TakeComplaintTypes();
            if (result != null)
                return Ok(result);
            else
                return NotFound();
        }

        [HttpGet("takebyid")]
        public async Task<IActionResult> TakeById(Guid id)
        {
            var result = await _complaintTypeServices.TakeComplaintTypeById(id);
            if (result != null)
                return Ok(result);
            else
                return NotFound();
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _complaintTypeServices.DeleteComplaintType(id);
            return Ok();
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update(ComplaintTypeVM complaintTypeVM)
        {
            await _complaintTypeServices.UpdateComplaintType(complaintTypeVM);
            return Ok();
        }
    }
}
