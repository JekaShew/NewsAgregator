using Microsoft.AspNetCore.Mvc;
using NewsAgregator.Abstract.AccountInterfaces;
using NewsAgregator.Abstract.ComplaintInterfaces;
using NewsAgregator.Services.AccountServices;
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

        [HttpGet("getparameters")]
        public async Task<IActionResult> GetParameters()
        {
            try
            {
                var result = await _roleServices.GetRoleParametersAsync();

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }

        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(RoleVM roleVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _roleServices.AddRoleAsync(roleVM);
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
                var result = await _roleServices.TakeRolesAsync();
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
                var result = await _roleServices.TakeRoleByIdAsync(id);
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
                await _roleServices.DeleteRoleAsync(id);
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
                if (ModelState.IsValid)
                {
                    await _roleServices.UpdateRoleAsync(roleVM);
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
