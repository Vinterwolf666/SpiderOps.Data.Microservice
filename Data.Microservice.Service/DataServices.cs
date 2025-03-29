using Customer.Notify.Microservice.APP;
using Customer.Notify.Microservice.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Microservice.Domain;
using Data.Microservice.Infrastructure;
using Data.Microservice.APP;

namespace Data.Microservice.App
{
    public class DataServices : IDataServices
    {
        private readonly IDataRepository _repository;
        private readonly INotifyRepository _notifyRepository;
        private readonly INotifyServices _s;

        public DataServices(IDataRepository repository, INotifyRepository notifyRepository, INotifyServices s)
        {
            _repository = repository;
            _notifyRepository = notifyRepository;
            _s = s;
        }

        public async Task<string> DeleteCustomerData(string email, string subject, string message, int customerId)
        {
            var success = await _repository.DeleteCustomerData(email, subject, message, customerId);
            if (string.IsNullOrEmpty(success))
            {
                return "Customer data not found";
            }


            var notification = new Notification
            {
                EMAIL = email,
                EMAIL_SUBJECT = subject,
                TEXT_MESSAGE = message,
                CUSTOMER_ID = customerId,
                TIME_OF_CREATION = DateTime.UtcNow
            };

            await _notifyRepository.AddNotificationAsync(notification);
            await _s.SendNotification(email, subject, message, customerId);
            return "Customer data deleted successfully and notification sent.";
        }

        public List<CData> GetAllData()
        {
            return _repository.GetAllData();
        }

        public List<CData> GetAllDataByCustomerID(int id)
        {
            return _repository.GetAllDataByCustomerID(id);
        }

        public async Task<string> NewCustomerData(CData c, string email, string subject, string message, int customerId)
        {
            await _repository.NewCustomerData(c, email,subject, message,customerId);

            var notification = new Notification
            {
                EMAIL = email,
                EMAIL_SUBJECT = "New Customer Data Added",
                TEXT_MESSAGE = "Your data has been registered successfully.",
                CUSTOMER_ID = c.CUSTOMERS_ID,
                TIME_OF_CREATION = DateTime.UtcNow
            };

            
            await _notifyRepository.AddNotificationAsync(notification);
            await _s.SendNotification(email, subject, message, customerId);
            return "Customer data added and notification sent.";
        }

        public async Task<string> UpdateCustomerData(CData c, string email)
        {
            var success = await _repository.UpDateCustomerData(c);

            if (string.IsNullOrEmpty(success))
                return "Failed to update customer data";

            var notification = new Notification
            {
                EMAIL = email,
                EMAIL_SUBJECT = "Customer Data Updated",
                TEXT_MESSAGE = "Your customer data has been updated.",
                CUSTOMER_ID = c.CUSTOMERS_ID,
                TIME_OF_CREATION = DateTime.UtcNow
            };

            await _notifyRepository.AddNotificationAsync(notification);
            return "Customer data updated and notification sent.";
        }

    }
}
