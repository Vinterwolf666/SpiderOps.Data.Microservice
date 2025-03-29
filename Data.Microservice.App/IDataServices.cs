using Data.Microservice.Domain;
using NETCore.MailKit.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Microservice.App
{
    public interface IDataServices
    {
        Task<string> DeleteCustomerData(string email, string subject, string message, int customerId);

        List<CData> GetAllData();

        List<CData> GetAllDataByCustomerID(int id);

        Task<string> NewCustomerData(CData c, string email, string subject, string message, int customerId);

        Task<string> UpdateCustomerData(CData c, string email);
    }
}
