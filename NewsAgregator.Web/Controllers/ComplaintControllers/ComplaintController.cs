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

        [HttpGet("getfullparameters")]
        public async Task<IActionResult> GetFullParameters()
        {
            try
            {
                var result = await _complaintServices.GetComplaintParametersAsync();

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }

        }

        [HttpGet("getparameters")]
        public async Task<IActionResult> GetParameters()
        {
            try
            {
                var result = await _complaintServices.GetComplaintParametersAsync();

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
                if (ModelState.IsValid)
                {
                    await _complaintServices.AddComplaintAsync(complaintVM);
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

        [HttpPost("sendcomplaint")]
        public async Task<IActionResult> SendComplaint([FromBody]ComplaintVM complaintVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _complaintServices.AddComplaintAsync(complaintVM);
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
                var result = await _complaintServices.TakeComplaintsAsync();
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
                var result = await _complaintServices.TakeComplaintByIdAsync(id);
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
                await _complaintServices.DeleteComplaintAsync(id);
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
                if (ModelState.IsValid)
                {
                    await _complaintServices.UpdateComplaintAsync(complaintVM);
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
