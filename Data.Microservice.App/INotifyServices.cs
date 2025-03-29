using Customer.Notify.Microservice.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Notify.Microservice.APP
{
    public interface INotifyServices
    {
        Task<string> SendNotification(string email, string subject, string message, int customerId);

        string SendEmail(string to, string subject, string body, string returnMessage);

        Task<List<Notification>> AllNotificationsAsync();

        Task<List<Notification>> AllNotificationsByIDAsync(int id);

        Task<bool> DeleteByIDAsync(int id);
    }
}
