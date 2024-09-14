using Microsoft.AspNetCore.Mvc;
using NewsAgregator.Abstract.AccountInterfaces;
using NewsAgregator.Abstract.WeatherInterfaces;
using NewsAgregator.ViewModels.Data;

namespace NewsAgregator.Web.Controllers.AccountControllers
{
    [Route("/api/accountstatus")]
    public class AccountStatusController : Controller
    {
        private readonly IAccountStatusServices _accountStatusServices;
        private readonly ILogger<AccountStatusController> _logger;

        public AccountStatusController(IAccountStatusServices accountStatusServices, ILogger<AccountStatusController> logger)
        {
            _accountStatusServices = accountStatusServices;
            _logger = logger;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromForm]AccountStatusVM accountStatusVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _accountStatusServices.AddAccountStatusAsync(accountStatusVM);
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
                var result = await _accountStatusServices.TakeAccountStatusesAsync();
                if (result.Count != 0)
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
                var result = await _accountStatusServices.TakeAccountStatusByIdAsync(id);
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
                await _accountStatusServices.DeleteAccountStatusAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }

        }

        [HttpPost("update")]
        public async Task<IActionResult> Update(AccountStatusVM accountStatusVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _accountStatusServices.UpdateAccountStatusAsync(accountStatusVM);
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
