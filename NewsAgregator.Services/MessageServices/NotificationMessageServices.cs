using AutoMapper;
using Mapper.Mappers.ParametersMappers;
using Microsoft.EntityFrameworkCore;
using NewsAgregator.Abstract.MessageInterfaces;
using NewsAgregator.Data;
using NewsAgregator.Data.Mappers;
using NewsAgregator.Data.Models;
using NewsAgregator.ViewModels.Additional;
using NewsAgregator.ViewModels.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAgregator.Services.MessageServices
{
    public class NotificationMessageServices : INotificationMessageServices
    {
        private readonly AppDBContext _appDBContext;
        public NotificationMessageServices(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }

        public async Task<NotificationMessageParameters> GetNotificationMessageParametersAsync()
        {
            var notificationMessageParameters = new NotificationMessageParameters()
            {
                Accounts = (await _appDBContext.Accounts.ToListAsync()).Select(a => NotificationParametersMapper.AccountToParameter(a)).ToList(),
                
            };
            return notificationMessageParameters;

        }

        public async Task AddNotificationMessageAsync(NotificationMessageVM notificationMessage)
        {
            var newNotificationMessage = NotificationMessageMapper.NotificationMessageVMToNotificationMessage(notificationMessage);
            newNotificationMessage.Id = Guid.NewGuid();

            await _appDBContext.AddAsync(newNotificationMessage);
            await _appDBContext.SaveChangesAsync();
        }

        public async Task DeleteNotificationMessageAsync(Guid id)
        {
            _appDBContext.NotificationMessages.Remove(await _appDBContext.NotificationMessages.FirstOrDefaultAsync(nm => nm.Id == id));
            await _appDBContext.SaveChangesAsync();
        }

        public async Task<NotificationMessageVM> TakeNotificationMessageByIdAsync(Guid id)
        {
            var notificationMessageVM = NotificationMessageMapper.NotificationMessageToNotificationMessageVM(await _appDBContext.NotificationMessages
                .AsNoTracking()
                .Include(u => u.User)
                .Include(a => a.Administrator)  
                .FirstOrDefaultAsync(nm => nm.Id == id));

            var notificationMessageParameters = await GetNotificationMessageParametersAsync();
            notificationMessageVM.Accounts = notificationMessageParameters.Accounts;

            return notificationMessageVM;
        }

        public async Task<List<NotificationMessageVM>> TakeNotificationMessagesAsync()
        {
            var notificationMessageVMs = (await _appDBContext.NotificationMessages
                .AsNoTracking()
                .Include(u => u.User)
                .Include(a => a.Administrator)
                .ToListAsync()).Select(nm=> NotificationMessageMapper.NotificationMessageToNotificationMessageVM(nm)).ToList();

            return notificationMessageVMs;
        }

        public async Task UpdateNotificationMessageAsync(NotificationMessageVM updatedNotificationMessageVM)
        {
            var notificationMessage = await _appDBContext.NotificationMessages.FirstOrDefaultAsync(nm => nm.Id == updatedNotificationMessageVM.Id);

            if (notificationMessage != null)
            {
                notificationMessage.Title = updatedNotificationMessageVM.Title;
                notificationMessage.Text = updatedNotificationMessageVM.Text;
                notificationMessage.UserId = updatedNotificationMessageVM.UserId;
                notificationMessage.AdministratorId = updatedNotificationMessageVM.AdministratorId;

                await _appDBContext.SaveChangesAsync();
            }
            else return;
        }
    }
}
