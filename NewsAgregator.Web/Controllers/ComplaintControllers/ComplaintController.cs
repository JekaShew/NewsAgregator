using Microsoft.AspNetCore.Mvc;
using NewsAgregator.Abstract.ComplaintInterfaces;
using NewsAgregator.ViewModels.Data;
using NewsAgregator.Web.Controllers.AccountControllers;

namespace NewsAgregator.Web.Controllers.ComplaintControllers
{
    [Route("/api/complaint")]
    public class ComplaintController : Controller
    {
        private readonly IComplaintServices _complaintServices;
        private readonly ILogger<ComplaintController> _logger;

        public ComplaintController(IComplaintServices complaintServices, ILogger<ComplaintController> logger)
        {
            _complaintServices = complaintServices;
            _logger = logger;
        }

        [HttpGet("getparameters")]
        public async Task<IActionResult> GetParameters()
        {
            try{
                var result = await _complaintServices.GetComplaintParameters();

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }

        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(ComplaintVM complaintVM)
        {
            try
            { 
                await _complaintServices.AddComplaint(complaintVM);
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
                var result = await _complaintServices.TakeComplaints();
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
                var result = await _complaintServices.TakeComplaintById(id);
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
                await _complaintServices.DeleteComplaint(id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }

        }

        [HttpPost("update")]
        public async Task<IActionResult> Update(ComplaintVM complaintVM)
        {
            try
            {
                await _complaintServices.UpdateComplaint(complaintVM);
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
