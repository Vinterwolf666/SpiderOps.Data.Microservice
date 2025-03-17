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

        public Task<string> DeleteCustomerData(int id, string email, string text_message)
        {
            var result = _repository.DeleteCustomerData(id, email, text_message);

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

        public async Task<string> NewCustomerData(CData c, string email)
        {
            var result = await _repository.NewCustomerData(c,email);

            return result;
        }

        public Task<string> UpDateCustomerData(CData c)
        {
            var result = _repository.UpDateCustomerData(c);

            return result;
        }
    }
}
