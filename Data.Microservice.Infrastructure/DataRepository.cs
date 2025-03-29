using Customer.Notify.Microservice.APP;
using Data.Microservice.App;
using Data.Microservice.APP;
using Data.Microservice.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Data.Microservice.Infrastructure
{
    public class DataRepository : IDataRepository
    {

        private readonly DataDbContext _dbContext;
        private readonly INotifyServices _n;

        public DataRepository(DataDbContext dbContext, INotifyServices notifyRepository)
        {

            _dbContext = dbContext;
            _n = notifyRepository;

        }


        public async Task<string> DeleteCustomerData(string email, string subject, string message, int customerId)
        {
            var dataID = await _dbContext.DataDomain.FirstOrDefaultAsync(a => a.CUSTOMERS_ID == customerId);

            if (dataID != null)
            {
                
                string notificationResult = await _n.SendNotification(email, subject,  message, customerId);

                _dbContext.DataDomain.Remove(dataID);
                await _dbContext.SaveChangesAsync();

                return $"Account removed successfully. Notification result: {notificationResult}";
            }
            else
            {
                return "Invalid account, does not exist";
            }
        }

        public List<CData> GetAllData()
        {
            var data = _dbContext.DataDomain.ToList();

            return data;
        }

        public List<CData> GetAllDataByCustomerID(int id)
        {
            var data = _dbContext.DataDomain.Where(a => a.CUSTOMERS_ID == id).ToList();

            return data;
        }

        public async Task<string> NewCustomerData(CData c, string email, string subject, string message, int customerId)
        {
            await _dbContext.DataDomain.AddAsync(c);
            await _dbContext.SaveChangesAsync();

            var id = c.CUSTOMERS_ID;

            
            string notificationResult = await _n.SendNotification(email, subject, message, customerId);

            return $"Account created successfully. Notification result: {notificationResult}";
        }



        public async Task<string> UpDateCustomerData(CData c)
        {
            var dataID = _dbContext.DataDomain.FirstOrDefault(a => a.CUSTOMERS_ID == c.CUSTOMERS_ID);

   

            if (dataID != null)
            {
                
                dataID.FIRSTNAME = c.FIRSTNAME;
                dataID.SECONDNAME = c.SECONDNAME;
                dataID.LASTNAME = c.LASTNAME;
                dataID.HOME_ADDRESS = c.HOME_ADDRESS;


               await _dbContext.SaveChangesAsync();

                return "Account updated successfully";
            }
            else
            {
                return "Invalid ID";
            }



        }
    }
}
