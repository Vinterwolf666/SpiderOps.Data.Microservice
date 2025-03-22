using Customer.Notify.Microservice.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Notify.Microservice.APP
{
    public class NotifyService : INotifyServices
    {
        private readonly INotifyRepository _repository;
        public NotifyService(INotifyRepository repository)
        {

            _repository = repository;

        }
        public List<Notification> AllNotifications()
        {
            var result = _repository.AllNotifications();

            return result;
        }

        public List<Notification> AllNotificationsByID(int id)
        {
            var result = _repository.AllNotificationsByID(id);

            return result;
        }

        public async Task<string> DeleteByID(int id)
        {
            var result = await _repository.DeleteByID(id);

            return result;
        }

        public async Task<string> SendAccountCreationNotification(string email, string text_message, int id)
        {
            var result = await _repository.SendAccountCreationNotification(email,text_message,id);

            return result;
        }

        public async Task<string> SendAccountRemovedNotification(string email, string text_message, int id)
        {
            var result = await _repository.SendAccountRemovedNotification(email,text_message,id);

            return result;
        }

        public async Task<string> SendDeploymentNotification(string email, string text_message, int id)
        {
            var result = await _repository.SendDeploymentNotification(email,text_message,id);

            return result;
        }

        public async Task<string> SendInfrastructureNotification(string email, string text_message, int id)
        {
            var result = await _repository.SendInfrastructureNotification(email,text_message,id);
            return result;
        }

        public async Task<string> SendRecoveyPassNotification(string email, string textMessage, int id)
        {
            var result = await _repository.SendRecoveyPassNotification(email,textMessage,id);

            return result;
        }

        public async Task<string> SendRemovedDeploymentNotification(string email, string text_message, int id)
        {
            var result = await _repository.SendRemovedDeploymentNotification(email,text_message,id);
            return result;
        }

        public async Task<string> SendUpdateAccountNotification(string email, string text_message, int id)
        {
            var result = await _repository.SendUpdateAccountNotification(email,text_message,id);
            return result;
        }
    }
}
