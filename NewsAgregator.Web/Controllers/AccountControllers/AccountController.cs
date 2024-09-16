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
            try
            {
                var result = await _accountServices.GetAccountParametersAsync();

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }

        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(CreateAccountVM createAccountVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _accountServices.AddAccountAsync(createAccountVM);
                    return Ok("Success!");
                }
                else return BadRequest("Invalid Data.");

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
                var result = await _accountServices.TakeAccountsAsync();
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
            try
            {
                var result = await _accountServices.TakeAccountByIdAsync(id);
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
                await _accountServices.DeleteAccountAsync(id);
                return Ok("Success!");
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
            try
            {
                if (ModelState.IsValid)
                {
                    await _accountServices.UpdateAccountAsync(accountVM);
                    return Ok("Success!");
                }
                else return BadRequest("Invalid Data.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("checkisloginregistered")]
        public async Task<IActionResult> CheckIsLoginRegistered(string login)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if(await _accountServices.CheckIsLoginRegisteredAsync(login))
                    return BadRequest("The Login already exists.");
                    else return Ok("Success!");
                }
                else return BadRequest("Invalid Data.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("canchangepassword")]
        public async Task<IActionResult> CanChangePassword(string login, string password)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (await _accountServices.CanChangePasswordAsync(login, password))
                        return Ok("Success!");
                    else
                        return BadRequest("Incorrect Login or Password!");
                }
                else return BadRequest("Invalid Data.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("canchangeforgottenpassword")]
        public async Task<IActionResult> CanChangeForgottenPassword(string login, string secretWord)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (await _accountServices.CanChangeForgottenPasswordAsync(login, secretWord))
                        return Ok("Success!");
                    else
                        return BadRequest("Incorrect Login or Secret Word.");
                }
                else return BadRequest("Invalid Data.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("changepassword")]
        public async Task<IActionResult> ChangePassword(string password)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _accountServices.ChangePasswordAsync(password);
                    return Ok("Success!");
                }
                else return BadRequest("Invalid Data.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }



    }
}
