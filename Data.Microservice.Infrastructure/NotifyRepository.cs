using Customer.Notify.Microservice.APP;
using Customer.Notify.Microservice.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;


namespace Customer.Notify.Microservice.Infrastructure
{
    public class NotifyRepository : INotifyRepository
    {

        private readonly NotifyDBContext _dbContext;
        public NotifyRepository(NotifyDBContext dbContext)
        {

            _dbContext = dbContext;

        }
        public List<Notification> AllNotifications()
        {
            var result = _dbContext.NotifyDBC.ToList();

            return result;
        }

        public List<Notification> AllNotificationsByID(int id)
        {
            var result = _dbContext.NotifyDBC.Where(a=>a.ID == id).ToList();

            return result;

            
        }

        public async Task<string> DeleteByID(int id)
        {
            var customerID = _dbContext.NotifyDBC.Where(a=>a.ID == id).FirstOrDefault();

            if(customerID != null)
            {
                _dbContext.NotifyDBC.Remove(customerID);
                await _dbContext.SaveChangesAsync();

                return "Notify removed succesfully";
            }
            else
            {
                return "Notify does not found";
            }

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

                return $"{returnMessage}";
            }
            catch (Exception ex)
            {
                return $"An error has occurred while sending a mail to your email: {ex.Message}"; 
            }
        }



        public async Task<string> SendAccountCreationNotification(string email, string text_message, int id)
        {


            var n = new Notification();

            n.EMAIL = email;
            n.CUSTOMER_ID = id;
            n.TIME_OF_CREATION = TimeZoneInfo.ConvertTime(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time"));
            n.EMAIL_SUBJECT = "SpiderOps - Account created, enjoy our platform!! / SpiderOps platform";
            n.TEXT_MESSAGE = "Account created successfully / SpiderOps platform";
            _dbContext.NotifyDBC.Add(n);
            await _dbContext.SaveChangesAsync();

            var result = SendEmail(n.EMAIL,n.EMAIL_SUBJECT , n.TEXT_MESSAGE, "An email was send to your email related to the account creation, Thanks for using this platform!!");

            return result;
        }

        public async Task<string> SendAccountRemovedNotification(string email, string text_message, int id)
        {
            

            var n = new Notification();

            n.CUSTOMER_ID = id;
            n.TIME_OF_CREATION = TimeZoneInfo.ConvertTime(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time"));
            n.EMAIL = email;
            n.EMAIL_SUBJECT = "SpiderOps - Removed account   / SpiderOps platform";
            n.TEXT_MESSAGE = text_message;
            var notificationID = await _dbContext.NotifyDBC.FindAsync(id);

            _dbContext.NotifyDBC.Add(n);
            await _dbContext.SaveChangesAsync();

            if (notificationID != null)
            {
                _dbContext.NotifyDBC.Remove(notificationID);
                await _dbContext.SaveChangesAsync();

                var result = SendEmail(n.EMAIL, "Your Account was removed / SpiderOps platform", n.TEXT_MESSAGE, "Your account was successfully removed from our platform, hope you comeback . Atte: SpiderOps CEO");

                return result;

            }
            else
            {
                return "An error has occurred while sending the removed account email";
            }
        }

        public async Task<string> SendDeploymentNotification(string email, string text_message, int id)
        {
            var n = new Notification();

            n.CUSTOMER_ID = id;
            n.TIME_OF_CREATION = TimeZoneInfo.ConvertTime(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time"));
            n.EMAIL = email;
            n.EMAIL_SUBJECT = "SpiderOps - App Deployment / SpiderOps platform";
            n.TEXT_MESSAGE = text_message;

            _dbContext.NotifyDBC.Add(n);
            await _dbContext.SaveChangesAsync();

            var result = SendEmail(n.EMAIL, "An application deployment was successfully publish / SpiderOps platform", n.TEXT_MESSAGE, "An email was send to your email related to an app deployment, Thanks for using this platform!!");

            return result;
        }

        public async Task<string> SendRecoveyPassNotification(string email, string text_message,int id)
        {

            var customers =  _dbContext.NotifyDBC.Where(a => a.EMAIL == email).FirstOrDefault();


            var n = new Notification();

            n.CUSTOMER_ID = id;
            n.TIME_OF_CREATION = TimeZoneInfo.ConvertTime(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time"));
            n.EMAIL = email;
            n.EMAIL_SUBJECT = "SpiderOps - Recovery password mail / SpiderOps platform";
            n.TEXT_MESSAGE = text_message;

            _dbContext.NotifyDBC.Add(n);
            await _dbContext.SaveChangesAsync();

            var result = SendEmail(email, n.EMAIL_SUBJECT, text_message, "An email was send to your email related to password recovery, Thanks for using this platform!!");

            return result;
        }

        public async Task<string> SendRemovedDeploymentNotification(string email, string text_message, int id)
        {
            var n = new Notification();

            n.CUSTOMER_ID = id;
            n.TIME_OF_CREATION = TimeZoneInfo.ConvertTime(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time"));
            n.EMAIL = email;
            n.EMAIL_SUBJECT = "SpiderOps - App Deployment removed/ SpiderOps platform";
            n.TEXT_MESSAGE = text_message;

            _dbContext.NotifyDBC.Add(n);
            await _dbContext.SaveChangesAsync();

            var result = SendEmail(n.EMAIL, "An application deployment was removed successfully publish / SpiderOps platform", n.TEXT_MESSAGE, "An email was send to your email related to a removed deployment, Thanks for using this platform!!");

            return result;
        }

        public async Task<string> SendUpdateAccountNotification(string email, string text_message, int id)
        {
            var n = new Notification();

            n.CUSTOMER_ID = id;
            n.TIME_OF_CREATION = TimeZoneInfo.ConvertTime(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time"));
            n.EMAIL = email;
            n.EMAIL_SUBJECT = "SpiderOps - Updated account  / SpiderOps platform";
            n.TEXT_MESSAGE = text_message;
            var notificationID = _dbContext.NotifyDBC.Where(a => a.CUSTOMER_ID == id).FirstOrDefault();

            _dbContext.NotifyDBC.Add(n);
            await _dbContext.SaveChangesAsync();

            if (notificationID != null)
            {
                _dbContext.NotifyDBC.Remove(notificationID);
                await _dbContext.SaveChangesAsync();

                var result = SendEmail(n.EMAIL, "Your Account was updated / SpiderOps platform", n.TEXT_MESSAGE, "Your account was successfully updated in our platform. Atte: SpiderOps CEO");

                return result;

            }
            else
            {
                return "An error has occurred while sending the removed account email";
            }
        }

        public async Task<string> SendInfrastructureNotification(string email, string text_message, int id)
        {
            var n = new Notification();

            n.CUSTOMER_ID = id;
            n.TIME_OF_CREATION = TimeZoneInfo.ConvertTime(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time"));
            n.EMAIL = email;
            n.EMAIL_SUBJECT = "SpiderOps - Infrastructure Deployment removed/ SpiderOps platform";
            n.TEXT_MESSAGE = text_message;

            _dbContext.NotifyDBC.Add(n);
            await _dbContext.SaveChangesAsync();

            var result = SendEmail(n.EMAIL, "Infrastructure deployment was successfully publish / SpiderOps platform", n.TEXT_MESSAGE, "An email was send to your email related to the infrastructure deployment, Thanks for using this platform!!");

            return result;
        }
    }
}
