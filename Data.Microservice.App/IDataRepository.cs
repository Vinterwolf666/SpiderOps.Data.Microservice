using Data.Microservice.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Microservice.App
{
    public interface IDataRepository
    {
        List<CData> GetAllData();

        List<CData> GetAllDataByCustomerID(int id);

        Task<string> NewCustomerData(CData c);

        Task<string> UpDateCustomerData(CData c);

        Task<string> DeleteCustomerData(int id);
    }
}
