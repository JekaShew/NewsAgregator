using Microsoft.AspNetCore.Mvc;
using NewsAgregator.Abstract.AccountInterfaces;
using NewsAgregator.Abstract.ComplaintInterfaces;
using NewsAgregator.ViewModels.Data;

namespace NewsAgregator.Web.Controllers.AccountControllers
{
    [Route("/api/role")]
    public class RoleController : Controller
    {
        private readonly IRoleServices _roleServices;

        public RoleController(IRoleServices roleServices)
        {
            _roleServices = roleServices;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(RoleVM roleVM)
        {
            await _roleServices.AddRole(roleVM);
            return Ok();
        }

        [HttpGet("takeall")]
        public async Task<IActionResult> TakeAll()
        {
            var result = await _roleServices.TakeRoles();
            if (result != null)
                return Ok(result);
            else
                return NotFound();
        }

        [HttpGet("takebyid")]
        public async Task<IActionResult> TakeById(Guid id)
        {
            var result = await _roleServices.TakeRoleById(id);
            if (result != null)
                return Ok(result);
            else
                return NotFound();
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _roleServices.DeleteRole(id);
            return Ok();
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update(RoleVM roleVM)
        {
            await _roleServices.UpdateRole(roleVM);
            return Ok();
        }
    }
}
