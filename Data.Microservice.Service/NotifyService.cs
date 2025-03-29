
using Customer.Notify.Microservice.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Data.Microservice.APP;
using Customer.Notify.Microservice.APP;

namespace Data.Microservice.Service
{
    public class NotifyService : INotifyServices
    {
        private readonly INotifyRepository _repository;
        public NotifyService(INotifyRepository repository)
        {

            _repository = repository;

        }
        public async Task<string> SendNotification(string email, string subject, string message, int customerId)
        {
            var notification = new Notification
            {
                EMAIL = email,
                CUSTOMER_ID = customerId,
                TIME_OF_CREATION = TimeZoneInfo.ConvertTime(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time")),
                EMAIL_SUBJECT = subject,
                TEXT_MESSAGE = message
            };
            await _repository.AddNotificationAsync(notification);
            var result = SendEmail(email, subject, message, "Notification sent successfully");
            return result;
        }

        public string SendEmail(string to, string subject, string body, string returnMessage)
        {
            try
            {
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("vinterwolf666@gmail.com", "ctpj vlad fulr ortq"),
                    EnableSsl = true,
                };

                MailMessage mail = new MailMessage
                {
                    From = new MailAddress("vinterwolf666@gmail.com"),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };

                mail.To.Add(to);
                smtpClient.Send(mail);

                return returnMessage;
            }
            catch (Exception ex)
            {
                return $"Error sending email: {ex.Message}";
            }
        }


        public async Task<List<Notification>> AllNotificationsAsync()
        {
            var result = await _repository.AllNotificationsAsync();

            return result;
        }


        public Task<List<Notification>> AllNotificationsByIDAsync(int id)
        {
            var result = _repository.AllNotificationsByIDAsync(id);

            return result;
        }


        public async Task<bool> DeleteByIDAsync(int id)
        {
            var result = await _repository.DeleteByIDAsync(id);

            return result;
        }


    }
}
