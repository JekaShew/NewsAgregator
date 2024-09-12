using Microsoft.AspNetCore.Mvc;
using NewsAgregator.Abstract.ComplaintInterfaces;
using NewsAgregator.Abstract.MessageInterfaces;
using NewsAgregator.ViewModels.Data;
using NewsAgregator.Web.Controllers.AccountControllers;

namespace NewsAgregator.Web.Controllers.MessageControllers
{
    [Route("/api/notificationmessage")]
    public class NotificationMessageController : Controller
    {
        private readonly INotificationMessageServices _notificationMessageServices;
        private readonly ILogger<NotificationMessageController> _logger;

        public NotificationMessageController(INotificationMessageServices notificationMessageServices, ILogger<NotificationMessageController> logger)
        {
            _notificationMessageServices = notificationMessageServices;
            _logger = logger;
        }

        [HttpGet("getparameters")]
        public async Task<IActionResult> GetParameters()
        {
            try
            {
                var result = await _notificationMessageServices.GetNotificationMessageParametersAsync();

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(NotificationMessageVM notificationMessageVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _notificationMessageServices.AddNotificationMessageAsync(notificationMessageVM);
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
                var result = await _notificationMessageServices.TakeNotificationMessagesAsync();
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
                var result = await _notificationMessageServices.TakeNotificationMessageByIdAsync(id);
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
                await _notificationMessageServices.DeleteNotificationMessageAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update(NotificationMessageVM notificationMessageVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _notificationMessageServices.UpdateNotificationMessageAsync(notificationMessageVM);
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
