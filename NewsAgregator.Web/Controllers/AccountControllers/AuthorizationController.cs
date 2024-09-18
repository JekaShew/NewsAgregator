using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using NewsAgregator.Abstract.AccountInterfaces;
using NewsAgregator.Services.AccountServices;
using NewsAgregator.ViewModels.Data;

namespace NewsAgregator.Web.Controllers.AccountControllers
{
    public class AuthorizationController : Controller
    { 
    private readonly IAccountServices _accountServices;
    private readonly IAccountAuthorizationServices _authorizationServices;
    private readonly ILogger<AccountController> _logger;

    public AuthorizationController(IAccountServices accountServices, IAccountAuthorizationServices authorizationServices, ILogger<AccountController> logger)
    {
        _accountServices = accountServices;
        _authorizationServices = authorizationServices;
        _logger = logger; 
    }

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn(string login, string password)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (!await _accountServices.CheckLoginPasswordAsync(login, password))
                        return Unauthorized("Wrong Login or Password!");
                    var accountId = await _accountServices.TakeAccountIdByLoginAsync(login);
                    
                    if (!accountId.HasValue)
                        return BadRequest();
                    var tokens = await GenerateTokenPair(accountId.Value);
                    return Ok(new { AccessToken = tokens.Item1, RefreshToken = tokens.Item2 });
                }
                else return BadRequest("Invalid Data.");

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
                else return BadRequest("Invalid Data.");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }

        }

        [HttpPost("signout")]
        public async Task<IActionResult> SignOut(Guid tokenId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _authorizationServices.RemoveRTokenByIdAsync(tokenId);
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

        [HttpPost("/refresh/")]
        public async Task<IActionResult> Refresh([FromBody] Guid refreshTokenId)
        {
            if (await _authorizationServices.RefreshTokenCorrect(refreshTokenId))
            {
                var accountVM = await _accountServices.TakeAccountByRTokenIdAsync(refreshTokenId);
                if (accountVM != null)
                {
                    var tokens = await GenerateTokenPair(accountVM.Id);

                    await _authorizationServices.RemoveRTokenByIdAsync(refreshTokenId);

                    return Ok(new { AccessToken = tokens.Item1, refreshToken = tokens.Item2 });
                }
            }
            return NotFound();
        }

        [HttpPatch("/revoke/{id}")]
        public async Task<IActionResult> RevokeTokenById([FromRoute] Guid refreshTokenId)
        {
            if (await _authorizationServices.RefreshTokenCorrect(refreshTokenId))
            {
                await _authorizationServices.RevokeTokenByIdAsync(refreshTokenId);
                return Ok("Success!");
            }
            else
                return BadRequest("Incorrect RToken!");
        }

        private async Task<(string, string)> GenerateTokenPair(Guid userId)
        {
            var jwt = await _authorizationServices.GenerateJwtTokenString(userId);
            var rt = await _authorizationServices.GenerateRefreshToken(userId);
            return (jwt, rt);
        }



    }
}