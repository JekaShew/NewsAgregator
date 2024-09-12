using NewsAgregator.Data.Models;
using NewsAgregator.ViewModels.Data;
using Riok.Mapperly.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAgregator.Data.Mappers
{
    [Mapper]
    public static partial class NotificationMessageMapper
    {
        [MapProperty([nameof(NotificationMessage.User), nameof(NotificationMessage.User.Id)],
            [nameof(NotificationMessageVM.User), nameof(NotificationMessageVM.User.Id)])]
        [MapProperty([nameof(NotificationMessage.User), nameof(NotificationMessage.User.UserName)],
            [nameof(NotificationMessageVM.User), nameof(NotificationMessageVM.User.Text)])]

        [MapProperty([nameof(NotificationMessage.Administrator), nameof(NotificationMessage.Administrator.Id)],
            [nameof(NotificationMessageVM.Administrator), nameof(NotificationMessageVM.Administrator.Id)])]
        [MapProperty([nameof(NotificationMessage.Administrator), nameof(NotificationMessage.Administrator.UserName)],
            [nameof(NotificationMessageVM.Administrator), nameof(NotificationMessageVM.Administrator.Text)])]
        public static partial NotificationMessageVM? NotificationMessageToNotificationMessageVM(NotificationMessage? notificationMessage);

        public static partial NotificationMessage? NotificationMessageVMToNotificationMessage(NotificationMessageVM? notificationMessageVM);

    }
}
