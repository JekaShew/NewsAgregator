using Microsoft.AspNetCore.Mvc;
using NewsAgregator.Abstract.AccountInterfaces;
using NewsAgregator.Abstract.ComplaintInterfaces;
using NewsAgregator.ViewModels.Data;
using NewsAgregator.Web.Controllers.AccountControllers;

namespace NewsAgregator.Web.Controllers.ComplaintControllers
{
    [Route("/api/complaintstatus")]
    public class ComplaintStatusController : Controller
    {
        private readonly IComplaintStatusServices _complaintStatus;
        private readonly ILogger<ComplaintStatusController> _logger;

        public ComplaintStatusController(IComplaintStatusServices complaintStatusServices, ILogger<ComplaintStatusController> logger)
        {
            _complaintStatus = complaintStatusServices;
            _logger = logger;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(ComplaintStatusVM complaintStatusVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _complaintStatus.AddComplaintStatus(complaintStatusVM);
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
                var result = await _complaintStatus.TakeComplaintStatuses();
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
                var result = await _complaintStatus.TakeComplaintStatusById(id);
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
                await _complaintStatus.DeleteComplaintStatus(id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }

        }

        [HttpPost("update")]
        public async Task<IActionResult> Update(ComplaintStatusVM complaintStatusVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _complaintStatus.UpdateComplaintStatus(complaintStatusVM);
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
