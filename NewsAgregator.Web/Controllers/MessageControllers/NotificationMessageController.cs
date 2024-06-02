using Microsoft.AspNetCore.Mvc;
using NewsAgregator.Abstract.ComplaintInterfaces;
using NewsAgregator.Abstract.MessageInterfaces;
using NewsAgregator.ViewModels.Data;

namespace NewsAgregator.Web.Controllers.MessageControllers
{
    [Route("/api/notificationmessage")]
    public class NotificationMessageController : Controller
    {
        private readonly INotificationMessageServices _notificationMessageServices;

        public NotificationMessageController(INotificationMessageServices notificationMessageServices)
        {
            _notificationMessageServices = notificationMessageServices;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(NotificationMessageVM notificationMessageVM)
        {
            await _notificationMessageServices.AddNotificationMessage(notificationMessageVM);
            return Ok();
        }

        [HttpGet("takeall")]
        public async Task<IActionResult> TakeAll()
        {
            var result = await _notificationMessageServices.TakeNotificationMessages();
            if (result != null)
                return Ok(result);
            else
                return NotFound();
        }

        [HttpGet("takebyid")]
        public async Task<IActionResult> TakeById(Guid id)
        {
            var result = await _notificationMessageServices.TakeNotificationMessageById(id);
            if (result != null)
                return Ok(result);
            else
                return NotFound();
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _notificationMessageServices.DeleteNotificationMessage(id);
            return Ok();
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update(NotificationMessageVM notificationMessageVM)
        {
            await _notificationMessageServices.UpdateNotificationMessage(notificationMessageVM);
            return Ok();
        }
    }
}
