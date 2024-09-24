using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using NewsAgregator.Abstract.AccountInterfaces;
using NewsAgregator.Services.AccountServices;
using NewsAgregator.ViewModels.Additional;
using NewsAgregator.ViewModels.Data;

namespace NewsAgregator.Web.Controllers.AccountControllers
{
    [Route("/api/authorization")]
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
        public async Task<IActionResult> SignIn([FromBody]SignInModel signINModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (!await _accountServices.CheckLoginPasswordAsync(signINModel.Login, signINModel.Password))
                        return Unauthorized("Wrong Login or Password!");
                    var accountId = await _accountServices.TakeAccountIdByLoginAsync(signINModel.Login);
                    
                    if (!accountId.HasValue)
                        return BadRequest();
                    var account = await _accountServices.TakeAccountByIdAsync(accountId.Value);
                    var tokens = await GenerateTokenPair(accountId.Value);
                    return Ok(new { atoken = tokens.Item1, rtoken = tokens.Item2, userName = account.UserName, role = account.Role?.Text });
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
        public async Task<IActionResult> SignUp([FromBody]CreateAccountVM accountVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (!await _accountServices.CheckIsLoginRegisteredAsync(accountVM.Login))
                    {
                        await _accountServices.AddAccountAsync(accountVM);
                        return Ok();
                    }
                    else return BadRequest("This Login exists");
                    
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
        public async Task<IActionResult> SignOut([FromBody]Guid tokenId)
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

        [HttpPost("/refresh")]
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