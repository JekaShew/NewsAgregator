using Microsoft.AspNetCore.Mvc;
using NewsAgregator.Abstract.AccountInterfaces;
using NewsAgregator.ViewModels.Data;

namespace NewsAgregator.Web.Controllers.AccountControllers
{
    public class AuthorizationController : Controller
    { 
    private readonly IAccountServices _accountServices;
    private readonly IAuthorizationServices _authorizationServices;
    private readonly ILogger<AccountController> _logger;

    public AuthorizationController(IAccountServices accountServices, IAuthorizationServices authorizationServices, ILogger<AccountController> logger)
    {
        _accountServices = accountServices;
        _authorizationServices = authorizationServices;
        _logger = logger; 
    }

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn(CreateAccountVM accountVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _accountServices.AddAccountAsync(accountVM);
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

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp(CreateAccountVM accountVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _accountServices.AddAccountAsync(accountVM);
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

        [HttpPost("signout")]
        public async Task<IActionResult> SignOut(CreateAccountVM accountVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _accountServices.AddAccountAsync(accountVM);
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