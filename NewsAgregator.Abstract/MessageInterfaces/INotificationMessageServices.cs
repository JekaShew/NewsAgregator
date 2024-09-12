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
        public Task<List<NotificationMessageVM>> TakeNotificationMessagesAsync();

        public Task<NotificationMessageVM> TakeNotificationMessageByIdAsync(Guid id);

        public Task AddNotificationMessageAsync(NotificationMessageVM notificationMessage);

        public Task DeleteNotificationMessageAsync(Guid id);

        public Task UpdateNotificationMessageAsync(NotificationMessageVM updatedNotificationMessage);
        public Task<NotificationMessageParameters> GetNotificationMessageParametersAsync();
    }
}
