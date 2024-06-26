﻿using Microsoft.AspNetCore.Mvc;
using NewsAgregator.Abstract.AccountInterfaces;
using NewsAgregator.ViewModels.Data;

namespace NewsAgregator.Web.Controllers.AccountControllers
{
    [Route("/api/account")]
    public class AccountController : Controller
    {
        private readonly IAccountServices _accountServices;

        public AccountController(IAccountServices accountServices)
        {
            _accountServices = accountServices;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(AccountVM accountVM)
        {
            await _accountServices.AddAccount(accountVM);
            return Ok();
        }

        [HttpGet("takeall")]
        public async Task<IActionResult> TakeAll()
        {
            var result = await _accountServices.TakeAccounts();
            if (result != null)
                return Ok(result);
            else
                return NotFound();
        }

        [HttpGet("takebyid")]
        public async Task<IActionResult> TakeById(Guid id)
        {
            var result = await _accountServices.TakeAccountById(id);
            if (result != null)
                return Ok(result);
            else
                return NotFound();
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _accountServices.DeleteAccount(id);
            return Ok();
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update(AccountVM accountVM)
        {
            await _accountServices.UpdateAccount(accountVM);
            return Ok();
        }

    }
}
