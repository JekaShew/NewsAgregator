using Microsoft.AspNetCore.Mvc;
using NewsAgregator.Abstract.AccountInterfaces;
using NewsAgregator.ViewModels.Data;

namespace NewsAgregator.Web.Controllers.AccountControllers
{
    [Route("/api/account")]
    public class AccountController : Controller
    {
        private readonly IAccountServices _accountServices;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IAccountServices accountServices, ILogger<AccountController> logger)
        {
            _accountServices = accountServices;
            _logger = logger;
        }

        [HttpGet("getparameters")]
        public async Task<IActionResult> GetParameters()
        {
            try {
                var result = await _accountServices.GetAccountParameters();

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }

        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(AccountVM accountVM)
        {
            try {
                if (ModelState.IsValid)
                {
                    await _accountServices.AddAccount(accountVM);
                    return Ok();
                }
                else return BadRequest(ModelState);
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500,ex.Message);
            }
            
        }

        [HttpGet("takeall")]
        public async Task<IActionResult> TakeAll()
        {
            try {
                var result = await _accountServices.TakeAccounts();
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
        public async Task<IActionResult> TakeById([FromRoute] Guid id)
        {
            try {
                var result = await _accountServices.TakeAccountById(id);
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
            try {
                await _accountServices.DeleteAccount(id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }

        }

        [HttpPost("update")]
        public async Task<IActionResult> Update(AccountVM accountVM)
        {
            try {
                if (ModelState.IsValid)
                {
                    await _accountServices.UpdateAccount(accountVM);
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
