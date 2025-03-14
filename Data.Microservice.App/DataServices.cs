using Data.Microservice.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Microservice.App
{
    public class DataServices : IDataServices
    {

        private readonly IDataRepository _repository;

        public DataServices(IDataRepository repository)
        {

            _repository = repository;

        }

        public Task<string> DeleteCustomerData(int id)
        {
            var result = _repository.DeleteCustomerData(id);

            return result;
        }

        public List<CData> GetAllData()
        {
            var result = _repository.GetAllData();

            return result;
        }

        public List<CData> GetAllDataByCustomerID(int id)
        {
            var result = _repository.GetAllDataByCustomerID(id);

            return result;
        }

        public Task<string> NewCustomerData(CData c)
        {
            var result = _repository.NewCustomerData(c);

            return result;
        }

        public Task<string> UpDateCustomerData(CData c)
        {
            var result = _repository.UpDateCustomerData(c);

            return result;
        }
    }
}
