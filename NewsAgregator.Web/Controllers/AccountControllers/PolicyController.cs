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

        public PolicyController(IPolicyServices policyServices)
        {
            _policyServices = policyServices;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(PolicyVM policyVM)
        {
            await _policyServices.AddPolicy(policyVM);
            return Ok();
        }

        [HttpGet("takeall")]
        public async Task<IActionResult> TakeAll()
        {
            var result = await _policyServices.TakePolicies();
            if (result != null)
                return Ok(result);
            else
                return NotFound();
        }

        [HttpGet("takebyid")]
        public async Task<IActionResult> TakeById(Guid id)
        {
            var result = await _policyServices.TakePolicyById(id);
            if (result != null)
                return Ok(result);
            else
                return NotFound();
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _policyServices.DeletePolicy(id);
            return Ok();
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update(PolicyVM policyVM)
        {
            await _policyServices.UpdatePolicy(policyVM);
            return Ok();
        }
    }
}
