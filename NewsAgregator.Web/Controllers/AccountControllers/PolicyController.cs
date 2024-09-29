using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsAgregator.Abstract.AccountInterfaces;
using NewsAgregator.Abstract.ComplaintInterfaces;
using NewsAgregator.ViewModels.Data;
using NewsAgregator.Web.Filters;

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
        [Authorize]
        public async Task<IActionResult> Add(PolicyVM policyVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _policyServices.AddPolicyAsync(policyVM);
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
        //[Authorize]
        //[PolicyAuthorization(Permission = "CanReadPolicy")]
        public async Task<IActionResult> TakeAll()
        {
            try
            {
                var result = await _policyServices.TakePoliciesAsync();
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
        //[Authorize]
        //[PolicyAuthorization(Permission = "CanReadPolicy")]
        public async Task<IActionResult> TakeById([FromRoute] Guid id)
        {
            try
            {
                var result = await _policyServices.TakePolicyByIdAsync(id);
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
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            try
            {
                await _policyServices.DeletePolicyAsync(id);
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
                if (ModelState.IsValid)
                {
                    await _policyServices.UpdatePolicyAsync(policyVM);
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
