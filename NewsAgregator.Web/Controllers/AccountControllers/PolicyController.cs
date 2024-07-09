using Microsoft.AspNetCore.Mvc;
using NewsAgregator.Abstract.AccountInterfaces;
using NewsAgregator.Abstract.ComplaintInterfaces;
using NewsAgregator.ViewModels.Data;

namespace NewsAgregator.Web.Controllers.AccountControllers
{
    [Route("/api/policy")]
    public class PolicyController : Controller
    {
        private readonly IPolicyServices _policyServices;
        private readonly ILogger<PolicyController> _logger;

        public PolicyController(IPolicyServices policyServices, ILogger<PolicyController> logger)
        {
            _policyServices = policyServices;
            _logger = logger;   
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(PolicyVM policyVM)
        {
            try
            {
                await _policyServices.AddPolicy(policyVM);
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
                var result = await _policyServices.TakePolicies();
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
        public async Task<IActionResult> TakeById([FromRoute]Guid id)
        {
            try
            {
                var result = await _policyServices.TakePolicyById(id);
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
        public async Task<IActionResult> Delete([FromRoute]Guid id)
        {
            try
            {
                await _policyServices.DeletePolicy(id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }

        }

        [HttpPost("update")]
        public async Task<IActionResult> Update(PolicyVM policyVM)
        {
            try
            {
                await _policyServices.UpdatePolicy(policyVM);
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
