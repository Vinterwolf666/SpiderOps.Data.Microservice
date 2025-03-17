using Data.Microservice.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Microservice.App
{
    public interface IDataServices
    {
        List<CData>  GetAllData();

        List<CData> GetAllDataByCustomerID(int id);

        Task<string> NewCustomerData(CData c, string email);

        Task<string> UpDateCustomerData(CData c);

        Task<string> DeleteCustomerData(int id, string email, string text_message);
    }
}
