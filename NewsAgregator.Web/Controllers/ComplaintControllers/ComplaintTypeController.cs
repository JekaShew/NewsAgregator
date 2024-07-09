﻿using Microsoft.AspNetCore.Mvc;
using NewsAgregator.Abstract.ComplaintInterfaces;
using NewsAgregator.Data.Models;
using NewsAgregator.ViewModels.Data;
using NewsAgregator.Web.Controllers.AccountControllers;

namespace NewsAgregator.Web.Controllers.ComplaintControllers
{
    [Route("/api/complainttype")]
    public class ComplaintTypeController : Controller
    {
        private readonly IComplaintTypeServices _complaintTypeServices;
        private readonly ILogger<ComplaintTypeController> _logger;

        public ComplaintTypeController(IComplaintTypeServices complaintTypeServices, ILogger<ComplaintTypeController> logger)
        {
            _complaintTypeServices = complaintTypeServices;
            _logger = logger;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(ComplaintTypeVM complaintTypeVM)
        {
            try
            {
                await _complaintTypeServices.AddComplaintType(complaintTypeVM);
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
                var result = await _complaintTypeServices.TakeComplaintTypes();
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
                var result = await _complaintTypeServices.TakeComplaintTypeById(id);
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
                await _complaintTypeServices.DeleteComplaintType(id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }

        }

        [HttpPost("update")]
        public async Task<IActionResult> Update(ComplaintTypeVM complaintTypeVM)
        {
            try
            {
                await _complaintTypeServices.UpdateComplaintType(complaintTypeVM);
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
