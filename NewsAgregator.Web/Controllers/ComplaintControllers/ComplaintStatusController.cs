using Microsoft.AspNetCore.Mvc;
using NewsAgregator.Abstract.AccountInterfaces;
using NewsAgregator.Abstract.ComplaintInterfaces;
using NewsAgregator.ViewModels.Data;

namespace NewsAgregator.Web.Controllers.ComplaintControllers
{
    [Route("/api/complaintstatus")]
    public class ComplaintStatusController : Controller
    {
        private readonly IComplaintStatusServices _complaintStatus;

        public ComplaintStatusController(IComplaintStatusServices complaintStatusServices)
        {
            _complaintStatus = complaintStatusServices;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(ComplaintStatusVM complaintStatusVM)
        {
            await _complaintStatus.AddComplaintStatus(complaintStatusVM);
            return Ok();
        }

        [HttpGet("takeall")]
        public async Task<IActionResult> TakeAll()
        {
            var result = await _complaintStatus.TakeComplaintStatuses();
            if (result != null)
                return Ok(result);
            else
                return NotFound();
        }

        [HttpGet("takebyid")]
        public async Task<IActionResult> TakeById(Guid id)
        {
            var result = await _complaintStatus.TakeComplaintStatusById(id);
            if (result != null)
                return Ok(result);
            else
                return NotFound();
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _complaintStatus.DeleteComplaintStatus(id);
            return Ok();
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update(ComplaintStatusVM complaintStatusVM)
        {
            await _complaintStatus.UpdateComplaintStatus(complaintStatusVM);
            return Ok();
        }
    }
}
