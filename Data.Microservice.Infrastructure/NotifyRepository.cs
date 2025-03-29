using Customer.Notify.Microservice.APP;
using Customer.Notify.Microservice.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Data.Microservice.APP;
using Customer.Notify.Microservice.Infrastructure;

namespace   Data.Microservice.Infrastructure
{
    public class NotifyRepository : INotifyRepository
    {

        private readonly NotifyDBContext _dbContext;
        public NotifyRepository(NotifyDBContext dbContext)
        {

            _dbContext = dbContext;

        }
        public async Task<List<Notification>> AllNotificationsAsync()
        {
            return await _dbContext.NotifyDBC.ToListAsync();
        }

        public async Task<List<Notification>> AllNotificationsByIDAsync(int id)
        {
            return await _dbContext.NotifyDBC.Where(a => a.CUSTOMER_ID == id).ToListAsync();
        }

        public async Task<bool> DeleteByIDAsync(int id)
        {
            var notification = await _dbContext.NotifyDBC.FindAsync(id);
            if (notification != null)
            {
                _dbContext.NotifyDBC.Remove(notification);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task AddNotificationAsync(Notification notification)
        {
            await _dbContext.NotifyDBC.AddAsync(notification);
            await _dbContext.SaveChangesAsync();
        }


        

    }
}
