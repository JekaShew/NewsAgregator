using Microsoft.AspNetCore.Mvc;
using NewsAgregator.Abstract.ComplaintInterfaces;
using NewsAgregator.ViewModels.Data;

namespace NewsAgregator.Web.Controllers.ComplaintControllers
{
    [Route("/api/complaint")]
    public class ComplaintController : Controller
    {
        private readonly IComplaintServices _complaintServices;

        public ComplaintController(IComplaintServices complaintServices)
        {
            _complaintServices = complaintServices;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(ComplaintVM complaintVM)
        {
            await _complaintServices.AddComplaint(complaintVM);
            return Ok();
        }

        [HttpGet("takeall")]
        public async Task<IActionResult> TakeAll()
        {
            var result = await _complaintServices.TakeComplaints();
            if (result != null)
                return Ok(result);
            else
                return NotFound();
        }

        [HttpGet("takebyid")]
        public async Task<IActionResult> TakeById(Guid id)
        {
            var result = await _complaintServices.TakeComplaintById(id);
            if (result != null)
                return Ok(result);
            else
                return NotFound();
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _complaintServices.DeleteComplaint(id);
            return Ok();
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update(ComplaintVM complaintVM)
        {
            await _complaintServices.UpdateComplaint(complaintVM);
            return Ok();
        }
    }
}
