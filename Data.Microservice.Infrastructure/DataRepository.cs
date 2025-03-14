using Data.Microservice.App;
using Data.Microservice.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Microservice.Infrastructure
{
    public class DataRepository : IDataRepository
    {

        private readonly DataDbContext _dbContext;

        public DataRepository(DataDbContext dbContext)
        {

            _dbContext = dbContext;

        }


        public async Task<string> DeleteCustomerData(int id)
        {
            var dataID = await _dbContext.DataDomain.FirstOrDefaultAsync(a => a.CUSTOMERS_ID == id);


            if (dataID != null)
            {
                 _dbContext.DataDomain.Remove(dataID);

                await _dbContext.SaveChangesAsync();

                return "Account removed successfully";
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

        public async Task<string> NewCustomerData(CData c)
        {
           var result = _dbContext.DataDomain.AddAsync(c);

            await _dbContext.SaveChangesAsync();

            return "Account created successfully";
        }

        public async Task<string> UpDateCustomerData(CData c)
        {
            var dataID = _dbContext.DataDomain.FirstOrDefault(a => a.CUSTOMERS_ID == c.CUSTOMERS_ID);

            if(dataID != null)
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
