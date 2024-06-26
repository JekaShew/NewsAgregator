﻿using Microsoft.AspNetCore.Mvc;
using NewsAgregator.Abstract.AccountInterfaces;
using NewsAgregator.Abstract.WeatherInterfaces;
using NewsAgregator.ViewModels.Data;

namespace NewsAgregator.Web.Controllers.AccountControllers
{
    [Route("/api/accountstatus")]
    public class AccountStatusController : Controller
    {
        private readonly IAccountStatusServices _accountStatusServices;

        public AccountStatusController(IAccountStatusServices accountStatusServices)
        {
            _accountStatusServices = accountStatusServices;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(AccountStatusVM accountStatusVM)
        {
            await _accountStatusServices.AddAccountStatus(accountStatusVM);
            return Ok();
        }

        [HttpGet("takeall")]
        public async Task<IActionResult> TakeAll()
        {
            var result = await _accountStatusServices.TakeAccountStatuses();
            if (result != null)
                return Ok(result);
            else
                return NotFound();
        }

        [HttpGet("takebyid")]
        public async Task<IActionResult> TakeById(Guid id)
        {
            var result = await _accountStatusServices.TakeAccountStatusById(id);
            if (result != null)
                return Ok(result);
            else
                return NotFound();
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _accountStatusServices.DeleteAccountStatus(id);
            return Ok();
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update(AccountStatusVM accountStatusVM)
        {
            await _accountStatusServices.UpdateAccountStatus(accountStatusVM);
            return Ok();
        }

    }
}
