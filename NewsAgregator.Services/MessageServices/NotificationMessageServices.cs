﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NewsAgregator.Abstract.MessageInterfaces;
using NewsAgregator.Data;
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
        private readonly IMapper _mapper;
        public NotificationMessageServices(AppDBContext appDBContext, IMapper mapper)
        {
            _appDBContext = appDBContext;
            _mapper = mapper;
        }

        public async Task<NotificationMessageParameters> GetNotificationMessageParameters()
        {
            var notificationMessageParameters = new NotificationMessageParameters()
            {
                Accounts = await _appDBContext.Accounts.Select(acc => new Parameter { Id = acc.Id, Text = acc.UserName }).ToListAsync(),
                
            };
            return notificationMessageParameters;

        }

        public async Task ConvertNotificationMessageParameters(NotificationMessageVM notificationMessageVM)
        {
            var user = await _appDBContext.Accounts
                        .AsNoTracking()
                        .FirstOrDefaultAsync(u => u.Id == notificationMessageVM.UserId);
            var administrator = await _appDBContext.Accounts
                        .AsNoTracking()
                        .FirstOrDefaultAsync(a => a.Id == notificationMessageVM.AdministratorId);
            notificationMessageVM.FromDataModel(user, administrator);

        }
        public async Task AddNotificationMessage(NotificationMessageVM notificationMessage)
        {
            var newNotificationMessage = _mapper.Map<Data.Models.NotificationMessage>(notificationMessage);
            newNotificationMessage.Id = Guid.NewGuid();
            newNotificationMessage.User = null;
            newNotificationMessage.Administrator = null;

            await _appDBContext.AddAsync(newNotificationMessage);
            await _appDBContext.SaveChangesAsync();
        }

        public async Task DeleteNotificationMessage(Guid id)
        {
            _appDBContext.NotificationMessages.Remove(await _appDBContext.NotificationMessages.FirstOrDefaultAsync(nm => nm.Id == id));
            await _appDBContext.SaveChangesAsync();
        }

        public async Task<NotificationMessageVM> TakeNotificationMessageById(Guid id)
        {
            var notificationMessage = _mapper.Map<NotificationMessageVM>(await _appDBContext.NotificationMessages
                .AsNoTracking()
                .Include(u => u.User)
                .Include(a => a.Administrator)  
                .FirstOrDefaultAsync(nm => nm.Id == id));

            var notificationMessageParameters = await GetNotificationMessageParameters();
            await ConvertNotificationMessageParameters(notificationMessage);
            notificationMessage.Accounts = notificationMessageParameters.Accounts;

            return notificationMessage;
        }

        public async Task<List<NotificationMessageVM>> TakeNotificationMessages()
        {
            var notificationMessageVMs = _mapper.Map<List<NotificationMessageVM>>(await _appDBContext.NotificationMessages
                .AsNoTracking()
                .Include(u => u.User)
                .Include(a => a.Administrator)
                .ToListAsync());

            foreach (var notifiactionMessageVM in notificationMessageVMs)
            {
                await ConvertNotificationMessageParameters(notifiactionMessageVM);
            }

            return notificationMessageVMs;
        }

        public async Task UpdateNotificationMessage(NotificationMessageVM updatedNotificationMessage)
        {
            var notificationMessage = await _appDBContext.NotificationMessages.FirstOrDefaultAsync(nm => nm.Id == updatedNotificationMessage.Id);

            if (notificationMessage != null)
            {
                notificationMessage.Title = updatedNotificationMessage.Title;
                notificationMessage.Text = updatedNotificationMessage.Text;
                notificationMessage.UserId = updatedNotificationMessage.UserId;
                notificationMessage.User = null;
                notificationMessage.AdministratorId = updatedNotificationMessage.AdministratorId;
                notificationMessage.Administrator = null;

                await _appDBContext.SaveChangesAsync();
            }
            else return;
        }
    }
}
