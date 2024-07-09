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
        private readonly ILogger<RoleController> _logger;

        public RoleController(IRoleServices roleServices, ILogger<RoleController> logger)
        {
            _roleServices = roleServices;
            _logger = logger;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(RoleVM roleVM)
        {
            try
            {
                await _roleServices.AddRole(roleVM);
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
                var result = await _roleServices.TakeRoles();
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

        [HttpGet("takebyid")]
        public async Task<IActionResult> TakeById(Guid id)
        {
            try {
                var result = await _roleServices.TakeRoleById(id);
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

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _roleServices.DeleteRole(id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update(RoleVM roleVM)
        {
            try
            {
                await _roleServices.UpdateRole(roleVM);
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
