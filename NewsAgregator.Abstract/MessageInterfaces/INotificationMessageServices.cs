using NewsAgregator.ViewModels.Additional;
using NewsAgregator.ViewModels.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAgregator.Abstract.MessageInterfaces
{
    public interface INotificationMessageServices
    {
        public Task<List<NotificationMessageVM>> TakeNotificationMessages();

        public Task<NotificationMessageVM> TakeNotificationMessageById(Guid id);

        public Task AddNotificationMessage(NotificationMessageVM notificationMessage);

        public Task DeleteNotificationMessage(Guid id);

        public Task UpdateNotificationMessage(NotificationMessageVM updatedNotificationMessage);
        public Task<NotificationMessageParameters> GetNotificationMessageParameters();
    }
}
