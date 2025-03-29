using Customer.Notify.Microservice.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Microservice.APP
{
    public interface INotifyRepository
    {
        Task<List<Notification>> AllNotificationsAsync();
        Task<List<Notification>> AllNotificationsByIDAsync(int id);
        Task<bool> DeleteByIDAsync(int id);
        Task AddNotificationAsync(Notification notification);
    }
}
