using Customer.Notify.Microservice.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Notify.Microservice.APP
{
    public interface INotifyRepository
    {
        List<Notification> AllNotifications();

        List<Notification> AllNotificationsByID(int id);

        Task<string> SendAccountCreationNotification(string email, string text_message, int id);

        Task<string> SendRecoveyPassNotification(string email, string textMessage, int id);


        Task<string> SendDeploymentNotification(string email, string text_message, int id);

        Task<string> SendRemovedDeploymentNotification(string email, string text_message, int id);

        Task<string> SendAccountRemovedNotification(string email, string text_message, int id);

        Task<string> SendUpdateAccountNotification(string email, string text_message, int id);

        Task<string> SendInfrastructureNotification(string email, string text_message, int id);
        Task<string> DeleteByID(int id);
    }
}
